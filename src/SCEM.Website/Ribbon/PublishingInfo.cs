using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

using Sitecore.Shell.Web.UI.WebControls;
using Sitecore.Data.Items;
using Sitecore.Data;

namespace SCEM.Website.Ribbon
{
    public class PublishingInfo : RibbonPanel
    {
        public override void Render(System.Web.UI.HtmlTextWriter output, Sitecore.Web.UI.WebControls.Ribbons.Ribbon ribbon, Sitecore.Data.Items.Item button, Sitecore.Shell.Framework.Commands.CommandContext context)
        {
            if ((ConfigurationManager.AppSettings["SCEnvironment"] == null) || (ConfigurationManager.AppSettings["SCEnvironment"].ToLower() != "production"))
            {
                if (context.Items.Length >= 1)
                {
                    Item contextItem = context.Items[0];

                    Database database = Sitecore.Configuration.Factory.GetDatabase("web");
                    Item item = database.SelectSingleItem(contextItem.ID.ToString());

                    Database m_database = Sitecore.Configuration.Factory.GetDatabase("master");
                    Item m_item = m_database.SelectSingleItem(contextItem.ID.ToString());

                    string htmlOutput = string.Empty;
                    string NotPublished = string.Empty;

                    if (item != null)
                    {
                        foreach (var language in item.Languages)
                        {
                            Item languageVersion = item.Versions.GetLatestVersion(language);
                            if (languageVersion != null && languageVersion.Versions.Count > 0)
                            {
                                if (item.Statistics.Updated < m_item.Statistics.Updated)
                                    htmlOutput = "This item is not currently published.";
                                else
                                    htmlOutput += string.Format("<div> {0} - {1}</div>", item.Statistics.Updated, item.Statistics.UpdatedBy);
                            }
                        }
                    }
                    if (htmlOutput == string.Empty)
                    {
                        htmlOutput = "This item is not currently published.";
                    }

                    if (htmlOutput.Contains("not"))
                        htmlOutput = string.Format("<div class='scRibbonToolbarText' style='overflow-y:scroll;padding-right: 20px;font-size:80%; float: none; display: inline-block;'><div style='font-weight:bold'>Pending Publish to Web</div>{0}</div>", htmlOutput);
                    else
                        htmlOutput = string.Format("<div class='scRibbonToolbarText' style='overflow-y:scroll;padding-right: 20px;font-size:80%; float: none; display: inline-block;'><div style='font-weight:bold'>Published to Web</div>{0}</div>", htmlOutput);

                    output.Write(htmlOutput);
                }
            }
        }
    }
}