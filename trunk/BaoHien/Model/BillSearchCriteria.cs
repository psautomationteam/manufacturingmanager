using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaoHien.Model
{
    public class BillSearchCriteria
    {
        DateTime? from;

        public DateTime? From
        {
            get { return from; }
            set { from = value; }
        }
        DateTime? to;

        public DateTime? To
        {
            get { return to; }
            set { to = value; }
        }
        string code;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        int? createdBy;

        public int? CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }
        int? cusId;

        public int? CustId
        {
            get { return cusId; }
            set { cusId = value; }
        }
    }
}
