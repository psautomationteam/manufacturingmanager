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

namespace BaoHien.Services.Employees
{
    class EmployeeLogService : BaseService<EmployeeLog>
    {
        public EmployeeLog GetEmployeeLog(System.Int32 id)
        {
            EmployeeLog employeeLog = OnGetItem<EmployeeLog>(id.ToString());
            return employeeLog;
        }

        public List<EmployeeLog> GetEmployeeLogs()
        {
            List<EmployeeLog> employeeLogs = OnGetItems<EmployeeLog>();
            return employeeLogs;
        }

        public bool AddEmployeeLog(EmployeeLog employeeLog)
        {
            return OnAddItem<EmployeeLog>(employeeLog);
        }

        public bool DeleteEmployeeLog(System.Int32 id)
        {
            return OnDeleteItem<EmployeeLog>(id.ToString());
        }

        public bool UpdateEmployeeLog(EmployeeLog employeeLog)
        {
            return OnUpdateItem<EmployeeLog>(employeeLog, employeeLog.Id.ToString());
        }

        public EmployeeLog GetNewestEmployeeLog(int employeeId)
        {
            EmployeeLog result = null;
            using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
            {
                result = context.EmployeeLogs.Where(c => c.EmployeeId == employeeId)
                    .OrderByDescending(c => c.CreatedDate).FirstOrDefault();
            } 
            if (result == null)
                result = new EmployeeLog
                {
                    Id = 0,
                    EmployeeId = 0,
                    AfterNumber = 0,
                    Amount = 0,
                    BeforeNumber = 0
                };
            return result;
        }

        public List<EmployeeLog> GetLogsOfEmployee(int employeeId, DateTime from, DateTime to)
        {
            List<EmployeeLog> result = new List<EmployeeLog>();
            using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
            {
                result = context.EmployeeLogs
                    .Where(c => c.EmployeeId == employeeId && c.CreatedDate >= from && c.CreatedDate <= to && c.Status == BHConstant.ACTIVE_STATUS)
                    .OrderByDescending(c => c.CreatedDate).ToList();
            }
            return result;
        }

        public List<EmployeeLog> GetLogsOfEmployees(DateTime from, DateTime to)
        {
            List<EmployeeLog> result = new List<EmployeeLog>();
            using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
            {
                result = context.EmployeeLogs
                    .Where(c => c.CreatedDate >= from && c.CreatedDate <= to && c.Status == BHConstant.ACTIVE_STATUS)
                    .OrderByDescending(el => el.CreatedDate)
                    .GroupBy(c => c.EmployeeId).Select(el => el.First()).ToList();
            }
            return result;
        }

        public List<EmployeeReport> GetReportsOfEmployee(int employeeId, DateTime from, DateTime to, ref double total)
        {
            List<EmployeeReport> result = new List<EmployeeReport>();
            using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
            {
                List<EmployeeLog> logs = context.EmployeeLogs
                    .Where(c => c.EmployeeId == employeeId && c.CreatedDate >= from && c.CreatedDate <= to && c.Status == BHConstant.ACTIVE_STATUS)
                    .OrderByDescending(c => c.CreatedDate).ToList();
                List<Order> orders = context.Orders.Where(x => logs.Select(y => y.RecordCode).Contains(x.OrderCode)).ToList();
                List<OrderDetail> details = context.OrderDetails.Where(x => orders.Select(y => y.Id).Contains(x.OrderId)).ToList();
                int curr_month = from.Month, curr_year = from.Year, month = curr_month, year = curr_year, index = 0;
                result.Add(new EmployeeReport
                        {
                            CustomerName = "Tháng " + curr_month.ToString() + "/" + curr_year.ToString()
                        });
                foreach (Order item in orders)
                {
                    month = item.CreatedDate.Month;
                    year = item.CreatedDate.Year;
                    if (month != curr_month || year != curr_year)
                    {
                        curr_month = month;
                        curr_year = year;
                        result.Add(new EmployeeReport
                            {
                                CustomerName = "Tháng " + curr_month.ToString() + "/" + curr_year.ToString()
                            });
                    }
                    List<OrderDetail> details_of_order = details.Where(x => x.OrderId == item.Id).ToList();
                    for (int i = 0; i < details_of_order.Count; i++)
                    {
                        result.Add(new EmployeeReport
                            {
                                Index = (++index).ToString(),
                                Date = i == 0 ? item.CreatedDate.ToString(BHConstant.DATE_FORMAT) : "",
                                CustomerName = item.Customer.CustomerName,
                                ProductName = details_of_order[i].Product.ProductName,
                                AttrName = details_of_order[i].BaseAttribute.AttributeName,
                                Number = details_of_order[i].NumberUnit.ToString(),
                                Unit = details_of_order[i].MeasurementUnit.Name,
                                Cost = Global.formatCurrencyTextWithoutMask(details_of_order[i].Cost.ToString()),
                                Commission = Global.formatCurrencyTextWithoutMask(details_of_order[i].Commission.ToString()),
                                RecordCode = item.OrderCode,
                            });
                        total += details_of_order[i].Commission;
                    }
                }
            }
            return result;
        }

        public List<EmployeesReport> GetReportsOfEmployees(DateTime from, DateTime to, ref double total)
        {
            List<EmployeesReport> result = new List<EmployeesReport>();
            using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
            {
                List<EmployeeLog> logs = context.EmployeeLogs
                    .Where(c => c.CreatedDate >= from && c.CreatedDate <= to && c.Status == BHConstant.ACTIVE_STATUS)
                    .OrderBy(x => x.CreatedDate).ToList();
                ConvertEmployeeLogsToReports(logs, ref result, ref total);
            }
            return result;
        }

        private void ConvertEmployeeLogsToReports(List<EmployeeLog> logs, ref List<EmployeesReport> reports, ref double total)
        {
            int index = 0;
            foreach (EmployeeLog log in logs)
            {
                reports.Add(new EmployeesReport
                {
                    EmployeeName = log.Employee.FullName,
                    RecordCode = log.RecordCode,
                    Amount = Global.formatVNDCurrencyText(log.Amount.ToString()),
                    CreatedDate = log.CreatedDate.ToString(BHConstant.DATE_FORMAT),
                    Index = ++index
                });
                total += log.Amount;
            }
        }

        public List<EmployeeLog> SelectEmployeeLogByWhere(Expression<Func<EmployeeLog, bool>> func)
        {
            return SelectItemByWhere<EmployeeLog>(func);
        }
    }
}
