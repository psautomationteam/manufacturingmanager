using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;

namespace BaoHien.Services.BaseAttributes
{
    public class BaseAttributeService : BaseService<BaseAttribute>
    {
        public BaseAttribute GetBaseAttribute(System.Int32 id)
        {
            BaseAttribute baseAttribute = OnGetItem<BaseAttribute>(id.ToString());

            return baseAttribute;
        }
        public List<BaseAttribute> GetBaseAttributes()
        {
            List<BaseAttribute> baseAttributes = OnGetItems<BaseAttribute>();

            return baseAttributes;
        }
        public bool AddBaseAttribute(BaseAttribute baseAttribute)
        {
            return OnAddItem<BaseAttribute>(baseAttribute);
        }
        public bool DeleteBaseAttribute(System.Int32 id)
        {
            return OnDeleteItem<BaseAttribute>(id.ToString());
        }
        public bool UpdateBaseAttribute(BaseAttribute baseAttribute)
        {
            return OnUpdateItem<BaseAttribute>(baseAttribute, baseAttribute.Id.ToString());
        }
    }
}
