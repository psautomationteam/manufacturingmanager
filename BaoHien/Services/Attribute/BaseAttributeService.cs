using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;

namespace BaoHien.Services.Attributes
{
    public class BaseAttributeService : BaseService<BaseAttribute>
    {
        public BaseAttribute GetAttribute(System.Int32 id)
        {
            BaseAttribute attribute = OnGetItem<BaseAttribute>(id.ToString());

            return attribute;
        }
        public List<BaseAttribute> GetBaseAttributes()
        {
            List<BaseAttribute> attributes = OnGetItems<BaseAttribute>();

            return attributes;
        }
        public bool AddProduct(BaseAttribute attribute)
        {
            return OnAddItem<BaseAttribute>(attribute);
        }
        public bool DeleteProduct(System.Int32 id)
        {
            return OnDeleteItem<BaseAttribute>(id.ToString());
        }
        public bool Updateattribute(BaseAttribute attribute)
        {
            return OnUpdateItem<BaseAttribute>(attribute, attribute.Id.ToString());
        }
    }
}
