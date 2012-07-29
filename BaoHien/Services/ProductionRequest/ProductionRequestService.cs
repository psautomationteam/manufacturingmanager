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
                    productionRequests = productionRequests.Where(pr => pr.ReqCode.Contains(productionRequestSearchCriteria.CodeRequest)).ToList();
                }
                if (productionRequestSearchCriteria.To.HasValue && productionRequestSearchCriteria.From.HasValue)
                {
                    productionRequests = productionRequests.
                        Where(pr => pr.RequestedDate.CompareTo(productionRequestSearchCriteria.From.Value) >= 0 
                            && pr.RequestedDate.CompareTo(productionRequestSearchCriteria.To.Value) <= 0)
                            .ToList();
                }
            }
            else
            {
                return productionRequests;
            }
            /*
            IQueryable<ProductionRequest> query = null;
            BaoHienDBDataContext context = BaoHienRepository.GetBaoHienDBDataContext();
            if (context != null)
            {
                query = from p in context.Products
                        join pt in context.ProductTypes on p.ProductType equals pt.Id
                        where (p.Status == null) && (productSearchCriteria.ProductTypeId == null || pt.Id == productSearchCriteria.ProductTypeId)
                        select p;
            }
            if (productSearchCriteria.ProductCode != null)
            {
                query = query.Where(p => p.ProductCode.Contains(productSearchCriteria.ProductCode));
            }
            if (productSearchCriteria.ProductName != null)
            {
                query = query.Where(p => p.ProductName.Contains(productSearchCriteria.ProductName));
            }
            if (productSearchCriteria.PurchaseStatus != null)
            {

            }
            if (query != null)
                return query.ToList();*/
            return productionRequests;
        }
    }
}
