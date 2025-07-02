using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Symbology;
using DotSpatial.Topology;

namespace Core
{
    public enum DrawMode { None, Point, Line, Polygon }

    /// <summary>
    /// 统一的鼠标绘制服务：负责在地图上交互式创建点/线/面 Shapefile，并实时预览。
    /// </summary>
    public static class DrawService
    {
        public static DrawMode CurrentMode { get; private set; } = DrawMode.None;
        private static FeatureSet _fs;
        private static IMapFeatureLayer _layer;
        private static List<Coordinate> _tempCoords = new List<Coordinate>();
        private static int _nextId = 0;
        private static Coordinate _currentCoord;

        /// <summary>开始绘制指定类型的要素，并启用预览</summary>
        public static void BeginDraw(DrawMode mode)
        {
            Cancel();
            CurrentMode = mode;
            _tempCoords.Clear();

            var map = MapContext.Instance.MainMap;
            // 禁用内置交互和右键菜单
            map.FunctionMode = FunctionMode.None;
            map.ContextMenuStrip = null;

            // 创建空 FeatureSet
            switch (mode)
            {
                case DrawMode.Point:
                    _fs = new FeatureSet(FeatureType.Point);
                    break;
                case DrawMode.Line:
                    _fs = new FeatureSet(FeatureType.Line);
                    break;
                case DrawMode.Polygon:
                    _fs = new FeatureSet(FeatureType.Polygon);
                    break;
                default:
                    _fs = null;
                    break;
            }
            if (_fs == null) return;
            _fs.Projection = map.Projection;
            _fs.DataTable.Columns.Add("ID", typeof(int));

            // 添加到地图图层并自动选中
            map.Layers.Add(_fs);
            var addedLayer = map.Layers[map.Layers.Count - 1] as IMapFeatureLayer;
            if (addedLayer != null)
            {
                _layer = addedLayer;
                addedLayer.LegendText = "Drawing " + mode;
                map.Layers.SelectedLayer = addedLayer;
            }

            // 绑定事件和设置光标
            map.Cursor = Cursors.Cross;
            map.MouseDown += Map_MouseDown;
            map.MouseUp += Map_MouseUp;
            map.MouseMove += Map_MouseMove;
            map.Paint += Map_Paint;
        }

        /// <summary>保存绘制结果并结束绘制</summary>
        public static void Save(string path)
        {
            if (_fs == null || _fs.Features.Count == 0)
            {
                MessageBox.Show("当前没有要保存的要素！");
                return;
            }
            _fs.SaveAs(path, true);
            Cancel();
        }

        /// <summary>取消当前绘制，移除事件绑定并恢复交互</summary>
        public static void Cancel()
        {
            if (CurrentMode == DrawMode.None) return;
            var map = MapContext.Instance.MainMap;
            map.MouseDown -= Map_MouseDown;
            map.MouseUp -= Map_MouseUp;
            map.MouseMove -= Map_MouseMove;
            map.Paint -= Map_Paint;
            map.FunctionMode = FunctionMode.Pan;
            map.ContextMenuStrip = null;
            map.Cursor = Cursors.Default;
            CurrentMode = DrawMode.None;
            _tempCoords.Clear();
            _fs = null;
            _layer = null;
        }

        private static void Map_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            var map = MapContext.Instance.MainMap;
            var coord = map.PixelToProj(e.Location);
            if (CurrentMode == DrawMode.Point)
            {
                var f = _fs.AddFeature(new DotSpatial.Topology.Point(coord));
                f.DataRow["ID"] = ++_nextId;
                map.ResetBuffer();
            }
            else
            {
                _tempCoords.Add(coord);
            }
        }

        private static void Map_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var map = MapContext.Instance.MainMap;
            if (CurrentMode == DrawMode.Line && _tempCoords.Count >= 2)
            {
                var f = _fs.AddFeature(new LineString(_tempCoords.ToArray()));
                f.DataRow["ID"] = ++_nextId;
            }
            else if (CurrentMode == DrawMode.Polygon && _tempCoords.Count >= 3)
            {
                var pts = new List<Coordinate>(_tempCoords) { _tempCoords[0] };
                var f = _fs.AddFeature(new Polygon(new LinearRing(pts.ToArray())));
                f.DataRow["ID"] = ++_nextId;
            }
            map.ResetBuffer();
            Cancel();
        }

        private static void Map_MouseMove(object sender, MouseEventArgs e)
        {
            if ((CurrentMode == DrawMode.Line || CurrentMode == DrawMode.Polygon) && _fs != null)
            {
                var map = MapContext.Instance.MainMap;
                _currentCoord = map.PixelToProj(e.Location);
                map.ResetBuffer();
            }
        }

        private static void Map_Paint(object sender, PaintEventArgs e)
        {
            // 仅在折线或多边形绘制模式下预览
            if ((CurrentMode != DrawMode.Line && CurrentMode != DrawMode.Polygon) || _fs == null) return;
            // 必须已有至少一个顶点并且鼠标已移动
            if (_tempCoords.Count == 0 || _currentCoord == null) return;
            var map = MapContext.Instance.MainMap;
            var pts = new List<PointF>();
            foreach (var coord in _tempCoords)
            {
                var p = map.ProjToPixel(coord);
                pts.Add(new PointF((float)p.X, (float)p.Y));
            }
            var cp = map.ProjToPixel(_currentCoord);
            pts.Add(new PointF((float)cp.X, (float)cp.Y));
            if (pts.Count < 2) return;
            using (var pen = new Pen(Color.Red, 2))
            {
                if (CurrentMode == DrawMode.Line)
                    e.Graphics.DrawLines(pen, pts.ToArray());
                else if (CurrentMode == DrawMode.Polygon)
                    e.Graphics.DrawPolygon(pen, pts.ToArray());
            }
        }
    }
}
