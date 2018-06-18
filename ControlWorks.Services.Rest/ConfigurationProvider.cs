using System.Configuration;

namespace ControlWorks.Services.Rest
{
    public static class ConfigurationProvider
    {
        public static string Port { get; } = ConfigurationManager.AppSettings["Port"] ?? "8080";
    }
}
