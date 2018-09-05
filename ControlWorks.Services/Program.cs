using System;
using System.Configuration;
using System.IO;
using ControlWorks.Common;
using log4net;
using Topshelf;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace ControlWorks.Services
{

    class Program
    {
        private static ILog Log;

        public static void Main(string[] args)
        {
            Startup.Initialize();
            Log = ConfigurationProvider.Logger;

            Log.Info("Starting Service...");

            var rc = HostFactory.Run(x =>
            {
                x.Service<Host>(s =>
                {
                    s.ConstructUsing(n => new Host());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalService();
                x.SetDescription("ControlWorks wrapper service for REST API");
                x.SetDisplayName("ControlWorksRESTApi");
                x.SetServiceName("ControlWorks.Services.Rest");

                x.EnableServiceRecovery(r =>
                {
                    r.RestartService(1);
                    r.SetResetPeriod(1);
                });

                x.OnException((exception) =>
                {
                    Log.Error("Topshelf service error");
                    Log.Error(exception.Message, exception);
                });
            });
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Fatal("Unhandled Application Domain Error");
            if (e.ExceptionObject is Exception ex) Log.Fatal(ex.Message, ex);
        }
    }
}
