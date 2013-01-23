using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Helper
{
    public class Constant
    {
        public const string PRIMARYKEY_PROPERTY_NAME = "Id";
        public const string DELETED_PROPERTY_NAME = "Status";
        public const byte DELETED_PROPERTY_VALUE = 3;
        public const byte ACTIVE_PROPERTY_VALUE = 1;
        public const byte DEACTIVE_PROPERTY_VALUE = 2;

        // FOR DB CONFIG
        public const string INIT_IP = "192.168.29.100";
        public const string INIT_PORT = "1433";
        public const string INIT_NETWORK_LIBRARY = "DBMSSOCN";
        public const string INIT_DATABASE_NAME = "BaoHienCompany";
        public const string INIT_USER_ID = "";
        public const string INIT_PW = "";
    }
}