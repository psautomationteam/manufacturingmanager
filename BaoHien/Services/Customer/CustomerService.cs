using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;

namespace BaoHien.Services.Customers
{
    public class CustomerService : BaseService<Customer>
    {
        public Customer GetCustomer(System.Int32 id)
        {
            Customer customer = OnGetItem<Customer>(id.ToString());

            return customer;
        }
        public List<Customer> GetCustomers()
        {
            List<Customer> customers = OnGetItems<Customer>();

            return customers;
        }
        public bool AddCustomer(Customer customer)
        {
            return OnAddItem<Customer>(customer);
        }
        public bool DeleteCustomer(System.Int32 id)
        {
            return OnDeleteItem<Customer>(id.ToString());
        }
        public bool UpdateCustomer(Customer customer)
        {
            return OnUpdateItem<Customer>(customer, customer.Id.ToString());
        }
    }
}
