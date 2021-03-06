﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ControlWorks.Common;
using Newtonsoft.Json;

namespace ControlWorks.Services.PVI.Variables
{
    public interface IVariableInfoCollection
    {
        List<VariableInfo> GetAll();
        void AddCpuRange(string[] cpuList);
        void RemoveCpuRange(string[] cpuList);
        VariableInfo FindByCpu(string name);
        void AddRange(string cpuName, IEnumerable<string> variableNames);
        void RemoveVariableRange(string cpuName, IEnumerable<string> variableNames);
        void Add(string cpuName, string variableName);
        void Remove(string cpuName, string variableName);
        void Open(string filepath);
        void Save(string filepath);
        void UpdateCpuVariables(string cpuName, IEnumerable<string> variableNames);

    }

    public class VariableDetail
    {
        public string CpuName { get; set; }
        public string VariableName { get; set; }
    }

    public class VariableInfo
    {
        public string CpuName { get; set; }
        public string[] Variables { get; set; }
    }
    public class VariableInfoCollection : IVariableInfoCollection
    {
        private const string VariableMaster = "VARIABLE_MASTER";

        private readonly Dictionary<string, VariableInfo> _variableLookup;
        private readonly IFileWrapper _fileWrapper;


        public VariableInfoCollection(IFileWrapper fileWrapper)
        {
            _variableLookup = new Dictionary<string, VariableInfo>();
            _fileWrapper = fileWrapper;

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

        public void UpdateCpuVariables(string cpuName, IEnumerable<string> variableNames)
        {
            Open(ConfigurationProvider.VariableSettingsFile);
            RemoveCpuRange(new[] { cpuName });
            AddRange(cpuName, variableNames);
            Save(ConfigurationProvider.VariableSettingsFile);
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
            var list = new List<string>(variableNames);

            foreach (var name in list)
            {
                Add(cpuName, name);
            }
        }

        public void RemoveVariableRange(string cpuName, IEnumerable<string> variableNames)
        {
            Open(ConfigurationProvider.VariableSettingsFile);

            foreach (var name in variableNames)
            {
                Remove(cpuName, name);
            }

            Save(ConfigurationProvider.VariableSettingsFile);

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
            var json = _fileWrapper.Read(filepath);

            if (!String.IsNullOrEmpty(json))
            {
                var list = JsonConvert.DeserializeObject<List<VariableInfo>>(json);
                _variableLookup.Clear();

                foreach (var v in list)
                {
                    _variableLookup.Add(v.CpuName, v);
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

            _fileWrapper.CreateBackup(path);

            var fi = new FileInfo(path);
            string json = JsonConvert.SerializeObject(new List<VariableInfo>(_variableLookup.Values));

            _fileWrapper.Write(fi.FullName, json);
        }
    }
}
