using System.Windows.Forms;
using Core;
using DotSpatial.Controls;

namespace Modules.RasterOps
{
    public class HillshadeModule : IRunnableModule
    {
        public string Name => "Hillshade";
        public string Category => "Raster";

        public void Run()
        {
            var map = MapContext.Instance.MainMap;
            if (map == null)
            {
                MessageBox.Show("请先初始化地图");
                return;
            }

            var layer = map.Layers.SelectedLayer as IMapRasterLayer;
            if (layer == null)
            {
                MessageBox.Show("请先在左侧图层管理中选择一个栅格图层");
                return;
            }

            layer.Symbolizer.ShadedRelief.ElevationFactor = 1;
            layer.Symbolizer.ShadedRelief.IsUsed = true;
            layer.WriteBitmap();
        }
    }
}
