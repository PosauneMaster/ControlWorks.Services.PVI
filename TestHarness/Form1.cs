using ControlWorks.Services.PVI;
using System;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace TestHarness
{
    public partial class Form1 : Form
    {

        CpuInfoCollection _collection;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var cpu = new CpuInfo();
            cpu.Name = txtName.Text;
            cpu.Description = txtDescription.Text;
            cpu.IpAddress = txtIpAddress.Text;

            _collection.AddOrUpdate(cpu);

            txtName.Clear();
            txtDescription.Clear();
            txtIpAddress.Clear();


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _collection = new CpuInfoCollection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtResult.Text = String.Empty;
            var cpu = _collection.FindByName(txtSearchName.Text);
            if (cpu != null)
            {
                txtResult.Text = JsonConvert.SerializeObject(cpu);
            }
        }

        private void btnSearchByIP_Click(object sender, EventArgs e)
        {
            txtResult.Text = String.Empty;
            var cpu = _collection.FindByIp(txtIpSearch.Text);
            {
                if (cpu != null)
                {
                    txtResult.Text = JsonConvert.SerializeObject(cpu);
                }
            }
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            var list = _collection.GetAll();
            if (list != null && list.Count > 0)
            {
                txtResult.Text = JsonConvert.SerializeObject(list);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtSaveFilePath.Text))
            {
                _collection.Save(txtSaveFilePath.Text);
            }

            _collection.Save(ConfigurationProvider.CpuSettingsFile);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    _collection.Open(openFileDialog1.FileName);
                }
            }
            catch(Exception ex)
            {
                txtResult.Text = ex.ToString();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var cpu = _collection.FindByName(txtRemoveName.Text);
            if (cpu != null)
            {
                _collection.Remove(cpu);
            }
        }
    }
}
