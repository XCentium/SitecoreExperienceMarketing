using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sitecore.Data.Items;
using Sitecore.Data;

namespace SitecoreHelper
{
    public class SitecoreItemWrapper
    {
        // Gets the wrapped fields.
        public List<SitecoreFieldWrapper> WrappedFields
        {
            get
            {
                return (from field in UnderlyingItem.Fields
                        select SitecoreFieldWrapper.Cast(field)).ToList();
            } //get
        }

        //   The underlying Sitecore Item.
        public Item UnderlyingItem { get; protected set; }

        //assigning property values
        public SitecoreItemWrapper(Item item)
        {
            UnderlyingItem = item;
        }

        //retriving field values from underlying item.
        public SitecoreFieldWrapper GetField(string fieldName)
        {
            return GetField(UnderlyingItem, fieldName);
        }

        //retriving field from item...
        public static SitecoreFieldWrapper GetField(Item item, string fieldName)
        {
            var field = item.Fields[fieldName];
            return field != null ? SitecoreFieldWrapper.Cast(field) : null;
        }

        //checks the template...
        public bool HasTemplate(ID templateID)
        {
            return GetTemplates().Any(XamlPageStatePersister => XamlPageStatePersister.ID.ToString() == templateID.ToString());
        }

        //gets the templates...
        private TemplateItem[] GetBaseTemplates(TemplateItem template)
        {
            var templates = new List<TemplateItem>();

            foreach (var currTemplate in template.BaseTemplates)
            {
                templates.Add(currTemplate);
                templates.AddRange(GetBaseTemplates(currTemplate));
            }   //foreach

            return templates.ToArray();
        }

        //gets the templates...
        private TemplateItem[] GetTemplates()
        {
            var templates = new List<TemplateItem>();
            templates.Add(UnderlyingItem.Template);

            foreach (var template in UnderlyingItem.Template.BaseTemplates)
            {
                templates.Add(template);
                templates.AddRange(GetBaseTemplates(template));
            }

            return templates.ToArray();
        }
    }
}
