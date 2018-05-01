using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlWorks.Services.Messaging
{
    public class Message
    {
        public Guid Id { get; set; }
        public Type Type { get; set; }
        public string Action { get; set; }
        public string Data { get; set; }
    }
}
