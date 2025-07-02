using System.Windows.Forms;
using Core;
using DotSpatial.Controls;

namespace Modules.Projection
{
    public class ShowCurrentProjModule : IRunnableModule
    {
        public string Name => "Current Projection";
        public string Category => "Projection";

        public void Run()
        {
            var map = MapContext.Instance.MainMap;
            if (map == null)
            {
                MessageBox.Show("地图未初始化！");
                return;
            }
            MessageBox.Show(
                map.Projection.ToEsriString(),
                "Map Projection (WKT)",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
