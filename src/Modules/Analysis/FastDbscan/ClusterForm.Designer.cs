namespace Modules.Analysis.FastDbscan
{
    partial class ClusterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblMinPts = new System.Windows.Forms.Label();
            this.numMinPts = new System.Windows.Forms.NumericUpDown();
            this.lblEps = new System.Windows.Forms.Label();
            this.numEps = new System.Windows.Forms.NumericUpDown();
            this.chkManualEps = new System.Windows.Forms.CheckBox();
            this.chkKdTree = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numMinPts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEps)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMinPts
            // 
            this.lblMinPts.AutoSize = true;
            this.lblMinPts.Location = new System.Drawing.Point(59, 42);
            this.lblMinPts.Name = "lblMinPts";
            this.lblMinPts.Size = new System.Drawing.Size(80, 18);
            this.lblMinPts.TabIndex = 0;
            this.lblMinPts.Text = "MinPts：";
            // 
            // numMinPts
            // 
            this.numMinPts.Location = new System.Drawing.Point(145, 40);
            this.numMinPts.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numMinPts.Name = "numMinPts";
            this.numMinPts.Size = new System.Drawing.Size(120, 28);
            this.numMinPts.TabIndex = 1;
            this.numMinPts.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // lblEps
            // 
            this.lblEps.AutoSize = true;
            this.lblEps.Location = new System.Drawing.Point(59, 96);
            this.lblEps.Name = "lblEps";
            this.lblEps.Size = new System.Drawing.Size(53, 18);
            this.lblEps.TabIndex = 2;
            this.lblEps.Text = "Eps：";
            // 
            // numEps
            // 
            this.numEps.DecimalPlaces = 3;
            this.numEps.Enabled = false;
            this.numEps.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numEps.Location = new System.Drawing.Point(145, 96);
            this.numEps.Name = "numEps";
            this.numEps.Size = new System.Drawing.Size(120, 28);
            this.numEps.TabIndex = 3;
            // 
            // chkManualEps
            // 
            this.chkManualEps.AutoSize = true;
            this.chkManualEps.Location = new System.Drawing.Point(62, 151);
            this.chkManualEps.Name = "chkManualEps";
            this.chkManualEps.Size = new System.Drawing.Size(70, 22);
            this.chkManualEps.TabIndex = 4;
            this.chkManualEps.Text = "手动";
            this.chkManualEps.UseVisualStyleBackColor = true;
            this.chkManualEps.CheckedChanged += new System.EventHandler(this.chkManualEps_CheckedChanged);
            // 
            // chkKdTree
            // 
            this.chkKdTree.AutoSize = true;
            this.chkKdTree.Location = new System.Drawing.Point(159, 151);
            this.chkKdTree.Name = "chkKdTree";
            this.chkKdTree.Size = new System.Drawing.Size(142, 22);
            this.chkKdTree.TabIndex = 5;
            this.chkKdTree.Text = "启用 KD-Tree";
            this.chkKdTree.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(99, 198);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 37);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(190, 198);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 37);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // ClusterForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(411, 333);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chkKdTree);
            this.Controls.Add(this.chkManualEps);
            this.Controls.Add(this.numEps);
            this.Controls.Add(this.lblEps);
            this.Controls.Add(this.numMinPts);
            this.Controls.Add(this.lblMinPts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClusterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Fast DBSCAN 参数";
            ((System.ComponentModel.ISupportInitialize)(this.numMinPts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEps)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMinPts;
        private System.Windows.Forms.NumericUpDown numMinPts;
        private System.Windows.Forms.Label lblEps;
        private System.Windows.Forms.NumericUpDown numEps;
        private System.Windows.Forms.CheckBox chkManualEps;
        private System.Windows.Forms.CheckBox chkKdTree;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}