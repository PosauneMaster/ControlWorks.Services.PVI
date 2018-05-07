﻿using BR.AN.PviServices;
using ControlWorks.Services.PVI.Panel;
using log4net;
using System;
using System.IO;
using System.Text;

namespace ControlWorks.Services.PVI
{
    public interface IPviManager
    {
        void ConnectPvi();
    }

    public class PviManager : IPviManager, IDisposable
    {
        private readonly ILog _log = LogManager.GetLogger("FileLogger");

        private readonly IEventNotifier _eventNotifier;

        public Service PviService { get; set; }
        private ICpuManager _cpuManager;
        private IVariableManager _variableManager;

        public PviManager() { }
        public PviManager(IEventNotifier eventNotifier)
        {
            _eventNotifier = eventNotifier;
        }

        public void ConnectPvi()
        {
            var serviceName = Guid.NewGuid().ToString();
            var service = new Service(serviceName);

            service.Connected += PviService_Connected;
            service.Disconnected += PviService_Disconnected;
            service.Error += PviService_Error;
            service.Connect();
        }

        private void PviService_Error(object sender, PviEventArgs e)
        {
            var pviEventMsg = Utils.FormatPviEventMessage("PviService_Error", e);
            _eventNotifier.OnPviServiceError(sender, new PviApplicationEventArgs() { Message = pviEventMsg });

            _log.Info(pviEventMsg);
        }

        private void PviService_Disconnected(object sender, PviEventArgs e)
        {
            var pviEventMsg = Utils.FormatPviEventMessage("PviService_Disconnected", e);
            _eventNotifier.OnPviServiceDisconnected(sender, new PviApplicationEventArgs() { Message = pviEventMsg });
            _log.Info(pviEventMsg);
        }

        private void PviService_Connected(object sender, PviEventArgs e)
        {
            var service = sender as Service;
            PviService = service;


            var settingFile = ConfigurationProvider.AppSettings.CpuSettingsFile;
            var collection = new CpuInfoCollection();

            try
            {
                _cpuManager = new CpuManager(PviService, _eventNotifier);
                //_variableManager = new VariableManager();

                collection.Open(settingFile);
                _cpuManager.LoadCpuCollection(collection.GetAll());
            }
            catch (System.Exception ex)
            {
                _log.Error("Error Loading Cpu Settings", ex);
            }

            var pviEventMsg = Utils.FormatPviEventMessage("PviService._service_Connected", e);
            _eventNotifier.OnPviServiceConnected(sender, new PviApplicationEventArgs() { Message = pviEventMsg });
            _log.Info(pviEventMsg);
        }

        #region IDisposable

        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (PviService != null)
                    {
                        if (PviService.IsConnected)
                        {
                            PviService.Disconnect();
                        }
                        PviService.Dispose();
                    }
                }
            }
            _disposed = true;
        }

        #endregion

    }

    class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }

}
