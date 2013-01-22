using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;
using DAL.Helper;
using BaoHien.Model;
using BaoHien.Common;

namespace BaoHien.Services.Customers
{
    class CustomerLogService : BaseService<CustomerLog>
    {
        public CustomerLog GetCustomerLog(System.Int32 id)
        {
            CustomerLog customerLog = OnGetItem<CustomerLog>(id.ToString());
            return customerLog;
        }

        public List<CustomerLog> GetCustomerLogs()
        {
            List<CustomerLog> customerLogs = OnGetItems<CustomerLog>();
            return customerLogs;
        }

        public bool AddCustomerLog(CustomerLog customerLog)
        {
            return OnAddItem<CustomerLog>(customerLog);
        }

        public bool DeleteCustomerLog(System.Int32 id)
        {
            return OnDeleteItem<CustomerLog>(id.ToString());
        }

        public bool UpdateCustomerLog(CustomerLog customerLog)
        {
            return OnUpdateItem<CustomerLog>(customerLog, customerLog.Id.ToString());
        }

        public CustomerLog GetNewestCustomerLog(int customerId)
        {
            CustomerLog result = null;
            using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
            {
                result = context.CustomerLogs.Where(c => c.CustomerId == customerId)
                    .OrderByDescending(c => c.CreatedDate).FirstOrDefault();
            }
            return result;
        }

        public List<CustomerLog> GetLogsOfCustomer(int customerId, DateTime from, DateTime to)
        {
            List<CustomerLog> result = new List<CustomerLog>();
            using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
            {
                result = context.CustomerLogs
                    .Where(c => c.CustomerId == customerId && c.CreatedDate >= from && c.CreatedDate <= to).ToList();
            }
            return result;
        }

        public List<CustomerLog> GetLogsOfCustomers(DateTime from, DateTime to)
        {
            List<CustomerLog> result = new List<CustomerLog>();
            using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
            {
                result = context.CustomerLogs.Where(c => c.CreatedDate >= from && c.CreatedDate <= to)
                    .OrderByDescending(c => c.CreatedDate)
                    .GroupBy(c => c.CustomerId).Select(c => c.First()).ToList();
            }
            return result;
        }

        public List<ArrearReportModel> GetReportsOfCustomer(int customerId, DateTime from, DateTime to)
        {
            List<ArrearReportModel> result = new List<ArrearReportModel>();
            using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
            {
                List<CustomerLog> logs = context.CustomerLogs
                    .Where(c => c.CustomerId == customerId && c.CreatedDate >= from && c.CreatedDate <= to).ToList();
                ConvertLogToReport(logs, ref result);
            }
            return result;
        }

        public List<ArrearReportModel> GetReportsOfCustomers(DateTime from, DateTime to)
        {
            List<ArrearReportModel> result = new List<ArrearReportModel>();
            using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
            {
                List<CustomerLog> logs = context.CustomerLogs.Where(c => c.CreatedDate >= from && c.CreatedDate <= to)
                    .OrderByDescending(c => c.CreatedDate)
                    .GroupBy(c => c.CustomerId).Select(c => c.First()).ToList();
                ConvertLogToReport(logs, ref result);
            }
            return result;
        }

        private void ConvertLogToReport(List<CustomerLog> logs, ref List<ArrearReportModel> reports)
        {
            int index = 0;
            foreach (CustomerLog log in logs)
            {
                reports.Add(new ArrearReportModel
                {
                    ID = log.Id,
                    CustomerName = log.Customer.CustomerName,
                    Date = log.CreatedDate.ToString(BHConstant.DATETIME_FORMAT),
                    Amount = Global.formatVNDCurrencyText((log.AfterDebit - log.BeforeDebit).ToString()),
                    AfterDebit = Global.formatVNDCurrencyText(log.AfterDebit.ToString()),
                    AfterDebitNumber = log.AfterDebit,
                    RecordCode = log.RecordCode,
                    Index = ++index
                });
            }
        }
    }
}
