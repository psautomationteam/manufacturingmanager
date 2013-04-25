using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaoHien.UI;
using DAL;
using BaoHien.Properties;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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

        public static string formatVNDCurrencyText(string content)
        {
            return formatCurrencyText(content, "(VND) ", '.', ',');
        }

        public static string convertCurrencyToText(string number)
        {
            string[] dv = { "", "mươi", "trăm", "nghìn", "triệu", "tỉ" };
            string[] cs = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string doc;
            int i, j, k, n, len, found, ddv, rd;

            len = number.Length;
            number += "ss";
            doc = "";
            found = 0;
            ddv = 0;
            rd = 0;

            i = 0;
            while (i < len)
            {
                //So chu so o hang dang duyet
                n = (len - i + 2) % 3 + 1;

                //Kiem tra so 0
                found = 0;
                for (j = 0; j < n; j++)
                {
                    if (number[i + j] != '0')
                    {
                        found = 1;
                        break;
                    }
                }

                //Duyet n chu so
                if (found == 1)
                {
                    rd = 1;
                    for (j = 0; j < n; j++)
                    {
                        ddv = 1;
                        switch (number[i + j])
                        {
                            case '0':
                                if (n - j == 3) doc += cs[0] + " ";
                                if (n - j == 2)
                                {
                                    if (number[i + j + 1] != '0') doc += "lẻ ";
                                    ddv = 0;
                                }
                                break;
                            case '1':
                                if (n - j == 3) doc += cs[1] + " ";
                                if (n - j == 2)
                                {
                                    doc += "mười ";
                                    ddv = 0;
                                }
                                if (n - j == 1)
                                {
                                    if (i + j == 0) k = 0;
                                    else k = i + j - 1;

                                    if (number[k] != '1' && number[k] != '0')
                                        doc += "mốt ";
                                    else
                                        doc += cs[1] + " ";
                                }
                                break;
                            case '5':
                                if (i + j == len - 1)
                                    doc += "lăm ";
                                else
                                    doc += cs[5] + " ";
                                break;
                            default:
                                doc += cs[(int)number[i + j] - 48] + " ";
                                break;
                        }

                        //Doc don vi nho
                        if (ddv == 1)
                        {
                            doc += dv[n - j - 1] + " ";
                        }
                    }
                }


                //Doc don vi lon
                if (len - i - n > 0)
                {
                    if ((len - i - n) % 9 == 0)
                    {
                        if (rd == 1)
                            for (k = 0; k < (len - i - n) / 9; k++)
                                doc += "tỉ ";
                        rd = 0;
                    }
                    else
                        if (found != 0) doc += dv[((len - i - n + 1) % 9) / 3 + 2] + " ";
                }

                i += n;
            }

            if (len == 1)
                if (number[0] == '0' || number[0] == '5') return cs[(int)number[0] - 48];

            return doc + " đồng";
        }

        public static bool isAdmin()
        {
            return (CurrentUser.Type == BHConstant.USER_TYPE_ID1) ? true : false;
        }
    }
}
