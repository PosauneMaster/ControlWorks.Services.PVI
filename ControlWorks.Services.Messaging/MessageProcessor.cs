using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlWorks.Services.PVI;
using ControlWorks.Services.PVI.Panel;
using ControlWorks.Services.PVI.Pvi;
using Newtonsoft.Json;

namespace ControlWorks.Services.Messaging
{
    public enum MessageAction
    {
        Start,
        Stop,
        IsConnected,
        IsError,
        AddCpu
    }
    public class MessageProcessor
    {
        private IPviAplication _application;

        public MessageProcessor()
        {
        }

        public MessageProcessor(IPviAplication application)
        {
            _application = application;
        }

        public ResponseMessage Process(string msg)
        {
            var message = JsonConvert.DeserializeObject<Message>(msg);

            switch (message.Action)
            {
                case MessageAction.Start:
                    _application.Connect();
                    return new ResponseMessage() {Message = "Connect", IsSuccess = true };
                case MessageAction.Stop:
                    _application.Disconnect();
                    return new ResponseMessage() { Message = "Disconnect", IsSuccess = true };
                case MessageAction.IsConnected:
                    dynamic isConnectedResponse = new ExpandoObject();
                    isConnectedResponse.IsConnected = _application.IsConnected;
                    return new ResponseMessage() { Message = JsonConvert.SerializeObject(isConnectedResponse), IsSuccess = true };
                case MessageAction.IsError:
                    dynamic isErrorResponse = new ExpandoObject();
                    isErrorResponse.IsConnected = _application.IsConnected;
                    return new ResponseMessage() { Message = JsonConvert.SerializeObject(isErrorResponse), IsSuccess = true };
                case MessageAction.AddCpu:
                    var cpuInfo = JsonConvert.DeserializeObject<CpuInfo>(message.Data);
                    _application.AddCpu(cpuInfo);
                    return new ResponseMessage() { Message = "AddCpu", IsSuccess = true };
                default:
                    break;
            }
            return null;
        }
    }
}
