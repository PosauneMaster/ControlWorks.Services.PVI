using System.Configuration;

namespace ControlWorks.Services.ConfigurationProvider
{
    public static class AppSettings
    {
        public static string CpuSettingsFile => ConfigurationManager.AppSettings["CpuSettingsFile"];
        public static string VariableSettingsFile => ConfigurationManager.AppSettings["VariableSettingsFile"];
        public static string Port => ConfigurationManager.AppSettings["Port"];
        public static string ShutdownTriggerVariable => ConfigurationManager.AppSettings["ShutdownTriggerVariable"];
    }
}
