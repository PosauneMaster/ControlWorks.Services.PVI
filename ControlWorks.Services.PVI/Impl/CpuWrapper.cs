using BR.AN.PviServices;
using ControlWorks.Services.PVI.Panel;
using System;
using System.Collections.Generic;
using System.Threading;
using ControlWorks.Common;

namespace ControlWorks.Services.PVI.Impl
{

    public interface ICpuWrapper
    {
        void Initialize(IEnumerable<CpuInfo> cpuList);
        void CreateCpu(CpuInfo cpuInfo);
        void DisconnectCpu(CpuInfo info);
        CpuDetailResponse GetCpuByName(CpuInfo info);
        List<CpuDetailResponse> GetAllCpus(IEnumerable<CpuInfo> cpuInfo);
        List<string> GetCpuNames();
    }

    public class CpuWrapper : ICpuWrapper
    {
        private readonly Service _service;
        private readonly IEventNotifier _eventNotifier;
        private int _initialCount;

        private AutoResetEvent _disconnectWaitHandle;

        public CpuWrapper(Service service, IEventNotifier eventNotifier)
        {
            _service = service;
            _eventNotifier = eventNotifier;
        }

        public void Initialize(IEnumerable<CpuInfo> cpuList)
        {
            var list = new List<CpuInfo>(cpuList);
            _initialCount = list.Count;

            foreach (var cpuInfo in list)
            {
                CreateCpu(cpuInfo);
            }
        }

        public List<string> GetCpuNames()
        {
            var list = new List<string>();
            foreach (Cpu cpu in _service.Cpus)
            {
                list.Add(cpu.Name);
            }

            return list;
        }

        public void CreateCpu(CpuInfo cpuInfo)
        {
            Cpu cpu = null;
            if (_service.Cpus.ContainsKey(cpuInfo.Name))
            {

                cpu = _service.Cpus[cpuInfo.Name];
                DisconnectCpu(cpuInfo);
            }
            else
            {
                cpu = new Cpu(_service, cpuInfo.Name);
            }

            cpu.Connection.DeviceType = DeviceType.TcpIp;
            cpu.Connection.TcpIp.SourceStation = ConfigurationProvider.SourceStationId;
            cpu.Connection.TcpIp.DestinationIpAddress = cpuInfo.IpAddress;

            cpu.Connected += Cpu_Connected; ;
            cpu.Error += Cpu_Error; ;
            cpu.Disconnected += Cpu_Disconnected; ;

            cpu.Connect();
        }
        public void DisconnectCpu(CpuInfo info)
        {
            if (_service.Cpus.ContainsKey(info.Name))
            {

                Cpu cpu = _service.Cpus[info.Name];

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

            if (_service.Cpus.Count == _initialCount)
            {
                _eventNotifier.OnCpuManangerInitialized(this, new EventArgs());
            }
        }

        public List<CpuDetailResponse> GetAllCpus(IEnumerable<CpuInfo> cpuInfo)
        {
            var list = new List<CpuDetailResponse>();

            foreach (var info in cpuInfo)
            {
                var response = GetCpuByName(info);
                if (response != null)
                {
                    list.Add(response);
                }
            }

            return list;
        }

        public CpuDetailResponse GetCpuByName(CpuInfo info)
        {
            if (_service.Cpus.ContainsKey(info.Name))
            {
                return Map(info, _service.Cpus[info.Name]);
            }

            return null;
        }

        private CpuDetailResponse Map(CpuInfo setting, Cpu cpu)
        {
            var detail = new CpuDetailResponse
            {
                Description = setting.Description,
                HasError = cpu.HasError,
                IpAddress = setting.IpAddress,
                IsConnected = cpu.IsConnected,
                Name = setting.Name,
                Error = cpu.HasError ? new CpuError { ErrorCode = cpu.ErrorCode, ErrorText = cpu.ErrorText } : null
            };

            return detail;
        }

    }
}
