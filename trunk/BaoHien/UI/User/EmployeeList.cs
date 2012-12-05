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
                int index = 0;
                var query = from employee in employees

                            select new
                            {
                                Id = employee.Id,
                                Address = employee.Address,
                                Code = employee.Code,
                                Description = employee.Description,
                                MobilePhone = employee.MobilePhone,
                                Phone = employee.Phone,
                                Status = employee.Status,
                                Type = employee.Type,
                                FullName = employee.FullName,
                                Index = ++index
                            };
                dgvEmployeeList.DataSource = query.ToList();

            }
        }
        private void SetupColumns()
        {
            dgvEmployeeList.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn indexColumn = new DataGridViewTextBoxColumn();
            indexColumn.Width = 30;
            indexColumn.DataPropertyName = "Index";
            indexColumn.HeaderText = "STT";
            indexColumn.ValueType = typeof(string);
            indexColumn.Frozen = true;
            dgvEmployeeList.Columns.Add(indexColumn);

            DataGridViewTextBoxColumn fullNameColumn = new DataGridViewTextBoxColumn();
            fullNameColumn.Width = 100;
            fullNameColumn.DataPropertyName = "FullName";
            fullNameColumn.HeaderText = "Tên nhân viên";
            fullNameColumn.ValueType = typeof(string);
            fullNameColumn.Frozen = true;
            dgvEmployeeList.Columns.Add(fullNameColumn);



            DataGridViewTextBoxColumn nicknameColumn = new DataGridViewTextBoxColumn();
            nicknameColumn.DataPropertyName = "NickName";
            nicknameColumn.Width = 50;
            nicknameColumn.HeaderText = "NickName";
            nicknameColumn.Frozen = true;
            nicknameColumn.ValueType = typeof(string);
            dgvEmployeeList.Columns.Add(nicknameColumn);

            DataGridViewTextBoxColumn addressColumn = new DataGridViewTextBoxColumn();
            addressColumn.DataPropertyName = "Address";
            addressColumn.Width = 70;
            addressColumn.HeaderText = "Địa chỉ";
            addressColumn.Frozen = true;
            addressColumn.ValueType = typeof(string);

            dgvEmployeeList.Columns.Add(addressColumn);

            DataGridViewTextBoxColumn phoneColumn = new DataGridViewTextBoxColumn();
            phoneColumn.DataPropertyName = "Phone";
            phoneColumn.Width = 70;
            phoneColumn.HeaderText = "Điện thoại bàn";
            phoneColumn.Frozen = true;
            phoneColumn.ValueType = typeof(string);

            dgvEmployeeList.Columns.Add(phoneColumn);

            DataGridViewTextBoxColumn mobilePhoneColumn = new DataGridViewTextBoxColumn();
            mobilePhoneColumn.DataPropertyName = "MobilePhone";
            mobilePhoneColumn.Width = 70;
            mobilePhoneColumn.HeaderText = "Di động";
            mobilePhoneColumn.Frozen = true;
            mobilePhoneColumn.ValueType = typeof(string);
            dgvEmployeeList.Columns.Add(mobilePhoneColumn);

            DataGridViewTextBoxColumn codeColumn = new DataGridViewTextBoxColumn();
            codeColumn.DataPropertyName = "Code";
            codeColumn.Width = 50;
            codeColumn.HeaderText = "Mã nhân viên";
            codeColumn.Frozen = true;
            codeColumn.ValueType = typeof(string);
            dgvEmployeeList.Columns.Add(codeColumn);

            DataGridViewTextBoxColumn typeColumn = new DataGridViewTextBoxColumn();
            typeColumn.DataPropertyName = "Type";
            typeColumn.Width = 50;
            typeColumn.HeaderText = "Kiểu";
            typeColumn.Frozen = true;
            typeColumn.ValueType = typeof(string);
            dgvEmployeeList.Columns.Add(typeColumn);

            DataGridViewImageColumn deleteButton = new DataGridViewImageColumn();
            deleteButton.Image = Properties.Resources.erase;
            deleteButton.Width = 100;
            deleteButton.HeaderText = "Xóa";
            deleteButton.ReadOnly = true;
            deleteButton.ImageLayout = DataGridViewImageCellLayout.Normal;

            DataGridViewTextBoxColumn descriptionColumn = new DataGridViewTextBoxColumn();
            descriptionColumn.DataPropertyName = "Description";
            descriptionColumn.Width = 160;//dgvProductList.Width - productNameColumn.Width - addressColumn.Width - emailColumn.Width - faxColumn.Width - employeeColumn.Width;
            descriptionColumn.HeaderText = "Mô tả nhân viên";
            descriptionColumn.Frozen = true;
            descriptionColumn.ValueType = typeof(string);

            dgvEmployeeList.Columns.Add(descriptionColumn);
            dgvEmployeeList.Columns.Add(deleteButton);

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
                            MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");

                        }
                        loadEmployeeList();
                    }

                }

            }
        }
    }
}
