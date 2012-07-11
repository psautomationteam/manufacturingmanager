using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;

namespace BaoHien.Services.ProductionRequests
{
    public class ProductRequestService : BaseService<ProductionRequest>
    {
        public ProductionRequest GetProductionRequest(System.Int32 id)
        {
            ProductionRequest productionRequest = OnGetItem<ProductionRequest>(id.ToString());

            return productionRequest;
        }
        public List<ProductionRequest> GetProductionRequests()
        {
            List<ProductionRequest> productionRequests = OnGetItems<ProductionRequest>();

            return productionRequests;
        }
        public bool AddProductionRequest(ProductionRequest product)
        {
            return OnAddItem<ProductionRequest>(product);
        }
        public bool DeleteProductionRequest(System.Int32 id)
        {
            return OnDeleteItem<ProductionRequest>(id.ToString());
        }
        public bool UpdateProductionRequest(ProductionRequest productionRequest)
        {
            return OnUpdateItem<ProductionRequest>(productionRequest, productionRequest.Id.ToString());
        }
    }
}
