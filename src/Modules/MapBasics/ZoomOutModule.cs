using Core;
using DotSpatial.Controls;

namespace Modules.MapBasics
{
    public class ZoomOutModule : IRunnableModule
    {
        public string Name
        {
            get { return "缩小"; }
        }
        public string Category
        {
            get { return "View"; }
        }

        public void Run()
        {
            var map = MapContext.Instance.MainMap;
            if (map != null)
            {
                map.ZoomOut();
            }
        }
    }
}
