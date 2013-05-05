using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;
using BaoHien.Model;

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
            return entranceStocks.OrderByDescending(x => x.EntrancedDate).ToList();
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

        public List<EntranceStock> SearchingEntranceStock(EntranceStockSearchCriteria entranceStockSearchCriteria)
        {
            List<EntranceStock> entranceStocks = OnGetItems<EntranceStock>();

            if (entranceStockSearchCriteria != null)
            {
                if (entranceStockSearchCriteria.CreatedBy.HasValue)
                {
                    entranceStocks = entranceStocks.Where(pr => pr.EntrancedBy == entranceStockSearchCriteria.CreatedBy.Value).ToList();
                }
                if (entranceStockSearchCriteria.Code != "")
                {
                    entranceStocks = entranceStocks.Where(pr => pr.EntranceCode.ToLower().Contains(entranceStockSearchCriteria.Code)).ToList();
                }
                if (entranceStockSearchCriteria.To.HasValue && entranceStockSearchCriteria.From.HasValue)
                {
                    entranceStocks = entranceStocks.
                        Where(pr => pr.EntrancedDate.CompareTo(entranceStockSearchCriteria.From.Value) >= 0
                            && pr.EntrancedDate.CompareTo(entranceStockSearchCriteria.To.Value) <= 0)
                            .ToList();
                }
            }
            else
            {
                return entranceStocks;
            }

            return entranceStocks;
        }
    }
}
