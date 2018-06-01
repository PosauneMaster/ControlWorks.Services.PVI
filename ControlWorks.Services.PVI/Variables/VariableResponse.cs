using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlWorks.Services.PVI.Variables
{
    public class VariableResponse
    {
        private List<VariableValue> _list = new List<VariableValue>();

        public VariableResponse()
        {
        }
        public VariableResponse(string cpuName)
        {
            CpuName = cpuName;
        }

        public string CpuName { get; set; }

        public VariableValue[] Values {
            get => _list.ToArray();
            set => _list = value.ToList();
        }
        public void AddValue(string name, string value)
        {
            _list.Add(new VariableValue() {Name = name, Value = value} );
        }
    }

    public class VariableValue
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
