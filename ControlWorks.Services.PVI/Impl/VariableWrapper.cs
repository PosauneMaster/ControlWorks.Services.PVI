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
        VariableResponse ReadVariables(VariableInfo info);
        VariableResponse ReadActiveVariables(VariableInfo info);
        void DisconnectVariables(string cpuName, IEnumerable<string> variableNames);
        List<VariableDetails> GetVariableDetails(VariableInfo info);

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

        public List<VariableDetails> GetVariableDetails(VariableInfo info)
        {
            if (info == null)
            {
                return null;
            };

            var list = new List<VariableDetails>();

            if (_service.Cpus.ContainsKey(info.CpuName))
            {
                var cpu = _service.Cpus[info.CpuName];

                foreach (var variable in info.Variables)
                {
                    if (cpu.Variables.ContainsKey(variable))
                    {
                        var value = ConvertVariableValue(cpu.Variables[variable].Value);

                        var details = new VariableDetails();
                        details.Name = cpu.Variables[variable].Name;
                        details.CpuName = info.CpuName;
                        details.Value = value;
                        details.IsConnected = cpu.Variables[variable].IsConnected;
                        details.HasError = cpu.Variables[variable].HasError;
                        details.ErrorCode = cpu.Variables[variable].ErrorCode.ToString();
                        details.ErrorText = cpu.Variables[variable].ErrorText;

                        list.Add(details);
                    }
                }
            }

            return list;

        }

        public VariableResponse ReadVariables(VariableInfo info)
        {
            if (info == null)
            {
                return null;
            };

            var response = new VariableResponse(info.CpuName);

            if (_service.Cpus.ContainsKey(info.CpuName))
            {
                var cpu = _service.Cpus[info.CpuName];

                foreach (var variable in info.Variables)
                {
                    if (cpu.Variables.ContainsKey(variable))
                    {
                        var value = ConvertVariableValue(cpu.Variables[variable].Value);
                        response.AddValue(variable, value);
                    }
                }
            }

            return response;
        }

        public VariableResponse ReadActiveVariables(VariableInfo info)
        {
            var response = new VariableResponse(info.CpuName);

            if (_service.Cpus.ContainsKey(info.CpuName))
            {
                var cpu = _service.Cpus[info.CpuName];

                foreach (var variable in info.Variables)
                {
                    if (cpu.Variables.ContainsKey(variable))
                    {
                        if (cpu.Variables[variable].IsConnected)
                        {
                            var value = ConvertVariableValue(cpu.Variables[variable].Value);
                            response.AddValue(variable, value);
                        }
                    }
                }
            }

            return response;
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
                var cpu = _service.Cpus[cpuName];
                var ipAddress = cpu.Connection.TcpIp.DestinationIpAddress;
                if (cpu.IsConnected)
                {
                    ConnectVariable(_service.Cpus[cpuName], name);
                }
            }
        }

        private string ConvertVariableValue(Value v)
        {
            string value = String.Empty;

            if (v == null)
            {
                value = String.Empty;
            }

            var iceDataType = v.IECDataType;

            switch (v.IECDataType)
            {

                case IECDataTypes.BOOL:
                    if (v.IsOfTypeArray && v.ArrayLength > 1)
                    {
                        value = v[0].ToBoolean(CultureInfo.CurrentCulture).ToString(CultureInfo.CurrentCulture);
                    }
                    else
                    {
                        value = v.ToBoolean(CultureInfo.CurrentCulture).ToString(CultureInfo.CurrentCulture);
                    }
                    break;

                case IECDataTypes.REAL:
                    value = v.ToDouble(CultureInfo.CurrentCulture).ToString("G", CultureInfo.CurrentCulture);
                    break;

                case IECDataTypes.DATE:
                case IECDataTypes.DATE_AND_TIME:
                    value = v.ToDateTime(CultureInfo.CurrentCulture).ToString("o", CultureInfo.CurrentCulture);
                    break;
                default:
                    value = v.ToIECString();
                    break;

            }

            return value == null ? String.Empty : value;

        }

        private void ConnectVariable(Cpu cpu, string name)
        {

            if (!cpu.Variables.ContainsKey(name) && !String.IsNullOrEmpty(name))
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
                DataType = Enum.GetName(typeof(IECDataTypes), variable.Value.IECDataType),
                VariableName = e.Name,
                Value = ConvertVariableValue(variable.Value)
            };

            _eventNotifier.OnVariableValueChanged(sender, new PviApplicationEventArgs() { Message = data.ToJson() });
        }

        private void Variable_Error(object sender, PviEventArgs e)
        {
            var cpuName = String.Empty;
            var cpuIp = String.Empty;

            var v = sender as Variable;
            if (v != null)
            {
                var c = v.Parent as Cpu;

                if (c != null)
                {
                    cpuName = c.Name;
                    cpuIp = c.Connection.TcpIp.DestinationIpAddress;
                }

            }

            var pviEventMsg = Utils.FormatPviEventMessage($"ServiceWrapper.Variable_Error Cpu Name={cpuName}, cpuIp={cpuIp} ", e);
            _eventNotifier.OnVariableError(sender, new PviApplicationEventArgs() { Message = pviEventMsg });
        }

        private void Variable_Connected(object sender, PviEventArgs e)
        {
            var cpuName = String.Empty;
            var cpuIp = String.Empty;

            var v = sender as Variable;
            if (v != null)
            {
                var c = v.Parent as Cpu;

                if (c != null)
                {
                    cpuName = c.Name;
                    cpuIp = c.Connection.TcpIp.DestinationIpAddress;
                }

            }

            var pviEventMsg = Utils.FormatPviEventMessage($"ServiceWrapper.Variable_Connected Cpu Name={cpuName}, cpuIp={cpuIp} ", e);
            _eventNotifier.OnVariableConnected(sender, new PviApplicationEventArgs() { Message = pviEventMsg });
        }

        public void DisconnectVariables(string cpuName, IEnumerable<string> variableNames)
        {
            foreach (var v in variableNames)
            {
                if (_service.Cpus.ContainsKey(cpuName))
                {
                    var variables = _service.Cpus[cpuName].Variables;
                    if (variables.ContainsKey(v))
                    {
                        variables[v].Disconnect();
                        variables.Remove(variables[v]);
                    }
                }
            }
        }

    }
}
