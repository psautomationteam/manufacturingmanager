using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaoHien.Model
{
    class EmployeesReport
    {
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

        string createdDate;
        public string CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

        string recordCode;
        public string RecordCode
        {
            get { return recordCode; }
            set { recordCode = value; }
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
    }
}
