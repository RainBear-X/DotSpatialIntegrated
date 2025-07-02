using Core;
using DotSpatial.Controls;

namespace Modules.MapBasics
{
    public class InfoModule : IRunnableModule
    {
        public string Name { get { return "信息查询"; } }
        public string Category { get { return "Tools"; } }

        public void Run()
        {
            var map = MapContext.Instance.MainMap;
            if (map != null)
            {
                map.FunctionMode = FunctionMode.Info;
            }
        }
    }
}
