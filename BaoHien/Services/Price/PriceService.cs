using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;

namespace BaoHien.Services.Prices
{
    public class PriceService : BaseService<Price>
    {
        public Price GetPrice(System.Int32 id)
        {
            Price price = OnGetItem<Price>(id.ToString());
            return price;
        }

        public List<Price> GetPrices()
        {
            List<Price> prices = OnGetItems<Price>();
            return prices;
        }

        public bool AddPrice(Price price)
        {
            return OnAddItem<Price>(price);
        }

        public bool DeletePrice(System.Int32 id)
        {
            return OnDeleteItem<Price>(id.ToString());
        }

        public bool UpdatePrice(Price price)
        {
            return OnUpdateItem<Price>(price, price.Id.ToString());
        }
    }
}
