using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;
using System.Linq.Expressions;
using BaoHien.Model;
using DAL.Helper;

namespace BaoHien.Services.Products
{
    public class ProductService : BaseService<Product>
    {
        public Product GetProduct(System.Int32 id)
        {
            Product product = OnGetItem<Product>(id.ToString());

            return product;
        }
        public List<Product> GetProducts()
        {
            List<Product> products = OnGetItems<Product>();

            return products;
        }
        public bool AddProduct(Product product)
        {
            return OnAddItem<Product>(product);
        }
        public bool DeleteProduct(System.Int32 id)
        {
            return OnDeleteItem<Product>(id.ToString());
        }
        public bool UpdateProduct(Product product)
        {
            return OnUpdateItem<Product>(product, product.Id.ToString());
        }
        public List<Product> SelectProductByWhere(Expression<Func<Product, bool>> func)
        {

            return SelectItemByWhere<Product>(func);
        }
        public List<Product> SearchingProduct(ProductSearchCriteria productSearchCriteria)
        {
            
            IQueryable<Product> query = null;
            BaoHienDBDataContext context = BaoHienRepository.GetBaoHienDBDataContext();
            if (context != null)
            {
                query = from p in context.Products
                        join pt in context.ProductTypes on p.ProductType equals pt.Id
                        where (p.Status == null) && (productSearchCriteria.ProductTypeId == null || pt.Id == productSearchCriteria.ProductTypeId)
                        select p;
            }
            if (productSearchCriteria.ProductCode != null)
            {
                query = query.Where(p => p.ProductCode.Contains(productSearchCriteria.ProductCode));
            }
            if (productSearchCriteria.ProductName != null)
            {
                query = query.Where(p => p.ProductName.Contains(productSearchCriteria.ProductName));
            }
            if (productSearchCriteria.PurchaseStatus != null)
            {
                
            }
            if (query != null)
                return query.ToList();
            return null;
        }

    }
}
