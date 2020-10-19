using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlWorks.Application.Configuration
{
    public class CpuClientInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IpAddress { get; set; }
        public string IsConnected { get; set; }
        public string HasError { get; set; }
        public CpuClentErrorInfo Error { get; set; }
    }

    public class CpuClentErrorInfo
    {
        public string ErrorCode { get; set; }
        public string ErrorText { get; set; }
    }
}
