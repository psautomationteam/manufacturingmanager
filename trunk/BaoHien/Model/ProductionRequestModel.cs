using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaoHien.Model
{
    public class ProductionRequestModel
    {
        private string reqCode;

        public string ReqCode
        {
            get { return reqCode; }
            set { reqCode = value; }
        }
        private string UserId;

        public string userId
        {
            get { return userId; }
            set { userId = value; }
        }
        private string note;

        public string Note
        {
            get { return note; }
            set { note = value; }
        }
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private byte status;

        public byte Status
        {
            get { return status; }
            set { status = value; }
        }
        private int index;

        public int Index
        {
            get { return index; }
            set { index = value; }
        }
    }
}
