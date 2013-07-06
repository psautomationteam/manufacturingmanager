using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                                 .GroupBy(p => p.UnitId).Select(p => p.First()).ToList();
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
            List<ProductLog> logs = GetProductLogs(productId, attrId, unitId);
            result = logs.FirstOrDefault();
            return result;
        }

        public ProductLog GetNewestProductLog(int productId, int attrId)
        {
            ProductLog result = null;
            using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
            {
                result = context.ProductLogs.Where(p => p.ProductId == productId && p.AttributeId == attrId)
                    .OrderByDescending(c => c.UpdatedDate).FirstOrDefault();
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
            DateTime dt = new DateTime(2013, 7, 1);
            using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
            {
                var produces = context.EntranceStockDetails.Join(context.EntranceStocks,
                    child => child.EntranceStockId, parent => parent.Id,
                    (child, parent) => new { EntranceStockDetail = child, EntranceStock = parent }).Where(x => x.EntranceStock.CreatedDate >= from
                    && x.EntranceStock.CreatedDate <= to && x.EntranceStock.Status == null && x.EntranceStock.CreatedDate > dt &&
                    x.EntranceStockDetail.ProductId == productId && x.EntranceStockDetail.AttributeId == attrId && x.EntranceStockDetail.UnitId == unitId);
                foreach (var item in produces)
                {
                    result.Add(new ProductReport()
                    {
                        Index = (index += 1),
                        RecordCode = item.EntranceStock.EntranceCode,
                        Amount = item.EntranceStockDetail.NumberUnit.ToString(),
                        Direction = BHConstant.DIRECTION_IN,
                        UpdatedDate = item.EntranceStock.CreatedDate,
                        UpdatedDateString = item.EntranceStock.CreatedDate.ToString(BHConstant.DATE_FORMAT)
                    });
                }
                var imports = context.ProductionRequestDetails.Join(context.ProductionRequests,
                    child => child.ProductionRequestId, parent => parent.Id,
                    (child, parent) => new { ProductionRequestDetail = child, ProductionRequest = parent }).Where(x => x.ProductionRequest.CreatedDate >= from
                    && x.ProductionRequest.CreatedDate <= to && x.ProductionRequest.Status == null && x.ProductionRequest.CreatedDate > dt &&
                    x.ProductionRequestDetail.ProductId == productId && x.ProductionRequestDetail.AttributeId == attrId && x.ProductionRequestDetail.UnitId == unitId);
                foreach (var item in imports)
                {
                    result.Add(new ProductReport()
                    {
                        Index = (index += 1),
                        RecordCode = item.ProductionRequest.ReqCode,
                        Amount = item.ProductionRequestDetail.NumberUnit.ToString(),
                        Direction = item.ProductionRequestDetail.Direction,
                        UpdatedDate = item.ProductionRequest.CreatedDate,
                        UpdatedDateString = item.ProductionRequest.CreatedDate.ToString(BHConstant.DATE_FORMAT)
                    });
                }
                var orders = context.OrderDetails.Join(context.Orders,
                    child => child.OrderId, parent => parent.Id,
                    (child, parent) => new { OrderDetail = child, Order = parent }).Where(x => x.Order.CreatedDate >= from
                    && x.Order.CreatedDate <= to && x.Order.Status == null && x.Order.CreatedDate > dt && 
                    x.OrderDetail.ProductId == productId && x.OrderDetail.AttributeId == attrId && x.OrderDetail.UnitId == unitId);
                foreach (var item in orders)
                {
                    result.Add(new ProductReport()
                    {
                        Index = (index += 1),
                        RecordCode = item.Order.OrderCode,
                        Amount = item.OrderDetail.NumberUnit.ToString(),
                        Direction = BHConstant.DIRECTION_OUT,
                        UpdatedDate = item.Order.CreatedDate,
                        UpdatedDateString = item.Order.CreatedDate.ToString(BHConstant.DATE_FORMAT)
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
            DateTime dt = new DateTime(2013, 7, 1);
            using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
            {
                var produces = context.EntranceStockDetails.Join(context.EntranceStocks,
                    child => child.EntranceStockId, parent => parent.Id,
                    (child, parent) => new { EntranceStockDetail = child, EntranceStock = parent }).Where(x =>
                    x.EntranceStock.CreatedDate <= to && x.EntranceStock.CreatedDate > dt && x.EntranceStock.Status == null);
                if (unitId > 0)
                    produces = produces.Where(x => x.EntranceStockDetail.UnitId == unitId);
                if (attrId > 0)
                    produces = produces.Where(x => x.EntranceStockDetail.AttributeId == attrId);
                if (productId > 0)
                    produces = produces.Where(x => x.EntranceStockDetail.ProductId == productId);
                if (productTypeId > 0)
                    produces = produces.Where(x => x.EntranceStockDetail.Product.ProductType == productTypeId);
                foreach (var item in produces)
                {
                    items.Add(new ProductDetail()
                    {
                        UnitId = item.EntranceStockDetail.UnitId,
                        AttributeId = item.EntranceStockDetail.AttributeId,
                        Direction = BHConstant.DIRECTION_IN,
                        ProductId = item.EntranceStockDetail.ProductId,
                        ProductTypeId = item.EntranceStockDetail.Product.ProductType,
                        Amount = item.EntranceStockDetail.NumberUnit,
                        Jampo = item.EntranceStockDetail.BaseAttribute.Jampo,
                        CreatedDate = item.EntranceStock.CreatedDate
                    });
                }
                var imports = context.ProductionRequestDetails.Join(context.ProductionRequests,
                    child => child.ProductionRequestId, parent => parent.Id,
                    (child, parent) => new { ProductionRequestDetail = child, ProductionRequest = parent }).Where(x =>
                    x.ProductionRequest.CreatedDate <= to && x.ProductionRequest.CreatedDate > dt && x.ProductionRequest.Status == null);
                if (unitId > 0)
                    imports = imports.Where(x => x.ProductionRequestDetail.UnitId == unitId);
                if (attrId > 0)
                    imports = imports.Where(x => x.ProductionRequestDetail.AttributeId == attrId);
                if (productId > 0)
                    imports = imports.Where(x => x.ProductionRequestDetail.ProductId == productId);
                if (productTypeId > 0)
                    imports = imports.Where(x => x.ProductionRequestDetail.Product.ProductType == productTypeId);
                foreach (var item in imports)
                {
                    items.Add(new ProductDetail()
                    {
                        UnitId = item.ProductionRequestDetail.UnitId,
                        AttributeId = item.ProductionRequestDetail.AttributeId,
                        Direction = item.ProductionRequestDetail.Direction,
                        ProductId = item.ProductionRequestDetail.ProductId,
                        ProductTypeId = item.ProductionRequestDetail.Product.ProductType,
                        Amount = item.ProductionRequestDetail.NumberUnit,
                        Jampo = item.ProductionRequestDetail.BaseAttribute.Jampo,
                        CreatedDate = item.ProductionRequest.CreatedDate
                    });
                }
                var orders = context.OrderDetails.Join(context.Orders,
                    child => child.OrderId, parent => parent.Id,
                    (child, parent) => new { OrderDetail = child, Order = parent }).Where(x =>
                    x.Order.CreatedDate <= to && x.Order.CreatedDate > dt && x.Order.Status == null);
                if (unitId > 0)
                    orders = orders.Where(x => x.OrderDetail.UnitId == unitId);
                if (attrId > 0)
                    orders = orders.Where(x => x.OrderDetail.AttributeId == attrId);
                if (productId > 0)
                    orders = orders.Where(x => x.OrderDetail.ProductId == productId);
                if (productTypeId > 0)
                    orders = orders.Where(x => x.OrderDetail.Product.ProductType == productTypeId);
                foreach (var item in orders)
                {
                    items.Add(new ProductDetail()
                    {
                        UnitId = item.OrderDetail.UnitId,
                        AttributeId = item.OrderDetail.AttributeId,
                        Direction = BHConstant.DIRECTION_OUT,
                        ProductId = item.OrderDetail.ProductId,
                        ProductTypeId = item.OrderDetail.Product.ProductType,
                        Amount = item.OrderDetail.NumberUnit,
                        Jampo = item.OrderDetail.BaseAttribute.Jampo,
                        CreatedDate = item.Order.CreatedDate
                    });
                }

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
                                ProductCode = p.ProductCode,
                                ProductName = p.ProductName + " - " + b.AttributeName,
                                Jampo = item.Jampo ? "Jampo" : "",
                                UnitName = m.Name,
                                Index = (++index).ToString()
                            };

                            var a = items.Where(x => x.ProductId == item.ProductId && x.AttributeId == item.AttributeId
                                && x.UnitId == item.UnitId);
                            var a1 = a.Where(x => x.CreatedDate < from);
                            pr.FirstNumber = a1.Sum(x => (x.Direction == BHConstant.DIRECTION_IN ? 1 : -1) * x.Amount).ToString();
                            pr.LastNumber = a.Sum(x => (x.Direction == BHConstant.DIRECTION_IN ? 1 : -1) * x.Amount).ToString();
                            var a2 = a.Where(x => x.CreatedDate >= from);
                            pr.ImportNumber = a2.Where(x => x.Direction == BHConstant.DIRECTION_IN).Sum(x => x.Amount).ToString();
                            pr.ExportNumber = a2.Where(x => x.Direction == BHConstant.DIRECTION_OUT).Sum(x => x.Amount).ToString();

                            type_products.Add(pr);
                        }
                        type_products = type_products.OrderBy(x => x.ProductCode).ThenBy(x => x.Jampo).ToList();
                        result.AddRange(type_products);
                    }
                }
            }
            return result;
        }        
    }
}
