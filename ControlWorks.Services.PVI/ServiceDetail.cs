using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlWorks.Services.PVI
{
    public class ServiceDetail
    {
        public string Name { get; set; }
        public bool IsConnected { get; set; }
        public int Cpus { get; set; }
        public DateTime ConnectTime { get; set; }
        public string License { get; set; }
    }
}
