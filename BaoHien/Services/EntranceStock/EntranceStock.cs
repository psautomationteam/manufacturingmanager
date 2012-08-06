using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;

namespace BaoHien.Services
{
    public class EntranceStockService : BaseService<EntranceStock>
    {
        public EntranceStock GetEntranceStock(System.Int32 id)
        {
            EntranceStock entranceStock = OnGetItem<EntranceStock>(id.ToString());

            return entranceStock;
        }
        public List<EntranceStock> GetEntranceStocks()
        {
            List<EntranceStock> entranceStocks = OnGetItems<EntranceStock>();

            return entranceStocks;
        }
        public bool AddEntranceStock(EntranceStock entranceStock)
        {
            return OnAddItem<EntranceStock>(entranceStock);
        }
        public bool DeleteEntranceStock(System.Int32 id)
        {
            return OnDeleteItem<EntranceStock>(id.ToString());
        }
        public bool UpdateEntranceStock(EntranceStock entranceStock)
        {
            return OnUpdateItem<EntranceStock>(entranceStock, entranceStock.Id.ToString());
        }
    }
}
