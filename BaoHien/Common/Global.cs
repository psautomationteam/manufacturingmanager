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
            nfi.CurrencyDecimalDigits = 0;
            nfi.CurrencyGroupSeparator = ".";
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

        public static string formatCurrencyText(string content, string _preFix, char _thousandsSeparator, char _decimalsSeparator)
        {
            int _decimalPlaces = 0;
            int counter = 1;
            int counter2 = 0;
            char[] charArray = content.ToCharArray();
            StringBuilder str = new StringBuilder();

            for (int i = charArray.Length - 1; i >= 0; i--)
            {
                str.Insert(0, charArray.GetValue(i));
                if (_decimalPlaces == 0 && counter == 3)
                {
                    counter2 = counter;
                }

                if (counter == _decimalPlaces && i > 0)
                {
                    if (_decimalsSeparator != Char.MinValue)
                        str.Insert(0, _decimalsSeparator);
                    counter2 = counter + 3;
                }
                else if (counter == counter2 && i > 0)
                {
                    if (_thousandsSeparator != Char.MinValue)
                        str.Insert(0, _thousandsSeparator);
                    counter2 = counter + 3;
                }
                counter = ++counter;
            }
            return (_preFix != "" && str.ToString() != "") ? _preFix + " " + str.ToString() : (str.ToString() != "") ? str.ToString() : "";
        }
    }
}
