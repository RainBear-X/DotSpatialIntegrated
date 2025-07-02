using System;
using System.Windows.Forms;
using Core;
using DotSpatial.Controls;
using DotSpatial.Data;

namespace Modules.RasterOps
{
    public class MultiplyRasterModule : IRunnableModule
    {
        public string Name => "栅格×自定义倍数";
        public string Category => "Raster";

        public void Run()
        {
            var map = MapContext.Instance.MainMap;
            if (map == null)
            {
                MessageBox.Show("请先初始化地图");
                return;
            }
            // 选中图层
            var layer = map.Layers.SelectedLayer as IMapRasterLayer;
            if (layer == null)
            {
                MessageBox.Show("请先在左侧图层管理中选择一个栅格图层");
                return;
            }

            // 弹出输入框，获取倍数
            if (InputBox("倍数", "请输入要乘以的倍数：", "2", out string input) != DialogResult.OK)
                return;
            if (!double.TryParse(input, out double factor))
            {
                MessageBox.Show("无效的数字，请重新输入");
                return;
            }

            // 创建新栅格
            IRaster src = layer.DataSet;
            IRaster dst = Raster.CreateRaster(
                "multiply.bgd", null,
                src.NumColumns, src.NumRows, 1, src.DataType, new string[0]);
            dst.Bounds = src.Bounds.Copy();
            dst.NoDataValue = src.NoDataValue;
            dst.Projection = src.Projection;

            // 值乘倍数
            for (int r = 0; r < src.NumRows; r++)
            {
                for (int c = 0; c < src.NumColumns; c++)
                {
                    double v = src.Value[r, c];
                    dst.Value[r, c] = (v == src.NoDataValue) ? v : v * factor;
                }
            }

            dst.Save();
            map.Layers.Add(dst);
        }

        // 简易 InputBox
        private DialogResult InputBox(string title, string prompt, string def, out string input)
        {
            using (Form f = new Form())
            {
                f.Text = title;
                f.FormBorderStyle = FormBorderStyle.FixedDialog;
                f.StartPosition = FormStartPosition.CenterParent;
                f.ClientSize = new System.Drawing.Size(300, 120);
                Label lbl = new Label { Left = 10, Top = 10, AutoSize = true, Text = prompt };
                TextBox txt = new TextBox { Left = 10, Top = 35, Width = 280, Text = def };
                Button ok = new Button { Text = "确定", Left = 130, Width = 70, Top = 70, DialogResult = DialogResult.OK };
                Button ca = new Button { Text = "取消", Left = 210, Width = 70, Top = 70, DialogResult = DialogResult.Cancel };
                f.Controls.AddRange(new Control[] { lbl, txt, ok, ca });
                f.AcceptButton = ok;
                f.CancelButton = ca;
                var dr = f.ShowDialog();
                input = txt.Text;
                return dr;
            }
        }
    }
}
