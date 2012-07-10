using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using BaoHien.Services.Products;

namespace BaoHienTest
{
    [TestClass]
    public class ProductTypeTest
    {
        [TestMethod]
        public void AddProductType()
        {
            //List<ProductType> list = (List<ProductType>)BaoHienRepository.SelectAll<ProductType>().Where<ProductType>(item => item.st == false).ToList<ProductType>();
            //int count = list.Count;
            ProductType productType = new ProductType
            {
                Description = "P Type 1",
                ProductName = "Product 1",
                TypeCode = "Type code 1"
            };
            ProductTypeService productTypeService = new ProductTypeService();
            bool result = productTypeService.AddProductType(productType);

            Assert.AreEqual<bool>(true, result); ;
        }
        [TestMethod]
        public void GetProductType()
        {
            //List<ProductType> list = (List<ProductType>)BaoHienRepository.SelectAll<ProductType>().Where<ProductType>(item => item.st == false).ToList<ProductType>();
            //int count = list.Count;
           
            ProductTypeService productTypeService = new ProductTypeService();
            ProductType productType = productTypeService.GetProductType(4);

            Assert.AreEqual<int>(4, productType.Id); ;
        }
        [TestMethod]
        public void GetProductTypes()
        {
            //List<ProductType> list = (List<ProductType>)BaoHienRepository.SelectAll<ProductType>().Where<ProductType>(item => item.st == false).ToList<ProductType>();
            //int count = list.Count;

            ProductTypeService productTypeService = new ProductTypeService();
            List<ProductType> productTypes = productTypeService.GetProductTypes();

            Assert.AreEqual<bool>(true, productTypes.Count > 0); ;
        }
        
    }
}
