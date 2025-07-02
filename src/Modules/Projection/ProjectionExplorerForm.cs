using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Core;
using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Projections;
using DotSpatial.Symbology;

namespace Modules.Projection
{
    public partial class ProjectionExplorerForm : Form
    {
        // 手工维护的投影显示名 + 投影信息数组（与 map1…map6 对应）
        private readonly (string DisplayName, ProjectionInfo ProjInfo)[] _projList = new[]
        {
            ("WGS84 (Geographic)", KnownCoordinateSystems.Geographic.World.WGS1984),
            ("Web Mercator",        KnownCoordinateSystems.Projected.World.WebMercator),
            ("Albers Equal Area",   KnownCoordinateSystems.Projected.NorthAmerica.NorthAmericaAlbersEqualAreaConic),
            ("Lambert Conformal",   KnownCoordinateSystems.Projected.NorthAmerica.USAContiguousLambertConformalConic),
            ("North Pole Azimuthal",KnownCoordinateSystems.Projected.Polar.NorthPoleAzimuthalEquidistant),
            ("Mollweide",           ProjectionInfo.FromProj4String(
                                         "+proj=moll +lon_0=0 +x_0=0 +y_0=0 +units=m +no_defs"))
        };

        private readonly Map[] _maps;
        private readonly Label[] _projLbl, _totLbl, _selLbl, _diffLbl;
        private readonly string _shpPath;

        public ProjectionExplorerForm(IFeatureSet src)
        {
            InitializeComponent();

            // 1) 获取源 Shapefile 路径
            _shpPath = src.Filename;
            if (string.IsNullOrEmpty(_shpPath))
            {
                MessageBox.Show("无法获取源 Shapefile 路径！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            // 2) 从设计器里取得控件数组
            _maps = new[] { map1, map2, map3, map4, map5, map6 };
            _projLbl = new[] { lblProj1, lblProj2, lblProj3, lblProj4, lblProj5, lblProj6 };
            _totLbl = new[] { lblTot1, lblTot2, lblTot3, lblTot4, lblTot5, lblTot6 };
            _selLbl = new[] { lblSel1, lblSel2, lblSel3, lblSel4, lblSel5, lblSel6 };
            _diffLbl = new[] { lblDiff1, lblDiff2, lblDiff3, lblDiff4, lblDiff5, lblDiff6 };

            // 3) 逐个 Map 初始化
            for (int i = 0; i < _projList.Length; i++)
            {
                int idx = i;  // 捕获用
                var map = _maps[idx];
                var dispName = _projList[idx].DisplayName;
                var projInfo = _projList[idx].ProjInfo;

                // 清除 & 设投影
                map.Layers.Clear();
                map.Projection = projInfo;

                // 重新打开 Shapefile
                var fs = (FeatureSet)FeatureSet.Open(_shpPath);
                if (fs.Projection == null || fs.Projection.GeographicInfo == null)
                {
                    fs.Projection = KnownCoordinateSystems.Geographic.World.WGS1984;
                }
                fs.Reproject(projInfo);

                // 添加到 Map
                var layer = map.Layers.Add(fs) as IMapPolygonLayer;

                // 缩放 & 刷新
                map.ZoomToMaxExtent();
                map.ResetBuffer();

                // 进入“选择要素”模式
                map.FunctionMode = FunctionMode.Select;

                // 半透明符号化
                if (layer != null)
                {
                    layer.Symbolizer = new PolygonSymbolizer(
                        Color.FromArgb(60, Color.LightBlue),
                        Color.DarkBlue);
                }

                // 更新投影名 & 总面积
                _projLbl[idx].Text = dispName;
                double totalArea = fs.Features.Sum(f => f.Area());
                _totLbl[idx].Text = "Total: " + totalArea.ToString("N0");

                // 初始化 Selected / Diff
                _selLbl[idx].Text = "Selected: 0";
                _diffLbl[idx].Text = "Diff: 0";

                // 绑定 SelectionChanged（用层自身事件）
                if (layer != null)
                {
                    layer.SelectionChanged += (s, e) =>
                    {
                        // 计算所选要素面积
                        double selArea = layer.Selection.ToFeatureList().Sum(f => f.Area());
                        _selLbl[idx].Text = "Selected: " + selArea.ToString("N0");

                        // 以第一个 Map（idx=0）选中面积为基准
                        double baseVal = 0;
                        double.TryParse(
                            _selLbl[0].Text.Replace("Selected: ", ""),
                            out baseVal);
                        _diffLbl[idx].Text = "Diff: " + (baseVal - selArea).ToString("N0");
                    };
                }
            }
        }
    }
}
