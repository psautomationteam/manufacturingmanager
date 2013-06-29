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
using DAL.Helper;

namespace BaoHien.UI
{
    public partial class EmployeeList : UserControl
    {
        public EmployeeList()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEmployee frmAddEmpl = new AddEmployee();
            frmAddEmpl.CallFromUserControll = this;
            frmAddEmpl.ShowDialog();
        }

        private void EmployeeList_Load(object sender, EventArgs e)
        {
            SetupColumns();
            loadEmployeeList();
        }

        public void loadEmployeeList()
        {
            EmployeeService employeeService = new EmployeeService();
            List<Employee> employees = employeeService.GetEmployees();
            setUpDataGrid(employees);
        }

        private void setUpDataGrid(List<Employee> employees)
        {
            if (employees != null)
            {
                dgvEmployeeList.DataSource = employees.ToList();
                lblTotalResult.Text = employees.Count.ToString();
            }
        }

        private void SetupColumns()
        {
            dgvEmployeeList.AutoGenerateColumns = false;

            dgvEmployeeList.Columns.Add(Global.CreateCell("Code", "Mã nhân viên", 200));
            dgvEmployeeList.Columns.Add(Global.CreateCell("FullName", "Tên nhân viên", 100));
            dgvEmployeeList.Columns.Add(Global.CreateCell("NickName", "Tên viết tắt", 100));
            dgvEmployeeList.Columns.Add(Global.CreateCell("Address", "Địa chỉ", 200));
            dgvEmployeeList.Columns.Add(Global.CreateCell("Phone", "Điện thoại bàn", 100));
            dgvEmployeeList.Columns.Add(Global.CreateCell("MobilePhone", "Di động", 100));
            dgvEmployeeList.Columns.Add(Global.CreateCell("Description", "Mô tả nhân viên", 300));
            dgvEmployeeList.Columns.Add(Global.CreateCellDeleteAction());
        }

        private void dgvEmployeeList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AddEmployee frmAddCustomer = new AddEmployee();
            DataGridViewRow currentRow = dgvEmployeeList.Rows[e.RowIndex];

            int id = ObjectHelper.GetValueFromAnonymousType<int>(currentRow.DataBoundItem, "Id");
            frmAddCustomer.loadDataForEditEmployee(id);

            frmAddCustomer.CallFromUserControll = this;
            frmAddCustomer.ShowDialog();
        }

        private void dgvEmployeeList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender is DataGridView)
            {
                DataGridViewCell cell = ((DataGridView)sender).CurrentCell;
                if (cell.ColumnIndex == ((DataGridView)sender).ColumnCount - 1)
                {
                    DialogResult result = MessageBox.Show("Bạn có muốn xóa nhân viên này?",
                    "Xoá nhân viên này",
                     MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        DataGridViewRow currentRow = dgvEmployeeList.Rows[e.RowIndex];

                        EmployeeService employeeService = new EmployeeService();
                        int id = ObjectHelper.GetValueFromAnonymousType<int>(currentRow.DataBoundItem, "Id");

                        if (!employeeService.DeleteEmployee(id))
                        {
                            MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                        loadEmployeeList();
                    }

                }

            }
        }

        private void dgvEmployeeList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView gridView = sender as DataGridView;
            if (null != gridView)
            {
                gridView.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders);
                foreach (DataGridViewRow r in gridView.Rows)
                {
                    gridView.Rows[r.Index].HeaderCell.Value = (r.Index + 1).ToString();
                }
            }
        }
    }
}
