namespace Modules.Attribute
{
    partial class AttributeQueryDialog
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
            this.lblOperator = new System.Windows.Forms.Label();
            this.cmbOperator = new System.Windows.Forms.ComboBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblLayer
            // 
            this.lblLayer.Location = new System.Drawing.Point(59, 45);
            // 
            // cmbLayer
            // 
            this.cmbLayer.Location = new System.Drawing.Point(118, 42);
            // 
            // lblField
            // 
            this.lblField.Location = new System.Drawing.Point(59, 100);
            // 
            // cmbField
            // 
            this.cmbField.Location = new System.Drawing.Point(118, 100);
            // 
            // lblOperator
            // 
            this.lblOperator.AutoSize = true;
            this.lblOperator.Location = new System.Drawing.Point(59, 154);
            this.lblOperator.Name = "lblOperator";
            this.lblOperator.Size = new System.Drawing.Size(53, 18);
            this.lblOperator.TabIndex = 0;
            this.lblOperator.Text = "条件:";
            // 
            // cmbOperator
            // 
            this.cmbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOperator.FormattingEnabled = true;
            this.cmbOperator.Items.AddRange(new object[] {
            "=",
            ">",
            "<",
            ">=",
            "<=",
            "!=",
            "LIKE"});
            this.cmbOperator.Location = new System.Drawing.Point(118, 154);
            this.cmbOperator.Name = "cmbOperator";
            this.cmbOperator.Size = new System.Drawing.Size(121, 26);
            this.cmbOperator.TabIndex = 1;
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(77, 203);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(35, 18);
            this.lblValue.TabIndex = 2;
            this.lblValue.Text = "值:";
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(118, 200);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(100, 28);
            this.txtValue.TabIndex = 3;
            // 
            // AttributeQueryDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 412);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.cmbOperator);
            this.Controls.Add(this.lblOperator);
            this.Name = "AttributeQueryDialog";
            this.Text = "AttributeQueryDialog";
            this.Controls.SetChildIndex(this.lblOperator, 0);
            this.Controls.SetChildIndex(this.cmbOperator, 0);
            this.Controls.SetChildIndex(this.lblValue, 0);
            this.Controls.SetChildIndex(this.txtValue, 0);
            this.Controls.SetChildIndex(this.lblLayer, 0);
            this.Controls.SetChildIndex(this.cmbLayer, 0);
            this.Controls.SetChildIndex(this.lblField, 0);
            this.Controls.SetChildIndex(this.cmbField, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblOperator;
        private System.Windows.Forms.ComboBox cmbOperator;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.TextBox txtValue;
    }
}