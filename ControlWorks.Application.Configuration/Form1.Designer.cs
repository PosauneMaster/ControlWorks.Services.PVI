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
            this.btnUpdate = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dgVariables = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCpuName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsConnected = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHasError = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colErrorCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colErrorText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgCpuPanels = new System.Windows.Forms.DataGridView();
            this.colCpuPanelName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCpuPanelDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCpuPanelIp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCpuPanelIsConnected = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCpuPanelHasError = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCpuPanelErrorCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCpuPanelErrorDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddVariable = new System.Windows.Forms.Button();
            this.btnDeleteVariable = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnRefreshCpu = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgVariables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCpuPanels)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblConnectedUrl,
            this.lblHeartbeatTime,
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 768);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1482, 22);
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
            this.label1.Location = new System.Drawing.Point(20, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server:";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(23, 37);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(182, 20);
            this.txtServer.TabIndex = 2;
            this.txtServer.Text = "localhost";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port:";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(211, 37);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 20);
            this.txtPort.TabIndex = 4;
            this.txtPort.Text = "9002";
            // 
            // cbCpuPanels
            // 
            this.cbCpuPanels.FormattingEnabled = true;
            this.cbCpuPanels.Location = new System.Drawing.Point(759, 37);
            this.cbCpuPanels.Name = "cbCpuPanels";
            this.cbCpuPanels.Size = new System.Drawing.Size(288, 21);
            this.cbCpuPanels.TabIndex = 6;
            this.cbCpuPanels.SelectedIndexChanged += new System.EventHandler(this.cbCpuPanels_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(762, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Cpu Panels";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(327, 35);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 9;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(403, 633);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(106, 23);
            this.btnUpdate.TabIndex = 24;
            this.btnUpdate.Text = "Add Cpu Panel";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(515, 633);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 23);
            this.button2.TabIndex = 25;
            this.button2.Text = "Delete Cpu Panel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dgVariables
            // 
            this.dgVariables.AllowUserToAddRows = false;
            this.dgVariables.AllowUserToDeleteRows = false;
            this.dgVariables.AllowUserToResizeRows = false;
            this.dgVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgVariables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgVariables.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colCpuName,
            this.colIsConnected,
            this.colHasError,
            this.colErrorCode,
            this.colErrorText,
            this.colValue});
            this.dgVariables.Location = new System.Drawing.Point(759, 103);
            this.dgVariables.MultiSelect = false;
            this.dgVariables.Name = "dgVariables";
            this.dgVariables.ReadOnly = true;
            this.dgVariables.RowHeadersVisible = false;
            this.dgVariables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgVariables.Size = new System.Drawing.Size(710, 513);
            this.dgVariables.TabIndex = 26;
            // 
            // colName
            // 
            this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colName.DataPropertyName = "Name";
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 60;
            // 
            // colCpuName
            // 
            this.colCpuName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colCpuName.DataPropertyName = "CpuName";
            this.colCpuName.HeaderText = "Cpu Name";
            this.colCpuName.Name = "colCpuName";
            this.colCpuName.ReadOnly = true;
            this.colCpuName.Width = 82;
            // 
            // colIsConnected
            // 
            this.colIsConnected.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colIsConnected.DataPropertyName = "IsConnected";
            this.colIsConnected.HeaderText = "Is Connected";
            this.colIsConnected.Name = "colIsConnected";
            this.colIsConnected.ReadOnly = true;
            this.colIsConnected.Width = 95;
            // 
            // colHasError
            // 
            this.colHasError.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colHasError.DataPropertyName = "HasError";
            this.colHasError.HeaderText = "Has Error";
            this.colHasError.Name = "colHasError";
            this.colHasError.ReadOnly = true;
            this.colHasError.Width = 76;
            // 
            // colErrorCode
            // 
            this.colErrorCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colErrorCode.DataPropertyName = "ErrorCode";
            this.colErrorCode.HeaderText = "Error Code";
            this.colErrorCode.Name = "colErrorCode";
            this.colErrorCode.ReadOnly = true;
            this.colErrorCode.Width = 82;
            // 
            // colErrorText
            // 
            this.colErrorText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colErrorText.DataPropertyName = "ErrorText";
            this.colErrorText.HeaderText = "Error Text";
            this.colErrorText.Name = "colErrorText";
            this.colErrorText.ReadOnly = true;
            this.colErrorText.Width = 78;
            // 
            // colValue
            // 
            this.colValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colValue.DataPropertyName = "Value";
            this.colValue.HeaderText = "Value";
            this.colValue.Name = "colValue";
            this.colValue.ReadOnly = true;
            // 
            // dgCpuPanels
            // 
            this.dgCpuPanels.AllowUserToAddRows = false;
            this.dgCpuPanels.AllowUserToDeleteRows = false;
            this.dgCpuPanels.AllowUserToResizeRows = false;
            this.dgCpuPanels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCpuPanels.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCpuPanelName,
            this.colCpuPanelDescription,
            this.colCpuPanelIp,
            this.colCpuPanelIsConnected,
            this.colCpuPanelHasError,
            this.colCpuPanelErrorCode,
            this.colCpuPanelErrorDescription});
            this.dgCpuPanels.Location = new System.Drawing.Point(23, 103);
            this.dgCpuPanels.MultiSelect = false;
            this.dgCpuPanels.Name = "dgCpuPanels";
            this.dgCpuPanels.ReadOnly = true;
            this.dgCpuPanels.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCpuPanels.Size = new System.Drawing.Size(710, 513);
            this.dgCpuPanels.TabIndex = 27;
            // 
            // colCpuPanelName
            // 
            this.colCpuPanelName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colCpuPanelName.DataPropertyName = "Name";
            this.colCpuPanelName.HeaderText = "Name";
            this.colCpuPanelName.Name = "colCpuPanelName";
            this.colCpuPanelName.ReadOnly = true;
            this.colCpuPanelName.Width = 60;
            // 
            // colCpuPanelDescription
            // 
            this.colCpuPanelDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colCpuPanelDescription.DataPropertyName = "Description";
            this.colCpuPanelDescription.HeaderText = "Description";
            this.colCpuPanelDescription.Name = "colCpuPanelDescription";
            this.colCpuPanelDescription.ReadOnly = true;
            this.colCpuPanelDescription.Width = 85;
            // 
            // colCpuPanelIp
            // 
            this.colCpuPanelIp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colCpuPanelIp.DataPropertyName = "IpAddress";
            this.colCpuPanelIp.HeaderText = "IP Address";
            this.colCpuPanelIp.Name = "colCpuPanelIp";
            this.colCpuPanelIp.ReadOnly = true;
            this.colCpuPanelIp.Width = 83;
            // 
            // colCpuPanelIsConnected
            // 
            this.colCpuPanelIsConnected.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colCpuPanelIsConnected.DataPropertyName = "IsConnected";
            this.colCpuPanelIsConnected.HeaderText = "Is Connected";
            this.colCpuPanelIsConnected.Name = "colCpuPanelIsConnected";
            this.colCpuPanelIsConnected.ReadOnly = true;
            this.colCpuPanelIsConnected.Width = 95;
            // 
            // colCpuPanelHasError
            // 
            this.colCpuPanelHasError.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colCpuPanelHasError.DataPropertyName = "HasError";
            this.colCpuPanelHasError.HeaderText = "Has Error";
            this.colCpuPanelHasError.Name = "colCpuPanelHasError";
            this.colCpuPanelHasError.ReadOnly = true;
            this.colCpuPanelHasError.Width = 76;
            // 
            // colCpuPanelErrorCode
            // 
            this.colCpuPanelErrorCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colCpuPanelErrorCode.DataPropertyName = "ErrorCode";
            this.colCpuPanelErrorCode.HeaderText = "Error Code";
            this.colCpuPanelErrorCode.Name = "colCpuPanelErrorCode";
            this.colCpuPanelErrorCode.ReadOnly = true;
            this.colCpuPanelErrorCode.Width = 82;
            // 
            // colCpuPanelErrorDescription
            // 
            this.colCpuPanelErrorDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCpuPanelErrorDescription.DataPropertyName = "ErrorText";
            this.colCpuPanelErrorDescription.HeaderText = "Error Description";
            this.colCpuPanelErrorDescription.Name = "colCpuPanelErrorDescription";
            this.colCpuPanelErrorDescription.ReadOnly = true;
            // 
            // btnAddVariable
            // 
            this.btnAddVariable.Location = new System.Drawing.Point(1139, 633);
            this.btnAddVariable.Name = "btnAddVariable";
            this.btnAddVariable.Size = new System.Drawing.Size(106, 23);
            this.btnAddVariable.TabIndex = 28;
            this.btnAddVariable.Text = "Add Variable";
            this.btnAddVariable.UseVisualStyleBackColor = true;
            this.btnAddVariable.Click += new System.EventHandler(this.btnAddVariable_Click);
            // 
            // btnDeleteVariable
            // 
            this.btnDeleteVariable.Location = new System.Drawing.Point(1251, 633);
            this.btnDeleteVariable.Name = "btnDeleteVariable";
            this.btnDeleteVariable.Size = new System.Drawing.Size(106, 23);
            this.btnDeleteVariable.TabIndex = 29;
            this.btnDeleteVariable.Text = "DeleteVariable";
            this.btnDeleteVariable.UseVisualStyleBackColor = true;
            this.btnDeleteVariable.Click += new System.EventHandler(this.btnDeleteVariable_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(354, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 18);
            this.label4.TabIndex = 30;
            this.label4.Text = "Panels";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1076, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 18);
            this.label5.TabIndex = 31;
            this.label5.Text = "Variables";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(1363, 633);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(106, 23);
            this.btnRefresh.TabIndex = 32;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnRefreshCpu
            // 
            this.btnRefreshCpu.Location = new System.Drawing.Point(627, 633);
            this.btnRefreshCpu.Name = "btnRefreshCpu";
            this.btnRefreshCpu.Size = new System.Drawing.Size(106, 23);
            this.btnRefreshCpu.TabIndex = 33;
            this.btnRefreshCpu.Text = "Refresh";
            this.btnRefreshCpu.UseVisualStyleBackColor = true;
            this.btnRefreshCpu.Click += new System.EventHandler(this.btnRefreshCpu_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1482, 790);
            this.Controls.Add(this.btnRefreshCpu);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.dgCpuPanels);
            this.Controls.Add(this.btnDeleteVariable);
            this.Controls.Add(this.btnAddVariable);
            this.Controls.Add(this.dgVariables);
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
            ((System.ComponentModel.ISupportInitialize)(this.dgVariables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCpuPanels)).EndInit();
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
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dgVariables;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCpuName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIsConnected;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHasError;
        private System.Windows.Forms.DataGridViewTextBoxColumn colErrorCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colErrorText;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.DataGridView dgCpuPanels;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCpuPanelName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCpuPanelDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCpuPanelIp;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCpuPanelIsConnected;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCpuPanelHasError;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCpuPanelErrorCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCpuPanelErrorDescription;
        private System.Windows.Forms.Button btnAddVariable;
        private System.Windows.Forms.Button btnDeleteVariable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnRefreshCpu;
    }
}

