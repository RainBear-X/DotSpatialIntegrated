using System.Windows.Forms;
using DotSpatial.Controls;
using Core;
using DotSpatial.Data;

namespace Modules.Projection
{
    public class CalcAreaModule : IRunnableModule
    {
        public string Name => "Total Area";
        public string Category => "Projection";

        public void Run()
        {
            var lyr = MapContext.Instance.MainMap.Layers.SelectedLayer as IMapFeatureLayer;
            if (lyr == null)
            {
                MessageBox.Show("请选择一个图层！");
                return;
            }
            double tot = ProjectionService.TotalArea(lyr.DataSet);
            MessageBox.Show(
                "Total area: " + tot.ToString("N0"),
                "面积统计",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
