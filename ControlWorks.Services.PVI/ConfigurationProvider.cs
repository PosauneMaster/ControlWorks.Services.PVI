using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlWorks.Services.PVI
{
    public static class ConfigurationProvider
    {
        public static string CpuSettingsFile => ConfigurationManager.AppSettings["CpuSettingsFile"];
        public static string VariableSettingsFile => ConfigurationManager.AppSettings["VariableSettingsFile"];


    }
}
