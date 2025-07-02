using System.Windows.Forms;
using Core;
using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Data.Rasters.GdalExtension;

namespace Modules.RasterOps
{
    public class LoadRasterModule : IRunnableModule
    {
        public string Name => "加载栅格";
        public string Category => "Raster";

        public void Run()
        {
            var map = MapContext.Instance.MainMap;
            if (map == null) return;

            using (OpenFileDialog ofd = new OpenFileDialog
            { Filter = "Raster|*.tif;*.img;*.asc;*.hdf|All|*.*" })
            {
                if (ofd.ShowDialog() != DialogResult.OK) return;

                GdalBoot.Init(); // 保证 GDAL 已初始化
                IRaster ras = new GdalRasterProvider().Open(ofd.FileName);
                if (ras == null) { MessageBox.Show("无法打开栅格"); return; }

                map.Layers.Add(ras);
                map.ZoomToMaxExtent();
            }
        }
    }
}
