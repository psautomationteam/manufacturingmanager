using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaoHien.Model
{
    public class CustomerReport
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

        string cost;
        public string Cost
        {
            get { return cost; }
            set { cost = value; }
        }
    }
}
