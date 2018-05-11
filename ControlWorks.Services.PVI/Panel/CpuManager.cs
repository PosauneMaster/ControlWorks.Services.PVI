using System;
using System.Collections.Generic;
using System.Threading;

namespace ControlWorks.Services.PVI.Panel
{
    public interface ICpuManager
    {
        //void CreateCpu(CpuInfo info);
        //void LoadCpuCollection(IList<CpuInfo> cpuCollection);
    }

    public class CpuManager : ICpuManager
    {
        private IServiceWrapper _serviceWrapper;
        private AutoResetEvent _disconnectWaitHandle;

        public event EventHandler<CpusLoadedEventArgs> CpusLoaded;


        public CpuManager(IServiceWrapper serviceWrapper, ICpuInfoService cpuInfoCollection)
        {
            _serviceWrapper = serviceWrapper;
        }

        public void LoadCpus()
        {
            var settingFile = ConfigurationProvider.AppSettings.CpuSettingsFile;
            var collection = new CpuInfoService(new FileWrapper());
            collection.Open(settingFile);
            var list = collection.GetAll();


        }




        //public void CreateCpu(CpuInfo info)
        //{
        //    _log.Info($"Creating Cpu. Name={info.Name}; IpAddress={info.IpAddress}; Description={info.Description}");

        //    Cpu cpu = null;
        //    if (_service.Cpus.ContainsKey(info.Name))
        //    {

        //        _log.Info($"A Cpu with the name {info.Name} already exists. Disconnecting and updating");
        //        cpu = _service.Cpus[info.Name];
        //        DisconnectCpu(info.Name);
        //    }
        //    else
        //    {
        //        cpu = new Cpu(_service, info.Name);
        //    }

        //    cpu.Connection.DeviceType = DeviceType.TcpIp;
        //    cpu.Connection.TcpIp.SourceStation = _sourceStationId;
        //    cpu.Connection.TcpIp.DestinationIpAddress = info.IpAddress;

        //    cpu.Connected += Cpu_Connected;
        //    cpu.Error += Cpu_Error;
        //    cpu.Disconnected += Cpu_Disconnected;

        //    cpu.Connect();
        //}

        //private void Cpu_Disconnected(object sender, PviEventArgs e)
        //{
        //    if (sender is Cpu cpu)
        //    {
        //        cpu.Connected -= Cpu_Connected;
        //        cpu.Error -= Cpu_Error;
        //        cpu.Disconnected -= Cpu_Disconnected;
        //    }

        //    _eventNotifier.OnCpuDisconnected(sender, new PviApplicationEventArgs() {Message = Utils.FormatPviEventMessage($"Cpu_Disconnected", e)});
        //}

        //private void Cpu_Error(object sender, PviEventArgs e)
        //{
        //    _eventNotifier.OnCpuError(sender, new PviApplicationEventArgs() { Message = Utils.FormatPviEventMessage($"Cpu_Error", e) });
        //}

        //private void Cpu_Connected(object sender, PviEventArgs e)
        //{
        //    _eventNotifier.OnCpuConnected(sender, new PviApplicationEventArgs() { Message = Utils.FormatPviEventMessage($"Cpu_Connected", e) });
        //}

        //public void DisconnectCpu(string name)
        //{
        //    if (_service.Cpus.ContainsKey(name))
        //    {

        //        _log.Info($"CpuManager.DisconnectCpu Name={name}");

        //        Cpu cpu = _service.Cpus[name];

        //        if (cpu.IsConnected)
        //        {
        //            _disconnectWaitHandle = new AutoResetEvent(false);

        //            cpu.Disconnect();

        //            _disconnectWaitHandle.WaitOne(1000);
        //            _disconnectWaitHandle.Dispose();
        //            _disconnectWaitHandle = null;
        //        } 

        //        _service.Cpus.Remove(cpu.Name);
        //    }
        //    else
        //    {
        //        _log.Info($"CpuManager.DisconnectCpu Name={name} Not found");
        //    }
        //}


        //public void LoadCpuCollection(IList<CpuInfo> cpuCollection)
        //{
        //    foreach (var cpu in cpuCollection)
        //    {
        //        CreateCpu(cpu);
        //    }
        //}
    }

    public class CpusLoadedEventArgs : EventArgs
    {
        public List<string> Cpus { get; set; }
    }
}
