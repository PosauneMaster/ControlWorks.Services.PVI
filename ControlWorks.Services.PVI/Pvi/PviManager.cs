using BR.AN.PviServices;
using log4net;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ControlWorks.Services.PVI
{
    internal interface IPviManager
    {
        void ConnectPvi();
    }

    internal class PviManager : IPviManager, IDisposable
    {
        private static ILog _log = LogManager.GetLogger("FileLogger");

        private IEventNotifier _eventNotifier;

        public Service PviService { get; set; }

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
            var pviEventMsg = FormatPviEventMessage("PviService_Error", e);
            _log.Info(pviEventMsg);
        }

        private void PviService_Disconnected(object sender, PviEventArgs e)
        {
            var pviEventMsg = FormatPviEventMessage("PviService_Disconnected", e);
            _log.Info(pviEventMsg);

        }

        private void PviService_Connected(object sender, PviEventArgs e)
        {
            var service = sender as Service;
            PviService = service;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Service));
            using (var textWriter = new Utf8StringWriter())
            {
                xmlSerializer.Serialize(textWriter, this);
                var result = textWriter.ToString();

            }




            var pviEventMsg = FormatPviEventMessage("PviService._service_Connected", e);
            _eventNotifier.OnPviServiceConnected(sender, new PviApplicationEventArgs() { Message = pviEventMsg, PviService = service });
            _log.Info(pviEventMsg);
        }

        private string FormatPviEventMessage(string message, PviEventArgs e)
        {
            return String.Format("{0}; Action={1}, Address={2}, Error Code={3}, Error Text={4}, Name={5} ",
                message, e.Action, e.Address, e.ErrorCode, e.ErrorText, e.Name);
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
