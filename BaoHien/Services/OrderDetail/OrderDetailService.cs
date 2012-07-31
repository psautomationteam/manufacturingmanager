using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;
using System.Linq.Expressions;
using BaoHien.Model;

namespace BaoHien.Services.OrderDetails
{
    public class OrderDetailService : BaseService<OrderDetail>
    {
        public OrderDetail GetOrderDetail(System.Int32 id)
        {
            OrderDetail orderDetail = OnGetItem<OrderDetail>(id.ToString());

            return orderDetail;
        }
        public List<OrderDetail> GetOrderDetails()
        {
            List<OrderDetail> orderDetails = OnGetItems<OrderDetail>();

            return orderDetails;
        }
        public bool AddOrderDetail(OrderDetail orderDetail)
        {
            return OnAddItem<OrderDetail>(orderDetail);
        }
        public bool DeleteOrderDetail(System.Int32 id)
        {
            return OnDeleteItem<OrderDetail>(id.ToString());
        }
        public bool UpdateOrderDetail(OrderDetail orderDetail)
        {
            return OnUpdateItem<OrderDetail>(orderDetail, orderDetail.Id.ToString());
        }
        public List<OrderDetail> SelectOrderDetailByWhere(Expression<Func<OrderDetail, bool>> func)
        {

            return SelectItemByWhere<OrderDetail>(func);
        }
        
    }
}
