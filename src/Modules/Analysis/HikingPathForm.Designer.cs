namespace Modules.Analysis
{
    partial class HikingPathForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HikingPathForm));
            this.pnlOperations = new System.Windows.Forms.Panel();
            this.lbltitle = new System.Windows.Forms.Label();
            this.btnViewElevation = new System.Windows.Forms.Button();
            this.btnDrawPath = new System.Windows.Forms.Button();
            this.btnLoadDEM = new System.Windows.Forms.Button();
            this.pnlLegend = new System.Windows.Forms.Panel();
            this.legend1 = new DotSpatial.Controls.Legend();
            this.pnlMap = new System.Windows.Forms.Panel();
            this.map1 = new DotSpatial.Controls.Map();
            this.appManager1 = new DotSpatial.Controls.AppManager();
            this.pnlOperations.SuspendLayout();
            this.pnlLegend.SuspendLayout();
            this.pnlMap.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOperations
            // 
            this.pnlOperations.Controls.Add(this.lbltitle);
            this.pnlOperations.Controls.Add(this.btnViewElevation);
            this.pnlOperations.Controls.Add(this.btnDrawPath);
            this.pnlOperations.Controls.Add(this.btnLoadDEM);
            this.pnlOperations.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOperations.Location = new System.Drawing.Point(0, 0);
            this.pnlOperations.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlOperations.Name = "pnlOperations";
            this.pnlOperations.Size = new System.Drawing.Size(994, 146);
            this.pnlOperations.TabIndex = 0;
            // 
            // lbltitle
            // 
            this.lbltitle.AutoSize = true;
            this.lbltitle.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltitle.Location = new System.Drawing.Point(428, 30);
            this.lbltitle.Name = "lbltitle";
            this.lbltitle.Size = new System.Drawing.Size(266, 37);
            this.lbltitle.TabIndex = 3;
            this.lbltitle.Text = "Hiking Path Finder";
            // 
            // btnViewElevation
            // 
            this.btnViewElevation.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewElevation.Location = new System.Drawing.Point(706, 92);
            this.btnViewElevation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnViewElevation.Name = "btnViewElevation";
            this.btnViewElevation.Size = new System.Drawing.Size(162, 37);
            this.btnViewElevation.TabIndex = 2;
            this.btnViewElevation.Text = "&View Elevation";
            this.btnViewElevation.UseVisualStyleBackColor = true;
            this.btnViewElevation.Click += new System.EventHandler(this.btnViewElevation_Click);
            // 
            // btnDrawPath
            // 
            this.btnDrawPath.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDrawPath.Location = new System.Drawing.Point(469, 92);
            this.btnDrawPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDrawPath.Name = "btnDrawPath";
            this.btnDrawPath.Size = new System.Drawing.Size(171, 37);
            this.btnDrawPath.TabIndex = 1;
            this.btnDrawPath.Text = "&Draw Hiking Path";
            this.btnDrawPath.UseVisualStyleBackColor = true;
            this.btnDrawPath.Click += new System.EventHandler(this.btnDrawPath_Click);
            // 
            // btnLoadDEM
            // 
            this.btnLoadDEM.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadDEM.Location = new System.Drawing.Point(304, 92);
            this.btnLoadDEM.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLoadDEM.Name = "btnLoadDEM";
            this.btnLoadDEM.Size = new System.Drawing.Size(110, 37);
            this.btnLoadDEM.TabIndex = 0;
            this.btnLoadDEM.Text = "&Load DEM";
            this.btnLoadDEM.UseVisualStyleBackColor = true;
            this.btnLoadDEM.Click += new System.EventHandler(this.btnLoadRaster_Click);
            // 
            // pnlLegend
            // 
            this.pnlLegend.Controls.Add(this.legend1);
            this.pnlLegend.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLegend.Location = new System.Drawing.Point(0, 146);
            this.pnlLegend.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlLegend.Name = "pnlLegend";
            this.pnlLegend.Size = new System.Drawing.Size(200, 564);
            this.pnlLegend.TabIndex = 1;
            // 
            // legend1
            // 
            this.legend1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.legend1.ControlRectangle = new System.Drawing.Rectangle(0, 0, 200, 564);
            this.legend1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.legend1.DocumentRectangle = new System.Drawing.Rectangle(0, 0, 187, 428);
            this.legend1.HorizontalScrollEnabled = true;
            this.legend1.Indentation = 30;
            this.legend1.IsInitialized = false;
            this.legend1.Location = new System.Drawing.Point(0, 0);
            this.legend1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.legend1.MinimumSize = new System.Drawing.Size(4, 5);
            this.legend1.Name = "legend1";
            this.legend1.ProgressHandler = null;
            this.legend1.ResetOnResize = false;
            this.legend1.SelectionFontColor = System.Drawing.Color.Black;
            this.legend1.SelectionHighlight = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(238)))), ((int)(((byte)(252)))));
            this.legend1.Size = new System.Drawing.Size(200, 564);
            this.legend1.TabIndex = 0;
            this.legend1.Text = "legend1";
            this.legend1.VerticalScrollEnabled = true;
            // 
            // pnlMap
            // 
            this.pnlMap.Controls.Add(this.map1);
            this.pnlMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMap.Location = new System.Drawing.Point(200, 146);
            this.pnlMap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlMap.Name = "pnlMap";
            this.pnlMap.Size = new System.Drawing.Size(794, 564);
            this.pnlMap.TabIndex = 2;
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
            this.map1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.map1.Name = "map1";
            this.map1.ProgressHandler = null;
            this.map1.ProjectionModeDefine = DotSpatial.Controls.ActionMode.Prompt;
            this.map1.ProjectionModeReproject = DotSpatial.Controls.ActionMode.Prompt;
            this.map1.RedrawLayersWhileResizing = false;
            this.map1.SelectionEnabled = true;
            this.map1.Size = new System.Drawing.Size(794, 564);
            this.map1.TabIndex = 0;
            this.map1.Load += new System.EventHandler(this.map1_Load);
            this.map1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.map1_MouseDown);
            // 
            // appManager1
            // 
            this.appManager1.Directories = ((System.Collections.Generic.List<string>)(resources.GetObject("appManager1.Directories")));
            this.appManager1.DockManager = null;
            this.appManager1.HeaderControl = null;
            this.appManager1.Legend = this.legend1;
            this.appManager1.Map = this.map1;
            this.appManager1.ProgressHandler = null;
            this.appManager1.ShowExtensionsDialogMode = DotSpatial.Controls.ShowExtensionsDialogMode.Default;
            // 
            // HikingPathForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 710);
            this.Controls.Add(this.pnlMap);
            this.Controls.Add(this.pnlLegend);
            this.Controls.Add(this.pnlOperations);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "HikingPathForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlOperations.ResumeLayout(false);
            this.pnlOperations.PerformLayout();
            this.pnlLegend.ResumeLayout(false);
            this.pnlMap.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlOperations;
        private System.Windows.Forms.Panel pnlLegend;
        private System.Windows.Forms.Panel pnlMap;
        private System.Windows.Forms.Button btnLoadDEM;
        private DotSpatial.Controls.Legend legend1;
        private DotSpatial.Controls.AppManager appManager1;
        private System.Windows.Forms.Label lbltitle;
        private System.Windows.Forms.Button btnViewElevation;
        private System.Windows.Forms.Button btnDrawPath;
        protected DotSpatial.Controls.Map map1;
    }
}

