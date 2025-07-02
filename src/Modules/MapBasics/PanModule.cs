using Core;
using DotSpatial.Controls;

namespace Modules.MapBasics
{
    public class PanModule : IRunnableModule
    {
        public string Name { get { return "平移"; } }
        public string Category { get { return "Tools"; } }

        public void Run()
        {
            var map = MapContext.Instance.MainMap;
            if (map != null)
            {
                map.FunctionMode = FunctionMode.Pan;
            }
        }
    }
}
