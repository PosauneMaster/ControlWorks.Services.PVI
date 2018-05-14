using System.Windows.Forms;
using BR.AN.PviServices;
using ControlWorks.Services.PVI.Impl;

namespace ControlWorks.Services.PVI.Pvi
{
    public class PviContext : ApplicationContext
    {
        public Service PviService { get; set; }

        public PviContext(IServiceWrapper serviceWrapper)
        {
            serviceWrapper.ConnectPviService();
        }
    }
}
