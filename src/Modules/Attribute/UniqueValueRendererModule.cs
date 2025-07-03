using System.Windows.Forms;
using Core;
using DotSpatial.Controls;
using DotSpatial.Symbology;

namespace Modules.Attribute
{
    public class UniqueValueRendererModule : IRunnableModule
    {
        public string Name => "唯一值着色";
        public string Category => "Attribute";

        public void Run()
        {
            var map = MapContext.Instance.MainMap;
            if (map == null) return;

            using (var dlg = new FieldSelectDialog(map, "唯一值着色 - 选择图层与字段"))
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;
                var layer = dlg.SelectedLayer;
                var field = dlg.SelectedField;

                // 1) 先进行空值检查
                if (layer == null || layer.DataSet?.DataTable == null)
                {
                    MessageBox.Show("请选择有效的图层和属性表。");
                    return;
                }

                // 2) 根据图层类型创建对应的方案
                IFeatureScheme scheme;
                if (layer is MapPointLayer)
                    scheme = new PointScheme();
                else if (layer is MapPolygonLayer)
                    scheme = new PolygonScheme();
                else if (layer is MapLineLayer)
                    scheme = new LineScheme();
                else
                {
                    MessageBox.Show("不支持的图层类型");
                    return;
                }

                // 3) 设置并应用方案
                scheme.EditorSettings.ClassificationType = ClassificationType.UniqueValues;
                scheme.EditorSettings.FieldName = field;
                scheme.CreateCategories(layer.DataSet.DataTable);
                layer.Symbology = scheme;
                map.ResetBuffer();
            }
        }
    }
}
