using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BR.AN.PviServices;
using ControlWorks.Services.PVI.Variables;

namespace ControlWorks.Services.PVI.Impl
{
    public interface IVariableWrapper
    {
        void ConnectVariables(string cpuName, IEnumerable<string> variables);
        void ConnectVariable(string cpuName, string name);

    }

    public class VariableWrapper : IVariableWrapper
    {
        private readonly Service _service;
        private readonly IEventNotifier _eventNotifier;

        public VariableWrapper(Service service, IEventNotifier eventNotifier)
        {
            _service = service;
            _eventNotifier = eventNotifier;
        }

        public void ConnectVariables(string cpuName, IEnumerable<string> variables)
        {
            if (!String.IsNullOrEmpty(cpuName) && variables != null)
            {
                foreach (var v in variables)
                {
                    ConnectVariable(cpuName, v);
                }
            }
        }

        public void ConnectVariable(string cpuName, string name)
        {
            if (_service.Cpus.ContainsKey(cpuName))
            {
                ConnectVariable(_service.Cpus[cpuName], name);
            }
        }

        private void ConnectVariable(Cpu cpu, string name)
        {
            if (!cpu.Variables.ContainsKey(name))
            {
                var variable = new Variable(cpu, name)
                {
                    UserTag = name,
                    UserData = cpu.UserData
                };
                variable.Connected += Variable_Connected; ;
                variable.Error += Variable_Error; ;
                variable.ValueChanged += Variable_ValueChanged; ;
                variable.Active = true;
                variable.Connect();
            }
        }

        private void Variable_ValueChanged(object sender, VariableEventArgs e)
        {
            if (!(sender is Variable variable))
            {
                return;
            }

            if (!(variable.Parent is Cpu cpu))
            {
                return;
            }

            if (variable.Value is null)
            {
                return;
            }

            var data = new VariableData()
            {
                CpuName = cpu.Name,
                IpAddress = cpu.Connection.TcpIp.DestinationIpAddress,
                DataType = Enum.GetName(typeof(DataType), variable.Value.DataType),
                VariableName = e.Name,
                Value = variable.Value.ToString(CultureInfo.InvariantCulture)
            };

            _eventNotifier.OnVariableValueChanged(sender, new PviApplicationEventArgs() { Message = data.ToJson() });
        }

        private void Variable_Error(object sender, PviEventArgs e)
        {
            var pviEventMsg = Utils.FormatPviEventMessage("ServiceWrapper.Variable_Error", e);
            _eventNotifier.OnVariableError(sender, new PviApplicationEventArgs() { Message = pviEventMsg });
        }

        private void Variable_Connected(object sender, PviEventArgs e)
        {
            var pviEventMsg = Utils.FormatPviEventMessage("ServiceWrapper.Variable_Connected", e);
            _eventNotifier.OnVariableConnected(sender, new PviApplicationEventArgs() { Message = pviEventMsg });
        }

    }
}
