using ControlWorks.Services.ConfigurationProvider;
using ControlWorks.Services.PVI;
using ControlWorks.Services.PVI.Panel;
using ControlWorks.Services.PVI.Pvi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace ControlWorks.Services.Messaging
{
    public enum MessageAction
    {
        Unknown,
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
        GetAllCpuData,
        ReadVariables,
        ReadAllVariables,
        AddVariables,
        RemoveVariables

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

        private ResponseMessage ProcessAction(Action action, Message message)
        {
            ResponseMessage response;

            try
            {
                action();
                response = BuildResponse(message.Id, message.Action.ToString(), true);
            }
            catch (Exception e)
            {
                ErrorResponse[] errors = { new ErrorResponse() { Error = e.Message } };
                response = BuildResponse(message.Id, message.Action.ToString(), false, errors);
            }
            return response;
        }

        private ResponseMessage ProcessAction(Func<bool> action, Message message, MessageAction messageAction)
        {
            ResponseMessage response;

            try
            {
                dynamic isConnectedResponse = new ExpandoObject();
                var dict = (IDictionary<string, object>) isConnectedResponse;
                dict.Add(messageAction.ToString(), action().ToString());
                response = BuildResponse(message.Id, JsonConvert.SerializeObject(isConnectedResponse), true);
            }
            catch (Exception e)
            {
                ErrorResponse[] errors = { new ErrorResponse() { Error = e.Message } };
                response = BuildResponse(message.Id, message.Action.ToString(), false, errors);
            }
            return response;
        }

        private ResponseMessage ProcessAction<T>(Action<T> action, Message message, T data)
        {
            ResponseMessage response;

            try
            {
                action(data);
                response = BuildResponse(message.Id, message.Action.ToString(), true);

            }
            catch (Exception e)
            {
                ErrorResponse[] errors = { new ErrorResponse() { Error = e.Message } };
                response = BuildResponse(message.Id, message.Action.ToString(), false, errors);
            }
            return response;
        }

        private ResponseMessage ProcessAction<T1, T2>(Action<T1, T2> action, T message)
        {
            ResponseMessage response;

            message.
            try
            {
                action( data);
                response = BuildResponse(message.Id, message.Action.ToString(), true);

            }
            catch (Exception e)
            {
                ErrorResponse[] errors = { new ErrorResponse() { Error = e.Message } };
                response = BuildResponse(message.Id, message.Action.ToString(), false, errors);
            }
            return response;
        }





        private ResponseMessage ProcessAction<T>(Func<string, T> action, Message message, string data)
        {
            ResponseMessage response;

            try
            {
                var result = action(data);
                response = BuildResponse(message.Id, JsonConvert.SerializeObject(result), true);

            }
            catch (Exception e)
            {
                ErrorResponse[] errors = { new ErrorResponse() { Error = e.Message } };
                response = BuildResponse(message.Id, message.Action.ToString(), false, errors);
            }
            return response;
        }

        private ResponseMessage Process(Message message)
        {
            switch (message.Action)
            {
                case MessageAction.Start:

                    return ProcessAction(_application.Connect, message);

                case MessageAction.Stop:

                    return ProcessAction(_application.Disconnect, message);

                case MessageAction.IsConnected:

                    return ProcessAction(_application.GetIsConnected, message, MessageAction.IsConnected);

                case MessageAction.IsError:

                    return ProcessAction(_application.GetHasError, message, MessageAction.IsError);

                case MessageAction.AddCpu:

                    var cpuInfo = JsonConvert.DeserializeObject<CpuInfo>(message.Data);
                    return ProcessAction(_application.AddCpu, message, cpuInfo);

                case MessageAction.UpdateCpu:

                    var cpuInfoUpdate = JsonConvert.DeserializeObject<CpuInfo>(message.Data);
                    return ProcessAction(_application.UpdateCpu, message, cpuInfoUpdate);

                case MessageAction.GetCpuByName:

                    return ProcessAction(_application.GetCpuByName, message, message.Data);

                case MessageAction.GetCpuByIp:

                    return ProcessAction(_application.GetCpuByIp, message, message.Data);

                case MessageAction.DeleteCpuByName:

                    Action<string> deleteByNameAction = _application.DeleteCpuByName;
                    return ProcessAction(deleteByNameAction, message, message.Data);

                case MessageAction.DeleteCpuByIp:

                    Action<string> deleteByIpAction = _application.DeleteCpuByIp;
                    return ProcessAction(deleteByIpAction, message, message.Data);

                case MessageAction.GetAllCpuData:

                    ResponseMessage getAllCpuDataResponse;

                    try
                    {
                        var result = _application.GetCpuData();

                        getAllCpuDataResponse = BuildResponse(message.Id, JsonConvert.SerializeObject(result), true);

                    }
                    catch (Exception e)
                    {
                        ErrorResponse[] errors = { new ErrorResponse() { Error = e.Message } };
                        getAllCpuDataResponse = BuildResponse(message.Id, message.Action.ToString(), false, errors);
                    }
                    return getAllCpuDataResponse;

                case MessageAction.ReadVariables:

                    ResponseMessage readVariablesRsponse;

                    try
                    {
                        var variableResponse = _application.ReadVariables(message.VariableRequest.CpuName, message.VariableRequest.VariableNames);

                        readVariablesRsponse = BuildResponse(message.Id, JsonConvert.SerializeObject(variableResponse), true);

                    }
                    catch (Exception e)
                    {
                        ErrorResponse[] errors = { new ErrorResponse() { Error = e.Message } };
                        readVariablesRsponse = BuildResponse(message.Id, message.Action.ToString(), false, errors);
                    }

                    return readVariablesRsponse;

                case MessageAction.ReadAllVariables:

                    return ProcessAction(_application.ReadAllVariables, message, message.Data);

                case MessageAction.AddVariables:

                default:
                    ErrorResponse[] error = { new ErrorResponse() { Error = "Unknown message action" } };
                    return new ResponseMessage()
                    {
                        Id = message.Id,
                        IsSuccess = false,
                        Message = $"Unknown message details {message}",
                        Errors = error
                    };
            }
        }

        private ResponseMessage BuildResponse(Guid guid, string message, Boolean success, ErrorResponse[] errors = null)
        {
            return new ResponseMessage
            {
                Id = guid,
                IsSuccess = success,
                Message = message,
                Errors = errors
            };

        }

        public ResponseMessage Process(string msg)
        {
            var message = JsonConvert.DeserializeObject<Message>(msg);

            var response = Task.Run(() => Process(message));
            var ts = TimeSpan.FromMilliseconds(AppSettings.MessageTimeout);

#if !DEBUG
            
            if (!response.Wait(ts))
            {
                ErrorResponse[] error = { new ErrorResponse() { Error = "Timeout Exception" }};

                return new ResponseMessage()
                {
                    Id = message.Id,
                    IsSuccess = false,
                    Message = $"A timeout occurred waiting for a response to message {msg}",
                    Errors = error
                };
            }
#endif


            return response.Result;
        }
    }
}
