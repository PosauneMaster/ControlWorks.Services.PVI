namespace ControlWorks.Application.Configuration
{
    partial class frmDeleteVariable
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
            this.label1 = new System.Windows.Forms.Label();
            this.cboVariableInfo = new System.Windows.Forms.ComboBox();
            this.btnDeleteVariable = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Variable Name:";
            // 
            // cboVariableInfo
            // 
            this.cboVariableInfo.FormattingEnabled = true;
            this.cboVariableInfo.Location = new System.Drawing.Point(15, 42);
            this.cboVariableInfo.Name = "cboVariableInfo";
            this.cboVariableInfo.Size = new System.Drawing.Size(320, 21);
            this.cboVariableInfo.TabIndex = 1;
            // 
            // btnDeleteVariable
            // 
            this.btnDeleteVariable.Location = new System.Drawing.Point(233, 116);
            this.btnDeleteVariable.Name = "btnDeleteVariable";
            this.btnDeleteVariable.Size = new System.Drawing.Size(102, 23);
            this.btnDeleteVariable.TabIndex = 2;
            this.btnDeleteVariable.Text = "Delete Variable";
            this.btnDeleteVariable.UseVisualStyleBackColor = true;
            this.btnDeleteVariable.Click += new System.EventHandler(this.btnDeleteVariable_Click);
            // 
            // frmDeleteVariable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 173);
            this.Controls.Add(this.btnDeleteVariable);
            this.Controls.Add(this.cboVariableInfo);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmDeleteVariable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Delete Variable";
            this.Load += new System.EventHandler(this.frmDeleteVariable_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboVariableInfo;
        private System.Windows.Forms.Button btnDeleteVariable;
    }
}