using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaoHien.Model
{
    public class ProductDetail
    {
        int Id;
        public int ID
        {
            get { return Id; }
            set { Id = value; }
        }

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

        double beforeNumber;
        public double BeforeNumber
        {
            get { return beforeNumber; }
            set { beforeNumber = value; }
        }

        double amount;
        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        double afterNumber;
        public double AfterNUmber
        {
            get { return afterNumber; }
            set { afterNumber = value; }
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

        int status;
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
