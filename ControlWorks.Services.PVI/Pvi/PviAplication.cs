using System.Threading.Tasks;
using System.Windows.Forms;
using BR.AN.PviServices;
using ControlWorks.Services.PVI.Impl;
using ControlWorks.Services.PVI.Panel;
using ControlWorks.Services.PVI.Variables;
using log4net;
using Task = System.Threading.Tasks.Task;

namespace ControlWorks.Services.PVI.Pvi
{
    public interface IPviAplication
    {
        void Connect();
        void Disconnect();
        bool IsConnected { get; }
        bool HasError { get; }
        Task AddCpu(CpuInfo info);
        Task UpdateCpu(CpuInfo info);
        Task<CpuDetailResponse> GetCpuByName(string name);
        Task DeleteCpuByName(string name);
        Task DeleteCpuByIp(string ip);


    }
    public class PviAplication : IPviAplication
    {
        private readonly ILog _log = LogManager.GetLogger("FileLogger");

        private PviContext _pviContext;
        private ICpuManager _cpuManager;
        private IVariableManager _variableManager;

        private readonly IEventNotifier _eventNotifier;
        private readonly IServiceWrapper _serviceWrapper;

        public bool IsConnected => _serviceWrapper.IsConnected;
        public bool HasError => _serviceWrapper.HasError;

        public PviAplication()
        {
            _eventNotifier = new EventNotifier();
            _serviceWrapper = new ServiceWrapper(_eventNotifier);
        }

        #region public interface

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

            _eventNotifier.CpuManangerInitialized += _eventNotifier_CpuManangerInitialized;


            _pviContext = new PviContext(_serviceWrapper);
            Application.Run(_pviContext);
        }

        private void _eventNotifier_CpuManangerInitialized(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        public void Disconnect()
        {
            _pviContext.Dispose();
        }

        public async Task AddCpu(CpuInfo info)
        {
            await Task.Run(() => _cpuManager.Add(info));
        }

        public async Task UpdateCpu(CpuInfo info)
        {
            await Task.Run(() => _cpuManager.Update(info));
        }

        public async Task<CpuDetailResponse> GetCpuByName(string name)
        {
            return await Task.Run(() => _cpuManager.FindCpuByName(name));
        }

        public async Task DeleteCpuByName(string name)
        {
            await Task.Run(() => _cpuManager.DisconnectCpuByName(name));
        }

        public async  Task DeleteCpuByIp(string ip)
        {
            await Task.Run(() => _cpuManager.DisconnectCpuByIp(ip));
        }

        #endregion


        private void _eventNotifier_PviServiceError(object sender, PviApplicationEventArgs e)
        {
        }

        private void _eventNotifier_PviServiceDisconnected(object sender, PviApplicationEventArgs e)
        {
        }

        private void _eventNotifier_PviServiceConnected(object sender, PviApplicationEventArgs e)
        {
            _cpuManager = e.CpuManager;
            _variableManager = e.VariableManager;

            _log.Info(e.Message);

            if (_cpuManager == null)
            {
                _log.Error($"_eventNotifier_PviServiceConnected CpuManager is null");
            }
            else
            {
                _cpuManager.LoadCpus();
            }
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

    }
}
