using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;
using System.Linq.Expressions;
using BaoHien.Model;
using DAL.Helper;

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

        public List<ProductAttributeModel> GetProductAndAttribute()
        {
            List<ProductAttributeModel> list = new List<ProductAttributeModel>();
            using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
            {
                var products = context.Products.Where(p => p.Status == null).ToList();
                foreach (Product product in products)
                {
                    var attrs = from a in context.BaseAttributes
                                join pa in context.ProductAttributes on a.Id equals pa.AttributeId
                                where pa.ProductId == product.Id
                                select a;
                    foreach (BaseAttribute attr in attrs)
                    {
                        ProductAttributeModel pad = new ProductAttributeModel();
                        pad.Id = GetProductAttribute(product.Id, attr.Id).Id;
                        pad.AttributeId = attr.Id;
                        pad.ProductId = product.Id;
                        pad.ProductAttribute = product.ProductName + " - " + attr.AttributeName;
                        list.Add(pad);
                    }
                }
            }
            return list;
        }

        public ProductAttribute GetProductAttribute(int ProductId, int AttributeId)
        {
            ProductAttribute pa = new ProductAttribute();
            using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
            {
                pa = context.ProductAttributes.Where(p => p.ProductId == ProductId && p.AttributeId == AttributeId).SingleOrDefault();
            }
            return pa;
        }
    }
}
