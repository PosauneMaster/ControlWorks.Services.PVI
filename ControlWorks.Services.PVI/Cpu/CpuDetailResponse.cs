using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlWorks.Services.PVI
{
    public class CpuDetailResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IpAddress { get; set; }
        public bool IsConnected { get; set; }
        public bool HasError { get; set; }
        public CpuError Error { get; set; }

    }

    public class CpuError
    {
        public int ErrorCode { get; set; }
        public string ErrorText { get; set; }
    }
}
