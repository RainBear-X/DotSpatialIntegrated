using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Data;

namespace Modules.Attribute
{
    public partial class FieldSelectDialog : Form
    {
        private readonly Map _map;

        public IMapFeatureLayer SelectedLayer { get; private set; }
        public string SelectedField { get; private set; }

        public FieldSelectDialog(Map map, string title, bool polygonOnly = false)
        {
            _map = map;
            InitializeComponent();

            // **在这里告诉下拉框，用 Layer.LegendText 来显示文字**
            cmbLayer.DisplayMember = "LegendText";
            // （如果你希望用 Layer.Name，则改成 "Name"）

            this.Text = title;

            // … 原有的收集和添加 layer 逻辑
            var featureLayers = new List<IMapFeatureLayer>();
            CollectFeatureLayers(_map.Layers, featureLayers, polygonOnly);
            cmbLayer.Items.Clear();
            foreach (var lyr in featureLayers)
                cmbLayer.Items.Add(lyr);
            if (cmbLayer.Items.Count > 0)
                cmbLayer.SelectedIndex = 0;
        }

        // 递归 helper：收集所有符合条件的 IMapFeatureLayer
        protected void CollectFeatureLayers(IEnumerable<IMapLayer> source,
                                          List<IMapFeatureLayer> dest,
                                          bool polygonOnly)
        {
            foreach (var lyr in source)
            {
                // 如果是要素图层，并满足多边形筛选
                if (lyr is IMapFeatureLayer fl)
                {
                    if (!polygonOnly || fl is MapPolygonLayer)
                        dest.Add(fl);
                }
                // 如果是图层组，递归其中的子图层
                if (lyr is IMapGroup grp)
                {
                    CollectFeatureLayers(grp.Layers, dest, polygonOnly);
                }
            }
        }

        protected void cmbLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbField.Items.Clear();
            SelectedLayer = cmbLayer.SelectedItem as IMapFeatureLayer;
            if (SelectedLayer == null) return;

            // 确保属性加载到内存
            SelectedLayer.DataSet.FillAttributes();

            foreach (var col in SelectedLayer.DataSet.GetColumns())
                cmbField.Items.Add(col.ColumnName);

            if (cmbField.Items.Count > 0)
                cmbField.SelectedIndex = 0;
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (cmbLayer.SelectedItem == null || cmbField.SelectedItem == null)
            {
                MessageBox.Show("请先选择图层和字段！");
                return;
            }
            SelectedLayer = cmbLayer.SelectedItem as IMapFeatureLayer;
            SelectedField = cmbField.SelectedItem.ToString();
            DialogResult = DialogResult.OK;
        }
    }
}
