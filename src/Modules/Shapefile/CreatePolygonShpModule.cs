using System.Windows.Forms;
using Core;

namespace Modules.Shapefile
{
    public class CreatePolygonShpModule : IRunnableModule
    {
        public string Name => "Create";
        public string Category => "Shapefile/Polygon";

        public void Run()
        {
            if (DrawService.CurrentMode != Core.DrawMode.None)
            {
                MessageBox.Show("请先完成或取消当前绘制。"); return;
            }
            DrawService.BeginDraw(Core.DrawMode.Polygon);
        }
    }
}
