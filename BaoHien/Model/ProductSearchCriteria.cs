using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaoHien.Model
{
    public class ProductSearchCriteria
    {
        string productCode = null;
        string productName = null;
        int? productTypeId = null;
        int? purchaseStatus = null;
        public string ProductCode
        {
            get
            {
                return productCode;
            }
            set
            {
                productCode = value;
            }
        }
        public string ProductName
        {
            get
            {
                return productName;
            }
            set
            {
                productName = value;
            }
        }
        public int? ProductTypeId
        {
            get
            {
                return productTypeId;
            }
            set
            {
                productTypeId = value;
            }
        }
        public int? PurchaseStatus
        {
            get
            {
                return purchaseStatus;
            }
            set
            {
                purchaseStatus = value;
            }
        }
    }
}
