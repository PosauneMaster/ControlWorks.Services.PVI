using System;
using System.Configuration;

namespace ControlWorks.Services.ConfigurationProvider
{
    public static class AppSettings
    {
        public static string CpuSettingsFile => ConfigurationManager.AppSettings["CpuSettingsFile"];
        public static string VariableSettingsFile => ConfigurationManager.AppSettings["VariableSettingsFile"];
        public static string Port => ConfigurationManager.AppSettings["Port"];
        public static string ShutdownTriggerVariable => ConfigurationManager.AppSettings["ShutdownTriggerVariable"];
        public static byte SourceStationId => (byte)(Convert.ToByte(ConfigurationManager.AppSettings["SourceStationId"]).Equals(0) ? 0x64 : Convert.ToByte(ConfigurationManager.AppSettings["SourceStationId"]));
    }
}
