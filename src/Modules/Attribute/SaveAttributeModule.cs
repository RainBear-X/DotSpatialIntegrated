using System;
using System.Windows.Forms;
using DotSpatial.Controls;
using Core;

namespace Modules.Attribute
{
    /// <summary>
    /// 保存当前选中要素图层的属性表到原始 Shapefile 的 .dbf 文件中。
    /// </summary>
    public class SaveAttributeModule : IRunnableModule
    {
        public string Name => "Save Attributes";
        public string Category => "Attribute";

        public void Run()
        {
            var map = MapContext.Instance.MainMap;
            var layer = map.Layers.SelectedLayer as IMapFeatureLayer;
            if (layer == null)
            {
                MessageBox.Show("请先在左侧图层管理中选择一个要素图层！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                layer.DataSet.Save();
                MessageBox.Show("属性表已成功保存！", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存属性表时出错：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}