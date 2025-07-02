using System;
using System.Windows.Forms;
using DotSpatial.Controls;
using Core;
using DotSpatial.Data;
using System.Data;

namespace Modules.Attribute
{
    /// <summary>
    /// 添加列到当前选中要素图层的属性表
    /// </summary>
    public class AddColumnModule : IRunnableModule
    {
        public string Name => "Add Column";
        public string Category => "Attribute";

        public void Run()
        {
            var map = MapContext.Instance.MainMap;
            var layer = map?.Layers.SelectedLayer as IMapFeatureLayer;
            if (layer == null)
            {
                MessageBox.Show("请先在左侧图层管理中选择一个要素图层！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 弹出对话框获取列名和类型
            using (var dlg = new Form())
            {
                dlg.Text = "Add Column";
                dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.ClientSize = new System.Drawing.Size(300, 140);
                dlg.MaximizeBox = false;
                dlg.MinimizeBox = false;

                var lblName = new Label { Left = 10, Top = 10, AutoSize = true, Text = "Column Name:" };
                var txtName = new TextBox { Left = 100, Top = 8, Width = 180 };

                var lblType = new Label { Left = 10, Top = 40, AutoSize = true, Text = "Data Type:" };
                var cmbType = new ComboBox { Left = 100, Top = 38, Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };
                cmbType.Items.AddRange(new string[] { "String", "Integer", "Double", "DateTime" });
                cmbType.SelectedIndex = 0;

                var btnOK = new Button { Text = "OK", Left = 120, Width = 70, Top = 80, DialogResult = DialogResult.OK };
                var btnCancel = new Button { Text = "Cancel", Left = 200, Width = 70, Top = 80, DialogResult = DialogResult.Cancel };

                dlg.Controls.AddRange(new Control[] { lblName, txtName, lblType, cmbType, btnOK, btnCancel });
                dlg.AcceptButton = btnOK;
                dlg.CancelButton = btnCancel;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string colName = txtName.Text.Trim();
                    if (string.IsNullOrEmpty(colName))
                    {
                        MessageBox.Show("列名不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var dt = layer.DataSet.DataTable;
                    if (dt.Columns.Contains(colName))
                    {
                        MessageBox.Show($"列 '{colName}' 已存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Type dataType;
                    switch (cmbType.SelectedItem.ToString())
                    {
                        case "String": dataType = typeof(string); break;
                        case "Integer": dataType = typeof(int); break;
                        case "Double": dataType = typeof(double); break;
                        case "DateTime": dataType = typeof(DateTime); break;
                        default: dataType = typeof(string); break;
                    }

                    // 添加列
                    dt.Columns.Add(colName, dataType);
                    MessageBox.Show($"已添加列 '{colName}' (类型: {dataType.Name})", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}