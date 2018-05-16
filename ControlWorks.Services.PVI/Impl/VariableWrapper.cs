using BR.AN.PviServices;
using ControlWorks.Services.PVI.Variables;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ControlWorks.Services.PVI.Impl
{
    public interface IVariableWrapper
    {
        void ConnectVariables(string cpuName, IEnumerable<string> variables);
        void ConnectVariable(string cpuName, string name);
        List<Tuple<string, string>> ReadVariables(VariableInfo info);

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

        public List<Tuple<string, string>> ReadVariables(VariableInfo info)
        {
            var list = new List<Tuple<string, string>>();
            if (_service.Cpus.ContainsKey(info.CpuName))
            {
                var cpu = _service.Cpus[info.CpuName];

                foreach (var variable in info.Variables)
                {
                    if (cpu.Variables.ContainsKey(variable))
                    {
                        var value = ConvertVariableValue(cpu.Variables[variable].Value);
                        list.Add(new Tuple<string, string>(variable, value));
                    }
                }
            }

            return list;
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

        private string ConvertVariableValue(Value v)
        {
            if (v == null)
            {
                return String.Empty;
            }

            switch (v.DataType)
            {

                case DataType.Boolean:
                    return v.ToBoolean(CultureInfo.CurrentCulture).ToString(CultureInfo.CurrentCulture);

                case DataType.SByte:
                case DataType.Int16:
                case DataType.Int32:
                case DataType.Int64:
                case DataType.Byte:
                case DataType.UInt16:
                case DataType.UInt32:
                case DataType.UInt64:
                case DataType.Single:
                case DataType.WORD:
                case DataType.DWORD:
                case DataType.UInt8:
                    return v.ToInt64(CultureInfo.CurrentCulture).ToString("G", CultureInfo.CurrentCulture);

                case DataType.Double:
                    return v.ToDecimal(CultureInfo.CurrentCulture).ToString("G", CultureInfo.CurrentCulture);

                case DataType.DateTime:
                case DataType.Date:
                case DataType.DT:
                    return v.ToDateTime(CultureInfo.CurrentCulture).ToString("o", CultureInfo.CurrentCulture);
            }

            return v.ToString(CultureInfo.CurrentCulture);

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
                Value = ConvertVariableValue(variable.Value)
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
