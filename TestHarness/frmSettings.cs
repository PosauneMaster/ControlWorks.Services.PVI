using ControlWorks.Services.PVI;
using System;
using System.Windows.Forms;

namespace TestHarness
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private void bynConnectionString_Click(object sender, EventArgs e)
        {
            //txtResult.Text = ConfigurationProvider.GetConnectionString(txtConnectionString.Text);
        }
    }
}
