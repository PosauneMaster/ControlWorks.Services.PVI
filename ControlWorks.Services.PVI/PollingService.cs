using System;
using System.Threading;
using System.Threading.Tasks;

using BR.AN.PviServices;

using ControlWorks.Common;
using ControlWorks.Services.PVI.Impl;
using log4net;

using Exception = System.Exception;

namespace ControlWorks.Services.PVI
{
    public class PollingService
    {
        private Service _service;
        ICpuWrapper _cpuWrapper;
        private CancellationTokenSource _cts;
        private System.Threading.Tasks.Task _pollingTask;
        private readonly ILog _log = LogManager.GetLogger("ControlWorksLogger");

        public PollingService(Service service, ICpuWrapper cpuWrapper)
        {
            _service = service;
            _cpuWrapper = cpuWrapper;
            _cts = new CancellationTokenSource();
        }

        public void Start()
        {
            _pollingTask = new System.Threading.Tasks.Task(Poll, _cts.Token, TaskCreationOptions.LongRunning);
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
            TimeSpan interval = TimeSpan.Zero;
            while (!token.WaitHandle.WaitOne(interval))
            {
                try
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }

                    _log.Info($"Polling at {DateTime.Now:HHmmss}....");
                }
                catch (Exception ex)
                {
                    _log.Error($"PollingService.Poll", ex);
                }

                interval = TimeSpan.FromMilliseconds(ConfigurationProvider.PollingMilliseconds);
            }
        }
    }
}
