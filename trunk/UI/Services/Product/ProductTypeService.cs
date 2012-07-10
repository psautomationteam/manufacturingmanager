using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BaoHienServices
{
    public class ProductTypeService : BaseService<ProductType>
    {
        
        public ProductType GetProductType(System.Int32 id)
        {
            ProductType productType = OnGetItem<ProductType>(id.ToString());
            
            return productType;
        }
        public List<ProductType> GetProductTypes()
        {
            List<ProductType> productTypes = OnGetItems<ProductType>();

            return productTypes;
        }
        public bool AddProductType(ProductType productType)
        {
            return OnAddItem<ProductType>(productType);
        }
        public bool DeleteProductType(System.Int32 id)
        {
            return OnDeleteItem<ProductType>(id.ToString());
        }
        public bool UpdateProductType(ProductType productType)
        {
            return OnUpdateItem <ProductType>(productType,productType.Id.ToString());
        }

    }
}
