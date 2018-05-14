using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ControlWorks.Services.PVI.Panel
{
    public class CpuInfoCollection
    {
        private static readonly object _syncLock = new object();
        private readonly Dictionary<string, CpuInfo> _cpuLookup;

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

        public List<CpuInfo> GetCpuList()
        {
            return _cpuLookup.Values.ToList();
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
            var fileAccess = new FileWrapper();

            lock (_syncLock)
            {
                try
                {
                    if (File.Exists(filepath))
                    {
                        var json = fileAccess.Read(filepath);
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
            var fileAccess = new FileWrapper();

            try
            {
                string path = filepath;
                if (String.IsNullOrEmpty(Path.GetExtension(filepath)))
                {
                    path = $"{filepath}.config";
                }
                var fi = new FileInfo(path);

                if (!String.IsNullOrEmpty(fi.DirectoryName) && !Directory.Exists(fi.DirectoryName))
                {
                    Directory.CreateDirectory(fi.DirectoryName);
                }

                string json = JsonConvert.SerializeObject(GetAll());
                fileAccess.Write(fi.FullName, json);

            }
            catch
            {
                throw;
            }

            return true;
        }
    }
}
