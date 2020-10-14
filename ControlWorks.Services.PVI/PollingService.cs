using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BR.AN.PviServices;
using ControlWorks.Common;
using log4net;
using Exception = System.Exception;

namespace ControlWorks.Services.PVI
{
    public class PollingService
    {
        private Service _service;
        private CancellationToken _cancellationToken;
        private CancellationTokenSource _cts;
        private System.Threading.Tasks.Task _pollingTask;
        private readonly ILog _log = LogManager.GetLogger("ControlWorksLogger");

        public PollingService(Service service)
        {
            _service = service;
            _cts = new CancellationTokenSource();
        }

        public void Start()
        {
            _pollingTask = new System.Threading.Tasks.Task(Poll, _cancellationToken, TaskCreationOptions.LongRunning);
            _pollingTask.Start();
        }

        public void Stop()
        {
            _cts.Cancel();
            _pollingTask.Wait();
        }

        private void Poll()
        {
            CancellationToken token = _cts.Token;
            TimeSpan ts = TimeSpan.FromMilliseconds(ConfigurationProvider.PollingMilliseconds);
            while (!token.WaitHandle.WaitOne(ts))
            {
                try
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }

                    _log.Info("Polling....");
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
