using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;
using System.Linq.Expressions;
using DAL.Helper;
using BaoHien.Model;

namespace BaoHien.Services.Products
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
        public List<ProductType> SelectProductTypeByWhere(Expression<Func<ProductType, bool>> func)
        {

            return SelectItemByWhere<ProductType>(func);
        }
        public List<ProductType> SearchingProductType(ProductTypeSearchCriteria productTypeSearchCriteria)
        {

            IQueryable<ProductType> query = null;
            BaoHienDBDataContext context = BaoHienRepository.GetBaoHienDBDataContext();
            if (context != null)
            {
                query = from pt in context.ProductTypes
                        where (pt.Status == null)
                        select pt;
            }
            if (productTypeSearchCriteria.ProductTypeCode != null)
            {
                query = query.Where(p => p.TypeCode.Contains(productTypeSearchCriteria.ProductTypeCode));
            }
            if (productTypeSearchCriteria.ProductTypeName != null)
            {
                query = query.Where(p => p.ProductName.Contains(productTypeSearchCriteria.ProductTypeName));
            }
            
            if (query != null)
                return query.ToList();
            return null;
        }
    }
}
