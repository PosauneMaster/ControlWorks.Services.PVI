using ControlWorks.Services.PVI.Impl;
using System.Collections.Generic;
using ControlWorks.Common;
using System.Linq;

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
        bool Update(CpuInfo info);
        List<string> GetCpuNames();
        CpuDetailResponse[] GetCpus();
        void Reconnect();

    }

    public class CpuManager : ICpuManager
    {
        private readonly ICpuWrapper _cpuWrapper;

        public CpuManager(ICpuWrapper cpuWrapper)
        {
            _cpuWrapper = cpuWrapper;
        }

        #region public interface

        public void LoadCpus()
        {
            var list = GetCpuSettings().GetCpuList().OrderBy(c => c.Name);
            _cpuWrapper.Initialize(list);

        }

        public void Reconnect()
        {
            var list = GetCpuSettings().GetCpuList();

            foreach (var info in list)
            {
                _cpuWrapper.Reconnect(info);
            }
        }

        public void DisconnectCpuByName(string name)
        {
            var info = FindByName(name);
            Disconnect(info);
        }

        public void DisconnectCpuByIp(string ip)
        {
            var info = FindByIp(ip);
            Disconnect(info);
        }

        private void Disconnect(CpuInfo info)
        {
            var settings = GetCpuSettings();
            settings.Remove(info);
            _cpuWrapper.DisconnectCpu(info);
            settings.Save(ConfigurationProvider.CpuSettingsFile);
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

        public bool Update(CpuInfo info)
        {
            var settings = GetCpuSettings();
            settings.AddOrUpdate(info);
            _cpuWrapper.CreateCpu(info);
            return settings.Save(ConfigurationProvider.CpuSettingsFile);
        }

        public List<string> GetCpuNames()
        {
            return _cpuWrapper.GetCpuNames();
        }

        public CpuDetailResponse[] GetCpus()
        {
            var responseList = new List<CpuDetailResponse>();

            var collection = new CpuInfoCollection();
            collection.Open(ConfigurationProvider.CpuSettingsFile);

            var cpuNames = GetCpuNames();

            foreach(var name in cpuNames)
            {
                var cpu = collection.FindByName(name);
                var cpuDetail = _cpuWrapper.GetCpuByName(cpu);
                responseList.Add(cpuDetail);
            }

            return responseList.OrderBy(c => c.Name).ToArray();
        }

        #endregion

        #region private methods

        private CpuInfoCollection GetCpuSettings()
        {
            var collection = new CpuInfoCollection();
            collection.Open(ConfigurationProvider.CpuSettingsFile);
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
