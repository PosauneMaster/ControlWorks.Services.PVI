using ControlWorks.Services.PVI;
using System;
using System.Windows.Forms;
using ControlWorks.Services.ConfigurationProvider;
using ControlWorks.Services.PVI.Variables;

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

            var collection = new VariableInfoCollection(new FileWrapper());
            collection.Open(filepath);
            var cpuName = txtCpuName.Text;
            var names = txtVariables.Lines;

            collection.AddRange(cpuName, names);
            collection.Save(filepath);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var filepath = AppSettings.VariableSettingsFile;

            var collection = new VariableInfoCollection(new FileWrapper());
            collection.Open(filepath);
            var cpuName = txtCpuName.Text;
            var names = txtVariables.Lines;

            collection.RemoveRange(cpuName, names);
            collection.Save(filepath);


        }

        private void btnToJson_Click(object sender, EventArgs e)
        {
            var info = new VariableData() {CpuName = "Cpu1", IpAddress = "123.2.5.123", DataType = "Int32", VariableName = "Variable1", Value = "987654"};

            var json = info.ToJson();
        }
    }
}
