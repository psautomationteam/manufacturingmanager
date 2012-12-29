using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaoHien.UI;
using DAL;
using BaoHien.Properties;
using System.Globalization;
using System.IO;

namespace BaoHien.Common
{
    public class Global
    {
        static SystemUser currentUser = null;
        static string iPAddrress;

        public static string convertToCurrency(string valueInput)

        {
            decimal total = 0;
            decimal.TryParse(valueInput, out total);
            CultureInfo vietnam = new CultureInfo(1066);
                CultureInfo usa = new CultureInfo("en-US");

                NumberFormatInfo nfi = usa.NumberFormat;
                nfi = (NumberFormatInfo)nfi.Clone();
                NumberFormatInfo vnfi = vietnam.NumberFormat;
                nfi.CurrencySymbol = vnfi.CurrencySymbol;
                nfi.CurrencyNegativePattern = vnfi.CurrencyNegativePattern;
                nfi.CurrencyPositivePattern = vnfi.CurrencyPositivePattern;
                string tmp = total.ToString("c", nfi);
                return tmp.Remove(tmp.ToString().Length - 1);
        }

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

        public static void checkDirSaveFile()
        {
            if (!Directory.Exists(BHConstant.SAVE_IN_DIRECTORY))
            {
                Directory.CreateDirectory(BHConstant.SAVE_IN_DIRECTORY);
            }
        }
    }
}
