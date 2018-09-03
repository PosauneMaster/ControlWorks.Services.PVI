using System.IO;
using System.Linq;
using log4net;
using log4net.Appender;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace ControlWorks.Common
{
    public static class Startup
    {
        private static ILog Log;
        public static void Initialize()
        {
            const string loggerName = "ControlWorksLogger";

            ConfigurationProvider.Logger = LogManager.GetLogger(loggerName);
            Log = ConfigurationProvider.Logger;

            Log.Info(new string('*', 30));
            Log.Info("Starting application...");
            Log.Info(new string('*', 30));

            Log.Info("Starting Initialization...");


            if (!Directory.Exists(ConfigurationProvider.BaseDirectory))
            {
                Directory.CreateDirectory(ConfigurationProvider.BaseDirectory);
            }

            ConfigurationProvider.SettingsDirectory = Path.Combine(ConfigurationProvider.BaseDirectory, "Settings");

            if (!Directory.Exists(ConfigurationProvider.SettingsDirectory))
            {
                Directory.CreateDirectory(ConfigurationProvider.SettingsDirectory);
            }

            ConfigurationProvider.LogFilename = GetLogFileName();

            WriteStartupLog();
            Log.Info("Initialization Complete.");
        }

        public static string GetLogFileName()
        {
            var rootAppender = LogManager.GetRepository()
                                         .GetAppenders()
                                         .OfType<RollingFileAppender>()
                                         .FirstOrDefault(fa => fa.Name == "LogFileAppender");

            return rootAppender != null ? rootAppender.File : string.Empty;
        }

        public static void WriteStartupLog()
        {
            Log.Info($"BaseDirectory={ConfigurationProvider.BaseDirectory}");
            Log.Info($"SettingsDirectory={ConfigurationProvider.SettingsDirectory}");
            Log.Info($"CpuSettingsFile={ConfigurationProvider.CpuSettingsFile}");
            Log.Info($"VariableSettingsFile={ConfigurationProvider.VariableSettingsFile}");
            Log.Info($"Port={ConfigurationProvider.Port}");
            Log.Info($"ShutdownTriggerVariable={ConfigurationProvider.ShutdownTriggerVariable}");
            Log.Info($"SourceStationId={ConfigurationProvider.SourceStationId}");
            Log.Info($"MessageTimeout={ConfigurationProvider.MessageTimeout}");
            Log.Info($"LogFilename={ConfigurationProvider.LogFilename}");
            Log.Info($"ServiceDescription={ConfigurationProvider.ServiceDescription}");
            Log.Info($"ServiceDisplayName={ConfigurationProvider.ServiceDisplayName}");
            Log.Info($"ServiceName={ConfigurationProvider.ServiceName}");
        }
    }
}
