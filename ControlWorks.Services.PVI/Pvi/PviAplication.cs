using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.AN.PviServices;
using ControlWorks.Services.PVI.Cpu;
using Task = System.Threading.Tasks.Task;

namespace ControlWorks.Services.PVI
{
    public interface IPviAplication
    {
        void Connect();
        bool IsConnected();
        bool HasError();

    }
    public class PviAplication
    {
        private PviContext _pviContext;
        private Service _service;


        public void Connect()
        {
            var notifier = new EventNotifier();
            notifier.PviServiceConnected += _eventNotifier_PviServiceConnected;
            notifier.PviServiceDisconnected += _eventNotifier_PviServiceDisconnected;
            notifier.PviServiceError += _eventNotifier_PviServiceError;

            _pviContext = new PviContext(notifier);
            Application.Run(_pviContext);
        }

        public bool IsConnected()
        {
            return _service.IsConnected;
        }

        public bool HasError()
        {
            return _service.HasError;
        }

        public async Task AddCpu(CpuInfo info)
        {
            var api = new CpuApi();

            await Task.Run(() =>
            {
                api.Add(info);
                //_pviContext.CpuService.CreateCpu(info.Name, info.IpAddress);
            }).ConfigureAwait(false);
        }


        public void Disconnect()
        {
            _pviContext.Dispose();
        }

        private void _eventNotifier_PviServiceError(object sender, PviApplicationEventArgs e)
        {
        }

        private void _eventNotifier_PviServiceDisconnected(object sender, PviApplicationEventArgs e)
        {
        }

        private void _eventNotifier_PviServiceConnected(object sender, PviApplicationEventArgs e)
        {
        }

    }
}
