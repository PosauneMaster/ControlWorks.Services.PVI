using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlWorks.Services.ConfigurationProvider;
using NetMQ;
using NetMQ.Sockets;

namespace ControlWorks.Services.Messaging
{
    public class Service
    {
        public void StartResponseServer()
        {
            Task.Factory.StartNew(() => ResponseServer(AppSettings.Port), TaskCreationOptions.LongRunning);
        }

        private void ResponseServer(string port)
        {
            using (var server = new ResponseSocket($@"tcp://*:{port}"))
            using (var poller = new NetMQPoller() { server })
            {
                server.ReceiveReady += (s, a) =>
                {
                    var message = a.Socket.ReceiveFrameString();
                    Console.WriteLine("Received {0}", message);
                    a.Socket.SendFrame($"Hello {message}");
                };

                poller.Run();
            }
        }
    }
}
