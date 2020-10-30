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
        private List<VariableInfo> _variableInfoList;

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
            var heartbeat = Task.Run(() => _restClient.GetHeartbeat());
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            _restClient = new RestClient(FormatUrl());
            _restClient.Heartbeat += _restClient_Heartbeat;
            _restClient.CpuInfoUpdated += _restClient_CpuInfoUpdated;
            _restClient.VariableInfoUpdated += _restClient_VariableInfoUpdated;


            lblConnectedUrl.Text = FormatUrl();
            timer1.Interval = 1000;
            timer1.Tick += Timer1_Tick;
            timer1.Start();
        }

        private void _restClient_VariableInfoUpdated(object sender, VariableInfoEventArgs e)
        {
            UpdateVariableInfo(e.VariableInfoList);
        }
        private void UpdateVariableInfo(List<VariableInfo> variableInfoList)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateVariableInfo(variableInfoList)));
            }
            else
            {
                _variableInfoList = variableInfoList;
                dgVariables.DataSource = _variableInfoList;
            }
        }


        private void _restClient_CpuInfoUpdated(object sender, CpuInfoEventArgs e)
        {
            UpdateCpuInfo(e.CpuClientInfo);
        }

        private void UpdateCpuInfo(CpuClientInfo[] info)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateCpuInfo(info)));
            }
            else
            {
                if (info != null)
                {
                    _cpuClientInfo = info.ToList();

                    cbCpuPanels.DataSource = _cpuClientInfo;
                    cbCpuPanels.DisplayMember = "Name";

                    dgCpuPanels.DataSource = _cpuClientInfo;
                }
            }
        }

        private void _restClient_Heartbeat(object sender, HeartbeatEventArgs e)
        {
            ProcessHeartbeat(e.HeartbeatTime);
        }

        private void ProcessHeartbeat(DateTime? dt)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ProcessHeartbeat(dt)));
            }
            else
            {
                if (dt.HasValue)
            {
                    lblHeartbeatTime.Text = dt.Value.ToString("MM/dd/yyyy HH:mm:ss");
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
        }

        private void GetCpuInfo()
        {
            Task.Run(async () => await _restClient.GetCpuClientInfo());
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_isConnected)
            {
                var addForm = new frmAddCpuPanelcs(_restClient);
                addForm.CpuAdded += AddForm_CpuAdded;
                addForm.ShowDialog(this);
            }
        }

        private void AddForm_CpuAdded(object sender, EventArgs e)
        {
            GetCpuInfo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_isConnected)
            {
                var selected = cbCpuPanels.SelectedItem as CpuClientInfo;

                if (selected != null)
                {
                    var message = $"Confirm DELETE Panel {selected.Name}";
                    if (MessageBox.Show(this, message, "Delete Panel", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                    {
                        return;
                    }
                    
                    var info = Task.Run(async () => await _restClient.DeleteCpu(selected.Name)).Result;
                }

                GetCpuInfo();
            }
        }

        private void btnAddVariable_Click(object sender, EventArgs e)
        {
            if (_isConnected)
            {
                var selected = cbCpuPanels.SelectedItem as CpuClientInfo;
                if (selected != null)
                {
                    var addForm = new frmAddVariable(_restClient, selected.Name);
                    addForm.VariableAdded += AddForm_VariableAdded;
                    addForm.ShowDialog(this);
                }
            }
        }

        private void AddForm_VariableAdded(object sender, EventArgs e)
        {
            RefreshVariables();
        }

        private void btnDeleteVariable_Click(object sender, EventArgs e)
        {
            if (_isConnected)
            {
                var selected = cbCpuPanels.SelectedItem as CpuClientInfo;
                if (selected != null)
                {
                    var variableNames = _variableInfoList.Select(v => v.Name).ToList();
                    var deleteForm = new frmDeleteVariable(_restClient, selected.Name, variableNames);
                    deleteForm.VariableDeleted += DeleteForm_VariableDeleted;

                    deleteForm.ShowDialog(this);
                }
            }
        }

        private void DeleteForm_VariableDeleted(object sender, EventArgs e)
        {
            RefreshVariables();
        }

        private void RefreshVariables()
        {
            var selected = cbCpuPanels.SelectedItem as CpuClientInfo;
            if (selected != null)
            {
                Task.Run(async () => await _restClient.GetVariableDetails(selected.Name));
            }
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (_isConnected)
            {
                RefreshVariables();
            }
        }

        private void btnRefreshCpu_Click(object sender, EventArgs e)
        {
            if (_isConnected)
            {
                GetCpuInfo();
            }
        }
    }
}
