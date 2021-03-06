﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlWorks.Services.PVI.Pvi;
using ControlWorks.Services.Rest;

namespace ControlWorks.Services
{
    public interface IHost
    {
        void Start();
        void Stop();
    }

    public class Host : IHost
    {
        //private PviAplication _application;
        public void Start()
        {

            WebApiApplication.Start();

            var pviApp = WebApiApplication.PviApp;
            var factory = new TaskFactory();
            factory.StartNew(() => pviApp.Connect(), TaskCreationOptions.LongRunning);

            //WebApiApplication.Start();
        }

        public void Stop()
        {
            WebApiApplication.PviApp.Disconnect();
        }

    }
}
