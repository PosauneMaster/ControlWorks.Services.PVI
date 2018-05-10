using BR.AN.PviServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlWorks.Services.PVI.Panel;
using ControlWorks.Services.PVI.Variables;

namespace ControlWorks.Services.PVI
{
    public interface IEventNotifier
    {
        event EventHandler<PviApplicationEventArgs> PviServiceConnected;
        event EventHandler<PviApplicationEventArgs> PviServiceDisconnected;
        event EventHandler<PviApplicationEventArgs> PviServiceError;
        event EventHandler<PviApplicationEventArgs> CpuConnected;
        event EventHandler<PviApplicationEventArgs> CpuDisconnected;
        event EventHandler<PviApplicationEventArgs> CpuError;
        event EventHandler<PviApplicationEventArgs> VariableConnected;
        event EventHandler<PviApplicationEventArgs> VariableError;
        event EventHandler<PviApplicationEventArgs> VariableValueChanged;

        void OnPviServiceConnected(object sender, PviApplicationEventArgs e);
        void OnPviServiceDisconnected(object sender, PviApplicationEventArgs e);
        void OnPviServiceError(object sender, PviApplicationEventArgs e);
        void OnCpuConnected(object sender, PviApplicationEventArgs e);
        void OnCpuDisconnected(object sender, PviApplicationEventArgs e);
        void OnCpuError(object sender, PviApplicationEventArgs e);
        void OnVariableConnected(object sender, PviApplicationEventArgs e);
        void OnVariableError(object sender, PviApplicationEventArgs e);
        void OnVariableValueChanged(object sender, PviApplicationEventArgs e);


    }
    public class EventNotifier : IEventNotifier
    {
        public event EventHandler<PviApplicationEventArgs> PviServiceConnected;
        public event EventHandler<PviApplicationEventArgs> PviServiceDisconnected;
        public event EventHandler<PviApplicationEventArgs> PviServiceError;
        public event EventHandler<PviApplicationEventArgs> CpuConnected;
        public event EventHandler<PviApplicationEventArgs> CpuDisconnected;
        public event EventHandler<PviApplicationEventArgs> CpuError;
        public event EventHandler<PviApplicationEventArgs> VariableConnected;
        public event EventHandler<PviApplicationEventArgs> VariableError;
        public event EventHandler<PviApplicationEventArgs> VariableValueChanged;

        public event EventHandler<EventArgs> PviManagerInitialized;
        public event EventHandler<EventArgs> CpuManangerInitialized;
        public event EventHandler<EventArgs> VariableManagerInitialized;


        public void OnPviServiceConnected(object sender, PviApplicationEventArgs e)
        {
            var temp = PviServiceConnected;
            temp?.Invoke(sender, e);
        }
        public void OnPviServiceDisconnected(object sender, PviApplicationEventArgs e)
        {
            var temp = PviServiceDisconnected;
            temp?.Invoke(sender, e);
        }
        public void OnPviServiceError(object sender, PviApplicationEventArgs e)
        {
            var temp = PviServiceError;
            temp?.Invoke(sender, e);
        }
        public void OnCpuConnected(object sender, PviApplicationEventArgs e)
        {
            var temp = CpuConnected;
            temp?.Invoke(sender, e);
        }
        public void OnCpuDisconnected(object sender, PviApplicationEventArgs e)
        {
            var temp = CpuDisconnected;
            temp?.Invoke(sender, e);
        }
        public void OnCpuError(object sender, PviApplicationEventArgs e)
        {
            var temp = CpuError;
            temp?.Invoke(sender, e);
        }

        public void OnVariableConnected(object sender, PviApplicationEventArgs e)
        {
            var temp = VariableConnected;
            temp?.Invoke(sender, e);
        }

        public void OnVariableError(object sender, PviApplicationEventArgs e)
        {
            var temp = VariableError;
            temp?.Invoke(sender, e);
        }

        public void OnVariableValueChanged(object sender, PviApplicationEventArgs e)
        {
            var temp = VariableValueChanged;
            temp?.Invoke(sender, e);
        }

        public void OnPviManagerInitialized(object sender, EventArgs e)
        {
            var temp = PviManagerInitialized;
            temp?.Invoke(sender, e);
        }

        public void OnCpuManangerInitialized(object sender, EventArgs e)
        {
            var temp = CpuManangerInitialized;
            temp?.Invoke(sender, e);
        }
        public void OnVariableManagerInitialized(object sender, EventArgs e)
        {
            var temp = VariableManagerInitialized;
            temp?.Invoke(sender, e);
        }
    }

    public class PviApplicationEventArgs : EventArgs
    {
        public IPviManager PviManager { get; set; }
        public ICpuManager CpuManager { get; set; }
        public IVariableManager VariableManager { get; set; }
        public string Message { get; set; }
    }
}
