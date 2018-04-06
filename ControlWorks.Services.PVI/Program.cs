using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace ControlWorks.Services.PVI
{
    class Program
    {
        private static ILog _log = LogManager.GetLogger("FileLogger");

        static void Main(string[] args)
        {
            _log.Info("Starting Service");

            var rc = HostFactory.Run(x =>
            {
                x.Service<Host>(s =>
                {
                    s.ConstructUsing(n => new Host());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalService();
                x.SetDescription("ControlWorks wrapper service for PVI Communication");
                x.SetDisplayName("ControlWorksPviService");
                x.SetServiceName("ControlWorks.Services.PVI");

                x.EnableServiceRecovery(r =>
                {
                    r.RestartService(1);
                    r.SetResetPeriod(1);
                });

                x.OnException((exception) =>
                {
                    _log.Error("Topshelf service error");
                    _log.Error(exception.Message, exception);
                });
            });
        }
    }
}
