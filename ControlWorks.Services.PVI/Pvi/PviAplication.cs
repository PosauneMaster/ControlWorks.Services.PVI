using System.Windows.Forms;
using BR.AN.PviServices;
using ControlWorks.Services.PVI.Panel;
using ControlWorks.Services.PVI.Variables;
using log4net;
using Task = System.Threading.Tasks.Task;

namespace ControlWorks.Services.PVI.Pvi
{
    public interface IPviAplication
    {
        void Connect();
        bool IsConnected();
        bool HasError();

    }
    public class PviAplication
    {
        private readonly ILog _log = LogManager.GetLogger("FileLogger");

        private PviContext _pviContext;
        private Service _service;
        private ICpuManager _cpuManager;
        private IVariableManager _variableManager;

        private readonly IEventNotifier _eventNotifier;
        private readonly IServiceWrapper _serviceWrapper;

        public PviAplication()
        {
            _eventNotifier = new EventNotifier();
            _serviceWrapper = new ServiceWrapper(_eventNotifier);
        }


        public void Connect()
        {
            _eventNotifier.PviServiceConnected += _eventNotifier_PviServiceConnected;
            _eventNotifier.PviServiceDisconnected += _eventNotifier_PviServiceDisconnected;
            _eventNotifier.PviServiceError += _eventNotifier_PviServiceError;
            _eventNotifier.CpuConnected += _eventNotifier_CpuConnected;
            _eventNotifier.CpuDisconnected += _eventNotifier_CpuDisconnected;
            _eventNotifier.CpuError += _eventNotifier_CpuError;
            _eventNotifier.VariableConnected += _eventNotifier_VariableConnected;
            _eventNotifier.VariableError += _eventNotifier_VariableError;
            _eventNotifier.VariableValueChanged += _eventNotifier_VariableValueChanged;

            _pviContext = new PviContext(_serviceWrapper);
            Application.Run(_pviContext);
        }

        private void CreateCpus()
        {

        }


        private void _eventNotifier_PviServiceError(object sender, PviApplicationEventArgs e)
        {
        }

        private void _eventNotifier_PviServiceDisconnected(object sender, PviApplicationEventArgs e)
        {
        }

        private void _eventNotifier_PviServiceConnected(object sender, PviApplicationEventArgs e)
        {
            _log.Info(e.Message);
            CreateCpus();
        }

        private void _eventNotifier_VariableValueChanged(object sender, PviApplicationEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void _eventNotifier_VariableError(object sender, PviApplicationEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void _eventNotifier_VariableConnected(object sender, PviApplicationEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void _eventNotifier_CpuError(object sender, PviApplicationEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void _eventNotifier_CpuDisconnected(object sender, PviApplicationEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void _eventNotifier_CpuConnected(object sender, PviApplicationEventArgs e)
        {
            throw new System.NotImplementedException();
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


    }
}
