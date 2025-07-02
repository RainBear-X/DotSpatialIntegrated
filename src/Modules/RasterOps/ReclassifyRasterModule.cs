using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Core;
using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Symbology;

namespace Modules.RasterOps
{
    public class ReclassifyRasterModule : IRunnableModule
    {
        public string Name => "栅格重分类（多级）";
        public string Category => "Raster";

        public void Run()
        {
            // 1. 获取地图 & 图层
            Map map = MapContext.Instance.MainMap;
            if (map == null)
            {
                MessageBox.Show("请先初始化地图。");
                return;
            }
            IMapRasterLayer srcLayer = map.Layers.SelectedLayer as IMapRasterLayer;
            if (srcLayer == null)
            {
                MessageBox.Show("请先在左侧图层管理中选择一个栅格图层。");
                return;
            }

            // 2. 弹出对话框，获取阈值列表
            string input;
            if (InputBox(
                    "重分类阈值",
                    "请输入分级阈值，用逗号分隔，例如：1000,2000,3000",
                    "",
                    out input) != DialogResult.OK)
            {
                return;
            }

            // 3. 解析 & 排序
            double[] thresholds;
            try
            {
                thresholds = input
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => double.Parse(s.Trim()))
                    .OrderBy(v => v)
                    .ToArray();
                if (thresholds.Length == 0) throw new Exception();
            }
            catch
            {
                MessageBox.Show("阈值格式错误，请输入逗号分隔的数字列表。");
                return;
            }

            // 4. 构建源 & 目标栅格
            IRaster src = srcLayer.DataSet;
            src.GetStatistics(); // 刷新统计信息

            int rows = src.NumRows, cols = src.NumColumns;
            IRaster dst = Raster.CreateRaster(
                "reclass.bgd",
                null,
                cols, rows,
                1, src.DataType,
                new string[0]);
            dst.Bounds = src.Bounds.Copy();
            dst.NoDataValue = src.NoDataValue;
            dst.Projection = src.Projection;

            // 5. 重分类：阈值 N 个 → N+1 类，类别索引 0..N
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    double v = src.Value[r, c];
                    if (v == src.NoDataValue)
                    {
                        dst.Value[r, c] = src.NoDataValue;
                    }
                    else
                    {
                        int cls = -1;
                        for (int k = 0; k < thresholds.Length; k++)
                        {
                            if (v < thresholds[k])
                            {
                                cls = k;
                                break;
                            }
                        }
                        if (cls < 0) cls = thresholds.Length;
                        dst.Value[r, c] = cls;
                    }
                }
            }

            // 6. 保存并添加为新图层
            dst.Save();
            IMapRasterLayer newLayer = map.Layers.Add(dst) as IMapRasterLayer;
            if (newLayer == null)
            {
                MessageBox.Show("添加重分类图层失败！");
                return;
            }

            // 7. 生成并应用色带，区间 [i-0.5, i+0.5)
            int classCount = thresholds.Length + 1;
            ColorScheme scheme = new ColorScheme();
            for (int i = 0; i < classCount; i++)
            {
                // 计算区间
                double low = i - 0.5;
                double high = i + 0.5;
                // 线性渐变色
                float frac = classCount == 1 ? 0f : (float)i / (classCount - 1);
                Color col = Color.FromArgb(
                    (int)(255 * frac),
                    0,
                    (int)(255 * (1 - frac)));
                // 图例文字
                string legendText;
                if (i == 0)
                    legendText = "< " + thresholds[0];
                else if (i == thresholds.Length)
                    legendText = "≥ " + thresholds[thresholds.Length - 1];
                else
                    legendText = thresholds[i - 1] + " – " + thresholds[i];

                ColorCategory cat = new ColorCategory(low, high, col, col)
                {
                    LegendText = legendText
                };
                scheme.AddCategory(cat);
            }

            newLayer.Symbolizer.Scheme = scheme;
            newLayer.WriteBitmap();

            // 8. 强制重绘
            map.ResetBuffer();
        }

        /// <summary>
        /// 简易输入对话框
        /// </summary>
        private DialogResult InputBox(string title, string prompt, string def, out string input)
        {
            using (var dlg = new Form())
            {
                dlg.Text = title;
                dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.ClientSize = new Size(380, 140);
                dlg.MaximizeBox = false;
                dlg.MinimizeBox = false;

                var lbl = new Label { Left = 10, Top = 10, AutoSize = true, Text = prompt };
                var txt = new TextBox { Left = 10, Top = 35, Width = 360, Text = def };
                var ok = new Button { Text = "确定", Left = 200, Width = 80, Top = 75, DialogResult = DialogResult.OK };
                var ca = new Button { Text = "取消", Left = 290, Width = 80, Top = 75, DialogResult = DialogResult.Cancel };

                dlg.Controls.AddRange(new Control[] { lbl, txt, ok, ca });
                dlg.AcceptButton = ok;
                dlg.CancelButton = ca;

                var dr = dlg.ShowDialog();
                input = txt.Text;
                return dr;
            }
        }
    }
}
