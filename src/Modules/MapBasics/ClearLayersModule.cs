using System.Windows.Forms;
using Core;

namespace Modules.MapBasics
{
    public class ClearLayersModule : IRunnableModule
    {
        public string Name { get { return "清空图层"; } }
        public string Category { get { return "File"; } }

        public void Run()
        {
            var map = MapContext.Instance.MainMap;
            if (map == null || map.Layers.Count == 0)
            {
                MessageBox.Show("当前没有任何图层。");
                return;
            }
            map.ClearLayers();
        }
    }
}
