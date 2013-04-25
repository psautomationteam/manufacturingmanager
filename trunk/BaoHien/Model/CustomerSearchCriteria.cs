using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaoHien.Model
{
    public class CustomerSearchCriteria
    {
        string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        string phone;
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        int? saler;
        public int? Saler
        {
            get { return saler; }
            set { saler = value; }
        }
    }
}
