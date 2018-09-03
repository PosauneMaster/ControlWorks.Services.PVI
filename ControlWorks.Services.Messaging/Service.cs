using ControlWorks.Services.PVI.Pvi;
using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;
using System.Threading.Tasks;
using ControlWorks.Common;

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
            Task.Factory.StartNew(() => ResponseServer(ConfigurationProvider.Port), TaskCreationOptions.LongRunning);
        }

        private void ResponseServer(string port)
        {
            using (var server = new ResponseSocket($@"tcp://*:{port}"))
            using (var poller = new NetMQPoller() { server })
            {
                
                server.ReceiveReady += (s, a) =>
                {
                    var message = a.Socket.ReceiveFrameString();

                    var response =  _msgProc.Process(message);

                    var responseJson = JsonConvert.SerializeObject(response);

                    a.Socket.SendFrame(responseJson);
                };

                poller.RunAsync();
            }
        }
    }
}
