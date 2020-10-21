using ControlWorks.Services.PVI.Impl;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using ControlWorks.Common;

namespace ControlWorks.Services.PVI.Variables
{
    public interface IVariableManager
    {
        void ConnectVariables(IList<string> cpuNames);
        void ConnectVariable(string cpuName);
        VariableResponse GetAllVariables(string cpuName);
        VariableResponse GetVariables(string cpuName, IList<string> variableNames);
        VariableResponse GetActiveVariables(string cpuName);
        void AddVariables(string cpuName, IList<string> variableNames);
        void RemoveVariables(string cpuName, IList<string> variableNames);
        List<VariableDetails> GetVariableDetails(string cpuName);


    }
    public class VariableManager : IVariableManager
    {
        private readonly IVariableWrapper _variableWrapper;
        private readonly IVariableInfoCollection _variableInfoCollection;


        public VariableManager(IVariableWrapper variableWrapper, IVariableInfoCollection variableInfoCollection)
        {
            _variableWrapper = variableWrapper;
            _variableInfoCollection = variableInfoCollection;
            _variableInfoCollection.Open(ConfigurationProvider.VariableSettingsFile);
        }

        public VariableResponse GetAllVariables(string cpuName)
        {
            var info = _variableInfoCollection.FindByCpu(cpuName);
            return _variableWrapper.ReadVariables(info);
        }

        public VariableResponse GetActiveVariables(string cpuName)
        {
            var info = _variableInfoCollection.FindByCpu(cpuName);
            return _variableWrapper.ReadActiveVariables(info);
        }

        public VariableResponse GetVariables(string cpuName, IList<string> variableNames)
        {
            var info = _variableInfoCollection.FindByCpu(cpuName);
            info.Variables = variableNames.ToArray();
            return _variableWrapper.ReadVariables(info);
        }

        public List<VariableDetails> GetVariableDetails(string cpuName)
        {
            var info = _variableInfoCollection.FindByCpu(cpuName);
            return _variableWrapper.GetVariableDetails(info);
        }

        public void ConnectVariables(IList<string> cpuNames)
        {
            foreach (var cpuName in cpuNames)
            {
                ConnectVariable(cpuName);
            }
        }

        public void ConnectVariable(string cpuName)
        {
            var info = _variableInfoCollection.FindByCpu(cpuName);
            if (info != null)
            {
                _variableWrapper.ConnectVariables(info.CpuName, info.Variables);
            }
        }

        public void AddVariables(string cpuName, IList<string> variableNames)
        {
            _variableInfoCollection.UpdateCpuVariables(cpuName, variableNames);
            _variableWrapper.ConnectVariables(cpuName, variableNames);
        }

        public void RemoveVariables(string cpuName, IList<string> variableNames)
        {
            _variableInfoCollection.RemoveVariableRange(cpuName, variableNames);
            _variableWrapper.DisconnectVariables(cpuName, variableNames);
        }

    }
}
