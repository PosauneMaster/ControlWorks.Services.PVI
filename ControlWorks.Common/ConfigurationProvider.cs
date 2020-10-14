using System;
using System.Configuration;
using log4net;

namespace ControlWorks.Common
{
    public static class ConfigurationProvider
    {
        public static string CpuSettingsFile => ConfigurationManager.AppSettings["CpuSettingsFile"];
        public static string VariableSettingsFile => ConfigurationManager.AppSettings["VariableSettingsFile"];
        public static string Port => ConfigurationManager.AppSettings["Port"];
        public static string ShutdownTriggerVariable => ConfigurationManager.AppSettings["ShutdownTriggerVariable"];
        public static byte SourceStationId => (byte)(Convert.ToByte(ConfigurationManager.AppSettings["SourceStationId"]).Equals(0) ? 0x64 : Convert.ToByte(ConfigurationManager.AppSettings["SourceStationId"]));
        public static int MessageTimeout
        {
            get
            {
                if (Int32.TryParse(ConfigurationManager.AppSettings["MessageTimeout"], out var timeout))
                {
                    return timeout;
                }

                return 1000;
            }
        }
        public static string BaseDirectory => ConfigurationManager.AppSettings["BaseDirectory"];
        public static string SettingsDirectory { get; internal set; }
        public static ILog Logger { get; internal set; }
        public static string LogFilename { get; internal set; }
        public static string ServiceDescription => "ControlWorks wrapper service for REST API";
        public static string ServiceDisplayName => "ControlWorksRESTApi";
        public static string ServiceName => "ControlWorks.Services.Rest";

        private static bool? _verboseVariableLogging = null;
        public static bool VerboseVariableLogging
        {
            get
            {
                if (_verboseVariableLogging.HasValue)
                {
                    return _verboseVariableLogging.Value;
                }

                if (Boolean.TryParse(ConfigurationManager.AppSettings["VerboseVariableLogging"], out var verboseVariables))
                {
                    return verboseVariables;
                }
                else
                {
                    return false;
                }
            }
        }

        public static int PollingMilliseconds
        {
            get 
            {
                const int defaultPollingTime = 60000;
                if (Int32.TryParse(ConfigurationManager.AppSettings["VerboseVariableLogging"], out var pollingTime))
                {
                    return pollingTime;
                }

                return defaultPollingTime;
            }
        }
    }
}
