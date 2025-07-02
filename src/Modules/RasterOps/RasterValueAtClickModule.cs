using System.Linq;
using System.Windows.Forms;
using Core;
using DotSpatial.Controls;
using DotSpatial.Data;

namespace Modules.RasterOps
{
    public static class RasterValueAtClickModule
    {
        private static MouseEventHandler _handler;

        public static void EnableClickValue(bool enable, Label outputLabel)
        {
            var map = MapContext.Instance.MainMap;
            if (map == null) return;

            // 解绑旧事件
            if (_handler != null)
            {
                map.MouseUp -= _handler;
                _handler = null;
            }

            if (!enable)
            {
                map.Cursor = Cursors.Arrow;
                return;
            }

            map.Cursor = Cursors.Cross;
            _handler = (s, e) =>
            {
                if (e.Button != MouseButtons.Left) return;

                var lyr = map.Layers.SelectedLayer as IMapRasterLayer;
                if (lyr == null)
                {
                    outputLabel.Text = "请在左侧选择栅格图层";
                    return;
                }

                var coord = map.PixelToProj(e.Location);
                var rc = lyr.DataSet.Bounds.ProjToCell(coord);

                if (rc.Row < 0 || rc.Row >= lyr.DataSet.NumRows ||
                    rc.Column < 0 || rc.Column >= lyr.DataSet.NumColumns)
                {
                    outputLabel.Text = "超出范围";
                }
                else
                {
                    var val = lyr.DataSet.Value[rc.Row, rc.Column];
                    outputLabel.Text = $"行:{rc.Row} 列:{rc.Column} 值:{val}";
                }
            };
            map.MouseUp += _handler;
        }
    }
}
