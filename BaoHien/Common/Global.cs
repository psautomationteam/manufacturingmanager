using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaoHien.UI;
using DAL;
using BaoHien.Properties;

namespace BaoHien.Common
{
    public class Global
    {
        static SystemUser currentUser = null;
        static string iPAddrress;

        
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
