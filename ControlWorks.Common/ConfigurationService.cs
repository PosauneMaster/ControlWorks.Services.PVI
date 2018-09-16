using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlWorks.Common
{
    public interface IConfigurationService
    {
        ExpandoObject GetSettings(string logFileName);
        string GetSettingFile();
        string GetConfigSource();
        void AddOrUpdateConnectionString(string name, string connectionString);
        void ChangePort(int port);
    }

    public class ConfigurationService : IConfigurationService
    {
        public ExpandoObject GetSettings(string logFileName)
        {
            dynamic main = new ExpandoObject();
            var mainDict = (IDictionary<string, object>)main;

            mainDict.Add("appSettingsFile", GetSettingFile());
            mainDict.Add("connectionStringFile", GetConfigSource());
            mainDict.Add("LogFile", logFileName);

            dynamic settings = new ExpandoObject();
            var settingsDict = (IDictionary<string, object>)settings;

            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
                settingsDict.Add(key, ConfigurationManager.AppSettings[key]);
            }

            mainDict.Add("AppSettings", settings);

            dynamic connections = new ExpandoObject();
            var connectionsDict = (IDictionary<string, object>)connections;

            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConnectionStringsSection connectionStringsSection = configuration.ConnectionStrings;
            ConnectionStringSettingsCollection collection = connectionStringsSection.ConnectionStrings;

            if (connections != null)
            {
                foreach (ConnectionStringSettings cs in collection)
                {

                    connectionsDict.Add(cs.Name, cs.ConnectionString);
                }

                mainDict.Add("ConnectionStrings", connections);
            }

            return main;
        }

        public string GetSettingFile()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            return Path.Combine(Directory.GetCurrentDirectory(), config.AppSettings.SectionInformation.ConfigSource);
        }

        public string GetConfigSource()
        {
            var connectionStringsSection = ConfigurationManager.GetSection("connectionStrings") as ConnectionStringsSection;

            return Path.Combine(Directory.GetCurrentDirectory(), connectionStringsSection.SectionInformation.ConfigSource);
        }

        public void AddOrUpdateConnectionString(string name, string connectionString)
        {
            var setting = new ConnectionStringSettings(name, connectionString);
            setting.ProviderName = "System.Data.SqlClient";

            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConnectionStringsSection connectionStringsSection = configuration.ConnectionStrings;

            if (connectionStringsSection.ConnectionStrings[name] != null)
            {
                var connectionStringSetting = connectionStringsSection.ConnectionStrings[name];
                connectionStringsSection.ConnectionStrings.Remove(connectionStringSetting);

            }

            connectionStringsSection.ConnectionStrings.Add(setting);
            configuration.Save(ConfigurationSaveMode.Modified);
        }

        public void ChangePort(int port)
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            AppSettingsSection appSettings = configuration.AppSettings;

            if (appSettings.Settings["Port"] != null)
            {
                appSettings.Settings["Port"].Value = port.ToString();
                configuration.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");

            }
        }
    }
}
