using ControlWorks.Services.PVI;
using System;
using System.Windows.Forms;
using ControlWorks.Services.ConfigurationProvider;

namespace TestHarness
{
    public partial class frmVariables : Form
    {
        public frmVariables()
        {
            InitializeComponent();
        }

        private void btnAddVariables_Click(object sender, EventArgs e)
        {

            var filepath = AppSettings.VariableSettingsFile;

            var collection = new VariableInfoCollection();
            collection.Open(filepath);
            var cpuName = txtCpuName.Text;
            var names = txtVariables.Lines;

            collection.AddRange(cpuName, names);
            collection.Save(filepath);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //var filepath = ConfigurationProvider.VariableSettingsFile;

            //var collection = new VariableInfoCollection();
            //collection.Open(filepath);
            //var cpuName = txtCpuName.Text;
            //var names = txtVariables.Lines;

            //collection.RemoveRange(cpuName, names);
            //collection.Save(filepath);


        }
    }
}
