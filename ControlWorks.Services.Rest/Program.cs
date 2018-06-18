using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Topshelf;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace ControlWorks.Services.Rest
{
    class Program
    {
        private static readonly ILog Log = LogManager.GetLogger("RestServiceLogger");

        static void Main(string[] args)
        {
            Log.Info("Starting Service");

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
            ILog exceptionLogger = LogManager.GetLogger("RestServiceLogger");
            exceptionLogger.Fatal("Unhandled Application Domain Error");
            if (e.ExceptionObject is Exception ex) exceptionLogger.Fatal(ex.Message, ex);
        }

    }
}
