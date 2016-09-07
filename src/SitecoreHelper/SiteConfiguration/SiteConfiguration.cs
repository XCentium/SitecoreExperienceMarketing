using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using SCEM.Constants;

namespace SitecoreHelper
{
    public class SiteConfiguration
    {
        public enum ConfigCategory
        {
            /// <summary>
            /// default config path, which is loandepot/config
            /// </summary>
            Config,
            Workflow
        }

        public static string GetConfigSetting(string Name, string Category)
        {
            return GetConfigSetting(Name, Category, ConfigCategory.Config);
        }

        public static string GetConfigSetting(string Name, string Category, ConfigCategory configCategory, bool blnUseDescription = false)
        {
            string strOutput = String.Empty;
            string configListPath = String.Empty;

            try
            {

                var contentDB = Sitecore.Context.Database.Name;

                if (contentDB.ToLower() == "core")
                {
                    contentDB = "master";
                }

                switch (configCategory)
                {
                    case ConfigCategory.Workflow:
                        {
                            configListPath = SitecoreConstants.WorkflowConfiguration;
                        }
                        break;
                    default:
                        {
                            configListPath = SitecoreConstants.SiteConfiguration;
                        }
                        break;
                }

                ContentProvider scProviderContent = new ContentProvider(contentDB);
                var configList = scProviderContent.GetItem(configListPath);
                var configTemplateId = new Sitecore.Data.ID(SitecoreConstants.ConfigSetting);
                List<Item> configItems = configList.Children.Where(x => x.TemplateID.Equals(configTemplateId)).ToList();

                if (configItems.Count > 0)
                {
                    var matchingConfiguration = (from x in configItems
                                                 where x.Fields["Name"].Value.ToLower().Equals(Name.ToLower()) &&
                                                 x.Fields["Category"].Value.ToLower().Equals(Category.ToLower())
                                                 select x).FirstOrDefault();

                    if (matchingConfiguration != null)
                    {
                        if (blnUseDescription)
                        {
                            strOutput = matchingConfiguration.Fields["Description"].Value;
                        }
                        else
                        {
                            strOutput = matchingConfiguration.Fields["Value"].Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return strOutput;
        }

        public static string GetConfigSetting(string Name, string Category, string ConfigLocationPath, Guid ConfigTemplateID, bool blnUseDescription = false)
        {
            string strOutput = String.Empty;

            Sitecore.Data.ID configTemplateId = new Sitecore.Data.ID(ConfigTemplateID);

            try
            {
                Item configList = Sitecore.Context.Database.GetItem(ConfigLocationPath);

                List<Item> configItems = configList.Children.Where(x => x.TemplateID.Equals(configTemplateId)).ToList();

                var matchingConfig = (from x in configItems
                                      where x.Fields["Name"].Value.ToLower().Equals(Name.ToLower()) &&
                                        x.Fields["Category"].Value.ToLower().Equals(Category.ToLower())
                                      select x).FirstOrDefault();

                if (matchingConfig != null)
                {
                    if (blnUseDescription)
                    {
                        strOutput = matchingConfig.Fields["Description"].Value;
                    }
                    else
                    {
                        strOutput = matchingConfig.Fields["Value"].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                //
            }

            return strOutput;
        }
    }
}
