namespace TestHarness
{
    partial class frmVariables
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCpuName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVariables = new System.Windows.Forms.TextBox();
            this.btnAddVariables = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 458);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(524, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cpu Name";
            // 
            // txtCpuName
            // 
            this.txtCpuName.Location = new System.Drawing.Point(21, 35);
            this.txtCpuName.Name = "txtCpuName";
            this.txtCpuName.Size = new System.Drawing.Size(164, 20);
            this.txtCpuName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(216, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Variable Names:";
            // 
            // txtVariables
            // 
            this.txtVariables.Location = new System.Drawing.Point(219, 35);
            this.txtVariables.Multiline = true;
            this.txtVariables.Name = "txtVariables";
            this.txtVariables.Size = new System.Drawing.Size(215, 189);
            this.txtVariables.TabIndex = 4;
            // 
            // btnAddVariables
            // 
            this.btnAddVariables.Location = new System.Drawing.Point(373, 340);
            this.btnAddVariables.Name = "btnAddVariables";
            this.btnAddVariables.Size = new System.Drawing.Size(139, 23);
            this.btnAddVariables.TabIndex = 5;
            this.btnAddVariables.Text = "Add Variables";
            this.btnAddVariables.UseVisualStyleBackColor = true;
            this.btnAddVariables.Click += new System.EventHandler(this.btnAddVariables_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(373, 369);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(139, 23);
            this.btnRemove.TabIndex = 6;
            this.btnRemove.Text = "Remove Variables";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // frmVariables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 480);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAddVariables);
            this.Controls.Add(this.txtVariables);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCpuName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "frmVariables";
            this.Text = "frmVariables";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCpuName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtVariables;
        private System.Windows.Forms.Button btnAddVariables;
        private System.Windows.Forms.Button btnRemove;
    }
}