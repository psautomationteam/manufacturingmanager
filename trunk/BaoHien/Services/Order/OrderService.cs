using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;
using System.Linq.Expressions;
using BaoHien.Model;

namespace BaoHien.Services.Orders
{
    public class OrderService : BaseService<Order>
    {
        public Order GetOrder(System.Int32 id)
        {
            Order order = OnGetItem<Order>(id.ToString());
            return order;
        }

        public List<Order> GetOrders()
        {
            List<Order> orders = OnGetItems<Order>();
            return orders.OrderByDescending(x => x.CreatedDate).ToList();
        }

        public bool AddOrder(Order order)
        {
            return OnAddItem<Order>(order);
        }

        public bool DeleteOrder(System.Int32 id)
        {
            return OnDeleteItem<Order>(id.ToString());
        }

        public bool UpdateOrder(Order order)
        {
            return OnUpdateItem<Order>(order, order.Id.ToString());
        }

        public List<Order> SelectOrderByWhere(Expression<Func<Order, bool>> func)
        {
            return SelectItemByWhere<Order>(func);
        }

        public List<Order> SearchingOrder(OrderSearchCriteria productionRequestSearchCriteria)
        {
            List<Order> orders = OnGetItems<Order>();
            if (productionRequestSearchCriteria != null)
            {
                if (productionRequestSearchCriteria.CreatedBy.HasValue)
                {
                    orders = orders.Where(pr => pr.CreateBy == productionRequestSearchCriteria.CreatedBy.Value).ToList();
                }
                if (productionRequestSearchCriteria.Customer.HasValue)
                {
                    orders = orders.Where(pr => pr.CustId == productionRequestSearchCriteria.Customer.Value).ToList();
                }
                if (productionRequestSearchCriteria.Code != "")
                {
                    orders = orders.Where(pr => pr.OrderCode.ToLower().Contains(productionRequestSearchCriteria.Code)).ToList();
                }
                if (productionRequestSearchCriteria.To.HasValue && productionRequestSearchCriteria.From.HasValue)
                {
                    orders = orders.
                        Where(pr => pr.CreatedDate.CompareTo(productionRequestSearchCriteria.From.Value) >= 0
                            && pr.CreatedDate.CompareTo(productionRequestSearchCriteria.To.Value) <= 0)
                            .ToList();
                }
            }
            return orders;
        }
    }
}
