using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlWorks.Services.Rest
{
    public enum MessageAction
    {
        Unknown,
        Start,
        Stop,
        ServiceDetails,
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

    public class Message
    {
        public Guid Id { get; set; }
        public MessageAction Action { get; set; }
        public string Data { get; set; }
        public VariableRequestMessage VariableRequest { get; set; }
    }

    public class VariableRequestMessage
    {
        public string CpuName { get; set; }
        public string[] VariableNames { get; set; }
    }
}
