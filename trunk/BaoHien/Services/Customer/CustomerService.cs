using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;
using BaoHien.Model;

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
            return customers.OrderBy(x => x.CustomerName).ToList();
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
        
        public List<Customer> SearchingCustomer(CustomerSearchCriteria search)
        {
            List<Customer> customers = OnGetItems<Customer>();
            if (search != null)
            {
                if (search.Saler.HasValue)
                {
                    customers = customers.Where(pr => pr.SalerId == search.Saler.Value).ToList();
                }
                if (search.Code != "")
                {
                    customers = customers.Where(pr => pr.CustCode.Contains(search.Code)).ToList();
                }
                if (search.Phone != "")
                {
                    customers = customers.Where(pr => pr.Phone.Contains(search.Phone)).ToList();
                }
                if (search.Name != "")
                {
                    customers = customers.Where(pr => pr.CustomerName.Contains(search.Name)).ToList();
                }
                if (search.FavorProduct != "")
                {
                    customers = customers.Where(pr => pr.FavoriteProduct.Contains(search.FavorProduct)).ToList();
                }
            }
            return customers.OrderBy(x => x.CustomerName).ToList();
        }
    }
}
