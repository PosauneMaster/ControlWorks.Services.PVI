﻿using System.Windows.Forms;
using BR.AN.PviServices;

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