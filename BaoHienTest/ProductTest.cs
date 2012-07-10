using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using BaoHienServices.Helper;
using BaoHienServices;

namespace BaoHienTest
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        
        public void AddProduct()
        {
            //List<ProductType> list = (List<ProductType>)BaoHienRepository.SelectAll<ProductType>().Where<ProductType>(item => item.st == false).ToList<ProductType>();
            //int count = list.Count;
            Product product = new Product
            {
                ProductCode = "Product code 1",
                ProductName = "Product Name 1",
                ProductType = 4
            };
            ProductService productTypeService = new ProductService();
            bool result = productTypeService.AddProduct(product);

            Assert.AreEqual<bool>(true, result); ;
        }
        [TestMethod]
        public void GetProduct()
        {
            //List<ProductType> list = (List<ProductType>)BaoHienRepository.SelectAll<ProductType>().Where<ProductType>(item => item.st == false).ToList<ProductType>();
            //int count = list.Count;
           
            ProductService productService = new ProductService();
            Product product = productService.GetProduct(1);

            Assert.AreEqual<int>(1, product.Id); ;
        }

        [TestMethod]
        public void GetProducts()
        {
            //List<ProductType> list = (List<ProductType>)BaoHienRepository.SelectAll<ProductType>().Where<ProductType>(item => item.st == false).ToList<ProductType>();
            //int count = list.Count;

            ProductService productService = new ProductService();
            List<Product> products = productService.GetProducts();

            Assert.AreEqual<bool>(true, products.Count > 0); ;
        }
        
    }
}
