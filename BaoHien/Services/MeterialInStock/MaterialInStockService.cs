using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;
using System.Linq.Expressions;

namespace BaoHien.Services.MaterialInStocks
{
    public class MaterialInStockService : BaseService<MaterialInStock>
    {
        public MaterialInStock GetEntranceStock(System.Int32 id)
        {
            MaterialInStock materialInStock = OnGetItem<MaterialInStock>(id.ToString());

            return materialInStock;
        }

        public List<MaterialInStock> SelectMaterialInStockByWhere(Expression<Func<MaterialInStock, bool>> func)
        {

            return SelectItemByWhere<MaterialInStock>(func);
        }
        public List<MaterialInStock> GetMaterialInStocks()
        {
            List<MaterialInStock> materialInStocks = OnGetItems<MaterialInStock>();

            return materialInStocks;
        }
        public bool AddMaterialInStock(MaterialInStock materialInStock)
        {
            return OnAddItem<MaterialInStock>(materialInStock);
        }
        public bool DeleteMaterialInStock(System.Int32 id)
        {
            return OnDeleteItem<MaterialInStock>(id.ToString());
        }

        public bool UpdateMaterialInStock(MaterialInStock materialInStock)
        {
            return OnUpdateItem<MaterialInStock>(materialInStock, materialInStock.Id.ToString());
        }
    }
}
