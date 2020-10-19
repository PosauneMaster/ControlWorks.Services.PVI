namespace ControlWorks.Application.Configuration
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblConnectedUrl = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblHeartbeatTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cbCpuPanels = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCpuDescription = new System.Windows.Forms.TextBox();
            this.txtCpuIpAddress = new System.Windows.Forms.TextBox();
            this.txtCpuIsConnected = new System.Windows.Forms.TextBox();
            this.txtCpuHasError = new System.Windows.Forms.TextBox();
            this.txtErrorCode = new System.Windows.Forms.TextBox();
            this.txtErrorDescription = new System.Windows.Forms.TextBox();
            this.txtCpuName = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblConnectedUrl,
            this.lblHeartbeatTime,
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 610);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1085, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblConnectedUrl
            // 
            this.lblConnectedUrl.Name = "lblConnectedUrl";
            this.lblConnectedUrl.Size = new System.Drawing.Size(118, 17);
            this.lblConnectedUrl.Text = "toolStripStatusLabel1";
            // 
            // lblHeartbeatTime
            // 
            this.lblHeartbeatTime.Name = "lblHeartbeatTime";
            this.lblHeartbeatTime.Size = new System.Drawing.Size(118, 17);
            this.lblHeartbeatTime.Text = "toolStripStatusLabel1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(118, 17);
            this.lblStatus.Text = "toolStripStatusLabel1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server:";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(38, 37);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(182, 20);
            this.txtServer.TabIndex = 2;
            this.txtServer.Text = "localhost";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(223, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port:";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(226, 37);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 20);
            this.txtPort.TabIndex = 4;
            this.txtPort.Text = "9002";
            // 
            // cbCpuPanels
            // 
            this.cbCpuPanels.FormattingEnabled = true;
            this.cbCpuPanels.Location = new System.Drawing.Point(38, 116);
            this.cbCpuPanels.Name = "cbCpuPanels";
            this.cbCpuPanels.Size = new System.Drawing.Size(288, 21);
            this.cbCpuPanels.TabIndex = 6;
            this.cbCpuPanels.SelectedIndexChanged += new System.EventHandler(this.cbCpuPanels_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Cpu Panels";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(251, 63);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 9;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Name:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(42, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Description:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(42, 223);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "IP Address:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(42, 255);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Is Connected:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(42, 287);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Has Error:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(42, 351);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Error Description:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(42, 319);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "Error Code:";
            // 
            // txtCpuDescription
            // 
            this.txtCpuDescription.Location = new System.Drawing.Point(152, 184);
            this.txtCpuDescription.Name = "txtCpuDescription";
            this.txtCpuDescription.ReadOnly = true;
            this.txtCpuDescription.Size = new System.Drawing.Size(174, 20);
            this.txtCpuDescription.TabIndex = 17;
            // 
            // txtCpuIpAddress
            // 
            this.txtCpuIpAddress.Location = new System.Drawing.Point(152, 216);
            this.txtCpuIpAddress.Name = "txtCpuIpAddress";
            this.txtCpuIpAddress.ReadOnly = true;
            this.txtCpuIpAddress.Size = new System.Drawing.Size(174, 20);
            this.txtCpuIpAddress.TabIndex = 18;
            // 
            // txtCpuIsConnected
            // 
            this.txtCpuIsConnected.Location = new System.Drawing.Point(152, 248);
            this.txtCpuIsConnected.Name = "txtCpuIsConnected";
            this.txtCpuIsConnected.ReadOnly = true;
            this.txtCpuIsConnected.Size = new System.Drawing.Size(174, 20);
            this.txtCpuIsConnected.TabIndex = 19;
            // 
            // txtCpuHasError
            // 
            this.txtCpuHasError.Location = new System.Drawing.Point(152, 280);
            this.txtCpuHasError.Name = "txtCpuHasError";
            this.txtCpuHasError.ReadOnly = true;
            this.txtCpuHasError.Size = new System.Drawing.Size(174, 20);
            this.txtCpuHasError.TabIndex = 20;
            // 
            // txtErrorCode
            // 
            this.txtErrorCode.Location = new System.Drawing.Point(152, 312);
            this.txtErrorCode.Name = "txtErrorCode";
            this.txtErrorCode.ReadOnly = true;
            this.txtErrorCode.Size = new System.Drawing.Size(174, 20);
            this.txtErrorCode.TabIndex = 21;
            // 
            // txtErrorDescription
            // 
            this.txtErrorDescription.Location = new System.Drawing.Point(152, 344);
            this.txtErrorDescription.Name = "txtErrorDescription";
            this.txtErrorDescription.ReadOnly = true;
            this.txtErrorDescription.Size = new System.Drawing.Size(174, 20);
            this.txtErrorDescription.TabIndex = 22;
            // 
            // txtCpuName
            // 
            this.txtCpuName.Location = new System.Drawing.Point(152, 152);
            this.txtCpuName.Name = "txtCpuName";
            this.txtCpuName.ReadOnly = true;
            this.txtCpuName.Size = new System.Drawing.Size(174, 20);
            this.txtCpuName.TabIndex = 23;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(170, 382);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 24;
            this.btnUpdate.Text = "Add";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(251, 382);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 25;
            this.button2.Text = "Delete";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1085, 632);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtCpuName);
            this.Controls.Add(this.txtErrorDescription);
            this.Controls.Add(this.txtErrorCode);
            this.Controls.Add(this.txtCpuHasError);
            this.Controls.Add(this.txtCpuIsConnected);
            this.Controls.Add(this.txtCpuIpAddress);
            this.Controls.Add(this.txtCpuDescription);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbCpuPanels);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.Text = "Control Works Configuration";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblConnectedUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.ToolStripStatusLabel lblHeartbeatTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox cbCpuPanels;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtCpuDescription;
        private System.Windows.Forms.TextBox txtCpuIpAddress;
        private System.Windows.Forms.TextBox txtCpuIsConnected;
        private System.Windows.Forms.TextBox txtCpuHasError;
        private System.Windows.Forms.TextBox txtErrorCode;
        private System.Windows.Forms.TextBox txtErrorDescription;
        private System.Windows.Forms.TextBox txtCpuName;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button button2;
    }
}

