using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlWorks.Application.Configuration
{
    public partial class frmAddCpuPanelcs : Form
    {
        private RestClient _restClient;
        public event EventHandler<EventArgs> CpuAdded;

        public frmAddCpuPanelcs()
        {
            InitializeComponent();
        }

        public frmAddCpuPanelcs(RestClient restClient)
        {
            InitializeComponent();

            _restClient = restClient;
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var updateInfo = new CpuUpdateInfo();
            updateInfo.Name = txtName.Text;
            updateInfo.Description = txtDescription.Text;
            updateInfo.IpAddress = txtIpAddress.Text;

            var info = Task.Run(async () => await _restClient.AddOrUpdateCpuClientInfo(updateInfo)).Result;

            if (info)
            {
                OnCpuAdded();
            }

            this.Close();
        }

        private void OnCpuAdded()
        {
            var temp = CpuAdded;
            temp?.Invoke(this, new EventArgs());
        }
    }
}
