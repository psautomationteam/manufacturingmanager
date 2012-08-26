using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaoHien.Model
{
    public class EntranceStockReport
    {
        int codeProduct;

        public int CodeProduct
        {
            get { return codeProduct; }
            set { codeProduct = value; }
        }
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string attributeName;

        public string AttributeName
        {
            get { return attributeName; }
            set { attributeName = value; }
        }
        string baseUnitName;

        public string BaseUnitName
        {
            get { return baseUnitName; }
            set { baseUnitName = value; }
        }
        int numberOfInput;

        public int NumberOfInput
        {
            get { return numberOfInput; }
            set { numberOfInput = value; }
        }
        int numberOfOutput;

        public int NumberOfOutput
        {
            get { return numberOfOutput; }
            set { numberOfOutput = value; }
        }
        int numberOfRemaining;

        public int NumberOfRemaining
        {
            get { return numberOfRemaining; }
            set { numberOfRemaining = value; }
        }

    }
}
