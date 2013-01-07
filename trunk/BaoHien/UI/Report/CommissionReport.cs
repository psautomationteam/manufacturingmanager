using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using BaoHien.Services.Employees;
using BaoHien.Common;
using BaoHien.Model;

namespace BaoHien.UI
{
    public partial class CommissionReport : UserControl
    {
        List<Employee> employees;
        List<EmployeeReport> employeeLogs;
        EmployeeLogService employeeLogService;

        public CommissionReport()
        {
            employeeLogService = new EmployeeLogService();
            InitializeComponent();
            LoadEmployees();
            dtpFrom.Value = DateTime.Today.AddDays(-DateTime.Now.Day + 1);
            dtpFrom.CustomFormat = BHConstant.DATE_FORMAT;
            dtpTo.CustomFormat = BHConstant.DATE_FORMAT;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgwEmployeeCommissionList.AutoGenerateColumns = false;
            LoadReport();
        }

        void LoadEmployees()
        {
            EmployeeService employeeService = new EmployeeService();
            employees = employeeService.GetEmployees();
            Employee e = new Employee
            {
                Id = 0,
                FullName = "Tất cả"
            };
            employees.Add(e);
            employees = employees.OrderBy(el => el.Id).ToList();
            if (employees != null)
            {
                cbmEmployees.DataSource = employees;
                cbmEmployees.DisplayMember = "FullName";
                cbmEmployees.ValueMember = "Id";
            }
        }

        void LoadReport()
        {
            if (cbmEmployees.SelectedValue != null)
            {
                int employeeId = (int)cbmEmployees.SelectedValue;
                if (employeeId == 0)
                {
                    employeeLogs = employeeLogService.GetReportsOfEmployees(dtpFrom.Value, dtpTo.Value.AddDays(1));
                    SetupDataGrid(employeeLogs);
                }
                else
                {
                    employeeLogs = employeeLogService.GetReportsOfEmployee(employeeId, dtpFrom.Value, dtpTo.Value.AddDays(1));
                    SetupDataGridDetail(employeeLogs);
                }
            }
        }

        private void cbmEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void SetupDataGrid(List<EmployeeReport> logs)
        {
            dgwEmployeeCommissionList.Columns.Clear();
            dgwEmployeeCommissionList.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn indexColumn = new DataGridViewTextBoxColumn();
            indexColumn.Width = 30;
            indexColumn.DataPropertyName = "Index";
            indexColumn.HeaderText = "STT";
            indexColumn.ValueType = typeof(string);
            //indexColumn.Frozen = true;
            dgwEmployeeCommissionList.Columns.Add(indexColumn);

            DataGridViewTextBoxColumn employeeNameColumn = new DataGridViewTextBoxColumn();
            employeeNameColumn.Width = 150;
            employeeNameColumn.DataPropertyName = "EmployeeName";
            employeeNameColumn.HeaderText = "Nhân viên";
            employeeNameColumn.ValueType = typeof(string);
            //customerNameColumn.Frozen = true;
            dgwEmployeeCommissionList.Columns.Add(employeeNameColumn);

            DataGridViewTextBoxColumn recordCodeColumn = new DataGridViewTextBoxColumn();
            recordCodeColumn.Width = 150;
            recordCodeColumn.DataPropertyName = "RecordCode";
            recordCodeColumn.HeaderText = "Mã phiếu (giao dịch cuối)";
            recordCodeColumn.ValueType = typeof(string);
            //recordCodeColumn.Frozen = true;
            dgwEmployeeCommissionList.Columns.Add(recordCodeColumn);

            DataGridViewTextBoxColumn dateColumn = new DataGridViewTextBoxColumn();
            dateColumn.DefaultCellStyle.Format = BHConstant.DATETIME_FORMAT;
            dateColumn.Width = 150;
            dateColumn.DataPropertyName = "Date";
            dateColumn.HeaderText = "Ngày";
            dateColumn.ValueType = typeof(string);
            //dateColumn.Frozen = true;
            dgwEmployeeCommissionList.Columns.Add(dateColumn);

            DataGridViewTextBoxColumn amountColumn = new DataGridViewTextBoxColumn();
            amountColumn.DataPropertyName = "AfterNumber";
            amountColumn.Width = 150;
            amountColumn.HeaderText = "Số tiền";
            //amountColumn.Frozen = true;
            amountColumn.ValueType = typeof(string);
            dgwEmployeeCommissionList.Columns.Add(amountColumn);
        }

        void SetupDataGridDetail(List<EmployeeReport> logs)
        {
            dgwEmployeeCommissionList.Columns.Clear();
            dgwEmployeeCommissionList.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn indexColumn = new DataGridViewTextBoxColumn();
            indexColumn.Width = 30;
            indexColumn.DataPropertyName = "Index";
            indexColumn.HeaderText = "STT";
            indexColumn.ValueType = typeof(string);
            //indexColumn.Frozen = true;
            dgwEmployeeCommissionList.Columns.Add(indexColumn);
            
            DataGridViewTextBoxColumn recordCodeColumn = new DataGridViewTextBoxColumn();
            recordCodeColumn.Width = 150;
            recordCodeColumn.DataPropertyName = "RecordCode";
            recordCodeColumn.HeaderText = "Mã phiếu (giao dịch cuối)";
            recordCodeColumn.ValueType = typeof(string);
            //recordCodeColumn.Frozen = true;
            dgwEmployeeCommissionList.Columns.Add(recordCodeColumn);

            DataGridViewTextBoxColumn dateColumn = new DataGridViewTextBoxColumn();
            dateColumn.DefaultCellStyle.Format = BHConstant.DATETIME_FORMAT;
            dateColumn.Width = 150;
            dateColumn.DataPropertyName = "Date";
            dateColumn.HeaderText = "Ngày";
            dateColumn.ValueType = typeof(string);
            //dateColumn.Frozen = true;
            dgwEmployeeCommissionList.Columns.Add(dateColumn);

            DataGridViewTextBoxColumn amountColumn = new DataGridViewTextBoxColumn();
            amountColumn.DataPropertyName = "Amount";
            amountColumn.Width = 150;
            amountColumn.HeaderText = "Số tiền";
            //amountColumn.Frozen = true;
            amountColumn.ValueType = typeof(string);
            dgwEmployeeCommissionList.Columns.Add(amountColumn);
        }
    }
}
