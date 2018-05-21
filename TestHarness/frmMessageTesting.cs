using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlWorks.Services.Messaging;
using ControlWorks.Services.PVI.Pvi;
using Newtonsoft.Json;

namespace TestHarness
{
    public partial class frmMessageTesting : Form
    {
        public frmMessageTesting()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var proc = new MessageProcessor(new PviAplication());

            var message = new ControlWorks.Services.Messaging.Message
            {
                Id = new Guid()
            };

            var request = JsonConvert.SerializeObject(message);


            var response = proc.Process(request);
        }
    }
}
