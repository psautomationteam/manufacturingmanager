using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaoHien.Model
{
    class EmployeeReport
    {
        int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        int index;
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        string employeeName;
        public string EmployeeName
        {
            get { return employeeName; }
            set { employeeName = value; }
        }

        DateTime createdDate;
        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

        // For detail employee
        string recordCode;
        public string RecordCode
        {
            get { return recordCode; }
            set { recordCode = value; }
        }

        string beforeNumber;
        public string BeforeNumber
        {
            get { return beforeNumber; }
            set { beforeNumber = value; }
        }

        string afterNumberText;
        public string AfterNumberText
        {
            get { return afterNumberText; }
            set { afterNumberText = value; }
        }

        double afterNumber;
        public double AfterNumber
        {
            get { return afterNumber; }
            set { afterNumber = value; }
        }

        string amount;
        public string Amount
        {
            get { return amount; }
            set { amount = value; }
        }
    }
}
