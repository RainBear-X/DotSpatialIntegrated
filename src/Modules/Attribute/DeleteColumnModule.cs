using System;
using System.Linq;
using System.Windows.Forms;
using DotSpatial.Controls;
using Core;
using DotSpatial.Data;
using System.Data;

namespace Modules.Attribute
{
    /// <summary>
    /// 删除当前选中要素图层属性表中的列
    /// </summary>
    public class DeleteColumnModule : IRunnableModule
    {
        public string Name => "Delete Column";
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

            var dt = layer.DataSet.DataTable;
            var cols = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray();
            if (cols.Length == 0)
            {
                MessageBox.Show("当前图层没有属性列可删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dlg = new Form())
            {
                dlg.Text = "Delete Column";
                dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.ClientSize = new System.Drawing.Size(300, 250);
                dlg.MaximizeBox = false;
                dlg.MinimizeBox = false;

                var lbl = new Label { Left = 10, Top = 10, AutoSize = true, Text = "Select Column(s):" };
                var lst = new ListBox { Left = 10, Top = 30, Width = 260, Height = 150, SelectionMode = SelectionMode.MultiExtended };
                lst.Items.AddRange(cols);

                var btnOK = new Button { Text = "OK", Left = 120, Width = 70, Top = 190, DialogResult = DialogResult.OK };
                var btnCancel = new Button { Text = "Cancel", Left = 200, Width = 70, Top = 190, DialogResult = DialogResult.Cancel };

                dlg.Controls.AddRange(new Control[] { lbl, lst, btnOK, btnCancel });
                dlg.AcceptButton = btnOK;
                dlg.CancelButton = btnCancel;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var selected = lst.SelectedItems.Cast<string>().ToList();
                    if (!selected.Any()) return;
                    foreach (var colName in selected)
                    {
                        if (dt.Columns.Contains(colName))
                        {
                            dt.Columns.Remove(colName);
                        }
                    }
                    MessageBox.Show($"已删除列: {string.Join(", ", selected)}", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}