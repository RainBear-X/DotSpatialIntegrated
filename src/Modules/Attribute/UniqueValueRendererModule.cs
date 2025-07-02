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

            // 1) 选择图层与字段
            using (var dlg = new FieldSelectDialog(map, "唯一值着色 - 选择图层与字段"))
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;
                var layer = dlg.SelectedLayer;
                var field = dlg.SelectedField;

                // 2) 创建唯一值方案
                PolygonScheme scheme = new PolygonScheme();
                scheme.EditorSettings.ClassificationType = ClassificationType.UniqueValues;
                scheme.EditorSettings.FieldName = field;
                scheme.CreateCategories(layer.DataSet.DataTable);
                layer.Symbology = scheme;
                map.ResetBuffer();
            }
        }
    }
}
