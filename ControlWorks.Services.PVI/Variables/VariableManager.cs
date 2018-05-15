using ControlWorks.Services.ConfigurationProvider;
using ControlWorks.Services.PVI.Impl;
using log4net;
using System;
using System.Collections.Generic;

namespace ControlWorks.Services.PVI.Variables
{
    public interface IVariableManager
    {
        void ConnectVariables(IEnumerable<string> cpuNames);
        List<Tuple<string, string>> GetVariables(string cpuName);
    }
    public class VariableManager : IVariableManager
    {
        private readonly ILog _log = LogManager.GetLogger("FileLogger");

        private readonly IVariableWrapper _variableWrapper;
        private readonly IVariableInfoCollection _variableInfoCollection;


        public VariableManager(IVariableWrapper variableWrapper, IVariableInfoCollection variableInfoCollection)
        {
            _variableWrapper = variableWrapper;
            _variableInfoCollection = variableInfoCollection;
        }

        public List<Tuple<string, string>> GetVariables(string cpuName)
        {
            var info = _variableInfoCollection.FindByCpu(cpuName);
            return _variableWrapper.ReadVariables(info);
        }

        public void ConnectVariables(IEnumerable<string> cpuNames)
        {
            _variableInfoCollection.Open(AppSettings.VariableSettingsFile);
            foreach (var cpuName in cpuNames)
            {
                var info = _variableInfoCollection.FindByCpu(cpuName);
                _variableWrapper.ConnectVariables(info.CpuName, info.Variables);
            }
        }
    }
}
