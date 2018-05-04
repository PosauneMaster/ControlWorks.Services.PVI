using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlWorks.Services.PVI;
using Newtonsoft.Json;

namespace ControlWorks.Services.Messaging
{
    public enum MessageAction
    {
        AddCpu
    }
    public class MessageProcessor
    {
        public void Process(string msg)
        {
            var message = JsonConvert.DeserializeObject<Message>(msg);

            switch (message.Action)
            {
                case MessageAction.AddCpu:
                    var cpuInfo = JsonConvert.DeserializeObject<CpuInfo>(message.Data);





                    break;
                default:
                    break;
            }
        }
    }
}
