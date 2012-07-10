using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BaoHienServices
{
    public class ProductTypeService : BaseService
    {
        public ProductTypeService():base()
        {
            
        }
        public ProductType GetProductType(System.Int32 id)
        {
            ProductType productType = null;
            
            return productType;
        }
        public ProductType AddProductType(ProductType productType)
        {
            return null;
        }
    }
}
