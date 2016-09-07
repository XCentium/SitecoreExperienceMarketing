using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sitecore;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace SitecoreHelper
{
    public class ContentProvider
    {
        private Database _db = null;

        //This will set sitecore current database to retrive data based on search criteria...
        public Database CurrentDatabase
        {
            get { return _db ?? (_db = Context.Database ?? GetDefaultDatabase()); }
            set { _db = value; }
        }

        //This will set the database to initalize....
        public ContentProvider(string databaseName)
        {
            if (string.IsNullOrEmpty(databaseName)) return;
            var db = Database.GetDatabase(databaseName);
            if (db != null)
                _db = db;
        }

        //based on the sitecore item id...this will return item from the current database...
        public Item GetItem(ID id)
        {
            return CurrentDatabase.GetItem(id);
        }

        //this will provide get default database...
        public Database GetDefaultDatabase()
        {
            var result = Context.Database;

            if (result == null)
            {
                var webInfo = Sitecore.Configuration.Settings.Sites.FirstOrDefault(x => x.Name == "website");
                result = Database.GetDatabase(webInfo == null ? "master" : webInfo.Database);
            } //if

            return result;
        }

        //this will return media file path...
        public static string GetMediaFilePath(Sitecore.Data.Items.Item itm, string field)
        {
            string filePath = null;
            string mediaPath = itm.Fields[field].Value;
            Sitecore.Data.Items.Item mediaItem = itm.Database.Items["/sitecore/media library/" + mediaPath];
            if (mediaItem != null)
            {
                filePath = mediaItem.Fields["path"].Value;
            }
            return (filePath);
        }

        //this will be give us Items by Item Id
        public Item GetItemByItemId(ID id)
        {
            Sitecore.Data.Items.Item myItem = CurrentDatabase.Items[id];
            return myItem;
        }

        //this will return based on itemPath
        public Item GetItem(string itemPath)
        {
            return CurrentDatabase.GetItem(itemPath);
        }

        //this will get children from SiteCore
        public ItemList GetChildren(Item item, bool recursive)
        {
            var children = new ItemList();
            var currChildren = item.GetChildren();
            children.AddRange(currChildren.ToList());

            if (recursive)
                foreach (Item currItem in currChildren)
                    children.AddRange(GetChildren(currItem, true).ToList());

            return children;
        }

        //this will get children from SiteCore
        public ItemList GetChildren(ID itemId, bool recursive)
        {
            var item = GetItem(itemId);

            return item != null ? GetChildren(item, recursive) : new ItemList();
        }

        //this will get children from SiteCore
        public ItemList GetChildren(Item item, ID templateId)
        {
            return GetChildren(item, false, templateId);
        }

        //this will get children from SiteCore
        public ItemList GetChildren(Item item, bool recursive, ID templateId)
        {
            var children = GetChildren(item, recursive);
            var list = new ItemList();
            list.AddRange(children.Where(x => new SitecoreItemWrapper(x).HasTemplate(templateId)));
            return list;
        }

        public static string ContentDB()
        {
            return Sitecore.Context.Database.Name;

            //string strOutput = SCEM.Common.SiteCoreConstants.DBMaster;

            //if ((ConfigurationManager.AppSettings["SCEnvironment"] != null) && (ConfigurationManager.AppSettings["SCEnvironment"].ToLower() == "production"))
            //{
            //    strOutput = SCEM.Common.SiteCoreConstants.DBWeb;
            //}

            //return strOutput;
        }

        public static string ContextDBSearchIndex()
        {
            string strOutput = String.Empty;

            switch (ContentDB().ToLower())
            {
                case "web":
                    {
                        strOutput = "sitecore_web_index";
                    }
                    break;
                default:
                    {
                        strOutput = "sitecore_master_index";
                    }
                    break;
            }

            return strOutput;
        }
    }
}
