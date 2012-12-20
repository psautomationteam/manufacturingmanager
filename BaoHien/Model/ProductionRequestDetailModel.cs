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
        int productId;
        int attributeId;
        int numberUnit;
        double price;
        double commission;

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

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
