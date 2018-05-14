using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ControlWorks.Services.PVI.Impl;

namespace ControlWorks.Services.PVI.Panel
{
    public interface ICpuManager
    {
        void LoadCpus();
        void DisconnectCpuByName(string name);
        void DisconnectCpuByIp(string ip);
        CpuDetailResponse FindCpuByName(string name);
        CpuDetailResponse FindCpuByIp(string ip);
        void Add(CpuInfo info);
        void Update(CpuInfo info);
    }

    public class CpuManager : ICpuManager
    {
        private readonly IServiceWrapper _serviceWrapper;
        private readonly ICpuWrapper _cpuWrapper;
        private readonly ICpuInfoService _cpuInfoService;

        public CpuManager(IServiceWrapper serviceWrapper, ICpuInfoService cpuInfoService, IFileWrapper fileWrapper, ICpuWrapper cpuWrapper)
        {
            _serviceWrapper = serviceWrapper;
            _cpuInfoService = cpuInfoService;
            _cpuWrapper = cpuWrapper;
        }

        #region public interface

        public void LoadCpus()
        {
            var list = GetCpuSettings().GetCpuList();
            _cpuWrapper.Initialize(list);

        }
        public void DisconnectCpuByName(string name)
        {
            var info = FindByName(name);
            GetCpuSettings()
                .Remove(info);
            _cpuWrapper.DisconnectCpu(info);
        }

        public void DisconnectCpuByIp(string ip)
        {
            var info = FindByIp(ip);
            GetCpuSettings()
                .Remove(info);
            _cpuWrapper.DisconnectCpu(FindByIp(ip));
        }

        public CpuDetailResponse FindCpuByName(string name)
        {
            return _cpuWrapper.GetCpuByName(FindByName(name));
        }

        public CpuDetailResponse FindCpuByIp(string ip)
        {
            return _cpuWrapper.GetCpuByName(FindByIp(ip));
        }

        public void Add(CpuInfo info)
        {
            Update(info);
        }

        public void Update(CpuInfo info)
        {
            GetCpuSettings()
                .AddOrUpdate(info);

            _cpuWrapper.CreateCpu(info);
        }

        #endregion

        #region private methods

        private CpuInfoCollection GetCpuSettings()
        {
            var collection = new CpuInfoCollection();
            collection.Open(ConfigurationProvider.AppSettings.CpuSettingsFile);
            return collection;
        }

        private CpuInfo FindByName(string name)
        {
            return GetCpuSettings().FindByName(name);
        }

        private CpuInfo FindByIp(string ip)
        {
            return GetCpuSettings().FindByIp(ip);
        }

        #endregion
    }
}
