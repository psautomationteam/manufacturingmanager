using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaoHien.Services.Base;
using DAL;
using DAL.Helper;
using BaoHien.Model;
using BaoHien.Common;

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
                    .Where(c => c.EmployeeId == employeeId && c.CreatedDate >= from && c.CreatedDate <= to)
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
                    .Where(c => c.CreatedDate >= from && c.CreatedDate <= to).OrderByDescending(el => el.CreatedDate)
                    .GroupBy(c => c.EmployeeId).Select(el => el.First()).ToList();
            }
            return result;
        }

        public List<EmployeeReport> GetReportsOfEmployee(int employeeId, DateTime from, DateTime to)
        {
            List<EmployeeReport> result = new List<EmployeeReport>();
            using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
            {
                List<EmployeeLog> logs = context.EmployeeLogs
                    .Where(c => c.EmployeeId == employeeId && c.CreatedDate >= from && c.CreatedDate <= to)
                    .OrderByDescending(c => c.CreatedDate).ToList();
                ConvertEmployeeLogsToReports(logs, ref result);
            }
            return result;
        }

        public List<EmployeeReport> GetReportsOfEmployees(DateTime from, DateTime to)
        {
            List<EmployeeReport> result = new List<EmployeeReport>();
            using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
            {
                List<EmployeeLog> logs = context.EmployeeLogs
                    .Where(c => c.CreatedDate >= from && c.CreatedDate <= to).OrderByDescending(el => el.CreatedDate)
                    .GroupBy(c => c.EmployeeId).Select(el => el.First()).ToList();
                ConvertEmployeeLogsToReports(logs, ref result);
            }
            return result;
        }

        private void ConvertEmployeeLogsToReports(List<EmployeeLog> logs, ref List<EmployeeReport> reports)
        {
            int index = 0;
            foreach (EmployeeLog log in logs)
            {
                reports.Add(new EmployeeReport
                {
                    ID = log.Id,
                    EmployeeName = log.Employee.FullName,
                    RecordCode = log.RecordCode,
                    BeforeNumber = log.BeforeNumber.ToString(),
                    Amount = Global.formatVNDCurrencyText((log.AfterNumber - log.BeforeNumber).ToString()),
                    AfterNumberText = Global.formatVNDCurrencyText(log.AfterNumber.ToString()),
                    AfterNumber = log.AfterNumber,
                    CreatedDate = log.CreatedDate,
                    Index = ++index
                });
            }
        }
    }
}
