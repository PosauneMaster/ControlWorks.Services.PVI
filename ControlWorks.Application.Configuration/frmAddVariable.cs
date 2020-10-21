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
    public partial class frmAddVariable : Form
    {
        private RestClient _restClient;
        private string _cpuName;
        public event EventHandler<EventArgs> VariableAdded;

        public frmAddVariable()
        {
            InitializeComponent();
        }

        public frmAddVariable(RestClient restClient, string cpuName)
        {
            InitializeComponent();
            _restClient = restClient;
            _cpuName = cpuName;
        }

        private void btnAddVariable_Click(object sender, EventArgs e)
        {
            var variable = txtVariableName.Text;

            var info = Task.Run(async () => await _restClient.AddVariable(_cpuName, variable));
            OnVariableAdded();

            this.Close();
        }

        private void btnCancelAddVariable_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddVariable_Load(object sender, EventArgs e)
        {
            txtCpuVariableName.Text = _cpuName;
        }

        private void OnVariableAdded()
        {
            var temp = VariableAdded;
            temp?.Invoke(this, new EventArgs());
        }

    }
}
