using System;
using System.Windows.Forms;
using DotSpatial.Controls;

namespace Modules.Attribute
{
    /// <summary>带条件和值输入的查询对话框</summary>
    public partial class AttributeQueryDialog : FieldSelectDialog
    {
        public string Condition => cmbOperator.SelectedItem.ToString();
        public string ValueText => txtValue.Text;

        public AttributeQueryDialog(Map map, string title)
            : base(map, title, true)   // 只允许多边形图层示例，可改 false
        {
            InitializeComponent();      // 添加额外控件
        }

        private void btnOK2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtValue.Text))
            {
                MessageBox.Show("请输入查询值！");
                return;
            }
            base.btnOK_Click(sender, e); // 调用父类验证并关闭对话框
        }
    }
}
