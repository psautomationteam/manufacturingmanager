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

        string updatedDateString;
        public string UpdatedDateString
        {
            get { return updatedDateString; }
            set { updatedDateString = value; }
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

        DateTime updatedDate;
        public DateTime UpdatedDate
        {
            get { return updatedDate; }
            set { updatedDate = value; }
        }

        bool direction;
        public bool Direction
        {
            get { return direction; }
            set { direction = value; }
        }
    }
}
