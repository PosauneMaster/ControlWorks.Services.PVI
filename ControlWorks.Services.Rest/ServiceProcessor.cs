using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ControlWorks.Services.Rest
{
    public interface IServiceProcessor
    {
        ServiceDetail GetServiceDetails();
    }

    public class ServiceProcessor : IServiceProcessor
    {
        private readonly AutoResetEvent _autoResetEvent = new AutoResetEvent(false);
        private ResponseMessage _responseMessage = null;
        private ServiceDetail _serviceDetail = null;

        public ServiceProcessor(IEventNotifier notifier)
        {
            notifier.ResponseReceived += Notifier_ResponseReceived;
        }

        private void Notifier_ResponseReceived(object sender, ResponseMessageEventArgs e)
        {
            _responseMessage = e.ResponseMessage;
            if (!_responseMessage.IsError)
            {
                try
                {
                    _serviceDetail = JsonConvert.DeserializeObject<ServiceDetail>(_responseMessage.Message);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }

            _autoResetEvent.Set();
        }

        public ServiceDetail GetServiceDetails()
        {
            _autoResetEvent.WaitOne();
            return _serviceDetail;
        }
    }
}
