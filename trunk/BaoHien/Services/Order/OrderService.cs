using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;
using System.Linq.Expressions;

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

            return orders;
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
    }
}
