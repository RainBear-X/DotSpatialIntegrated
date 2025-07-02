using System;
using System.IO;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Data;
using Core;

namespace Modules.Shapefile
{
    /// <summary>
    /// 保存当前选中的面 Shapefile（Polygon）
    /// </summary>
    public class SavePolygonShpModule : IRunnableModule
    {
        public string Name => "Save";
        public string Category => "Shapefile/Polygon";

        public void Run()
        {
            var map = MapContext.Instance.MainMap;
            var layer = map.Layers.SelectedLayer as IMapFeatureLayer;
            if (layer == null)
            {
                MessageBox.Show("请先在左侧图层管理中选择一个要素图层！");
                return;
            }
            string orig = (layer.DataSet as IFeatureSet)?.Filename;
            string defaultName = !string.IsNullOrEmpty(orig)
                                 ? Path.GetFileName(orig)
                                 : "new_polygon.shp";
            using (var sfd = new SaveFileDialog
            {
                Filter = "Shapefile|*.shp",
                FileName = defaultName
            })
            {
                if (sfd.ShowDialog() != DialogResult.OK) return;
                layer.DataSet.SaveAs(sfd.FileName, true);
                MessageBox.Show("已保存至：" + sfd.FileName);
            }
        }
    }
}
