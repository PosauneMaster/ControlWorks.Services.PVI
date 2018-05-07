using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ControlWorks.Services.PVI
{
    public class CpuInfoCollection
    {
        private static readonly object SyncLock = new object();
        readonly Dictionary<string, CpuInfo> _cpuLookup;

        public CpuInfoCollection()
        {
            _cpuLookup = new Dictionary<string, CpuInfo>();
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
                    if (File.Exists(filepath))
                    {
                        var json = FileAccess.Read(filepath);
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
            try
            {
                string path = filepath;
                if (String.IsNullOrEmpty(Path.GetExtension(filepath)))
                {
                    path = $"{filepath}.config";
                }
                var fi = new FileInfo(path);

                if (!Directory.Exists(fi.DirectoryName))
                {
                    Directory.CreateDirectory(fi.DirectoryName);
                }

                string json = JsonConvert.SerializeObject(GetAll());
                FileAccess.Write(fi.FullName, json);

            }
            catch
            {
                throw;
            }

            return true;
        }
    }

    public class CpuInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IpAddress { get; set; }
    }
}
