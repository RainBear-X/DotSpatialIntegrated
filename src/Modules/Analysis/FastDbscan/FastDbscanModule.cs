// FastDbscanModule.cs — DotSpatial 1.7.2 & C# 7.3
// ► 改进：耗时聚类放入 Task.Run，避免 UI 卡死 / ContextSwitchDeadlock

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;             // ★ async/await
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Topology;
using Core.Algorithms.Clustering;          // FastDbscan
using Core.Algorithms.Clustering.Models;

namespace Modules.Analysis.FastDbscan
{
    public class FastDbscanModule : IRunnableModule
    {
        public string Name => "Fast DBSCAN";
        public string Category => "Analysis";

        // ───────────────────────── 主入口 ─────────────────────────
        public async void Run()                // ★ 改为 async
        {
            // ① 查找 Map 控件
            Map map = null;
            foreach (Form frm in Application.OpenForms)
            {
                map = FindMapRecursive(frm);
                if (map != null) break;
            }
            if (map == null)
            {
                MessageBox.Show("当前窗口中未检测到 DotSpatial Map 控件", "Fast DBSCAN");
                return;
            }

            // ② 确认选中图层
            if (!(map.Layers.SelectedLayer is IMapFeatureLayer selLayer))
            {
                MessageBox.Show("请先在图层管理器中选择一个【点图层】！", "Fast DBSCAN");
                return;
            }
            if (selLayer.DataSet.FeatureType != FeatureType.Point)
            {
                MessageBox.Show("当前图层不是点图层！", "Fast DBSCAN");
                return;
            }

            // ③ 参数窗体
            using (var dlg = new ClusterForm())
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;

                int minPts = dlg.MinPts;
                double epsVal = dlg.ManualEps ? dlg.EpsValue : 0;
                bool useKd = dlg.UseKdTree;

                // ④ 构造 DataPoint 列表
                IFeatureSet fs = (IFeatureSet)selLayer.DataSet;
                var pts = new List<DataPoint>(fs.NumRows());
                int idx = 0;
                foreach (var feat in fs.Features)
                {
                    if (feat.BasicGeometry is IPoint pt)
                    {
                        Coordinate c = pt.Coordinate;
                        pts.Add(new DataPoint(idx++, new[] { c.X, c.Y }));
                    }
                }

                // 如果没手动，则自动寻找最佳 eps
                if (!dlg.ManualEps)
                {
                    // 计算距离矩阵（仅 2D 点）
                    int n = pts.Count;
                    var dists = new List<double>();
                    for (int i = 0; i < n; i++)
                    {
                        var row = new List<double>();
                        for (int j = 0; j < n; j++)
                            if (i != j)
                                row.Add(Math.Sqrt(Math.Pow(pts[i].Values[0] - pts[j].Values[0], 2) +
                                                 Math.Pow(pts[i].Values[1] - pts[j].Values[1], 2)));
                        row.Sort();
                        dists.Add(row[minPts - 1]);
                    }
                    dists.Sort();
                    double p90 = dists[(int)(dists.Count * 0.9)];
                    epsVal = p90;
                    Console.WriteLine($"[自动] k={minPts}-distance P90 = {p90:F4}");
                }

                // ⑤ 耗时聚类放后台线程
                Cursor.Current = Cursors.WaitCursor;
                ClusterResult result;
                try
                {
                    result = await Task.Run(() =>
                        Core.Algorithms.Clustering.FastDbscan.Cluster(pts, minPts, epsVal, useKdTreeSearch: useKd, parallel: true));
                }
                catch (Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show($"聚类过程中出现错误：\n{ex.Message}", "Fast DBSCAN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Cursor.Current = Cursors.Default;

                // ⑥ 写字段并保存
                const string fld = "CLU_ID";
                if (!fs.DataTable.Columns.Contains(fld))
                    fs.DataTable.Columns.Add(fld, typeof(int));
                for (int i = 0; i < result.Labels.Length; i++)
                    fs.DataTable.Rows[i][fld] = result.Labels[i];
                fs.Save();

                MessageBox.Show($"聚类完成！\n簇数：{result.ClusterCount}    噪声点：{result.NoiseCount}", "Fast DBSCAN");
            }
        }

        // ── 递归查找 Map 控件 ──
        private Map FindMapRecursive(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is Map m) return m;
                var deeper = FindMapRecursive(c);
                if (deeper != null) return deeper;
            }
            return null;
        }
    }
}
