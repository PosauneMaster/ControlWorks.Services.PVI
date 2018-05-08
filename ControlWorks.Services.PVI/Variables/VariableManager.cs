using System;
using System.Collections.Generic;
using System.Globalization;
using BR.AN.PviServices;
using log4net;

namespace ControlWorks.Services.PVI.Variables
{
    public interface IVariableManager
    {
        System.Threading.Tasks.Task ConnectVariablesAsync(Cpu cpu);
        List<Tuple<string, string>> ReadVariables(string cpuName, IEnumerable<string> variables);

    }
    public class VariableManager : IVariableManager
    {
        private readonly ILog _log = LogManager.GetLogger("FileLogger");
        private readonly Service _service;
        private readonly IVariableApi _variableApi;
        private readonly IEventNotifier _eventNotifier;


        public VariableManager(Service service, IVariableApi variableApi) : this(service, variableApi, null)
        {

        }
        public VariableManager(Service service, IVariableApi variableApi, IEventNotifier eventNotifier)
        {
            _eventNotifier = eventNotifier;
            _service = service;
            _variableApi = variableApi;
        }


        public List<Tuple<string, string>> ReadVariables(string cpuName)
        {
            var variableInfo = _variableApi.FindByCpuName(cpuName);

            var list = new List<Tuple<string, string>>();

            if (_service.Cpus.ContainsKey(cpuName))
            {
                var cpu = _service.Cpus[cpuName];

                foreach (var variable in variableInfo.VariableNames)
                {
                    if (cpu.Variables.ContainsKey(variable))
                    {
                        list.Add(new Tuple<string, string>(variable, cpu.Variables[variable].Value));
                    }
                }
            }

            return list;
        }

        public async System.Threading.Tasks.Task ConnectVariablesAsync(Cpu cpu)
        {
            var cpuVariables = _variableApi.FindByCpuName(cpu.Name).VariableNames;
            await System.Threading.Tasks.Task.Run(() => CreateVariables(cpu, cpuVariables));
        }

        private void CreateVariables(Cpu cpu, string[] variables)
        {
            foreach (var variableName in variables)
            {
                CreateVariable(cpu, variableName);
            }

            var trigger = ConfigurationProvider.AppSettings.ShutdownTriggerVariable;

            CreateEventVariable(cpu, trigger);
        }

        private void CreateEventVariable(Cpu cpu, string name)
        {
            if (!cpu.Variables.ContainsKey(name))
            {
                var variable = new Variable(cpu, name)
                {
                    UserTag = name,
                    UserData = cpu.UserData
                };
                variable.Connected += Variable_Connected;
                variable.Error += Variable_Error;
                variable.ValueChanged += Variable_ValueChanged;
                variable.Active = true;
                variable.Connect();
                return;
            }
            _log.Info($"A variable with the name {name} already exists for Cpu {cpu.Name}");
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

        private void CreateVariable(Cpu cpu, string name)
        {
            if (!cpu.Variables.ContainsKey(name))
            {
                var variable = new Variable(cpu, name)
                {
                    UserTag = name,
                    UserData = cpu.UserData
                };
                variable.Connected += Variable_Connected;
                variable.Error += Variable_Error;
                variable.Active = true;
                variable.Connect();
                return;
            }
            _log.Info($"A variable with the name {name} already exists for Cpu {cpu.Name}");
        }

        private void Variable_Error(object sender, PviEventArgs e)
        {
            if (sender is Variable variable && variable.Parent is Cpu cpu)
            {
                _log.Info(Utils.FormatPviEventMessage($"Variable Error Cpu={cpu.Name}; Variable={variable.Name}", e));
            }
        }

        private void Variable_Connected(object sender, PviEventArgs e)
        {
            if (sender is Variable variable && variable.Parent is Cpu cpu)
            {
                _log.Info(Utils.FormatPviEventMessage($"Variable Connected. Cpu={cpu.Name}; Variable={variable.Name}", e));
            }
        }

        public List<Tuple<string, string>> ReadVariables(string cpuName, IEnumerable<string> variables)
        {
            throw new NotImplementedException();
        }
    }
}
