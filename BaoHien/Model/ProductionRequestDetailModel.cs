using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaoHien.Model
{
    public class ProductionRequestDetailModel
    {
        int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        double price;
        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        double commission;
        public double Commission
        {
            get { return commission; }
            set { commission = value; }
        }

        double tax;
        public double Tax
        {
            get { return tax; }
            set { tax = value; }
        }

        double total;
        public double Total
        {
            get { return total; }
            set { total = value; }
        }

        int productId;
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

        int attributeId;
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

        int unitId;
        public int UnitId
        {
            get
            {
                return unitId;
            }
            set
            {
                unitId = value;
            }
        }

        int numberUnit;
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

        string note;
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
