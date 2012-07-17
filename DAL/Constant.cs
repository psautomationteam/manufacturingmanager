using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class Constant
    {
        public const string PRIMARYKEY_PROPERTY_NAME = "Id";
        public const string DELETED_PROPERTY_NAME = "Status";
        public const byte DELETED_PROPERTY_VALUE = 3;
        public const byte ACTIVE_PROPERTY_VALUE = 1;
        public const byte DEACTIVE_PROPERTY_VALUE = 2;
    }
}
