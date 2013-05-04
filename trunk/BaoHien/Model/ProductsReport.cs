using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaoHien.Model
{
    public class ProductsReport
    {
        string index;
        public string Index
        {
            get { return index; }
            set { index = value; }
        }

        string productCode;
        public string ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }

        string productName;
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        string unitName;
        public string UnitName
        {
            get { return unitName; }
            set { unitName = value; }
        }

        string firstNumber;
        public string FirstNumber
        {
            get { return firstNumber; }
            set { firstNumber = value; }
        }

        string importNumber;
        public string ImportNumber
        {
            get { return importNumber; }
            set { importNumber = value; }
        }

        string exportNumber;
        public string ExportNumber
        {
            get { return exportNumber; }
            set { exportNumber = value; }
        }

        string lastNumber;
        public string LastNumber
        {
            get { return lastNumber; }
            set { lastNumber = value; }
        }
    }
}
