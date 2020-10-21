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
    public partial class frmDeleteVariable : Form
    {
        private RestClient _restClient;
        private List<string> _variableNames;
        private string _cpuName;
        public event EventHandler<EventArgs> VariableDeleted;


        public frmDeleteVariable()
        {
            InitializeComponent();
        }

        public frmDeleteVariable(RestClient restClient, string cpuName, List<string> variableNames)
        {
            InitializeComponent();

            _variableNames = variableNames;
            _restClient = restClient;
            _cpuName = cpuName;
        }


        private void frmDeleteVariable_Load(object sender, EventArgs e)
        {
            cboVariableInfo.DataSource = _variableNames;
        }

        private void btnDeleteVariable_Click(object sender, EventArgs e)
        {
            var variable = cboVariableInfo.SelectedItem as String;

            var info = Task.Run(async () => await _restClient.DeleteVariable(_cpuName, variable));

            OnVariableDeleted();

            this.Close();
        }

        private void OnVariableDeleted()
        {
            var temp = VariableDeleted;
            temp?.Invoke(this, new EventArgs());
        }

    }
}
