using System;
using System.Collections.Generic;
using System.Linq;
using BaoHien.Services.Base;
using DAL;
using DAL.Helper;
using BaoHien.Model;
using BaoHien.Common;
using System.Linq.Expressions;

namespace BaoHien.Services.ProductLogs
{
    class ProductLogService : BaseService<ProductLog>
    {
        public ProductLog GetProductLog(System.Int32 id)
        {
            ProductLog productLog = OnGetItem<ProductLog>(id.ToString());
            return productLog;
        }

        public List<ProductLog> GetProductLogs()
        {
            List<ProductLog> productLogs = OnGetItems<ProductLog>();
            return productLogs;
        }

        public bool AddProductLog(ProductLog productLog)
        {
            return OnAddItem<ProductLog>(productLog);
        }

        public bool DeleteProductLog(System.Int32 id)
        {
            return OnDeleteItem<ProductLog>(id.ToString());
        }

        public bool UpdateProductLog(ProductLog productLog)
        {
            return OnUpdateItem<ProductLog>(productLog, productLog.Id.ToString());
        }

        public List<ProductLog> SelectProductLogByWhere(Expression<Func<ProductLog, bool>> func)
        {
            return SelectItemByWhere<ProductLog>(func);
        }

        public List<MeasurementUnit> GetUnitsOfProductAttribute(int productId, int attrId)
        {
            List<MeasurementUnit> result = new List<MeasurementUnit>();
            using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
            {
                List<ProductLog> logs = context.ProductLogs.Where(p => p.ProductId == productId && p.AttributeId == attrId)
                                 .OrderByDescending(x => x.UpdatedDate).GroupBy(p => p.UnitId).Select(p => p.First()).ToList();
                foreach (ProductLog log in logs)
                {
                    result.Add(log.MeasurementUnit);
                }
            }
            return result;
        }
        
        public List<ProductLog> GetProductLogs(int productId, int attrId, int unitId)
        {
            List<ProductLog> logs = GetProductLogs();
            if (productId != null && productId > 0)
                logs = logs.Where(x => x.ProductId == productId).ToList();
            if (attrId != null && attrId > 0)
                logs = logs.Where(x => x.AttributeId == attrId).ToList();
            if (unitId != null && unitId > 0)
                logs = logs.Where(x => x.UnitId == unitId).ToList();
            return logs;
        }

        public ProductLog GetProductLog(int productId, int attrId, int unitId)
        {
            ProductLog result = null;
            using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
            {
                result = context.ProductLogs.Where(p => p.ProductId == productId && p.AttributeId == attrId && p.UnitId == unitId)
                    .OrderByDescending(c => c.Id).FirstOrDefault();
                if (result == null)
                {
                    result = new ProductLog()
                    {
                        ProductId = productId,
                        AttributeId = attrId,
                        UnitId = unitId,
                        Amount = 0,
                        AfterNumber = 0,
                        BeforeNumber = 0,
                        RecordCode = "",
                        Status = BHConstant.DEACTIVE_STATUS,
                        Direction = BHConstant.DIRECTION_IN,
                        UpdatedDate = context.GetSystemDate()
                    };
                    context.ProductLogs.InsertOnSubmit(result);
                    context.SubmitChanges();
                }
            }
            return result;
        }
        
        public void DeactiveProductLog(string RecordCode)
        {
            using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
            {
                context.ProductLogs.Where(p => p.RecordCode == RecordCode && p.Status == BHConstant.ACTIVE_STATUS)
                    .ToList().ForEach(x => x.Status = BHConstant.DEACTIVE_STATUS);
                context.SubmitChanges();
            }
        }

        public ProductLog GetNewestProductLog(int productId, int attrId)
        {
            ProductLog result = null;
            using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
            {
                result = context.ProductLogs.Where(p => p.ProductId == productId && p.AttributeId == attrId)
                    .OrderByDescending(c => c.Id).FirstOrDefault();
            }
            return result;
        }

        public List<ProductReport> GetReportsOfProductAttributeUnit(int productId, int attrId, int unitId, DateTime from, DateTime to)
        {
            List<ProductReport> result = new List<ProductReport>();
            int index = 0;
            if (from > to)
            {
                DateTime d = from;
                from = to;
                to = d;
            }
            using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
            {
                var produces = context.ProductLogs.Where(x => x.UpdatedDate >= from && x.UpdatedDate <= to &&
                    x.ProductId == productId && x.AttributeId == attrId && x.UnitId == unitId && 
                    x.Status == BHConstant.ACTIVE_STATUS && x.RecordCode != "");
                foreach (var item in produces)
                {
                    result.Add(new ProductReport()
                    {
                        Index = (index += 1),
                        RecordCode = item.RecordCode,
                        Amount = item.Amount.ToString(),
                        Direction = item.Direction,
                        UpdatedDate = item.UpdatedDate,
                        UpdatedDateString = item.UpdatedDate.ToString(BHConstant.DATE_FORMAT)
                    });
                }
                result = result.OrderByDescending(x => x.UpdatedDate).ToList();
            }
            return result;
        }

        public List<ProductsReport> GetReportsOfProducts(int productTypeId, int productId, int attrId, int unitId, DateTime from, DateTime to)
        {
            List<ProductsReport> result = new List<ProductsReport>();
            List<ProductDetail> items = new List<ProductDetail>(); 
            if (from > to)
            {
                DateTime d = from;
                from = to;
                to = d;
            }
            using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
            {
                // Products has transaction on period of time
                var produces = context.ProductLogs.Where(x => x.UpdatedDate <= to && x.UpdatedDate >= from && x.RecordCode != "");
                if (unitId > 0)
                    produces = produces.Where(x => x.UnitId == unitId);
                if (attrId > 0)
                    produces = produces.Where(x => x.AttributeId == attrId);
                if (productId > 0)
                    produces = produces.Where(x => x.ProductId == productId);
                if (productTypeId > 0)
                    produces = produces.Where(x => x.Product.ProductType == productTypeId);
                foreach (var item in produces)
                {
                    items.Add(new ProductDetail()
                    {
                        ID = item.Id,
                        UnitId = item.UnitId,
                        AttributeId = item.AttributeId,
                        Direction = item.Direction,
                        ProductId = item.ProductId,
                        ProductTypeId = item.Product.ProductType,
                        Amount = item.Amount,
                        Jampo = item.BaseAttribute.Jampo,
                        CreatedDate = item.UpdatedDate,
                        BeforeNumber = item.BeforeNumber,
                        AfterNUmber = item.AfterNumber,
                        Status = item.Status
                    });
                }

                // Products out period of time
                var old_products = context.ProductLogs.Where(x => x.UpdatedDate < from &&
                    !items.Select(y => y.ProductId.ToString() + '_' +
                            y.AttributeId.ToString() + '_' + y.UnitId.ToString()).Contains(x.ProductId.ToString() + '_' +
                            x.AttributeId.ToString() + '_' + x.UnitId.ToString()))
                            .GroupBy(
                                x => new { x.ProductId, x.AttributeId, x.UnitId },
                                (x, y) => new
                                {
                                    Key = new { x.ProductId, x.AttributeId, x.UnitId },
                                    Value = y.OrderByDescending(z => z.UpdatedDate).First()
                                })
                             .Select(x => x.Value);
                if (unitId > 0)
                    old_products = old_products.Where(x => x.UnitId == unitId);
                if (attrId > 0)
                    old_products = old_products.Where(x => x.AttributeId == attrId);
                if (productId > 0)
                    old_products = old_products.Where(x => x.ProductId == productId);
                if (productTypeId > 0)
                    old_products = old_products.Where(x => x.Product.ProductType == productTypeId);
                foreach (var item in old_products)
                {
                    items.Add(new ProductDetail()
                    {
                        ID = item.Id,
                        UnitId = item.UnitId,
                        AttributeId = item.AttributeId,
                        Direction = item.Direction,
                        ProductId = item.ProductId,
                        ProductTypeId = item.Product.ProductType,
                        Amount = 0,
                        Jampo = item.BaseAttribute.Jampo,
                        CreatedDate = item.UpdatedDate,
                        BeforeNumber = item.AfterNumber,
                        AfterNUmber = item.AfterNumber,
                        Status = item.Status
                    });
                }

                // Start collecting report
                var dits = items.GroupBy(x => new { x.ProductTypeId, x.ProductId, x.AttributeId, x.UnitId });
                List<int> type_distincts = items.GroupBy(x => x.ProductTypeId).Select(x => x.First()).Select(x => x.ProductTypeId).ToList();
                List<ProductType> types = context.ProductTypes.OrderBy(x => x.TypeName).ToList();

                int index = 0;
                foreach (ProductType type in types)
                {
                    if (type_distincts.Contains(type.Id))
                    {
                        List<ProductsReport> type_products = new List<ProductsReport>();
                        type_products.Add(new ProductsReport()
                        {
                            ProductName = type.TypeName
                        });

                        var item_types = items.Where(x => x.ProductTypeId == type.Id);
                        var distincts = item_types.GroupBy(x => new { x.ProductId, x.AttributeId, x.UnitId }).Select(x => x.First());
                        foreach (var item in distincts)
                        {
                            Product p = context.Products.Where(x => x.Id == item.ProductId).First();
                            BaseAttribute b = context.BaseAttributes.Where(x => x.Id == item.AttributeId).First();
                            MeasurementUnit m = context.MeasurementUnits.Where(x => x.Id == item.UnitId).First();

                            ProductsReport pr = new ProductsReport()
                            {
                                ProductCode = p.ProductCode, //p.Id + " - " + b.Id + " - " + m.Id,//p.ProductCode,
                                ProductName = p.ProductName + " - " + b.AttributeName,
                                Jampo = item.Jampo ? "Jampo" : "",
                                UnitName = m.Name,
                                Index = (++index).ToString(),
                                ProductId = p.Id,
                                AttrId = b.Id,
                                UnitId = m.Id
                            };

                            var a = items.Where(x => x.ProductId == item.ProductId && x.AttributeId == item.AttributeId
                                && x.UnitId == item.UnitId).OrderBy(x => x.ID);
                            pr.FirstNumber = a.First().BeforeNumber.ToString();
                            pr.LastNumber = a.Last().AfterNUmber.ToString();
                            pr.ImportNumber = a.Where(x => x.Direction == BHConstant.DIRECTION_IN && x.Status == BHConstant.ACTIVE_STATUS).Sum(x => x.Amount).ToString();
                            pr.ExportNumber = a.Where(x => x.Direction == BHConstant.DIRECTION_OUT && x.Status == BHConstant.ACTIVE_STATUS).Sum(x => x.Amount).ToString();

                            if (!(pr.FirstNumber == "0" && pr.LastNumber == "0" && pr.ImportNumber == "0" && pr.ExportNumber == "0"))
                                type_products.Add(pr);
                        }
                        type_products = type_products.OrderBy(x => x.ProductCode).ThenBy(x => x.Jampo).ToList();
                        result.AddRange(type_products);
                    }
                }
            }
            return result;
        }        
        
        public string GetNameOfProductLog(ProductLog pl)
        {
            ProductLog result = null;
            using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
            {
                result = context.ProductLogs.Where(p => p.ProductId == pl.ProductId && p.AttributeId == pl.AttributeId && p.UnitId == pl.UnitId)
                    .OrderByDescending(c => c.Id).FirstOrDefault();
                return result.Product.ProductName + " " + result.BaseAttribute.AttributeName + " (" + result.MeasurementUnit.Name + ")";
            }
        }
    }
}
