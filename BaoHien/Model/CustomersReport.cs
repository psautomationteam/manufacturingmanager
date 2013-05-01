using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaoHien.Model
{
    public class CustomersReport
    {
        int index;
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        string date;
        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        string recordCode;
        public string RecordCode
        {
            get { return recordCode; }
            set { recordCode = value; }
        }

        string amount;
        public string Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        string customerName;
        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

        string customerCode;
        public string CustomerCode
        {
            get { return customerCode; }
            set { customerCode = value; }
        }
    }
}
