using BR.AN.PviServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlWorks.Services.PVI
{
    public interface ICpuApi
    {
        List<CpuDetailResponse> GetAll();
        CpuDetailResponse FindByName(string name);
        CpuDetailResponse FindByIp(string ip);
        bool Add(CpuInfo info);
        bool Update(CpuInfo info);
        bool RemoveByName(string name);
        bool RemoveByIp(string ip);

    }

    public class CpuApi : ICpuApi
    {
        readonly CpuCollection _collection;

        public CpuApi()
        {

        }

        public CpuApi(CpuCollection collection)
        {
            _collection = collection;
        }

        public List<CpuDetailResponse> GetAll()
        {
            var list = new List<CpuDetailResponse>();
            var settings = GetSettings();

            foreach (Cpu cpu in _collection.Values)
            {
                var setting = settings.FindByName(cpu.Name);

                list.Add(Map(setting, cpu));
            }

            return list;
        }

        public CpuDetailResponse FindByName(string name)
        {

            if (!_collection.ContainsKey(name))
            {
                return null;
            }

            var collection = GetSettings();
            var cpu = _collection[name];
            var setting = collection.FindByName(cpu.Name);

            return Map(setting, cpu);
        }

        public CpuDetailResponse FindByIp(string ip)
        {
            var settings = GetSettings();
            var info = settings.FindByIp(ip);

            if (info == null)
            {
                return null;
            }

            if (!_collection.ContainsKey(info.Name))
            {
                return null;
            }

            var cpu = _collection[info.Name];

            return Map(info, cpu);
        }

        public bool Add(CpuInfo info)
        {
            return AddOrUpdate(info);
        }

        public bool Update(CpuInfo info)
        {
            return AddOrUpdate(info);
        }

        private bool AddOrUpdate(CpuInfo info)
        {
            var settings = GetSettings();
            settings.AddOrUpdate(info);
            return settings.Save(ConfigurationProvider.CpuSettingsFile);
        }

        public bool RemoveByName(string name)
        {
            var settings = GetSettings();
            var cpu = settings.FindByName(name);
            if (cpu != null)
            {
                settings.Remove(cpu);
                settings.Save(ConfigurationProvider.CpuSettingsFile);
                return true;
            }
            return false;
        }

        public bool RemoveByIp(string ip)
        {
            var settings = GetSettings();
            var cpu = settings.FindByIp(ip);
            if (cpu != null)
            {
                settings.Remove(cpu);
                settings.Save(ConfigurationProvider.CpuSettingsFile);
                return true;
            }
            return false;
        }


        private CpuInfoCollection GetSettings()
        {
            var settingFile = ConfigurationProvider.CpuSettingsFile;
            var collection = new CpuInfoCollection();
            collection.Open(settingFile);

            return collection;
        }

        private CpuDetailResponse Map(CpuInfo setting, Cpu cpu)
        {
            var detail = new CpuDetailResponse();
            detail.Description = setting.Description;
            detail.HasError = cpu.HasError;
            detail.IpAddress = setting.IpAddress;
            detail.IsConnected = cpu.IsConnected;
            detail.Name = setting.Name;
            if (cpu.HasError)
            {
                detail.Error = new CpuError { ErrorCode = cpu.ErrorCode, ErrorText = cpu.ErrorText };
            }
            else
            {
                detail.Error = null;
            }

            return detail;
        }

    }
}
