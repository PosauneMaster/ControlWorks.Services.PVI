namespace ControlWorks.Application.Configuration
{
    partial class frmAddVariable
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtCpuVariableName = new System.Windows.Forms.TextBox();
            this.txtVariableName = new System.Windows.Forms.TextBox();
            this.btnAddVariable = new System.Windows.Forms.Button();
            this.btnCancelAddVariable = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cpu Panel Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Variable Name:";
            // 
            // txtCpuVariableName
            // 
            this.txtCpuVariableName.Enabled = false;
            this.txtCpuVariableName.Location = new System.Drawing.Point(108, 28);
            this.txtCpuVariableName.Name = "txtCpuVariableName";
            this.txtCpuVariableName.Size = new System.Drawing.Size(178, 20);
            this.txtCpuVariableName.TabIndex = 2;
            // 
            // txtVariableName
            // 
            this.txtVariableName.Location = new System.Drawing.Point(108, 71);
            this.txtVariableName.Name = "txtVariableName";
            this.txtVariableName.Size = new System.Drawing.Size(178, 20);
            this.txtVariableName.TabIndex = 3;
            // 
            // btnAddVariable
            // 
            this.btnAddVariable.Location = new System.Drawing.Point(185, 132);
            this.btnAddVariable.Name = "btnAddVariable";
            this.btnAddVariable.Size = new System.Drawing.Size(75, 23);
            this.btnAddVariable.TabIndex = 4;
            this.btnAddVariable.Text = "Add Variable";
            this.btnAddVariable.UseVisualStyleBackColor = true;
            this.btnAddVariable.Click += new System.EventHandler(this.btnAddVariable_Click);
            // 
            // btnCancelAddVariable
            // 
            this.btnCancelAddVariable.Location = new System.Drawing.Point(275, 132);
            this.btnCancelAddVariable.Name = "btnCancelAddVariable";
            this.btnCancelAddVariable.Size = new System.Drawing.Size(75, 23);
            this.btnCancelAddVariable.TabIndex = 5;
            this.btnCancelAddVariable.Text = "Cancel";
            this.btnCancelAddVariable.UseVisualStyleBackColor = true;
            this.btnCancelAddVariable.Click += new System.EventHandler(this.btnCancelAddVariable_Click);
            // 
            // frmAddVariable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 173);
            this.Controls.Add(this.btnCancelAddVariable);
            this.Controls.Add(this.btnAddVariable);
            this.Controls.Add(this.txtVariableName);
            this.Controls.Add(this.txtCpuVariableName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAddVariable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Variable";
            this.Load += new System.EventHandler(this.frmAddVariable_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCpuVariableName;
        private System.Windows.Forms.TextBox txtVariableName;
        private System.Windows.Forms.Button btnAddVariable;
        private System.Windows.Forms.Button btnCancelAddVariable;
    }
}