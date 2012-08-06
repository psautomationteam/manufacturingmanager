using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;
using System.Linq.Expressions;

namespace BaoHien.Services.ProductInStocks
{
    public class EntranceStockDetailService : BaseService<EntranceStockDetail>
    {
        public EntranceStockDetail GetEntranceStockDetail(System.Int32 id)
        {
            EntranceStockDetail entranceStockDetail = OnGetItem<EntranceStockDetail>(id.ToString());

            return entranceStockDetail;
        }
        public List<EntranceStockDetail> GetEntranceStockDetails()
        {
            List<EntranceStockDetail> entranceStockDetails = OnGetItems<EntranceStockDetail>();

            return entranceStockDetails;
        }
        public bool AddEntranceStockDetail(EntranceStockDetail entranceStockDetail)
        {
            return OnAddItem<EntranceStockDetail>(entranceStockDetail);
        }
        public bool DeleteEntranceStockDetail(System.Int32 id)
        {
            return OnDeleteItem<EntranceStockDetail>(id.ToString());
        }
        public bool UpdateEntranceStockDetail(EntranceStockDetail entranceStockDetail)
        {
            return OnUpdateItem<EntranceStockDetail>(entranceStockDetail, entranceStockDetail.Id.ToString());
        }
        public List<EntranceStockDetail> SelectEntranceStockDetailByWhere(Expression<Func<EntranceStockDetail, bool>> func)
        {

            return SelectItemByWhere<EntranceStockDetail>(func);
        }
    }
}
