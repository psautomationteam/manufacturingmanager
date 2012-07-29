using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;

namespace BaoHien.Services.ProductionRequestDetails
{
    public class ProductionRequestDetailService : BaseService<ProductionRequestDetail>
    {
        public ProductionRequestDetail GetProductionRequestDetail(List<System.Int32> ids)
        {
            List<string> idsInString = new List<string>();
            foreach (System.Int32 id in ids)
            {
                idsInString.Add(id.ToString());
            }
            ProductionRequestDetail productionRequestDetail = OnGetItem<ProductionRequestDetail>(idsInString);

            return productionRequestDetail;
        }
        public List<ProductionRequestDetail> GetProductionRequestDetails()
        {
            List<ProductionRequestDetail> productionRequestDetails = OnGetItems<ProductionRequestDetail>();

            return productionRequestDetails;
        }
        public bool AddProductionRequestDetail(ProductionRequestDetail product)
        {
            return OnAddItem<ProductionRequestDetail>(product);
        }
        public bool DeleteProductionRequestDetail(List<System.Int32> ids)
        {
            List<string> idsInString = new List<string>();
            foreach (System.Int32 id in ids)
            {
                idsInString.Add(id.ToString());
            }
            return OnDeleteItem<ProductionRequestDetail>(idsInString);
        }
        public bool UpdateProductionRequestDetail(ProductionRequestDetail productionRequestDetail)
        {
            return OnUpdateItem<ProductionRequestDetail>(productionRequestDetail, productionRequestDetail.Id.ToString());
        }
    }
}
