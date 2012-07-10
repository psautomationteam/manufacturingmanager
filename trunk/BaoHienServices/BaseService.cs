using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
namespace BaoHienServices
{
    
    public class BaseService
    {
        protected static BaoHienDBEntities baoHienDBEntities;
        public  BaseService()
        {
            if (baoHienDBEntities == null)
                baoHienDBEntities = new BaoHienDBEntities();
        }
        public ProductType GetProductType(System.Int32 id)
        {
            ProductType productType = null;
            if (baoHienDBEntities != null)
            {
                productType = baoHienDBEntities.ProductTypes.Where<ProductType>(pt => pt.ProductTypeId == id).Single();
            }
            return productType;
        }
    }
}
