﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlWorks.Services.ConfigurationProvider;
using ControlWorks.Services.PVI.Pvi;
using NetMQ;
using NetMQ.Sockets;

namespace ControlWorks.Services.Messaging
{
    public class Service
    {
        private MessageProcessor _msgProc;

        public Service()
        {
        }


        public void StartResponseServer()
        {
            var pviApplication = new PviAplication();
            _msgProc = new MessageProcessor(pviApplication);
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
