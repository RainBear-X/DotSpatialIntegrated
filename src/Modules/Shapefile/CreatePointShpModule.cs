using System.Windows.Forms;
using Core;
using DrawModeEnum = Core.DrawMode;

namespace Modules.Shapefile
{
    public class CreatePointShpModule : IRunnableModule
    {
        public string Name => "Create";
        public string Category => "Shapefile/Point";

        public void Run()
        {
            if (DrawService.CurrentMode != Core.DrawMode.None)
            {
                MessageBox.Show("请先完成或取消当前绘制。"); return;
            }
            DrawService.BeginDraw(DrawModeEnum.Point);
        }
    }
}
