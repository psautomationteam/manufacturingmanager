using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaoHien.Model
{
    public class ProductDetail
    {
        int typeId;
        public int ProductTypeId
        {
            get { return typeId; }
            set { typeId = value; }
        }

        int productId;
        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        int attrId;
        public int AttributeId
        {
            get { return attrId; }
            set { attrId = value; }
        }

        int unitId;
        public int UnitId
        {
            get { return unitId; }
            set { unitId = value; }
        }

        double amount;
        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        DateTime createdDate;
        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

        bool direction;
        public bool Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        bool jampo;
        public bool Jampo
        {
            get { return jampo; }
            set { jampo = value; }
        }
    }
}
