using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BR.AN.PviServices;
using ControlWorks.Services.ConfigurationProvider;
using ControlWorks.Services.PVI.Panel;
using ControlWorks.Services.PVI.Variables;
using Microsoft.SqlServer.Server;

namespace ControlWorks.Services.PVI
{
    public interface IServiceWrapper
    {
        void ConnectPviService();
        void CreateCpu(CpuInfo cpuInfo);
        void DisconnectCpu(string name);
        void ConnectVariables(string cpuName, IEnumerable<string> variables);
        void ConnectVariable(string cpuName, string name);
    }

    public class ServiceWrapper : IServiceWrapper
    {
        private Service _service;
        private readonly IEventNotifier _eventNotifier;
        private AutoResetEvent _disconnectWaitHandle;

        public ServiceWrapper(IEventNotifier eventNotifier)
        {
            _eventNotifier = eventNotifier;
        }

        public void ConnectPviService()
        {
            _service = new Service(Guid.NewGuid().ToString());

            _service.Connected += _service_Connected;
            _service.Disconnected += _service_Disconnected;
            _service.Error += _service_Error;

        }

        public void CreateCpu(CpuInfo cpuInfo)
        {
            Cpu cpu = null;
            if (_service.Cpus.ContainsKey(cpuInfo.Name))
            {

                cpu = _service.Cpus[cpuInfo.Name];
                DisconnectCpu(cpuInfo.Name);
            }
            else
            {
                cpu = new Cpu(_service, cpuInfo.Name);
            }

            cpu.Connection.DeviceType = DeviceType.TcpIp;
            cpu.Connection.TcpIp.SourceStation = AppSettings.SourceStationId;
            cpu.Connection.TcpIp.DestinationIpAddress = cpuInfo.IpAddress;

            cpu.Connected += Cpu_Connected; ;
            cpu.Error += Cpu_Error; ;
            cpu.Disconnected += Cpu_Disconnected; ;

            cpu.Connect();
        }
        public void DisconnectCpu(string name)
        {
            if (_service.Cpus.ContainsKey(name))
            {

                Cpu cpu = _service.Cpus[name];

                if (cpu.IsConnected)
                {
                    using (_disconnectWaitHandle = new AutoResetEvent(false))
                    {
                        cpu.Disconnect();
                        _disconnectWaitHandle.WaitOne(1000);
                    }
                }
            }
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

        private void Cpu_Disconnected(object sender, PviEventArgs e)
        {
            if (sender is Cpu cpu)
            {
                if (_service.Cpus.ContainsKey(cpu.Name))
                {
                    _service.Cpus.Remove(cpu.Name);
                }
            }

            var pviEventMsg = Utils.FormatPviEventMessage("ServiceWrapper.Cpu_Disconnected", e);
            _eventNotifier.OnCpuDisconnected(sender, new PviApplicationEventArgs() { Message = pviEventMsg });
        }

        private void Cpu_Error(object sender, PviEventArgs e)
        {
            var pviEventMsg = Utils.FormatPviEventMessage("ServiceWrapper.Cpu_Error", e);
            _eventNotifier.OnCpuError(sender, new PviApplicationEventArgs() { Message = pviEventMsg });
        }

        private void Cpu_Connected(object sender, PviEventArgs e)
        {
            var pviEventMsg = Utils.FormatPviEventMessage("ServiceWrapper.Cpu_Connected", e);
            _eventNotifier.OnCpuConnected(sender, new PviApplicationEventArgs() { Message = pviEventMsg });
        }

        private void _service_Error(object sender, PviEventArgs e)
        {
            var pviEventMsg = Utils.FormatPviEventMessage("ServiceWrapper._service_Error", e);
            _eventNotifier.OnPviServiceError(sender, new PviApplicationEventArgs() { Message = pviEventMsg });
        }

        private void _service_Disconnected(object sender, PviEventArgs e)
        {
            var pviEventMsg = Utils.FormatPviEventMessage("ServiceWrapper._service_Disconnected", e);
            _eventNotifier.OnPviServiceDisconnected(sender, new PviApplicationEventArgs() { Message = pviEventMsg });
        }

        private void _service_Connected(object sender, PviEventArgs e)
        {
            string serviceName = String.Empty;
            if (sender is Service service)
            {
                serviceName = service.FullName;
            }

            var pviEventMsg = Utils.FormatPviEventMessage($"ServiceWrapper._service_Connected; ServiceName={serviceName}", e);
            _eventNotifier.OnPviServiceConnected(sender, new PviApplicationEventArgs() { Message = pviEventMsg });
        }
    }
}
