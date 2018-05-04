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

        public PviContext(IEventNotifier eventNotifier)
        {
            _pviManager = new PviManager(eventNotifier);
            _pviManager.ConnectPvi();

        }
    }
}
