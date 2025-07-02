using Core;
using DotSpatial.Controls;

namespace Modules.MapBasics
{
    public class SelectModule : IRunnableModule
    {
        public string Name { get { return "框选"; } }
        public string Category { get { return "Tools"; } }

        public void Run()
        {
            var map = MapContext.Instance.MainMap;
            if (map != null)
            {
                map.FunctionMode = FunctionMode.Select;
            }
        }
    }
}
