using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaoHien.Common
{
    public class BHConstant
    {
        
        public const string COMPANY_NAME = "Cty TNHH BẢO HIẾN";
        public const string COMPANY_ADDRESS = "341/42B Lạc Long Quân - P.5 - Q.11";
        public const string COMPANY_PHONE = "08.38619903 - 39746346";
        public const string COMPANY_FAX = "08.38600093";

        public const string DATABASE_NAME = "BaoHienCompany";
        public const string USER_TYPE_NAME1 = "Quản Trị";
        public const string USER_TYPE_NAME2 = "Kế Toán";
        public const string USER_TYPE_NAME3 = "Người Bán Hàng";

        public const short USER_TYPE_ID1 = 1;   // Admin
        public const short USER_TYPE_ID2 = 2;
        public const short USER_TYPE_ID3 = 3;

        public const bool PRODUCTION_REQUEST_DETAIL_IN = false;
        public const bool PRODUCTION_REQUEST_DETAIL_OUT = true;

        public const short SIZE_OF_CODE = 4;
        public const string PREFIX_FOR_ORDER = "BH";
        public const string PREFIX_FOR_PRODUCTION = "SX";
        public const string PREFIX_FOR_PRODUCT = "SP";
        public const string PREFIX_FOR_ENTRANCE = "NK";
        public const string PREFIX_FOR_BILLING = "TT";

        public const int MAX_ID = 4;

        public const string REGULAR_EXPRESSION_FOR_NUMBER = "[0-9]";
        public const string REGULAR_EXPRESSION_FOR_CURRENCY = "[0-9]?.?[0-9]";

        public const byte DATA_STATUS_IN_STOCK_FOR_INPUT = 1;
        public const byte DATA_STATUS_IN_STOCK_FOR_OUTPUT = 2;
        public const byte DATA_STATUS_IN_STOCK_FOR_EDIT = 3;

        public static string SAVE_IN_DIRECTORY = AppDomain.CurrentDomain.BaseDirectory + @"Temp";

        public static string DATETIME_FORMAT = "dd/MM/yyyy HH:mm:ss";
        public static string DATE_FORMAT = "dd/MM/yyyy";

        // FOR DB CONFIG
        public const string INIT_IP = "127.0.0.1";
        public const string INIT_PORT = "1433";
        public const string INIT_NETWORK_LIBRARY = "DBMSSOCN";
        public const string INIT_DATABASE_NAME = "BaoHienCompany";
        public const string INIT_USER_ID = "";
        public const string INIT_PW = "";

        public const string CONTENT_FONT_PATH = @"C:\Windows\Fonts\CALIBRI.TTF";
        public const string HEADER_FONT_PATH = @"C:\Windows\Fonts\CAMBRIAB.TTF";
    }
}
