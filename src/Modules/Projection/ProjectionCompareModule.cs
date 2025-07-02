using System.Linq;
using System.Windows.Forms;
using DotSpatial.Controls;
using Core;
using DotSpatial.Symbology;

namespace Modules.Projection
{
    public class ProjectionCompareModule : IRunnableModule
    {
        public string Name => "Compare Selected Area";
        public string Category => "Projection";

        public void Run()
        {
            var lyr = MapContext.Instance.MainMap.Layers.SelectedLayer as IMapFeatureLayer;
            if (lyr == null)
            {
                MessageBox.Show("请选择图层！");
                return;
            }
            var feats = lyr.Selection.ToFeatureList();
            if (feats.Count == 0)
            {
                MessageBox.Show("未选择要素！");
                return;
            }
            double sel = ProjectionService.SelectedArea(feats);
            double tot = ProjectionService.TotalArea(lyr.DataSet);
            MessageBox.Show(
                $"Selected: {sel:N0}\r\nTotal: {tot:N0}\r\nRatio: {sel / tot:P2}",
                "Compare Area",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
