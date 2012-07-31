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
        public List<Order> SearchingOrder(OrderSearchCriteria productionRequestSearchCriteria)
        {
            List<Order> orders = OnGetItems<Order>();

            if (productionRequestSearchCriteria != null)
            {
                if (productionRequestSearchCriteria.CreatedBy.HasValue)
                {
                    orders = orders.Where(pr => pr.CreateBy == productionRequestSearchCriteria.CreatedBy.Value).ToList();
                }
                if (productionRequestSearchCriteria.Code != "")
                {
                    orders = orders.Where(pr => pr.OrderCode.Contains(productionRequestSearchCriteria.Code)).ToList();
                }
                if (productionRequestSearchCriteria.To.HasValue && productionRequestSearchCriteria.From.HasValue)
                {
                    orders = orders.
                        Where(pr => pr.CreatedDate.CompareTo(productionRequestSearchCriteria.From.Value) >= 0
                            && pr.CreatedDate.CompareTo(productionRequestSearchCriteria.To.Value) <= 0)
                            .ToList();
                }
            }
            else
            {
                return orders;
            }
            /*
            IQueryable<ProductionRequest> query = null;
            BaoHienDBDataContext context = BaoHienRepository.GetBaoHienDBDataContext();
            if (context != null)
            {
                query = from p in context.Products
                        join pt in context.ProductTypes on p.ProductType equals pt.Id
                        where (p.Status == null) && (productSearchCriteria.ProductTypeId == null || pt.Id == productSearchCriteria.ProductTypeId)
                        select p;
            }
            if (productSearchCriteria.ProductCode != null)
            {
                query = query.Where(p => p.ProductCode.Contains(productSearchCriteria.ProductCode));
            }
            if (productSearchCriteria.ProductName != null)
            {
                query = query.Where(p => p.ProductName.Contains(productSearchCriteria.ProductName));
            }
            if (productSearchCriteria.PurchaseStatus != null)
            {

            }
            if (query != null)
                return query.ToList();*/
            return orders;
        }
    }
}
