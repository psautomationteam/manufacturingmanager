using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;
using System.Linq.Expressions;
using BaoHien.Model;
using BaoHien.Common;

namespace BaoHien.Services.Seeds
{
    public class SeedService : BaseService<SeedID>
    {
        public string AddSeedID(string prefix)
        {
            SeedID seed = SelectSeedIDByWhere(x => x.Prefix == prefix && x.CreateDate.Date == DateTime.Now.Date).FirstOrDefault();
            SeedID newseed = new SeedID
            {
                CreateDate = DateTime.Now,
                Prefix = prefix
            };
            int max_id = 1;
            if (seed != null)
            {
                max_id = Convert.ToInt32(seed.Value) + 1;
            }
            newseed.Value = max_id.ToString();
            newseed.Result = newseed.Prefix + newseed.CreateDate.Date.ToString("ddMMyy") +
                String.Concat(Enumerable.Repeat("0", BHConstant.MAX_ID - max_id.ToString().Length)) + max_id.ToString();
            OnAddItem<SeedID>(newseed);
            return newseed.Result;
        }

        public List<SeedID> SelectSeedIDByWhere(Expression<Func<SeedID, bool>> func)
        {
            return SelectItemByWhere<SeedID>(func);
        }
    }
}
