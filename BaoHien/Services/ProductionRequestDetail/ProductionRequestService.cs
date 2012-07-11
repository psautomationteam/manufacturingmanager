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
        public ProductionRequestDetail GetProductionRequestDetail(System.Int32 id)
        {
            ProductionRequestDetail productionRequestDetail = OnGetItem<ProductionRequestDetail>(id.ToString());

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
        public bool DeleteProductionRequestDetail(System.Int32 id)
        {
            return OnDeleteItem<ProductionRequestDetail>(id.ToString());
        }
        public bool UpdateProductionRequestDetail(ProductionRequestDetail productionRequestDetail)
        {
            return OnUpdateItem<ProductionRequestDetail>(productionRequestDetail, productionRequestDetail.Id.ToString());
        }
    }
}
