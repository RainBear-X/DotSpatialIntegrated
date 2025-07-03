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
            if (map == null) return;
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
                string dir = Path.GetDirectoryName(lyr.DataSet.Filename);
                string baseName = Path.GetFileNameWithoutExtension(lyr.DataSet.Filename);
                string newPath = Path.Combine(dir,
                    baseName + "_" + target.Name.Replace(' ', '_') + ".shp");
                newFs.SaveAs(newPath, true);

                IMapFeatureLayer added = map.AddLayer(newPath) as IMapFeatureLayer;
                if (added != null)
                {
                    map.Layers.SelectedLayer = added;
                    map.Legend?.RefreshNodes();
                }
            }
        }
    }
}
