using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using Sitecore.Analytics.Data;
using Sitecore.Data.Fields;
using Sitecore.Resources.Media;

namespace SitecoreHelper
{
    public static class SitecoreExtensions
    {
        public static Sitecore.Data.Items.Item GetContextOrDatasourceItem(RenderingContext context)
        {
            return GetContextOrDatasourceItem(context, Sitecore.Context.Item.ID.Guid);
        }

        public static Sitecore.Data.Items.Item GetContextOrDatasourceItem(RenderingContext context, Guid contentId)
        {
            ContentProvider scProviderContent = new ContentProvider(ContentProvider.ContentDB());
            Sitecore.Data.Items.Item item = null;

            if (!String.IsNullOrEmpty(context.Rendering.DataSource))
            {
                item = scProviderContent.GetItem(context.Rendering.DataSource);
            }
            else
            {
                item = scProviderContent.GetItem(new ID(contentId));

                if (item == null)
                {
                    item = scProviderContent.GetItem(Sitecore.Context.Item.ID);
                }
            }

            return item;
        }

        public static string DrawMediaItem(ImageField fld, string cssClass = "", string imageAlignment = "", string maxWidth = "")
        {
            string strOutput = String.Empty;

            if (fld.MediaItem != null)
            {
                if (!String.IsNullOrEmpty(imageAlignment))
                {
                    imageAlignment = " style=\"float: " + imageAlignment + ";\" ";
                }

                if (!String.IsNullOrEmpty(maxWidth))
                {
                    maxWidth = "?mw=" + maxWidth;
                }

                strOutput = "<img src=\"" + MediaManager.GetMediaUrl(fld.MediaItem) + maxWidth + "\" alt=\"" + fld.Alt + "\" class=\"" + cssClass + "\"" + imageAlignment + " />";
            }

            return strOutput;
        }

        public static string DrawMediaItem(MediaItem mediaItem, string cssClass = "", string imageAlignment = "")
        {
            string strOutput = String.Empty;

            if (mediaItem != null)
            {
                if (!String.IsNullOrEmpty(imageAlignment))
                {
                    imageAlignment = " style=\"float: " + imageAlignment + ";\" ";
                }

                strOutput = "<img src=\"" + MediaManager.GetMediaUrl(mediaItem) + "\" alt=\"" + mediaItem.Alt + "\" class=\"" + cssClass + "\"" + imageAlignment + " />";
            }

            return strOutput;
        }

        public static string GetRenderingParameterValue(RenderingContext rContext, string ParameterKey)
        {
            string strOutput = String.Empty;

            var parametersAsString = rContext.Rendering.Properties["Parameters"];
            var parameters = HttpUtility.ParseQueryString(parametersAsString);

            strOutput = parameters[ParameterKey];

            return strOutput;
        }

        public static string GetImageUrl(Field field)
        {
            Sitecore.Data.Fields.ImageField imgField = field;
            if (imgField != null)
            {
                if (imgField.MediaItem != null)
                {
                    return Sitecore.Resources.Media.MediaManager.GetMediaUrl(imgField.MediaItem);
                }
            }

            return string.Empty;
        }
        public static string GetMediaUrl(this Sitecore.Mvc.Helpers.SitecoreHelper sitecoreHelper, string fieldName)
        {
            return GetMediaUrl(sitecoreHelper, fieldName, sitecoreHelper.CurrentItem);
        }

        public static string GetMediaUrl(this Sitecore.Mvc.Helpers.SitecoreHelper sitecoreHelper, string fieldName, Item item)
        {
            MediaItem mediaItem = item.Fields[fieldName].Item;
            return Sitecore.Resources.Media.MediaManager.GetMediaUrl(mediaItem);
        }
    }
}
