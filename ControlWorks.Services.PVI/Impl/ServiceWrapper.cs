using System;
using BR.AN.PviServices;
using ControlWorks.Services.PVI.Panel;
using ControlWorks.Services.PVI.Variables;

namespace ControlWorks.Services.PVI.Impl
{
    public interface IServiceWrapper
    {
        void ConnectPviService();
        void DisconnectPviService();
        bool IsConnected { get; }
        bool HasError { get; }
        ServiceDetail ServiceDetails();

    }

    public class ServiceWrapper : IServiceWrapper
    {
        private Service _service;
        private PollingService _pollingService;
        private readonly IEventNotifier _eventNotifier;
        private DateTime _connectionTime;

        public bool IsConnected => _service.IsConnected;
        public bool HasError => _service.HasError;
        public ServiceWrapper(IEventNotifier eventNotifier)
        {
            _eventNotifier = eventNotifier;
        }

        public void ConnectPviService()
        {
            _connectionTime = DateTime.Now;
            _service = new Service(Guid.NewGuid().ToString());

            _service.Connected += _service_Connected;
            _service.Disconnected += _service_Disconnected;
            _service.Error += _service_Error;

            _service.Connect();
        }

        public void DisconnectPviService()
        {
            _service.Disconnect();
        }


        public ServiceDetail ServiceDetails()
        {
            return new ServiceDetail()
            {
                Name = _service.Name,
                IsConnected = _service.IsConnected,
                Cpus = _service.Cpus.Count,
                ConnectTime = _connectionTime,
                License = _service.LicenceInfo.ToString()
            };
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
            _pollingService.Stop();
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

            var fw = new FileWrapper();
            var cpuManager = new CpuManager(cpuWrapper);

            var variableInfo = new VariableInfoCollection(fw);

            var variableManager = new VariableManager(variableWrapper, variableInfo);

            var pviEventMsg = Utils.FormatPviEventMessage($"ServiceWrapper._service_Connected; ServiceName={serviceName}", e);
            _eventNotifier.OnPviServiceConnected(sender, new PviApplicationEventArgs()
            {
                Message = pviEventMsg,
                ServiceWrapper = this,
                CpuManager = cpuManager,
                CpuWrapper = cpuWrapper,
                VariableManager = variableManager,
                VariableWrapper = variableWrapper
            });

            _pollingService = new PollingService(_service, cpuWrapper);
            _pollingService.Start();
        }
    }
}
