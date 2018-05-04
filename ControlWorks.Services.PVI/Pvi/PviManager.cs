using BR.AN.PviServices;
using log4net;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using ControlWorks.Services.PVI.Cpu;

namespace ControlWorks.Services.PVI
{
    internal interface IPviManager
    {
        void ConnectPvi();
    }

    internal class PviManager : IPviManager, IDisposable
    {
        private readonly ILog _log = LogManager.GetLogger("FileLogger");

        private readonly IEventNotifier _eventNotifier;

        public Service PviService { get; set; }
        public ICpuManager CpuManager { get; set; }

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
            CpuManager = new CpuManager(service);

            try
            {
                collection.Open(settingFile);
                CpuManager = new CpuManager(PviService);
                CpuManager.CpusLoaded += CpuManager_CpusLoaded;
                CpuManager.LoadCpuCollection(collection.GetAll());
            }
            catch (System.Exception ex)
            {
                _log.Error("Error Loading Cpu Settings", ex);
            }

            var pviEventMsg = Utils.FormatPviEventMessage("PviService._service_Connected", e);
            _eventNotifier.OnPviServiceConnected(sender, new PviApplicationEventArgs() { Message = pviEventMsg, PviService = service });
            _log.Info(pviEventMsg);
        }

        private void CpuManager_CpusLoaded(object sender, CpusLoadedEventArgs e)
        {
            throw new NotImplementedException();
        }

        #region IDisposable

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
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
            disposed = true;
        }

        #endregion

    }

    class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }

}
