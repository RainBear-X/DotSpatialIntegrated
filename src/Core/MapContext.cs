using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotSpatial.Controls;
using System.Windows.Forms;

namespace Core
{
    public sealed class MapContext
    {
        private MapContext() { }
        private static readonly MapContext _instance = new MapContext();
        public static MapContext Instance { get { return _instance; } }

        public Map MainMap { get; set; }
        public Legend MainLegend { get; set; }
        public DataGridView AttributeGrid { get; set; }
    }
}

