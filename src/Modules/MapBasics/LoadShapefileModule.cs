using System.Windows.Forms;
using Core;
using DotSpatial.Controls;

namespace Modules.MapBasics
{
    public class LoadLayerModule : IRunnableModule
    {
        public string Name { get { return "加载矢量"; } }
        public string Category { get { return "File"; } }

        public void Run()
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "所有支持文件|*.shp;*.tif;*.img|Shapefile|*.shp|Raster|*.tif;*.img";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var map = MapContext.Instance.MainMap;
                    if (map != null)
                    {
                        map.AddLayer(dlg.FileName);
                        map.ZoomToMaxExtent();
                    }
                }
            }
        }
    }
}
