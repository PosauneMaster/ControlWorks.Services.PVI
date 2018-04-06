using BR.AN.PviServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlWorks.Services.PVI
{
    public interface IEventNotifier
    {
        event EventHandler<PviApplicationEventArgs> PviServiceConnected;
        event EventHandler<PviApplicationEventArgs> PviServiceDisconnected;
        event EventHandler<PviApplicationEventArgs> PviServiceError;

        void OnPviServiceConnected(object sender, PviApplicationEventArgs e);
        void OnPviServiceDisconnected(object sender, PviApplicationEventArgs e);
        void OnPviServiceError(object sender, PviApplicationEventArgs e);
    }
    public class EventNotifier : IEventNotifier
    {
        public event EventHandler<PviApplicationEventArgs> PviServiceConnected;
        public event EventHandler<PviApplicationEventArgs> PviServiceDisconnected;
        public event EventHandler<PviApplicationEventArgs> PviServiceError;


        public void OnPviServiceConnected(object sender, PviApplicationEventArgs e)
        {
            var temp = PviServiceConnected;
            if (temp != null)
            {
                temp(sender, e);
            }
        }
        public void OnPviServiceDisconnected(object sender, PviApplicationEventArgs e)
        {
            var temp = PviServiceDisconnected;
            if (temp != null)
            {
                temp(sender, e);
            }
        }
        public void OnPviServiceError(object sender, PviApplicationEventArgs e)
        {
            var temp = PviServiceError;
            if (temp != null)
            {
                temp(sender, e);
            }
        }
    }

    public class PviApplicationEventArgs : EventArgs
    {
        public Service PviService { get; set; }
        public string Message { get; set; }
    }
}
