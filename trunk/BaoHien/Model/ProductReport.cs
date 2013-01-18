using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaoHien.Model
{
    public class ProductReport
    {
        int index;
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        string productCode;
        public string ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }

        string productName;
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        string attributeName;
        public string AttributeName
        {
            get { return attributeName; }
            set { attributeName = value; }
        }

        string unitName;
        public string UnitName
        {
            get
            {
                return unitName;
            }
            set
            {
                unitName = value;
            }
        }

        string quantity;
        public string Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        DateTime createdDate;
        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

        // For detail product
        string recordCode;
        public string RecordCode
        {
            get { return recordCode; }
            set { recordCode = value; }
        }

        string beforeNumber;
        public string BeforeNumber
        {
            get { return beforeNumber; }
            set { beforeNumber = value; }
        }

        string afterNumber;
        public string AfterNumber
        {
            get { return afterNumber; }
            set { afterNumber = value; }
        }

        string amount;
        public string Amount
        {
            get { return amount; }
            set { amount = value; }
        }
    }
}
