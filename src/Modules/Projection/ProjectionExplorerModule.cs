using System.Windows.Forms;
using Modules.Projection;
using Core;
using DotSpatial.Controls;

namespace Modules.Projection
{
    public class ProjectionExplorerModule : IRunnableModule
    {
        public string Name => "Explorer…";
        public string Category => "Projection";

        public void Run()
        {
            var lyr = MapContext.Instance.MainMap.Layers.SelectedLayer as IMapFeatureLayer;
            if (lyr == null)
            {
                MessageBox.Show("请先选择一个面图层！");
                return;
            }
            new ProjectionExplorerForm(lyr.DataSet).Show();
        }
    }
}
