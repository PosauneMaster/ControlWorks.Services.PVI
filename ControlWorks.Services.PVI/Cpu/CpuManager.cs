using System;
using System.Collections.Generic;
using BR.AN.PviServices;
using log4net;

namespace ControlWorks.Services.PVI.Cpu
{
    public interface ICpuManager
    {
        event EventHandler<CpusLoadedEventArgs> CpusLoaded;
        void CreateCpu(CpuInfo info);
        void LoadCpuCollection(IList<CpuInfo> cpuCollection);
    }

    public class CpuManager : ICpuManager
    {
        private static readonly ILog Log = LogManager.GetLogger("FileLogger");

        private Service _service;
        private int _expectedCpus;


        public event EventHandler<CpusLoadedEventArgs> CpusLoaded;

        public CpuManager(Service service)
        {
            _service = service;
        }

        public void CreateCpu(CpuInfo info)
        {
            Log.Info($"Creating Cpu. Name={info.Name}; IpAddress={info.IpAddress}; Description={info.Description}");

        }

        public void LoadCpuCollection(IList<CpuInfo> cpuCollection)
        {
            _expectedCpus = cpuCollection.Count;
            foreach (var cpu in cpuCollection)
            {
                CreateCpu(cpu);
            }
        }
    }

    public class CpusLoadedEventArgs : EventArgs
    {
        public List<string> Cpus { get; set; }
    }
}
