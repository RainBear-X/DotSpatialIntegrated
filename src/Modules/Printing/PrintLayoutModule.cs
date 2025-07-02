using System.Windows.Forms;
using Core;
using DotSpatial.Controls;

namespace Modules.Printing
{
    public class PrintLayoutModule : IRunnableModule
    {
        public string Name => "打印地图";
        public string Category => "Map";

        public void Run()
        {
            var layout = new LayoutForm
            {
                MapControl = MapContext.Instance.MainMap
            };
            layout.Show();   // 使用 DotSpatial 自带布局窗
        }
    }
}
