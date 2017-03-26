using Bittrex.Helpers;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bittrex
{
    public class Settings
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static void WriteLog(string configName, object defaultValue)
        {
            if (!ConfigurationManager.AppSettings.AllKeys.Any(r => r.Equals(configName, StringComparison.InvariantCultureIgnoreCase)))
            {
                Logger.Info(string.Format("Config Name {0} is using default value {1}      <add key=\"{0}\" value=\"{1}\" />", configName, defaultValue));
            }
        }
        public static string GetConfigString(string configName, string defaultValue = "")
        {
            var appValue = ConfigurationManager.AppSettings[configName];
            if (string.IsNullOrEmpty(appValue))
            {
                WriteLog(configName, defaultValue);
                return defaultValue;
            }
            else
            {
                return appValue;
            }
        }

        public static bool GetConfigBool(string configName, bool defaultValue = false)
        {

            var configValue = defaultValue;
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[configName]))
            {
                configValue = ConfigurationManager.AppSettings[configName].ToBool();
            }
            else
            {
                WriteLog(configName, defaultValue);
            }
            return configValue;

        }

        public static int GetConfigInt(string configName, int defaultValue = 0)
        {
            int configValue = -1;
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[configName]))
            {
                configValue = ConfigurationManager.AppSettings[configName].ToInt();
            }
            else
            {
                WriteLog(configName, defaultValue);
            }
            return configValue == -1 ? defaultValue : configValue;
        }


    }
}
