using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using Itboy.Properties;

namespace Itboy.Design
{
    [AttributeUsage(AttributeTargets.All)]
    internal sealed class PropertyCategoryAttribute : CategoryAttribute
    {
        // Methods
        public PropertyCategoryAttribute(string categoryKey)
            : base(categoryKey)
        {

        }
        protected override string GetLocalizedString(string value)
        {
            return Resources.ResourceManager.GetString(value);
        }
    }
}
