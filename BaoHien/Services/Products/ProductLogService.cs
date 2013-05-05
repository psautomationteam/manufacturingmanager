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

        public ProductLog GetNewestProductUnitLog(int productId, int attrId, int unitId)
        {
            ProductLog result = null;
            using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
            {
                result = context.ProductLogs.Where(p => p.ProductId == productId && p.AttributeId == attrId
                    && p.UnitId == unitId).OrderByDescending(c => c.CreatedDate).FirstOrDefault();
            }
            if (result == null)
                result = new ProductLog
                {
                    Id = 0,
                    ProductId = 0,
                    AttributeId = 0,
                    UnitId = 0,
                    AfterNumber = 0,
                    Amount = 0,
                    BeforeNumber = 0
                };
            return result;
        }

        public ProductLog GetNewestProductLog(int productId, int attrId)
        {
            ProductLog result = null;
            using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
            {
                result = context.ProductLogs.Where(p => p.ProductId == productId && p.AttributeId == attrId)
                    .OrderByDescending(c => c.CreatedDate).FirstOrDefault();
            }
            if (result == null)
                result = new ProductLog
                {
                    Id = 0,
                    ProductId = 0,
                    AttributeId = 0,
                    UnitId = 0,
                    AfterNumber = 0,
                    Amount = 0,
                    BeforeNumber = 0
                };
            return result;
        }

        public List<ProductReport> GetReportsOfProductAttributeUnit(int productId, int attrId, int unitId, DateTime from, DateTime to)
        {
            List<ProductReport> result = new List<ProductReport>();
            using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
            {
                List<ProductLog> logs = context.ProductLogs.Where(p => p.ProductId == productId && p.AttributeId == attrId &&
                    p.UnitId == unitId && p.CreatedDate >= from && p.CreatedDate <= to && p.Status == BHConstant.ACTIVE_STATUS)
                    .OrderByDescending(p => p.CreatedDate).ToList();
                ConvertLogToReportDetail(logs, ref result);
            }
            return result;
        }

        public List<ProductsReport> GetReportsOfProducts(int productTypeId, int productId, int attrId, int unitId, DateTime from, DateTime to)
        {
            List<ProductsReport> result = new List<ProductsReport>();
            using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
            {
                List<ProductLog> logs = context.ProductLogs.Where(x => x.CreatedDate >= from && x.CreatedDate <= to)
                    .OrderBy(pl => pl.CreatedDate).ToList();
                if (unitId > 0)
                    logs = logs.Where(x => x.UnitId == unitId).ToList();
                if (attrId > 0)
                    logs = logs.Where(x => x.AttributeId == attrId).ToList();
                if (productId > 0)
                    logs = logs.Where(x => x.ProductId == productId).ToList();
                if (productTypeId > 0)
                    logs = logs.Where(x => x.Product.ProductType == productTypeId).ToList();
                List<ProductType> types = context.ProductTypes.Where(x => logs.Select(y => y.Product.ProductType).Contains(x.Id)).ToList();
                ConvertLogToReport(logs, types, ref result);
            }
            return result;
        }
        
        private void ConvertLogToReport(List<ProductLog> logs, List<ProductType> types, ref List<ProductsReport> reports)
        {
            int index = 0;
            foreach (ProductType type in types)
            {
                reports.Add(new ProductsReport
                {
                     ProductName = type.ProductName
                });

                List<ProductLog> tmp = logs.Where(x => x.Product.ProductType == type.Id && x.Status == BHConstant.ACTIVE_STATUS)
                    .GroupBy(x => new { x.ProductId, x.AttributeId, x.UnitId }).Select(x => x.First()).ToList();
                index = 0;
                foreach (ProductLog item in tmp)
                {
                    ProductsReport pr = new ProductsReport
                    {
                        ProductCode = item.Product.ProductCode,
                        ProductName = item.Product.ProductName + " - " + item.BaseAttribute.AttributeName,
                        UnitName = item.MeasurementUnit.Name,
                        FirstNumber = item.BeforeNumber.ToString(),
                        Index = (++index).ToString()
                    };

                    pr.LastNumber = logs.Where(x => x.ProductId == item.ProductId && x.AttributeId == item.AttributeId && x.UnitId == item.UnitId)
                        .OrderByDescending(x => x.CreatedDate).First().AfterNumber.ToString();
                    pr.ImportNumber = logs.Where(x => x.ProductId == item.ProductId && x.AttributeId == item.AttributeId && x.UnitId == item.UnitId
                        && x.Status == BHConstant.ACTIVE_STATUS && x.BeforeNumber < x.AfterNumber).Sum(x => x.Amount).ToString();
                    pr.ExportNumber = logs.Where(x => x.ProductId == item.ProductId && x.AttributeId == item.AttributeId && x.UnitId == item.UnitId
                        && x.Status == BHConstant.ACTIVE_STATUS && x.BeforeNumber > x.AfterNumber).Sum(x => x.Amount).ToString();
                    reports.Add(pr);
                }
            }
        }

        private void ConvertLogToReportDetail(List<ProductLog> logs, ref List<ProductReport> reports)
        {
            int index = 0;
            foreach (ProductLog log in logs)
            {
                reports.Add(new ProductReport
                {
                    AfterNumber = log.AfterNumber.ToString(),
                    BeforeNumber = log.BeforeNumber.ToString(),
                    Amount = log.Amount.ToString(),
                    RecordCode = log.RecordCode,
                    CreatedDate = log.CreatedDate.ToString(BHConstant.DATE_FORMAT),
                    Index = ++index
                });
            }
        }
    }
}
