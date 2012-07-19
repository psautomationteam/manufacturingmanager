using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;
using System.Linq.Expressions;

namespace BaoHien.Services.ProductAttributes
{
    public class ProductAttributeService : BaseService<ProductAttribute>
    {
        public ProductAttribute GetProductAttribute(System.Int32 id)
        {
            ProductAttribute productAttribute = OnGetItem<ProductAttribute>(id.ToString());

            return productAttribute;
        }
        public List<ProductAttribute> GetProductAttributes()
        {
            List<ProductAttribute> productAttributes = OnGetItems<ProductAttribute>();

            return productAttributes;
        }
        public bool AddProductAttribute(ProductAttribute orderDetail)
        {
            return OnAddItem<ProductAttribute>(orderDetail);
        }
        public bool DeleteProductAttribute(System.Int32 id)
        {
            return OnDeleteItem<ProductAttribute>(id.ToString());
        }
        public bool UpdateProductAttribute(ProductAttribute productAttribute)
        {
            return OnUpdateItem<ProductAttribute>(productAttribute, productAttribute.Id.ToString());
        }
        public List<ProductAttribute> SelectProductAttributeByWhere(Expression<Func<ProductAttribute, bool>> func)
        {

            return SelectItemByWhere<ProductAttribute>(func);
        }
    }
}
