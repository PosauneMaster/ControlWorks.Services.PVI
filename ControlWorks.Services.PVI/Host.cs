using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlWorks.Services.PVI.Pvi;

namespace ControlWorks.Services.PVI
{
    public interface IHost
    {
        void Start();
        void Stop();
    }

    public class Host : IHost
    {
        private PviAplication _application;
        public void Start()
        {
            _application = new PviAplication();
            _application.Connect();
        }

        public void Stop()
        {
            _application.Disconnect();
        }

    }
}
