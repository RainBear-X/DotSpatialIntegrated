using Core;

namespace Modules.MapBasics
{
    public class ZoomInModule : IRunnableModule
    {
        public string Name { get { return "放大"; } }
        public string Category { get { return "View"; } }

        public void Run()
        {
            MapContext.Instance.MainMap?.ZoomIn();
        }
    }
}
