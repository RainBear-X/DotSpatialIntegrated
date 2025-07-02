using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Core;
using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Symbology;

namespace Modules.Attribute
{
    /// <summary>
    /// 同步 DataGridView 与当前选中图层的要素选中状态，并安全捕获异常
    /// </summary>
    public static class AttributeTableSync
    {
        /// <summary>
        /// 绑定 DataGridView 的 SelectionChanged 事件
        /// </summary>
        public static void Initialize()
        {
            var grid = MapContext.Instance.AttributeGrid;
            if (grid == null) return;
            grid.SelectionChanged -= Grid_SelectionChanged;
            grid.SelectionChanged += Grid_SelectionChanged;
        }

        private static void Grid_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!(sender is DataGridView dgv)) return;
                var map = MapContext.Instance.MainMap;
                if (map == null) return;

                var layer = map.Layers.SelectedLayer as IMapFeatureLayer;
                if (layer == null) return;

                // 清除现有选中
                layer.ClearSelection();

                DataTable dt = layer.DataSet.DataTable;
                bool hasIdField = dt.Columns.Contains("ID") && dgv.Columns.Contains("ID");

                foreach (DataGridViewRow row in dgv.SelectedRows)
                {
                    if (hasIdField)
                    {
                        var raw = row.Cells["ID"].Value;
                        if (raw == null) continue;
                        string expr;
                        if (dt.Columns["ID"].DataType == typeof(string))
                            expr = $"[ID] = '{raw}'";
                        else
                            expr = $"[ID] = {raw}";
                        layer.SelectByAttribute(expr);
                    }
                    else
                    {
                        int idx = row.Index;
                        var fs = layer.DataSet as FeatureSet;
                        if (fs != null && idx >= 0 && idx < fs.Features.Count)
                        {
                            var feat = fs.Features[idx];
                            layer.Select(feat);
                        }
                    }
                }

                // 刷新地图显示选中
                map.ResetBuffer();
            }
            catch (Exception ex)
            {
                // 捕获并记录异常，防止因 DotSpatial.Legend 内部错误导致程序崩溃
                MessageBox.Show("同步属性表选择时发生错误：" + ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
