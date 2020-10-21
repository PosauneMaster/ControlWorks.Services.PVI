using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlWorks.Application.Configuration
{
    public partial class Form1 : Form
    {

        private RestClient _restClient;
        private bool _isConnected = false;
        private List<CpuClientInfo> _cpuClientInfo;

        private string FormatUrl()
        {
            var url = $"http://{txtServer.Text}:{txtPort.Text}/api";
            return url;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblStatus.BackColor = Color.DarkRed;
            lblStatus.ForeColor = Color.White;
            lblStatus.Text = "Disconnected";

            dgVariables.AutoGenerateColumns = false;
            dgCpuPanels.AutoGenerateColumns = false;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            var heartbeat = Task.Run(async () => await _restClient.GetHeartbeat()).Result;
            if (heartbeat.HasValue)
            {
                lblHeartbeatTime.Text = heartbeat.Value.ToString("MM/dd/yyyy HH:mm:ss");
                lblStatus.BackColor = Color.DarkGreen;
                lblStatus.ForeColor = Color.White;
                lblStatus.Text = "Connected";

                if (!_isConnected)
                {
                    GetCpuInfo();
                    _isConnected = true;
                }
            }
            else
            {
                lblStatus.BackColor = Color.DarkRed;
                lblStatus.ForeColor = Color.White;
                lblStatus.Text = "Disconnected";
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            _restClient = new RestClient(FormatUrl());

            lblConnectedUrl.Text = FormatUrl();
            timer1.Interval = 1000;
            timer1.Tick += Timer1_Tick;
            timer1.Start();
        }

        private void GetCpuInfo()
        {
            var info = Task.Run(async () => await _restClient.GetCpuClientInfo()).Result;

            if (info != null)
            {
                _cpuClientInfo = info.ToList();

                cbCpuPanels.DataSource = _cpuClientInfo;
                cbCpuPanels.DisplayMember = "Name";

                dgCpuPanels.DataSource = _cpuClientInfo;
            }
        }

        private void cbCpuPanels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cpuClientInfo != null)
            {
                var cb = sender as ComboBox;
                if (cb != null)
                {
                    RefreshVariables();
                }
            }
        }

        private List<VariableInfo> GetVariableDetails(string cpuName)
        {
            dgVariables.DataSource = null;
            var details = Task.Run(async () => await _restClient.GetVariableDetails(cpuName)).Result;
            if (details != null)
            {
                dgVariables.DataSource = details;
                return details;
            }

            return null;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var addForm = new frmAddCpuPanelcs(_restClient);
            addForm.CpuAdded += AddForm_CpuAdded;
            addForm.ShowDialog(this);
        }

        private void AddForm_CpuAdded(object sender, EventArgs e)
        {
            GetCpuInfo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var selected = cbCpuPanels.SelectedItem as CpuClientInfo;

            if (selected != null)
            {
                var info = Task.Run(async () => await _restClient.DeleteCpu(selected.Name)).Result;
            }

            GetCpuInfo();
        }

        private void btnAddVariable_Click(object sender, EventArgs e)
        {
            var selected = cbCpuPanels.SelectedItem as CpuClientInfo;
            if (selected != null)
            {
                var addForm = new frmAddVariable(_restClient, selected.Name);
                addForm.VariableAdded += AddForm_VariableAdded;
                addForm.ShowDialog(this);
            }
        }

        private void AddForm_VariableAdded(object sender, EventArgs e)
        {
            RefreshVariables();
        }

        private void btnDeleteVariable_Click(object sender, EventArgs e)
        {
            var selected = cbCpuPanels.SelectedItem as CpuClientInfo;
            if (selected != null)
            {
                var variableNames = GetVariableDetails(selected.Name).Select(v => v.Name).ToList();
                var deleteForm = new frmDeleteVariable(_restClient, selected.Name, variableNames);
                deleteForm.VariableDeleted += DeleteForm_VariableDeleted;

                deleteForm.ShowDialog(this);
            }
        }

        private void DeleteForm_VariableDeleted(object sender, EventArgs e)
        {
            RefreshVariables();
        }

        private void RefreshVariables()
        {
            System.Threading.Thread.Sleep(500);
            var selected = cbCpuPanels.SelectedItem as CpuClientInfo;
            if (selected != null)
            {
                GetVariableDetails(selected.Name);
            }
        }
    }
}
