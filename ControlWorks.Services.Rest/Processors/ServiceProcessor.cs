using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ControlWorks.Services.PVI.Pvi;
using ControlWorks.Services.PVI;

namespace ControlWorks.Services.Rest
{
    public interface IServiceProcessor
    {
        Task<ServiceDetail> GetServiceDetails();
    }

    public class ServiceProcessor : IServiceProcessor
    {
        private readonly AutoResetEvent _autoResetEvent = new AutoResetEvent(false);
        private ResponseMessage _responseMessage = null;
        private ServiceDetail _serviceDetail = null;

        IPviApplication _pviApplication;

        public ServiceProcessor(IPviApplication pviApplication)
        {
            _pviApplication = pviApplication;
        }

        //private void Notifier_ResponseReceived(object sender, ResponseMessageEventArgs e)
        //{
        //    _responseMessage = e.ResponseMessage;
        //    if (!_responseMessage.IsError)
        //    {
        //        try
        //        {
        //            _serviceDetail = JsonConvert.DeserializeObject<ServiceDetail>(_responseMessage.Message);
        //        }
        //        catch (Exception exception)
        //        {
        //            Console.WriteLine(exception);
        //        }
        //    }

        //    _autoResetEvent.Set();
        //}

        public async Task<ServiceDetail> GetServiceDetails()
        {
            var details = await Task.Run(() => _pviApplication.ServiceDetails());
            return details;
        }
    }
}
