using System.Collections.Generic;
using BR.AN.PviServices;
using ControlWorks.Services.ConfigurationProvider;

namespace ControlWorks.Services.PVI.Panel
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

            foreach (BR.AN.PviServices.Cpu cpu in _collection.Values)
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
            return settings.Save(AppSettings.CpuSettingsFile);
        }

        public bool RemoveByName(string name)
        {
            var settings = GetSettings();
            var cpu = settings.FindByName(name);
            if (cpu != null)
            {
                settings.Remove(cpu);
                settings.Save(AppSettings.CpuSettingsFile);
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
                settings.Save(AppSettings.CpuSettingsFile);
                return true;
            }
            return false;
        }


        private CpuInfoCollection GetSettings()
        {
            var settingFile = AppSettings.CpuSettingsFile;
            var collection = new CpuInfoCollection();
            collection.Open(settingFile);

            return collection;
        }

        private CpuDetailResponse Map(CpuInfo setting, BR.AN.PviServices.Cpu cpu)
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
