using System;

namespace ControlWorks.Services.Messaging
{
    public class Message
    {
        public Guid Id { get; set; }
        public Type Type { get; set; }
        public MessageAction Action { get; set; }
        public string Data { get; set; }
    }

    public class VariableRequestMessage
    {
        public string CpuName { get; set; }
        public string[] VariableNames { get; set; }
    }
}
