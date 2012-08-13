using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using Itboy.Properties;

namespace Itboy.Design
{
    [AttributeUsage(AttributeTargets.All)]
    internal sealed class PropertyDescriptionAttribute:DescriptionAttribute
    {
        public PropertyDescriptionAttribute(string descriptionKey)
            : base(Resources.ResourceManager.GetString(descriptionKey))
        {

        }
    }
}
