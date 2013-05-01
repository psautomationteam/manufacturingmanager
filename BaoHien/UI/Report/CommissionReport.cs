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
using DAL.Helper;
using BaoHien.Services.Orders;

namespace BaoHien.UI
{
    public partial class CommissionReport : UserControl
    {
        List<Employee> employees;
        List<EmployeesReport> employeeLogs;
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
            lbTotal.Text = "(VND) 0";
            if (cbmEmployees.SelectedValue != null)
            {
                int employeeId = (int)cbmEmployees.SelectedValue;
                if (employeeId == 0)
                {
                    employeeLogs = employeeLogService.GetReportsOfEmployees(dtpFrom.Value, dtpTo.Value.AddDays(1).Date);
                    dgwEmployeeCommissionList.DataSource = employeeLogs;
                    SetupDataGrid(employeeLogs);
                    double total = employeeLogs.Sum(a => a.AfterNumber);
                    lbTotal.Text = Global.formatVNDCurrencyText(total.ToString());
                }
                else
                {
                    double total = 0.0;
                    List<EmployeeReport> employee_reports = employeeLogService.GetReportsOfEmployee(employeeId, dtpFrom.Value, 
                        dtpTo.Value.AddDays(1).Date, ref total);
                    dgwEmployeeCommissionList.DataSource = employee_reports;
                    SetupDataGridDetail(employee_reports);
                    lbTotal.Text = Global.formatVNDCurrencyText(total.ToString());
                }
            }
        }

        private void cbmEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void SetupDataGrid(List<EmployeesReport> logs)
        {
            dgwEmployeeCommissionList.Columns.Clear();
            dgwEmployeeCommissionList.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn indexColumn = new DataGridViewTextBoxColumn();
            indexColumn.Width = 30;
            indexColumn.DataPropertyName = "Index";
            indexColumn.HeaderText = "STT";
            indexColumn.ValueType = typeof(string);
            dgwEmployeeCommissionList.Columns.Add(indexColumn);

            DataGridViewTextBoxColumn dateColumn = new DataGridViewTextBoxColumn();
            dateColumn.DefaultCellStyle.Format = BHConstant.DATETIME_FORMAT;
            dateColumn.Width = 150;
            dateColumn.DataPropertyName = "CreatedDate";
            dateColumn.HeaderText = "Ngày";
            dateColumn.ValueType = typeof(string);
            dgwEmployeeCommissionList.Columns.Add(dateColumn);

            DataGridViewTextBoxColumn employeeNameColumn = new DataGridViewTextBoxColumn();
            employeeNameColumn.Width = 150;
            employeeNameColumn.DataPropertyName = "EmployeeName";
            employeeNameColumn.HeaderText = "Nhân viên";
            employeeNameColumn.ValueType = typeof(string);
            dgwEmployeeCommissionList.Columns.Add(employeeNameColumn);

            DataGridViewTextBoxColumn recordCodeColumn = new DataGridViewTextBoxColumn();
            recordCodeColumn.Width = 150;
            recordCodeColumn.DataPropertyName = "RecordCode";
            recordCodeColumn.HeaderText = "Mã phiếu (giao dịch cuối)";
            recordCodeColumn.ValueType = typeof(string);
            dgwEmployeeCommissionList.Columns.Add(recordCodeColumn);

            DataGridViewTextBoxColumn amountColumn = new DataGridViewTextBoxColumn();
            amountColumn.DataPropertyName = "AfterNumberText";
            amountColumn.Width = 150;
            amountColumn.HeaderText = "Số tiền";
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
            dgwEmployeeCommissionList.Columns.Add(indexColumn);

            DataGridViewTextBoxColumn dateColumn = new DataGridViewTextBoxColumn();
            dateColumn.DefaultCellStyle.Format = BHConstant.DATETIME_FORMAT;
            dateColumn.Width = 100;
            dateColumn.DataPropertyName = "Date";
            dateColumn.HeaderText = "Ngày";
            dateColumn.ValueType = typeof(string);
            dgwEmployeeCommissionList.Columns.Add(dateColumn);

            DataGridViewTextBoxColumn customerNameColumn = new DataGridViewTextBoxColumn();
            customerNameColumn.Width = 150;
            customerNameColumn.DataPropertyName = "CustomerName";
            customerNameColumn.HeaderText = "Khách hàng";
            customerNameColumn.ValueType = typeof(string);
            dgwEmployeeCommissionList.Columns.Add(customerNameColumn);

            DataGridViewTextBoxColumn productNameColumn = new DataGridViewTextBoxColumn();
            productNameColumn.Width = 150;
            productNameColumn.DataPropertyName = "ProductName";
            productNameColumn.HeaderText = "Mặt hàng";
            productNameColumn.ValueType = typeof(string);
            dgwEmployeeCommissionList.Columns.Add(productNameColumn);

            DataGridViewTextBoxColumn AttrNameColumn = new DataGridViewTextBoxColumn();
            AttrNameColumn.Width = 100;
            AttrNameColumn.DataPropertyName = "AttrName";
            AttrNameColumn.HeaderText = "Quy cách";
            AttrNameColumn.ValueType = typeof(string);
            dgwEmployeeCommissionList.Columns.Add(AttrNameColumn);

            DataGridViewTextBoxColumn numberColumn = new DataGridViewTextBoxColumn();
            numberColumn.Width = 60;
            numberColumn.DataPropertyName = "Number";
            numberColumn.HeaderText = "Số lượng";
            numberColumn.ValueType = typeof(string);
            dgwEmployeeCommissionList.Columns.Add(numberColumn);

            DataGridViewTextBoxColumn UnitColumn = new DataGridViewTextBoxColumn();
            UnitColumn.Width = 100;
            UnitColumn.DataPropertyName = "Unit";
            UnitColumn.HeaderText = "ĐVT";
            UnitColumn.ValueType = typeof(string);
            dgwEmployeeCommissionList.Columns.Add(UnitColumn);

            DataGridViewTextBoxColumn costColumn = new DataGridViewTextBoxColumn();
            costColumn.Width = 100;
            costColumn.DataPropertyName = "Cost";
            costColumn.HeaderText = "Đơn giá";
            costColumn.ValueType = typeof(string);
            dgwEmployeeCommissionList.Columns.Add(costColumn);

            DataGridViewTextBoxColumn commissionColumn = new DataGridViewTextBoxColumn();
            commissionColumn.DataPropertyName = "Commission";
            commissionColumn.Width = 100;
            commissionColumn.HeaderText = "Hoa hồng";
            commissionColumn.ValueType = typeof(string);
            dgwEmployeeCommissionList.Columns.Add(commissionColumn);
        }

        private void dgwEmployeeCommissionList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow currentRow = dgwEmployeeCommissionList.Rows[e.RowIndex];
            string RecordCode = ObjectHelper.GetValueFromAnonymousType<string>(currentRow.DataBoundItem, "RecordCode");
            OrderService orderService = new OrderService();
            Order order = orderService.GetOrders().Where(o => o.OrderCode == RecordCode).FirstOrDefault();
            if (order != null)
            {
                AddOrder frmAddOrder = new AddOrder();
                frmAddOrder.loadDataForEditOrder(order.Id);

                frmAddOrder.CallFromUserControll = this;
                frmAddOrder.ShowDialog();
            }
        }
    }
}
