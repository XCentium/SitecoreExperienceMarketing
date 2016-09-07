using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using Sitecore.Data;

namespace SitecoreHelper.Workflow
{
    public class WorkflowHelper
    {
        public Sitecore.Data.Items.Item item { get; set; }
        public Sitecore.Workflows.Simple.WorkflowPipelineArgs args { get; set; }
        public Sitecore.Security.Accounts.User _submittingUser;

        private string _emailcontentpath;
        private string _emailrecipientspath;
        private Sitecore.Workflows.IWorkflow _contentWorkflow;
        private Sitecore.Workflows.WorkflowEvent[] _contentHistory;

        public WorkflowHelper(Sitecore.Data.Items.Item contentItem, Sitecore.Workflows.Simple.WorkflowPipelineArgs workflowArgs, string emailcontentpath, string emailrecipientspath)
        {
            item = contentItem;
            args = workflowArgs;
            _emailcontentpath = emailcontentpath;
            _emailrecipientspath = emailrecipientspath;

            _contentWorkflow = item.Database.WorkflowProvider.GetWorkflow(contentItem);
            if (_contentWorkflow != null)
                _contentHistory = _contentWorkflow.GetHistory(contentItem);
            _submittingUser = GetSubmittingUser();
        }

        /// <summary>
        /// Gets list of email recipients
        /// </summary>
        /// <returns></returns>
        public List<string> GetEmailRecipients()
        {
            string path = String.Format("{0}_{1}", _emailrecipientspath, GetEnvironment());

            string contentDB = ContentProvider.ContentDB();

            if (contentDB.ToLower() == "core")
            {
                contentDB = "master";
            }

            Sitecore.Data.Database scDB = Database.GetDatabase(contentDB);

            var item = scDB.GetItem(path);
            var recipients = new List<string>();

            if (item != null)
            {
                var list = item["Value"].ToString().ToLower().Split(new char[] { ',', ';' });

                if (list != null && list.Count() > 0)
                    recipients.AddRange(list);
            }

            //var publishingGroupItems = scDB.GetItem(LPWorkflows.LPGeneralWorkflow.PublishingGroups);

            //if (publishingGroupItems != null)
            //{
            //    var publishingGroupList = publishingGroupItems["Value"].ToString().Split(';').ToList<string>();
            //    var publishingGroupRole = (from x in publishingGroupList
            //                               where _submittingUser.IsInRole("sitecore\\" + x)
            //                               select x).ToList();

            //    foreach (var pubGroup in publishingGroupRole)
            //    {
            //        IEnumerable<Sitecore.Security.Accounts.User> roleUsers = Sitecore.Security.Accounts.RolesInRolesManager.GetUsersInRole(Sitecore.Security.Accounts.Role.FromName("sitecore\\" + pubGroup), true);

            //        var pubGroupRecipients = (from x in roleUsers
            //                                  where recipients.Contains(x.Profile.Email.ToLower())
            //                                  select x.Profile.Email).ToList();

            //        recipients.AddRange(pubGroupRecipients);
            //    }
            //}

            return recipients;
        }

        /// <summary>
        /// Gets email subject which includes the item's name, workflow name and submitting user
        /// </summary>
        /// <returns></returns>
        public string GetEmailSubject(string action)
        {
            string contentDB = ContentProvider.ContentDB();

            if (contentDB.ToLower() == "core")
            {
                contentDB = "master";
            }

            Sitecore.Data.Database scDB = Database.GetDatabase(contentDB);

            var workflowItem = scDB.GetItem(new ID(_contentWorkflow.WorkflowID));
            string workflowName = "";
            if (workflowItem != null)
                workflowName = workflowItem.Name;

            return String.Format("{0} {1} to {2} workflow by {3}",
                item.Name,
                action,
                workflowName,
                ((_submittingUser != null && _submittingUser.Profile != null) ? _submittingUser.Profile.FullName : "")
            );
        }

        /// <summary>
        /// Gets the HTML content for the workflow email
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetEmailContent()
        {
            string contentDB = ContentProvider.ContentDB();

            if (contentDB.ToLower() == "core")
            {
                contentDB = "master";
            }

            Sitecore.Data.Database scDB = Database.GetDatabase(contentDB);

            var item = scDB.GetItem(_emailcontentpath);

            if (item != null)
            {
                string message = item["Description"].ToString();
                if (!String.IsNullOrWhiteSpace(message))
                {
                    message = ReplaceEmailVariables(message);
                }
                return message;
            }

            return String.Empty;
        }

        /// <summary>
        /// Takes a string (of email content) and replaces the variables with values based on the item and workflow
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private string ReplaceEmailVariables(string content)
        {
            if (String.IsNullOrEmpty(content)) return String.Empty;

            content = content.Replace("{comments}", args.Comments);
            content = content.Replace("{item}", item.DisplayName);
            content = content.Replace("{path}", item.Paths.FullPath);
            content = content.Replace("{url}", Sitecore.Links.LinkManager.GetItemUrl(item));

            if (_submittingUser != null)
            {
                content = content.Replace("{user}", _submittingUser.Profile.FullName);
                content = content.Replace("{email}", _submittingUser.Profile.Email);
            }
            else
            {
                content = content.Replace("{user}", "n/a");
                content = content.Replace("{email}", "n/a");
            }

            return content;
        }

        /// <summary>
        /// Get submitting user from workflow
        /// </summary>
        /// <returns></returns>
        private Sitecore.Security.Accounts.User GetSubmittingUser()
        {
            if (_contentHistory.Length > 0)
            {
                //submitting user (string)
                string lastUser = _contentHistory[_contentHistory.Length - 1].User;
                return Sitecore.Security.Accounts.User.FromName(lastUser, false);
            }

            return null;
        }

        /// <summary>
        /// Same as SCEM.Common.Framework.Config.GetEnvironment. Cannot reference here due to circular reference
        /// </summary>
        /// <returns></returns>
        private string GetEnvironment()
        {
            string environment = String.Empty;

            var machineName = System.Environment.MachineName.ToLower();

            if (HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToLower().StartsWith("qa-"))
            {
                machineName = "qa-" + machineName;
            }

            environment = SiteConfiguration.GetConfigSetting(machineName, "Environment");

            if (String.IsNullOrEmpty(environment))
            {
                environment = SiteConfiguration.GetConfigSetting("default", "Environment");
            }

            return environment;
        }

        /// <summary>
        /// Gets list of emails for Sitecore users in a particular role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static List<string> GetUserEmailsInRole(string roleName)
        {
            var emails = new List<string>();

            var users = System.Web.Security.Roles.GetUsersInRole("sitecore\\" + roleName);

            foreach (var user in users)
            {
                var membershipUser = System.Web.Security.Membership.GetUser(user);
                var email = membershipUser.Email;
                emails.Add(email);
            }

            return emails;
        }
    }
}
