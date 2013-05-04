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

        string createdDate;
        public string CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

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
