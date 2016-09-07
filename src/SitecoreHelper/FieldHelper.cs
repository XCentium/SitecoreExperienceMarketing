using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sitecore.Data.Items;

namespace SitecoreHelper
{
    public class FieldHelper
    {
        public static string LinkUrl(Sitecore.Data.Fields.LinkField lf)
        {
            string strOutput = String.Empty;

            switch (lf.LinkType.ToLower())
            {
                case "internal":
                    {
                        // Use LinkMananger for internal links, if link is not empty
                        if (lf.TargetItem != null)
                        {
                            strOutput = Sitecore.Links.LinkManager.GetItemUrl(lf.TargetItem);
                        }
                    }
                    break;
                case "media":
                    {
                        // Use MediaManager for media links, if link is not empty
                        if (lf.TargetItem != null)
                        {
                            strOutput = Sitecore.Resources.Media.MediaManager.GetMediaUrl(lf.TargetItem);
                        }
                    }
                    break;
                case "external":
                    {
                        // Just return external links
                        strOutput = lf.Url;
                    }
                    break;
                case "anchor":
                    {
                        // Prefix anchor link with # if link if not empty
                        if (!string.IsNullOrEmpty(lf.Anchor))
                        {
                            strOutput = "#" + lf.Anchor;
                        }
                    }
                    break;
                case "mailto":
                    {
                        // Just return mailto link
                        strOutput = lf.Url;
                    }
                    break;
                case "javascript":
                    {
                        // Just return javascript
                        strOutput = lf.Url;
                    }
                    break;
                default:
                    {
                        // Just please the compiler, this
                        // condition will never be met
                        strOutput = lf.Url;
                    }
                    break;
            }

            return strOutput;
        }

        public static string GetValue(Item item, string fieldName)
        {
            if (item == null)
            {
                return string.Empty;
            }

            if (item.Fields[fieldName] == null)
            {
                return string.Empty;
            }

            return item.Fields[fieldName].ToString();
        }

        public static string GetPageTitleValue(Item item)
        {
            return GetValue(item, "PageTitle");
        }

        /// <summary>
        /// compare sitecore checkbox, check the value if 1 is checked, 0 or empty is not check.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static bool GetBoolField(Item item, string fieldName)
        {
            if (item == null)
            {
                return false;
            }

            var field = item.Fields[fieldName];
            if (field == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(field.Value))
            {
                return false;
            }

            // if value is 1 then means checked at sitecore
            return field.Value == "1";
        }
    }
}
