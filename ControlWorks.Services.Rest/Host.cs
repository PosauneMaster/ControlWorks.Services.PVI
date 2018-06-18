using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlWorks.Services.Rest
{

    public interface IHost
    {
        void Start();
        void Stop();
    }

    public class Host : IHost
    {
        public void Start()
        {
        }

        public void Stop()
        {
        }

    }
}
