﻿using ControlWorks.Common;
using ControlWorks.Services.PVI.Impl;
using ControlWorks.Services.PVI.Panel;
using ControlWorks.Services.PVI.Variables;
using log4net;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace ControlWorks.Services.PVI.Pvi
{
    public interface IPviApplication
    {
        void Connect();
        void Disconnect();
        ServiceDetail ServiceDetails();
        bool GetIsConnected();
        bool GetHasError();
        void AddCpu(CpuInfo info);
        void UpdateCpu(CpuInfo info);
        CpuDetailResponse GetCpuByName(string name);
        CpuDetailResponse GetCpuByIp(string ip);
        void DeleteCpuByName(string name);
        void DeleteCpuByIp(string ip);
        CpuDetailResponse[] GetCpuData();
        void AddVariables(string cpuName, IList<string> variableNames);
        void RemoveVariables(string cpuName, IList<string> variableNames);
        VariableResponse ReadVariables(string cpuName, IList<string> variableNames);
        VariableResponse ReadAllVariables(string cpuName);
        VariableResponse ReadActiveVariables(string cpuName);
        List<VariableDetails> GetVariableDetails(string cpuName);

    }
    public class PviAplication : IPviApplication
    {
        private readonly ILog _log = LogManager.GetLogger("ControlWorksLogger");

        private PviContext _pviContext;
        private ICpuManager _cpuManager;
        private IVariableManager _variableManager;

        private readonly IEventNotifier _eventNotifier;
        private readonly IServiceWrapper _serviceWrapper;

        public PviAplication()
        {
            _eventNotifier = new EventNotifier();
            _serviceWrapper = new ServiceWrapper(_eventNotifier);
        }

        #region public interface

        public void Connect()
        {
            _eventNotifier.PviServiceConnected += _eventNotifier_PviServiceConnected;
            _eventNotifier.PviServiceDisconnected += _eventNotifier_PviServiceDisconnected;
            _eventNotifier.PviServiceError += _eventNotifier_PviServiceError;
            _eventNotifier.CpuConnected += _eventNotifier_CpuConnected;
            _eventNotifier.CpuDisconnected += _eventNotifier_CpuDisconnected;
            _eventNotifier.CpuError += _eventNotifier_CpuError;
            _eventNotifier.VariableConnected += _eventNotifier_VariableConnected;
            _eventNotifier.VariableError += _eventNotifier_VariableError;
            _eventNotifier.VariableValueChanged += _eventNotifier_VariableValueChanged;

            _eventNotifier.CpuManangerInitialized += _eventNotifier_CpuManangerInitialized;

            _pviContext = new PviContext(_serviceWrapper);
            Application.Run(_pviContext);
        }

        public ServiceDetail ServiceDetails()
        {
            return _serviceWrapper.ServiceDetails();
        }

        public bool GetIsConnected()
        {
            return _serviceWrapper.IsConnected;
        }

        public bool GetHasError()
        {
            return _serviceWrapper.HasError;
        }

        private void _eventNotifier_CpuManangerInitialized(object sender, System.EventArgs e)
        {
            _variableManager.ConnectVariables(_cpuManager.GetCpuNames());
        }

        public void Disconnect()
        {
            _serviceWrapper.DisconnectPviService();
            _pviContext.Dispose();
        }

        public void AddCpu(CpuInfo info)
        {
            _cpuManager.Add(info);
        }

        public void UpdateCpu(CpuInfo info)
        {
            _cpuManager.Update(info);
        }

        public CpuDetailResponse GetCpuByName(string name)
        {
            return _cpuManager.FindCpuByName(name);
        }

        public CpuDetailResponse GetCpuByIp(string ip)
        {
            return _cpuManager.FindCpuByIp(ip);
        }

        public void DeleteCpuByName(string name)
        {
            _cpuManager.DisconnectCpuByName(name);
        }

        public void DeleteCpuByIp(string ip)
        {
            _cpuManager.DisconnectCpuByIp(ip);
        }

        public CpuDetailResponse[] GetCpuData()
        {
            return _cpuManager.GetCpus();
        }

        public VariableResponse ReadVariables(string cpuName, IList<string> variableNames)
        {
            return _variableManager.GetVariables(cpuName, variableNames);
        }

        public VariableResponse ReadAllVariables(string cpuName)
        {
            return _variableManager.GetAllVariables(cpuName);
        }

        public List<VariableDetails> GetVariableDetails(string cpuName)
        {
            return _variableManager.GetVariableDetails(cpuName);
        }

        public VariableResponse ReadActiveVariables(string cpuName)
        {
            return _variableManager.GetActiveVariables(cpuName);
        }

        public void AddVariables(string cpuName, IList<string> variableNames)
        {
            _variableManager.AddVariables(cpuName, variableNames);
        }

        public void RemoveVariables(string cpuName, IList<string> variableNames)
        {
            _variableManager.RemoveVariables(cpuName, variableNames);
        }

        #endregion


        private void _eventNotifier_PviServiceError(object sender, PviApplicationEventArgs e)
        {
        }

        private void _eventNotifier_PviServiceDisconnected(object sender, PviApplicationEventArgs e)
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            _serviceWrapper.ReConnectPviService();
        }

        private void _eventNotifier_PviServiceConnected(object sender, PviApplicationEventArgs e)
        {
            _cpuManager = e.CpuManager;
            _variableManager = e.VariableManager;

            _log.Info(e.Message);

            if (_cpuManager == null)
            {
                _log.Error($"_eventNotifier_PviServiceConnected CpuManager is null");
            }
            else
            {
                _cpuManager.LoadCpus();
            }
        }

        private void _eventNotifier_VariableValueChanged(object sender, PviApplicationEventArgs e)
        {
            if (ConfigurationProvider.VerboseVariableLogging)
            {
                _log.Info(e.Message);
            }
        }

        private void _eventNotifier_VariableError(object sender, PviApplicationEventArgs e)
        {
            _log.Info(e.Message);
        }

        private void _eventNotifier_VariableConnected(object sender, PviApplicationEventArgs e)
        {
            _log.Info(e.Message);

        }

        private void _eventNotifier_CpuError(object sender, PviApplicationEventArgs e)
        {
            _log.Info(e.Message);
        }

        private void _eventNotifier_CpuDisconnected(object sender, PviApplicationEventArgs e)
        {
        }

        private void _eventNotifier_CpuConnected(object sender, CpuConnectionArgs e)
        {
            if (e.Cpu != null && e.Cpu.IsConnected && !e.Cpu.HasError)
            {
                _log.Info(e.Message);
                _variableManager.ConnectVariable(e.Cpu.Name);
            }
        }

    }
}
