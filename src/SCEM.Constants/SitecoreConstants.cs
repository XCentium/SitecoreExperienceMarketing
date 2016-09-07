using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCEM.Constants
{
    public class SitecoreConstants
    {
        public const string DBMaster = "master";
        public const string DBWeb = "web";
        public const string DBCore = "core";

        #region -- CONTENT TREE CONSTANTS --

        public const string SCEMContentPath = "/sitecore/content/SCEM";
        public const string SCEMConfigurationPath = "/sitecore/content/SiteConfiguration";

        #endregion

        #region -- SCEM CONFIGURATION CONSTANTS --

        public const string SiteConfiguration = SCEMConfigurationPath + "/ConfigSettings";
        public const string WorkflowConfiguration = SCEMConfigurationPath + "/Workflow";

        #endregion

        #region -- SCEM CONTENT CONSTANTS --

        #region - PAGES -
        public const string Homepage = SCEMContentPath + "/Home";
        public const string Articles = SCEMContentPath + "/Home/Learn/Articles";
        #endregion

        #region - MODULES -

        public const string HomepageHeroBannerPath = "/sitecore/content/SCEM/ContentModules/HeroBanners/Homepage";

        #endregion

        #endregion

        #region -- TEMPLATE ID CONSTANTS --

        #region -- SCEM TEMPLATES --

        #region - PAGES -

        public static Guid Article = new Guid("C54542AF-2298-4159-8226-4385C8076A37");

        #endregion

        #region - CONFIGURATION -

        public static Guid ConfigSetting = new Guid("73CD7728-AC00-4B77-B3F0-6AE2B8941E34");

        #endregion

        #region - RENDERING ITEMS -

        public static Guid HeroBannerModuleId = new Guid("B431E176-C705-46D9-8CD0-F96B78BA7AA7");

        #endregion

        #endregion

        #endregion

    }
}
