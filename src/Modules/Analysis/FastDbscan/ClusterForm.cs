using System;
using System.Windows.Forms;

namespace Modules.Analysis.FastDbscan
{
    public partial class ClusterForm : Form
    {
        public ClusterForm() { InitializeComponent(); }

        // 公开只读属性供外部读取
        public int MinPts => (int)numMinPts.Value;
        public bool ManualEps => chkManualEps.Checked;
        public double EpsValue => (double)numEps.Value;
        public bool UseKdTree => chkKdTree.Checked;

        // 手动 Eps 开关 ⇒ 控制 numEps 启用状态
        private void chkManualEps_CheckedChanged(object sender, EventArgs e)
        {
            numEps.Enabled = chkManualEps.Checked;
        }
    }
}
