using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;

namespace BaoHien.Services.MaterialInStocks
{
    public class MaterialInStockService : BaseService<MaterialInStock>
    {
        public MaterialInStock GetMaterialInStock(System.Int32 id)
        {
            MaterialInStock materialInStock = OnGetItem<MaterialInStock>(id.ToString());

            return materialInStock;
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
