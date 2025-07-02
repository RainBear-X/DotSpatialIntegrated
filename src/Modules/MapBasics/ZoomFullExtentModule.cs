using Core;

namespace Modules.MapBasics
{
    public class ZoomFullExtentModule : IRunnableModule
    {
        public string Name { get { return "全图"; } }
        public string Category { get { return "View"; } }

        public void Run()
        {
            MapContext.Instance.MainMap?.ZoomToMaxExtent();
        }
    }
}
