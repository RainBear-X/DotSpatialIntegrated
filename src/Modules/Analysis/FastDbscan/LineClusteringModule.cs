// LineClusteringModule.cs  —  Fast‑DBSCAN (Line) for DotSpatial 1.7.2 & C# 7.3
// 点击菜单后弹出与点聚类相同的 ClusterForm，让用户设定 MinPts / Eps / KD‑Tree（Eps 仅作展示）。
// 本版：
//   • 不再尝试唯一值随机着色，专注聚类字段写回
//   • 与 FastDbscanModule.cs 接口保持一致（同一 ClusterForm）
//--------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Topology;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

namespace Modules.Analysis.FastDbscan
{
    public class LineClusteringModule : IRunnableModule
    {
        public string Name => "Fast DBSCAN (Line)";
        public string Category => "Analysis";
        public string Description => "Density‑based clustering for polylines using Fast‑DTW + DBSCAN";

        private const int DTW_RADIUS = 5;   // Sakoe‑Chiba 半径
        private const int MinPtsDef = 16;
        private const double EpsDef = 1.971; // km

        // DotSpatial 会通过反射调用 Run()——无需另外 Initialize()
        public async void Run()
        {
            // ① 找 Map 控件
            Map map = null;
            foreach (Form frm in Application.OpenForms)
            {
                map = FindMapRecursive(frm);
                if (map != null) break;
            }
            if (map == null)
            {
                MessageBox.Show("未找到 Map 控件");
                return;
            }

            // ② 必须选中线或面图层
            if (!(map.Layers.SelectedLayer is IMapFeatureLayer fl))
            {
                MessageBox.Show("请先选中一个【线/面图层】");
                return;
            }
            var fs = fl.DataSet;
            if (fs.FeatureType != FeatureType.Line && fs.FeatureType != FeatureType.Polygon)
            {
                MessageBox.Show("当前图层不是折线/多边形");
                return;
            }

            // ③ 取参数（复用 ClusterForm，但 Eps/UseKdTree 仅作展示）
            int minPts = MinPtsDef;
            double epsVal = EpsDef;
            bool manualEps = false;  // 多加一个变量，保存用户选择
            using (var dlg = new ClusterForm())
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;
                minPts = dlg.MinPts;
                manualEps = dlg.ManualEps;           // ← 取出标志
                epsVal = dlg.ManualEps ? dlg.EpsValue : 0;
            }

            Cursor curPrev = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                // ④ 收集折线轨迹
                var trajs = await Task.Run(() => CollectPolylines(fl));
                // ⑤ Fast‑DTW 距离矩阵 + 自动 eps
                var D = await Task.Run(() => BuildDistanceMatrix(trajs, minPts));

                if (!manualEps)
                {
                    // 读取上次 BuildDistanceMatrix 打印出的 p90
                    // 或直接再算一遍
                    var kdists = new List<double>(D.GetLength(0));
                    int n = D.GetLength(0);
                    for (int i = 0; i < n; i++)
                    {
                        var row = new List<double>(n - 1);
                        for (int j = 0; j < n; j++)
                            if (i != j) row.Add(D[i, j]);
                        row.Sort();
                        kdists.Add(row[minPts - 1]);
                    }
                    kdists.Sort();
                    double p90 = kdists[(int)(kdists.Count * 0.9)];
                    epsVal = p90;
                    Console.WriteLine($"[自动] k={minPts}-distance P90 = {p90:F4}");
                }

                // ⑥ 预计算距离 DBSCAN
                var labels = await Task.Run(() => PrecomputedDbscan.Cluster(D, minPts, epsVal));
                // ⑦ 写回字段
                await Task.Run(() => WriteClusterId(fl, labels, "CLU_ID"));
                MessageBox.Show($"线聚类完成：簇 = {labels.Max() + 1}，噪声 = {labels.Count(l => l == -1)}");
                map.ResetBuffer();
            }
            catch (Exception ex) { MessageBox.Show("Fast‑DBSCAN (Line) 出错：" + ex.Message); }
            finally { Cursor.Current = curPrev; }
        }

        // ─────────── 帮助函数 ───────────
        private Map FindMapRecursive(Control p)
        {
            foreach (Control c in p.Controls)
            {
                if (c is Map m) return m;
                var d = FindMapRecursive(c);
                if (d != null) return d;
            }
            return null;
        }

        private static List<List<double[]>> CollectPolylines(IMapFeatureLayer fl)
        {
            var list = new List<List<double[]>>(fl.DataSet.NumRows());
            foreach (var f in fl.DataSet.Features)
            {
                var pts = f.BasicGeometry.Coordinates.Select(c => new[] { c.X, c.Y }).ToList();
                if (pts.Count > 1) list.Add(pts);
            }
            return list;
        }

        private static double Euclid(double[] a, double[] b)
            => Math.Sqrt((a[0] - b[0]) * (a[0] - b[0]) + (a[1] - b[1]) * (a[1] - b[1]));

        private static double DtwDist(IList<double[]> a, IList<double[]> b, int r)
        {
            int n = a.Count, m = b.Count;
            double inf = 1e30;
            var dtw = new double[n + 1, m + 1];
            for (int i = 0; i <= n; i++) for (int j = 0; j <= m; j++) dtw[i, j] = inf;
            dtw[0, 0] = 0;
            for (int i = 1; i <= n; i++)
            {
                int j0 = Math.Max(1, i - r), j1 = Math.Min(m, i + r);
                for (int j = j0; j <= j1; j++)
                {
                    double d = Euclid(a[i - 1], b[j - 1]);
                    double best = Math.Min(dtw[i - 1, j], Math.Min(dtw[i, j - 1], dtw[i - 1, j - 1]));
                    dtw[i, j] = d + best;
                }
            }
            double dist = dtw[n, m];
            double norm = (n + m) * 0.5;          // 归一化
            return dist / norm;
        }
        private double[,] BuildDistanceMatrix(List<List<double[]>> ts, int minPts)
        {
            int n = ts.Count;
            var D = new double[n, n];
            // 1) 先填距离矩阵
            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)
                    D[i, j] = D[j, i] = DtwDist(ts[i], ts[j], DTW_RADIUS);

            // 2) 计算 k‑distance 统计
            var kdists = new List<double>(n);
            for (int i = 0; i < n; i++)
            {
                var row = new List<double>(n - 1);
                for (int j = 0; j < n; j++)
                    if (i != j) row.Add(D[i, j]);
                row.Sort();
                kdists.Add(row[minPts - 1]);
            }
            kdists.Sort();
            double min = kdists.First();
            double p50 = kdists[kdists.Count / 2];
            double p90 = kdists[(int)(kdists.Count * 0.9)];
            Console.WriteLine($"k={minPts}-distance  min={min:F2}  P50={p50:F2}  P90={p90:F2}");
            return D;
        }

        private static class PrecomputedDbscan
        {
            public static int[] Cluster(double[,] D, int minPts, double eps)
            {
                int n = D.GetLength(0);
                var lbl = Enumerable.Repeat(-2, n).ToArray();
                int cid = -1;
                for (int i = 0; i < n; i++)
                {
                    if (lbl[i] != -2) continue;
                    var nbr = Range(D, i, eps);
                    if (nbr.Count < minPts) { lbl[i] = -1; continue; }
                    lbl[i] = ++cid;
                    var q = new Queue<int>(nbr);
                    while (q.Count > 0) { int p = q.Dequeue(); if (lbl[p] == -1) lbl[p] = cid; if (lbl[p] != -2) continue; lbl[p] = cid; var n2 = Range(D, p, eps); if (n2.Count >= minPts) foreach (var t in n2) if (lbl[t] <= -1) q.Enqueue(t); }
                }
                return lbl;
            }
            static List<int> Range(double[,] D, int i, double eps) { int n = D.GetLength(0); var l = new List<int>(); for (int j = 0; j < n; j++) if (D[i, j] <= eps) l.Add(j); return l; }
        }

        private static void WriteClusterId(IMapFeatureLayer fl, int[] lbl, string fld)
        {
            var fs = fl.DataSet;
            if (!fs.DataTable.Columns.Contains(fld))
                fs.DataTable.Columns.Add(new DataColumn(fld, typeof(int)));
            for (int i = 0; i < lbl.Length && i < fs.DataTable.Rows.Count; i++)
                fs.DataTable.Rows[i][fld] = lbl[i];
            fs.Save();
        }
    }

}
