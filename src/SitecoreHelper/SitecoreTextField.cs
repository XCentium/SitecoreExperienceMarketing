using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sitecore.Data.Fields;

namespace SitecoreHelper
{
    public class SitecoreTextField : SitecoreFieldWrapper
    {
        #region Public Properties
        public override object Value
        {
            get { return CastedValue; }
        }

        public string CastedValue
        {
            get { return UnderlyingField.Value; }
        }
        #endregion

        #region .ctor(Field)
        public SitecoreTextField(Field field)
            : base(field)
        { }
        #endregion
    }
}
