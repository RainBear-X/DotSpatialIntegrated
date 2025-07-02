using System;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Data;
using Core;

namespace Modules.Attribute
{
    /// <summary>
    /// 将当前选中要素图层的属性表绑定到主界面 DataGridView 并显示。
    /// </summary>
    public class ViewAttributeModule : IRunnableModule
    {
        public string Name => "View Attributes";
        public string Category => "Attribute";

        public void Run()
        {
            var map = MapContext.Instance.MainMap;
            var grid = MapContext.Instance.AttributeGrid;
            if (map == null || grid == null)
            {
                MessageBox.Show("地图或属性表控件未初始化！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var layer = map.Layers.SelectedLayer as IMapFeatureLayer;
            if (layer == null)
            {
                MessageBox.Show("请先在左侧图层管理中选择一个要素图层！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 绑定并显示属性表
            grid.DataSource = layer.DataSet.DataTable;
            grid.Focus();
        }
    }
}