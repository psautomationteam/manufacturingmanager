using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaoHien.UI;
using DAL;

namespace BaoHien.Common
{
    public class Global
    {
        static SystemUser currentUser = null;
        public static SystemUser CurrentUser
        {
            get
            {
                return currentUser;
            }
            set
            {
                currentUser = value;
            }
        }
    }
}
