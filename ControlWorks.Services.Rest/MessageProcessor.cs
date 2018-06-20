using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlWorks.Services.Rest
{
    public interface IMessageProcessor
    {
        void Start();
        void Stop();
        string SendMessage(Message message);
    }
    public class MessageProcessor
    {
        public MessageProcessor()
    }
}
