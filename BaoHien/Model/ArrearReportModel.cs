using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaoHien.Model
{
    public class ArrearReportModel
    {
        string date;

        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        string productName;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
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
        int numberOfItem;

        public int NumberOfItem
        {
            get { return numberOfItem; }
            set { numberOfItem = value; }
        }
        double price;

        public double Price
        {
            get { return price; }
            set { price = value; }
        }
        double total;

        public double Total
        {
            get { return total; }
            set { total = value; }
        }
    }
}
