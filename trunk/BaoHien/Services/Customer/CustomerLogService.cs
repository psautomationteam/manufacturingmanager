using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;
using DAL.Helper;

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
    }
}
