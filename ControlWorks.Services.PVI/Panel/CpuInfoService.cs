using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ControlWorks.Services.PVI.Panel
{
    public interface ICpuInfoService
    {
        CpuInfo FindByName(string name);
        CpuInfo FindByIp(string ip);
        CpuInfo AddOrUpdate(CpuInfo cpu);
        void AddRange(IEnumerable<CpuInfo> cpuList);
        void Remove(CpuInfo cpu);
        bool Open(string filepath);
        List<CpuInfo> GetAll();
        bool Save(string filepath);

    }
    public class CpuInfoService : ICpuInfoService
    {
        private static readonly object SyncLock = new object();
        private readonly Dictionary<string, CpuInfo> _cpuLookup;
        private readonly IFileWrapper _fileWrapper;

        public CpuInfoService(IFileWrapper filewrapper)
        {
            _cpuLookup = new Dictionary<string, CpuInfo>();
            _fileWrapper = filewrapper;
        }

        public CpuInfo FindByName(string name)
        {
            if (_cpuLookup.ContainsKey(name))
            {
                return _cpuLookup[name];
            }
            return null;
        }

        public CpuInfo FindByIp(string ip)
        {
            var pair = _cpuLookup.FirstOrDefault(p => p.Value.IpAddress == ip);
            return pair.Value;
        }

        public CpuInfo AddOrUpdate(CpuInfo cpu)
        {
            Remove(cpu);

            _cpuLookup.Add(cpu.Name, cpu);

            return cpu;
        }

        public void AddRange(IEnumerable<CpuInfo> cpuList)
        {
            foreach (var cpu in cpuList)
            {
                AddOrUpdate(cpu);
            }
        }

        public void Remove(CpuInfo cpu)
        {
            if (_cpuLookup.ContainsKey(cpu.Name))
            {
                _cpuLookup.Remove(cpu.Name);
            }
        }

        public List<CpuInfo> GetAll()
        {
            return _cpuLookup.Values.ToList();
        }

        public bool Open(string filepath)
        {
            lock (SyncLock)
            {
                try
                {
                    if (_fileWrapper.Exists(filepath))
                    {
                        var json = _fileWrapper.Read(filepath);
                        var list = JsonConvert.DeserializeObject<List<CpuInfo>>(json);
                        _cpuLookup.Clear();
                        foreach (var cpu in list)
                        {
                            AddOrUpdate(cpu);
                        }
                    }
                }
                catch
                {
                    throw;
                }
            }

            return true;
        }
        public bool Save(string filepath)
        {
            string path = filepath;
            if (String.IsNullOrEmpty(Path.GetExtension(filepath)))
            {
                path = $"{filepath}.config";
            }
            var fi = new FileInfo(path);

            string json = JsonConvert.SerializeObject(GetAll());
            _fileWrapper.Write(fi.FullName, json);

            return true;
        }
    }
}
