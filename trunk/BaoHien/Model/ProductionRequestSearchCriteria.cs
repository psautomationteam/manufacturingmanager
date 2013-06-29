using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaoHien.Model
{
    public class ProductionRequestSearchCriteria
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
        string codeRequest;

        public string CodeRequest
        {
            get { return codeRequest; }
            set { codeRequest = value; }
        }
        int? userId;

        public int? UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        
    }
}
