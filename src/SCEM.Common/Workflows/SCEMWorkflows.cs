using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SCEM.Constants;

namespace SCEM.Common.Workflows
{
    public static class SCEMWorkflows
    {
        public static class LPGeneralWorkflow
        {
            public static string ItemSubmittedRecipients = SitecoreConstants.SCEMConfigurationPath + "/Workflow/ItemSubmittedRecipients";
            public static string ItemSubmittedEmail = SitecoreConstants.SCEMConfigurationPath + "/Workflow/ItemSubmittedEmail";

            public static string ItemAcceptedRecipients = SitecoreConstants.SCEMConfigurationPath + "/Workflow/ItemAcceptedRecipients";
            public static string ItemAcceptedEmail = SitecoreConstants.SCEMConfigurationPath + "/Workflow/ItemAcceptedEmail";

            public static string ItemRejectedRecipients = SitecoreConstants.SCEMConfigurationPath + "/Workflow/ItemRejectedRecipients";
            public static string ItemRejectedEmail = SitecoreConstants.SCEMConfigurationPath + "/Workflow/ItemRejectedEmail";

            public static string ItemPublishedRecipients = SitecoreConstants.SCEMConfigurationPath + "/Workflow/ItemPublishedRecipients";
            public static string ItemPublishedEmail = SitecoreConstants.SCEMConfigurationPath + "/Workflow/ItemPublishedEmail";

            public static string PublishingGroups = SitecoreConstants.SCEMConfigurationPath + "/Workflow/PublishingGroups";
        }
    }
}
