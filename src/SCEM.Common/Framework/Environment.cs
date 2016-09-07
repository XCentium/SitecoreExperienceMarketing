using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Text.RegularExpressions;

using SitecoreHelper;
using SCEM.Constants;

namespace SCEM.Common.Framework
{
    public static class Environment
    {
        public static string AppSetting(string key)
        {
            string environment = String.Empty;

            environment = WhichEnvironment();

            string strOutput = String.Empty;
            strOutput = SiteConfiguration.GetConfigSetting(key, environment);

            return strOutput;

        }

        public static string WhichEnvironment()
        {
            string environment = String.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.ServerVariables != null && HttpContext.Current.Request.ServerVariables.Count > 0)
            {
                var serverName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
                if (!string.IsNullOrWhiteSpace(serverName))
                {
                    Match match = Regex.Match(serverName.ToLower(), RegexConstants.REGEX_FINDCHAR_BEFORE_HYPHEN);
                    environment = match.Value;
                }
            }

            if (string.IsNullOrEmpty(environment))
            {
                environment = SiteConfiguration.GetConfigSetting("default", "Environment");
            }

            return environment;
        }

        #region -- APPS --
        public static string GetFacebookAppID()
        {
            var obj = AppSetting("FacebookAppID");

            return obj;
        }
        #endregion
    }
}
