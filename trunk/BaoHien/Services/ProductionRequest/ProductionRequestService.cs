using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;
using BaoHien.Model;
using DAL.Helper;

namespace BaoHien.Services.ProductionRequests
{
    public class ProductionRequestService : BaseService<ProductionRequest>
    {
        public ProductionRequest GetProductionRequest(System.Int32 id)
        {
            ProductionRequest productionRequest = OnGetItem<ProductionRequest>(id.ToString());
            return productionRequest;
        }

        public List<ProductionRequest> GetProductionRequests()
        {
            List<ProductionRequest> productionRequests = OnGetItems<ProductionRequest>();
            return productionRequests.OrderByDescending(x => x.RequestedDate).ToList();
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

        public List<ProductionRequest> SearchingProductionRequest(ProductionRequestSearchCriteria productionRequestSearchCriteria)
        {
            List<ProductionRequest> productionRequests = OnGetItems<ProductionRequest>();           
            if (productionRequestSearchCriteria != null)
            {
                if (productionRequestSearchCriteria.RequestedBy.HasValue)
                {
                    productionRequests = productionRequests.Where(pr => pr.RequestedBy == productionRequestSearchCriteria.RequestedBy.Value).ToList();
                }
                if (productionRequestSearchCriteria.CodeRequest != "")
                {
                    productionRequests = productionRequests.Where(pr => pr.ReqCode.ToLower().Contains(productionRequestSearchCriteria.CodeRequest)).ToList();
                }
                if (productionRequestSearchCriteria.To.HasValue && productionRequestSearchCriteria.From.HasValue)
                {
                    productionRequests = productionRequests.
                        Where(pr => pr.RequestedDate.CompareTo(productionRequestSearchCriteria.From.Value) >= 0 
                            && pr.RequestedDate.CompareTo(productionRequestSearchCriteria.To.Value) <= 0)
                            .ToList();
                }
            }
            return productionRequests;
        }
    }
}
