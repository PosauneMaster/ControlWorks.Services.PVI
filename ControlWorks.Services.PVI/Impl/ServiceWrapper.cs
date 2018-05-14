using System;
using BR.AN.PviServices;

namespace ControlWorks.Services.PVI.Impl
{
    public interface IServiceWrapper
    {
        void ConnectPviService();
        bool IsConnected { get; }
        bool HasError { get; }
    }

    public class ServiceWrapper : IServiceWrapper
    {
        private Service _service;
        private readonly IEventNotifier _eventNotifier;

        public bool IsConnected => _service.IsConnected;
        public bool HasError => _service.HasError;

        public ServiceWrapper(IEventNotifier eventNotifier)
        {
            _eventNotifier = eventNotifier;
        }

        public void ConnectPviService()
        {
            _service = new Service(Guid.NewGuid().ToString());

            _service.Connected += _service_Connected;
            _service.Disconnected += _service_Disconnected;
            _service.Error += _service_Error;
        }

        private void _service_Error(object sender, PviEventArgs e)
        {
            var pviEventMsg = Utils.FormatPviEventMessage("ServiceWrapper._service_Error", e);
            _eventNotifier.OnPviServiceError(sender, new PviApplicationEventArgs() { Message = pviEventMsg });
        }

        private void _service_Disconnected(object sender, PviEventArgs e)
        {
            var pviEventMsg = Utils.FormatPviEventMessage("ServiceWrapper._service_Disconnected", e);
            _eventNotifier.OnPviServiceDisconnected(sender, new PviApplicationEventArgs() { Message = pviEventMsg });
        }

        private void _service_Connected(object sender, PviEventArgs e)
        {
            string serviceName = String.Empty;
            if (sender is Service service)
            {
                serviceName = service.FullName;
            }

            var cpuWrapper = new CpuWrapper(_service, _eventNotifier);
            var variableWrapper = new VariableWrapper(_service, _eventNotifier);

            var pviEventMsg = Utils.FormatPviEventMessage($"ServiceWrapper._service_Connected; ServiceName={serviceName}", e);
            _eventNotifier.OnPviServiceConnected(sender, new PviApplicationEventArgs()
            {
                Message = pviEventMsg,
                ServiceWrapper = this,
                CpuWrapper = cpuWrapper,
                VariableWrapper = variableWrapper
            });
        }

    }
}
