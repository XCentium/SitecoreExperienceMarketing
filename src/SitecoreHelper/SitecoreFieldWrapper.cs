using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sitecore.Data.Fields;

namespace SitecoreHelper
{
    public abstract class SitecoreFieldWrapper
    {
        // Gets the underlying field.
        public Field UnderlyingField
        {
            get;
            private set;
        }

        // Gets the field's value.
        public abstract object Value { get; }

        //below method will assign SiteCore Field...to property field
        protected SitecoreFieldWrapper(Field field)
        {
            UnderlyingField = field;
        }

        //below method will type cast the SiteCore Field...
        public static SitecoreFieldWrapper Cast(Field field)
        {
            if (field != null)
            {
                return new SitecoreTextField(field);
            }   //if

            return null;
        }
    }
}
