﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaoHien.Common
{
    public class BHConstant
    {
        public const string COMPANY_NAME = "Cty TNHH BẢO HIẾN";
        public const string DATABASE_NAME = "BaoHienCompany";
        public const string USER_TYPE_NAME1 = "Kế Toán";
        public const string USER_TYPE_NAME2 = "Quản Trị";
        public const string USER_TYPE_NAME3 = "Người Bán Hàng";

        public const short USER_TYPE_ID1 = 1;
        public const short USER_TYPE_ID2 = 2;
        public const short USER_TYPE_ID3 = 3;

        public const bool PRODUCTION_REQUEST_DETAIL_IN = false;
        public const bool PRODUCTION_REQUEST_DETAIL_OUT = true;

        public const short SIZE_OF_CODE = 4;
        public const string PREFIX_FOR_ORDER = "BH";
        public const string PREFIX_FOR_PRODUCTION = "SX";
        public const string PREFIX_FOR_ENTRANCE = "NK";
        public const string PREFIX_FOR_BILLING = "TT";

        public const string REGULAR_EXPRESSION_FOR_NUMBER = "[0-9]";
        public const string REGULAR_EXPRESSION_FOR_CURRENCY = "[0-9].[0-9]";

        public const bool DATA_STATUS_IN_STOCK_FOR_INPUT = true;
        public const bool DATA_STATUS_IN_STOCK_FOR_OUTPUT = false;
    }
}