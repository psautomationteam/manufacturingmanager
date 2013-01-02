using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaoHien.Model
{
    public class ArrearReportModel
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

        
    }
}
