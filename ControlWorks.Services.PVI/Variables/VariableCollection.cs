using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ControlWorks.Services.PVI
{
    public class VariableInfo
    {
        public string CpuName { get; set; }
        public string[] Variables { get; set; }
    }
    public class VariableInfoCollection
    {
        public static readonly object SyncLock = new object();

        private const string VariableMaster = "VARIABLE_MASTER";

        private readonly Dictionary<string, VariableInfo> _variableLookup;
        private readonly IFileWrapper _fileWrapper;


        public VariableInfoCollection()
        {
            _variableLookup = new Dictionary<string, VariableInfo>();

        }

        public List<VariableInfo> GetAll()
        {
            if (_variableLookup.ContainsKey(VariableMaster))
            {
                var responseList = new List<VariableInfo>();
                var master = _variableLookup[VariableMaster];
                responseList.Add(master);

                foreach (var vi in _variableLookup.Values)
                {
                    if (vi.CpuName != VariableMaster)
                    {
                        var list = new List<string>(vi.Variables);
                        list.AddRange(master.Variables);
                        responseList.Add(new VariableInfo { CpuName = vi.CpuName, Variables = list.ToArray() });
                    }
                }

                return responseList;

            }

            return _variableLookup.Values.ToList();
        }

        public void AddCpuRange(string[] cpuList)
        {
            foreach (var cpu in cpuList)
            {
                if (!_variableLookup.ContainsKey(cpu))
                {
                    _variableLookup.Add(cpu, new VariableInfo { CpuName = cpu, Variables = new List<string>().ToArray() });
                }
            }

        }

        public void RemoveCpuRange(string[] cpuList)
        {
            foreach (var cpu in cpuList)
            {
                if (_variableLookup.ContainsKey(cpu))
                {
                    _variableLookup.Remove(cpu);
                }
            }
        }

        public VariableInfo FindByCpu(string name)
        {
            return GetAll().FirstOrDefault(v => v.CpuName == name);
        }

        public void AddRange(string cpuName, IEnumerable<string> variableNames)
        {
            foreach (var name in variableNames)
            {
                Add(cpuName, name);
            }
        }

        public void RemoveRange(string cpuName, IEnumerable<string> variableNames)
        {
            foreach (var name in variableNames)
            {
                Remove(cpuName, name);
            }
        }

        public void Add(string cpuName, string variableName)
        {
            if (_variableLookup.ContainsKey(VariableMaster))
            {
                var master = _variableLookup[VariableMaster];
                if (master.Variables.Contains(variableName))
                {
                    return;
                }
            }

            if (!_variableLookup.ContainsKey(cpuName))
            {
                var info = new VariableInfo
                {
                    CpuName = cpuName,
                    Variables = new List<string>().ToArray()
                };
                _variableLookup.Add(cpuName, info);
            }

            var vInfo = _variableLookup[cpuName];

            if (!vInfo.Variables.Contains(variableName))
            {
                var list = new List<string>(vInfo.Variables) {variableName};
                vInfo.Variables = list.ToArray();
            }
        }

        public void Remove(string cpuName, string variableName)
        {
            if (!_variableLookup.ContainsKey(VariableMaster))
            {
                RemoveVariable(cpuName, variableName);
            }
            else
            {
                var master = _variableLookup[VariableMaster];
                if (master.Variables.Contains(variableName))
                {
                    RemoveVariable(VariableMaster, variableName);
                }
            }
        }

        private void RemoveVariable(string cpuName, string variableName)
        {
            if (_variableLookup.ContainsKey(cpuName))
            {
                var vInfo = _variableLookup[cpuName];
                var name = vInfo.Variables.FirstOrDefault();
                if (!String.IsNullOrEmpty(name))
                {
                    var list = new List<string>(vInfo.Variables);
                    list.Remove(variableName);
                    vInfo.Variables = list.ToArray();
                }

                if (!vInfo.Variables.Any())
                {
                    _variableLookup.Remove(cpuName);
                }
            }
        }

        public void Open(string filepath)
        {
            lock (SyncLock)
            {
                if (File.Exists(filepath))
                {
                    var json = _fileWrapper.Read(filepath);
                    var list = JsonConvert.DeserializeObject<List<VariableInfo>>(json);
                    _variableLookup.Clear();

                    foreach (var v in list)
                    {
                        _variableLookup.Add(v.CpuName, v);
                    }
                }
            }
        }

        public void Save(string filepath)
        {
            string path = filepath;
            if (String.IsNullOrEmpty(Path.GetExtension(filepath)))
            {
                path = $"{filepath}.config";
            }
            var fi = new FileInfo(path);

            if (fi.DirectoryName != null)
            {
                if (!Directory.Exists(fi.DirectoryName))
                {
                    Directory.CreateDirectory(fi.DirectoryName);
                }

                string json = JsonConvert.SerializeObject(new List<VariableInfo>(_variableLookup.Values));

                _fileWrapper.Write(fi.FullName, json);
            }
        }
    }
}
