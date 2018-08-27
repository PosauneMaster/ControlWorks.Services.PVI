using System;
using System.Configuration;

namespace ControlWorks.Services.Rest
{
    public static class ConfigurationProvider
    {
        public static string Port { get; } =  String.IsNullOrEmpty(ConfigurationManager.AppSettings["Port"]) ? "8080" : ConfigurationManager.AppSettings["Port"];
    }
}
