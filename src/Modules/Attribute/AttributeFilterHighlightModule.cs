// AttributeFilterHighlightModule.cs
using System.Drawing;
using System.Windows.Forms;
using Core;
using DotSpatial.Controls;
using DotSpatial.Symbology;

namespace Modules.Attribute
{
    public class AttributeFilterHighlightModule : IRunnableModule
    {
        public string Name => "条件查询高亮";
        public string Category => "Attribute";

        public void Run()
        {
            var map = MapContext.Instance.MainMap;
            if (map == null || map.Layers.Count == 0)
            {
                MessageBox.Show("请先加载一个矢量图层！");
                return;
            }
            using (var dlg = new AttributeQueryDialog(map, "属性过滤并高亮"))
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;
                var layer = dlg.SelectedLayer as MapPolygonLayer;
                if (layer == null)
                {
                    MessageBox.Show("所选图层不是多边形图层！");
                    return;
                }
                layer.DataSet.FillAttributes();
                var scheme = new PolygonScheme();
                var cat = new PolygonCategory(Color.Yellow, Color.Red, 1)
                {
                    FilterExpression = $"[{dlg.SelectedField}] {dlg.Condition} '{dlg.ValueText}'",
                    LegendText = dlg.ValueText
                };
                scheme.AddCategory(cat);
                layer.Symbology = scheme;
                map.ResetBuffer();
            }
        }
    }
}
