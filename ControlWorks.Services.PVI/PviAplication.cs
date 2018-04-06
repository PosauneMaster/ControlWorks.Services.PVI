using BR.AN.PviServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlWorks.Services.PVI
{
    public interface IPviAplication
    {
        void Connect();
        bool IsConnected();
        bool HasError();

    }
    public class PviAplication
    {
        private PviContext _pviContext;
        private Service _service;


        public void Connect()
        {
            var notifier = new EventNotifier();
            notifier.PviServiceConnected += _eventNotifier_PviServiceConnected;
            notifier.PviServiceDisconnected += _eventNotifier_PviServiceDisconnected;
            notifier.PviServiceError += _eventNotifier_PviServiceError;

            _pviContext = new PviContext();
            Application.Run(_pviContext);
        }

        public bool IsConnected()
        {
            return _service.IsConnected;
        }

        public bool HasError()
        {
            return _service.HasError;
        }


        public void Disconnect()
        {
            _pviContext.DisconnectPvi();
            _pviContext.Dispose();
        }

        private void _eventNotifier_PviServiceError(object sender, PviApplicationEventArgs e)
        {
        }

        private void _eventNotifier_PviServiceDisconnected(object sender, PviApplicationEventArgs e)
        {
        }

        private void _eventNotifier_PviServiceConnected(object sender, PviApplicationEventArgs e)
        {
        }

    }
}
