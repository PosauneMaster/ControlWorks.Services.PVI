using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BR.AN.PviServices;
using Newtonsoft.Json;

namespace ControlWorks.Services.PVI.Variables
{
    public class VariableData
    {

        public string VariableName { get; set; }
        public string CpuName { get; set; }
        public string IpAddress { get; set; }
        public string DataType { get; set; }
        public string Value { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
