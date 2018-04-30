using BR.AN.PviServices;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ControlWorks.Services.PVI
{
    public class PviContext : ApplicationContext
    {
        private readonly PviManager _pviManager;

        public Service PviService { get; set; }

        private readonly IEventNotifier _eventNotifier;

        public PviContext()
        {
            _pviManager = new PviManager(_eventNotifier);
            _pviManager.ConnectPvi();

        }

        public PviContext(IEventNotifier eventNotifier)
        {
            _eventNotifier = eventNotifier;
            _eventNotifier.PviServiceConnected += _eventNotifier_PviServiceConnected;
            _eventNotifier.PviServiceDisconnected += _eventNotifier_PviServiceDisconnected;
            _eventNotifier.PviServiceError += _eventNotifier_PviServiceError;
            _pviManager = new PviManager(_eventNotifier);
            _pviManager.ConnectPvi();

        }

        public void DisconnectPvi()
        {
            if (_pviManager != null)
            {
                _pviManager.Dispose();
            }
        }

        private void _eventNotifier_PviServiceError(object sender, PviApplicationEventArgs e)
        {
        }

        private void _eventNotifier_PviServiceDisconnected(object sender, PviApplicationEventArgs e)
        {
        }

        private void _eventNotifier_PviServiceConnected(object sender, PviApplicationEventArgs e)
        {
            PviService = e.PviService;
        }
    }
}
