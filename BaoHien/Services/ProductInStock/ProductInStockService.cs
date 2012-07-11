using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;

namespace BaoHien.Services.ProductInStocks
{
    public class ProductInStockService : BaseService<ProductInStock>
    {
        public ProductInStock GetProductInStock(System.Int32 id)
        {
            ProductInStock productInStock = OnGetItem<ProductInStock>(id.ToString());

            return productInStock;
        }
        public List<ProductInStock> GetProductInStocks()
        {
            List<ProductInStock> productInStocks = OnGetItems<ProductInStock>();

            return productInStocks;
        }
        public bool AddProductInStock(ProductInStock orderDetail)
        {
            return OnAddItem<ProductInStock>(orderDetail);
        }
        public bool DeleteProductInStock(System.Int32 id)
        {
            return OnDeleteItem<ProductInStock>(id.ToString());
        }
        public bool UpdateProductInStock(ProductInStock productInStock)
        {
            return OnUpdateItem<ProductInStock>(productInStock, productInStock.Id.ToString());
        }
    }
}
