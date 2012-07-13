using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaoHien.Model
{
    public class ProductTypeSearchCriteria
    {
        string productTypeCode = null;
        string productTypeName = null;
        
        public string ProductTypeCode
        {
            get
            {
                return productTypeCode;
            }
            set
            {
                productTypeCode = value;
            }
        }
        public string ProductTypeName
        {
            get
            {
                return productTypeName;
            }
            set
            {
                productTypeName = value;
            }
        }
        
    }
}
