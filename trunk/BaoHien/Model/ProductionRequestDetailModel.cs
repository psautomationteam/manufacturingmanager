using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaoHien.Model
{
    public class ProductionRequestDetailModel
    {
        int productId;
        int attributeId;
        int numberUnit;

        string note;
        public int ProductId
        {
            get
            {
                return productId;
            }
            set
            {
                productId = value;
            }
        }
        public int AttributeId
        {
            get
            {
                return attributeId;
            }
            set
            {
                attributeId = value;
            }
        }
        public int NumberUnit
        {
            get
            {
                return numberUnit;
            }
            set
            {
                numberUnit = value;
            }
        }
        public string Note
        {
            get
            {
                return note;
            }
            set
            {
                note = value;
            }
        }
    }
}
