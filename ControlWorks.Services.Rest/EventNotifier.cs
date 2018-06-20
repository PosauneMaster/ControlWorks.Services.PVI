using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlWorks.Services.Rest
{
    public interface IEventNotifier
    {
        event EventHandler<ResponseMessageEventArgs> ResponseReceived;
        void OnResponseReceived(object sender, ResponseMessageEventArgs e);

    }

    public class EventNotifier : IEventNotifier
    {
        public event EventHandler<ResponseMessageEventArgs> ResponseReceived;

        public void OnResponseReceived(object sender, ResponseMessageEventArgs e)
        {
            var temp = ResponseReceived;
            temp?.Invoke(sender, e);
        }
    }

    public class ResponseMessageEventArgs : EventArgs
    {
        public ResponseMessage ResponseMessage { get; set; }
    }

    public class ResponseMessage
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
    }
}
