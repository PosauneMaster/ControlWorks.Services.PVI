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
        AddCpu,
        UpdateCpu,
        GetCpuByName,
        GetCpuByIp,
        DeleteCpuByName,
        DeleteCpuByIp,
        GetAllCpuData

    }
    public class MessageProcessor
    {
        private readonly IPviAplication _application;

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
                case MessageAction.UpdateCpu:
                    var cpuUpdateInfo = JsonConvert.DeserializeObject<CpuInfo>(message.Data);
                    _application.UpdateCpu(cpuUpdateInfo);
                    return new ResponseMessage() { Message = "UpdateCpu", IsSuccess = true };
                case MessageAction.GetCpuByName:
                    _application.GetCpuByName(message.Data);
                    return new ResponseMessage() { Message = "GetCpuByName", IsSuccess = true };
                case MessageAction.GetCpuByIp:
                    _application.GetCpuByIp(message.Data);
                    return new ResponseMessage() { Message = "GetCpuByIp", IsSuccess = true };
                case MessageAction.DeleteCpuByName:
                    _application.DeleteCpuByName(message.Data);
                    return new ResponseMessage() { Message = "DeleteCpuByName", IsSuccess = true };
                case MessageAction.DeleteCpuByIp:
                    _application.DeleteCpuByIp(message.Data);
                    return new ResponseMessage() { Message = "DeleteCpuByIp", IsSuccess = true };
                case MessageAction.GetAllCpuData:
                    var requestData = JsonConvert.DeserializeObject<VariableRequestMessage>(message.Data);
                    _application.GetCpuDataAsync(requestData.CpuName, requestData.VariableNames);
                    return new ResponseMessage() { Message = "GetAllCpuData", IsSuccess = true };


                default:
                    break;
            }
            return null;
        }
    }
}
