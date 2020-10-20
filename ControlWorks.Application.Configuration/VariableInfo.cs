using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlWorks.Application.Configuration
{
    public class VariableInfo
    {
        public string Name { get; set; }
        public string CpuName { get; set; }
        public string Value { get; set; }
        public bool IsConnected { get; set; }
        public bool HasError { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorText { get; set; }
    }
}
