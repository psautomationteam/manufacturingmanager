using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaoHien.Model
{
    public class EmployeeReport
    {
        string index;
        public string Index
        {
            get { return index; }
            set { index = value; }
        }

        string recordCode;
        public string RecordCode
        {
            get { return recordCode; }
            set { recordCode = value; }
        }

        string date;
        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        string customerName;
        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

        string productName;
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        string attrName;
        public string AttrName
        {
            get { return attrName; }
            set { attrName = value; }
        }

        string number;
        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        string unit;
        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        string price;
        public string Price
        {
            get { return price; }
            set { price = value; }
        }

        string commission;
        public string Commission
        {
            get { return commission; }
            set { commission = value; }
        }
    }
}
