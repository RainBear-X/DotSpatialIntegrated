using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Symbology;
using DotSpatial.Topology;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotSpatial.Data.Rasters.GdalExtension;
using ZedGraph;


namespace Modules.Analysis
{
    public partial class HikingPathForm: Form
    {
        #region 类级变量
        // 当前 DEM 栅格
        private IRaster _demRaster;
        // 临时存储绘制路径的坐标
        private List<Coordinate> _tempCoords;
        // 地图上的线图层
        private IMapLineLayer _pathLayer;
        // 线要素集
        private FeatureSet lineF;
        // 标记是否处于绘制状态
        private bool _isDrawing;
        #endregion
        public HikingPathForm()
        {
            InitializeComponent();
        }
        public class PathPoint
        {
            public double X;
            public double Y;
            public double Distance;
            public double Elevation;
        }
        public List<PathPoint> ExtractElevation(double startX, double startY, double endX, double endY, IMapRasterLayer raster)
        {
            double curX = startX;
            double curY = startY;
            double curElevation = 0;
            List<PathPoint> pathPointList = new List<PathPoint>();
            int numberofpoints = 100;
            double constXdif = ((endX - startX) / numberofpoints);
            double constYdif = ((endY - startY) / numberofpoints);
            for (int i = 0; i <= numberofpoints; i++)
            {
                PathPoint newPathPoint = new PathPoint();
                if ((i == 0))
                {
                    curX = startX;
                    curY = startY;
                }
                else
                {
                    curX = curX + constXdif;
                    curY = curY + constYdif;
                }
                Coordinate coordinate = new Coordinate(curX, curY);
                RcIndex rowColumn = raster.DataSet.Bounds.ProjToCell(coordinate);
                curElevation = raster.DataSet.Value[rowColumn.Row, rowColumn.Column];
                //set the properties of new PathPoint
                newPathPoint.X = curX;
                newPathPoint.Y = curY;
                newPathPoint.Elevation = curElevation;
                pathPointList.Add(newPathPoint);
            }
            return pathPointList;
        }

        private void map1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void btnLoadRaster_Click(object sender, EventArgs e)
        {
            try
            {
                // 初始化 GDAL 配置
                GdalConfiguration.ConfigureGdal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("GDAL 初始化失败: " + ex.Message);
                return;
            }

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "栅格文件 (*.tif;*.img)|*.tif;*.img|所有文件 (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string filePath = ofd.FileName;
                    try
                    {
                        // 使用 GDAL 提供程序打开栅格文件
                        var provider = new GdalRasterProvider();
                        IRaster raster = provider.Open(filePath);

                        if (raster != null)
                        {
                            map1.Layers.Add(raster);
                            map1.ZoomToMaxExtent();
                        }
                        else
                        {
                            MessageBox.Show("无法打开选定的文件。");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"加载文件时出错: {ex.Message}");
                    }
                }
            }
        }

        private void btnDrawPath_Click(object sender, EventArgs e)
        {
            _isDrawing = true;
            _tempCoords = new List<Coordinate>();
            // 关闭地图默认交互以捕获 MouseDown
            map1.FunctionMode = FunctionMode.None;
            map1.Cursor = Cursors.Cross;
            // 初始化空的线要素集
            lineF = new FeatureSet(FeatureType.Line)
            {
                Projection = map1.Projection
            };
            // 添加 ID 字段
            var idCol = new DataColumn("ID", typeof(int));
            lineF.DataTable.Columns.Add(idCol);
            // 移除旧的路径图层
            if (_pathLayer != null) map1.Layers.Remove(_pathLayer);
            // 新增图层并符号化
            _pathLayer = (IMapLineLayer)map1.Layers.Add(lineF);
            _pathLayer.Symbolizer = new LineSymbolizer(Color.Red, 2);
            _pathLayer.LegendText = "Hiking Path";
        }

        private void map1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!_isDrawing) return;

            if (e.Button == MouseButtons.Left)
            {
                // 1) 记录点击坐标
                var coord = map1.PixelToProj(e.Location);
                _tempCoords.Add(coord);

                // 2) 只有当点数 >= 2 时，才更新线要素
                if (_tempCoords.Count >= 2)
                {
                    // 清除旧要素
                    lineF.Features.Clear();
                    // 构造新的 LineString（保证 Count>=2）
                    var lineGeom = new DotSpatial.Topology.LineString(_tempCoords.ToArray());
                    // 添加到要素集
                    lineF.AddFeature(lineGeom);
                    // 刷新地图
                    map1.ResetBuffer();
                }
            }
            else if (e.Button == MouseButtons.Right && _tempCoords.Count > 1)
            {
                // … 结束前删除 <2 点要素 …
                var invalid = lineF.Features
                                   .Where(f => f.BasicGeometry.Coordinates.Count <= 1)
                                   .ToList();
                foreach (var f in invalid) lineF.Features.Remove(f);

                lineF.SaveAs("linepath.shp", overwrite: true);
                btnViewElevation.Enabled = true;
                _isDrawing = false;
                map1.FunctionMode = FunctionMode.Pan;
                map1.Cursor = Cursors.Default;
                MessageBox.Show("Hiking path 已保存");
            }
        }

        private void btnViewElevation_Click(object sender, EventArgs e)
        {
            try
            {
                //extract the complete elevation
                //get the raster layer
                IMapRasterLayer rasterLayer = default(IMapRasterLayer);
                if (map1.GetRasterLayers().Count() == 0)
                {
                    MessageBox.Show("Please add a raster layer");
                    return;
                }

                //use the first raster layer in the map
                rasterLayer = map1.GetRasterLayers()[0];

                //get the ski path line layer
                IMapLineLayer pathLayer = default(IMapLineLayer);
                if (map1.GetLineLayers().Count() == 0)
                {
                    MessageBox.Show("Please add the ski path");
                    return;
                }
                pathLayer = map1.GetLineLayers()[0];

                IFeatureSet featureSet = pathLayer.DataSet;
                //get the coordinates of the ski path. this is the first feature of
                //the feature set.
                IList<Coordinate> coordinateList = featureSet.Features[0].Coordinates;

                //get elevation of all segments of the path
                List<PathPoint> fullPathList = new List<PathPoint>();

                for (int i = 0; i < coordinateList.Count - 1; i++)
                {
                    //for each line segment
                    Coordinate startCoord = coordinateList[i];
                    Coordinate endCoord = coordinateList[i + 1];
                    List<PathPoint> segmentPointList = ExtractElevation(startCoord.X, startCoord.Y, endCoord.X, endCoord.Y, rasterLayer);
                    //add list of points from this line segment to the complete list
                    fullPathList.AddRange(segmentPointList);
                }

                //calculate the distance
                double distanceFromStart = 0;
                for (int i = 1; i <= fullPathList.Count - 1; i++)
                {
                    //distance between two neighbouring points
                    double x1 = fullPathList[i - 1].X;
                    double y1 = fullPathList[i - 1].Y;
                    double x2 = fullPathList[i].X;
                    double y2 = fullPathList[i].Y;
                    double distance12 = Math.Sqrt(((x2 - x1) * (x2 - x1)) + ((y2 - y1) * (y2 - y1)));
                    distanceFromStart += distance12;
                    fullPathList[i].Distance = distanceFromStart;
                }

                HikingGraphForm graphForm = new HikingGraphForm(fullPathList);
                graphForm.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error calculating elevation. the whole path should be inside the DEM area");
            }
        }
    }
}
