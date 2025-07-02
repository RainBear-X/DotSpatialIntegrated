using System.Drawing;
using System.Windows.Forms;
using Core;
using DotSpatial.Controls;
using DotSpatial.Symbology;

namespace Modules.Attribute
{
    public class AttributeLabelModule : IRunnableModule
    {
        public string Name => "按字段标签";
        public string Category => "Attribute";

        public void Run()
        {
            var map = MapContext.Instance.MainMap;
            if (map == null || map.Layers.Count == 0)
            {
                MessageBox.Show("请先加载一个矢量图层！");
                return;
            }
            using (var dlg = new FieldSelectDialog(map, "字段标签 - 选择图层与字段", true))
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;

                // 把 MapFeatureLayer 改成 IMapFeatureLayer
                var layer = dlg.SelectedLayer as IMapFeatureLayer;
                if (layer == null)
                {
                    MessageBox.Show("所选图层不是要素图层！");
                    return;
                }

                // 只有多边形图层才支持 AddLabels，需再 cast
                var poly = layer as MapPolygonLayer;
                if (poly == null)
                {
                    MessageBox.Show("所选图层不是多边形图层！");
                    return;
                }

                // 用 Map 的扩展方法给 poly 添加标签
                map.AddLabels(poly, "[" + dlg.SelectedField + "]",
                              new Font("Tahoma", 8.0f),
                              System.Drawing.Color.Black);
                map.ResetBuffer();
            }
        }
    }
}
