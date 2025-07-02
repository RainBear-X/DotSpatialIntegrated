using System.IO;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Projections.Forms;
using Core;

namespace Modules.Projection
{
    public class ReprojectLayerModule : IRunnableModule
    {
        public string Name => "Reproject Layer…";
        public string Category => "Projection";

        public void Run()
        {
            var map = MapContext.Instance.MainMap;
            var lyr = map.Layers.SelectedLayer as IMapFeatureLayer;
            if (lyr == null)
            {
                MessageBox.Show("请选择要素图层！");
                return;
            }
            using (var dlg = new ProjectionSelectDialog())
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;
                var target = dlg.SelectedCoordinateSystem;
                FeatureSet newFs = ProjectionService.ReprojectCopy(lyr.DataSet, target);
                string baseName = Path.GetFileNameWithoutExtension(lyr.DataSet.Filename);
                newFs.Filename = baseName + "_" + target.Name.Replace(' ', '_') + ".shp";
                var newLayer = map.Layers.Add(newFs);
                map.Layers.SelectedLayer = newLayer;
            }
        }
    }
}
