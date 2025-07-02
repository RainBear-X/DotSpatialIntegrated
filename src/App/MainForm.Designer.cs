namespace App
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvAttributeTable = new System.Windows.Forms.DataGridView();
            this.map1 = new DotSpatial.Controls.Map();
            this.legend1 = new DotSpatial.Controls.Legend();
            this.splcMain = new System.Windows.Forms.SplitContainer();
            this.chbShowRasterValue = new System.Windows.Forms.CheckBox();
            this.lblRasterValue = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttributeTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splcMain)).BeginInit();
            this.splcMain.Panel1.SuspendLayout();
            this.splcMain.Panel2.SuspendLayout();
            this.splcMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1692, 36);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvAttributeTable);
            this.panel1.Controls.Add(this.splcMain);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1692, 912);
            this.panel1.TabIndex = 1;
            // 
            // dgvAttributeTable
            // 
            this.dgvAttributeTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAttributeTable.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvAttributeTable.Location = new System.Drawing.Point(0, 732);
            this.dgvAttributeTable.Name = "dgvAttributeTable";
            this.dgvAttributeTable.RowHeadersWidth = 62;
            this.dgvAttributeTable.RowTemplate.Height = 30;
            this.dgvAttributeTable.Size = new System.Drawing.Size(1692, 180);
            this.dgvAttributeTable.TabIndex = 2;
            // 
            // map1
            // 
            this.map1.AllowDrop = true;
            this.map1.BackColor = System.Drawing.Color.White;
            this.map1.CollectAfterDraw = false;
            this.map1.CollisionDetection = false;
            this.map1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.map1.ExtendBuffer = false;
            this.map1.FunctionMode = DotSpatial.Controls.FunctionMode.None;
            this.map1.IsBusy = false;
            this.map1.IsZoomedToMaxExtent = false;
            this.map1.Legend = this.legend1;
            this.map1.Location = new System.Drawing.Point(0, 0);
            this.map1.Name = "map1";
            this.map1.ProgressHandler = null;
            this.map1.ProjectionModeDefine = DotSpatial.Controls.ActionMode.Prompt;
            this.map1.ProjectionModeReproject = DotSpatial.Controls.ActionMode.Prompt;
            this.map1.RedrawLayersWhileResizing = false;
            this.map1.SelectionEnabled = true;
            this.map1.Size = new System.Drawing.Size(1375, 912);
            this.map1.TabIndex = 3;
            // 
            // legend1
            // 
            this.legend1.BackColor = System.Drawing.Color.White;
            this.legend1.ControlRectangle = new System.Drawing.Rectangle(0, 0, 313, 912);
            this.legend1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.legend1.DocumentRectangle = new System.Drawing.Rectangle(0, 0, 187, 428);
            this.legend1.HorizontalScrollEnabled = true;
            this.legend1.Indentation = 30;
            this.legend1.IsInitialized = false;
            this.legend1.Location = new System.Drawing.Point(0, 0);
            this.legend1.MinimumSize = new System.Drawing.Size(5, 5);
            this.legend1.Name = "legend1";
            this.legend1.ProgressHandler = null;
            this.legend1.ResetOnResize = false;
            this.legend1.SelectionFontColor = System.Drawing.Color.Black;
            this.legend1.SelectionHighlight = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(238)))), ((int)(((byte)(252)))));
            this.legend1.Size = new System.Drawing.Size(313, 912);
            this.legend1.TabIndex = 4;
            this.legend1.Text = "legend1";
            this.legend1.VerticalScrollEnabled = true;
            // 
            // splcMain
            // 
            this.splcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splcMain.Location = new System.Drawing.Point(0, 0);
            this.splcMain.Name = "splcMain";
            // 
            // splcMain.Panel1
            // 
            this.splcMain.Panel1.Controls.Add(this.legend1);
            // 
            // splcMain.Panel2
            // 
            this.splcMain.Panel2.Controls.Add(this.map1);
            this.splcMain.Size = new System.Drawing.Size(1692, 912);
            this.splcMain.SplitterDistance = 313;
            this.splcMain.TabIndex = 5;
            // 
            // chbShowRasterValue
            // 
            this.chbShowRasterValue.AutoSize = true;
            this.chbShowRasterValue.Location = new System.Drawing.Point(1170, 2);
            this.chbShowRasterValue.Name = "chbShowRasterValue";
            this.chbShowRasterValue.Size = new System.Drawing.Size(124, 22);
            this.chbShowRasterValue.TabIndex = 2;
            this.chbShowRasterValue.Text = "显示栅格值";
            this.chbShowRasterValue.UseVisualStyleBackColor = true;
            this.chbShowRasterValue.CheckedChanged += new System.EventHandler(this.ChbShowRasterValue_CheckedChanged);
            // 
            // lblRasterValue
            // 
            this.lblRasterValue.AutoSize = true;
            this.lblRasterValue.Location = new System.Drawing.Point(1325, 2);
            this.lblRasterValue.Name = "lblRasterValue";
            this.lblRasterValue.Size = new System.Drawing.Size(0, 18);
            this.lblRasterValue.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1692, 948);
            this.Controls.Add(this.lblRasterValue);
            this.Controls.Add(this.chbShowRasterValue);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttributeTable)).EndInit();
            this.splcMain.Panel1.ResumeLayout(false);
            this.splcMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splcMain)).EndInit();
            this.splcMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvAttributeTable;
        private System.Windows.Forms.SplitContainer splcMain;
        private DotSpatial.Controls.Legend legend1;
        private DotSpatial.Controls.Map map1;
        private System.Windows.Forms.CheckBox chbShowRasterValue;
        private System.Windows.Forms.Label lblRasterValue;
    }
}

