using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ControlWorks.Services.PVI.Panel
{
    public interface ICpuManager
    {
        //void CreateCpu(CpuInfo info);
        //void LoadCpuCollection(IList<CpuInfo> cpuCollection);
    }

    public class CpuManager : ICpuManager
    {
        private readonly IServiceWrapper _serviceWrapper;
        private readonly ICpuInfoService _cpuInfoService;

        public CpuManager(IServiceWrapper serviceWrapper, ICpuInfoService cpuInfoService, IFileWrapper fileWrapper)
        {
            _serviceWrapper = serviceWrapper;
            _cpuInfoService = cpuInfoService;
        }

        public void CreateCpu(CpuInfo cpuInfo)
        {
            _serviceWrapper.CreateCpu(cpuInfo);
        }

        public void LoadCpus()
        {
            var settingFile = ConfigurationProvider.AppSettings.CpuSettingsFile;
            _cpuInfoService.Open(settingFile);
            var list = _cpuInfoService.GetAll();

            foreach (var cpuInfo in list)
            {
                CreateCpu(cpuInfo);
            }
        }

        public void DisconnectCpu(string name)
        {
            _serviceWrapper.DisconnectCpu(name);
        }

        public Task<CpuDetailResponse> GetCpuByName(string name)
        {

            return await Task.Run(() => _cpuInfoService.FindByName(name));
        }


    }
}
