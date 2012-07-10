using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BaoHienServices
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
    }
}
