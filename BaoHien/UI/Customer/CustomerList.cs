using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaoHien.Services.SystemUsers;
using DAL;
using BaoHien.Common;
using BaoHien.Services.Customers;
using DAL.Helper;

namespace BaoHien.UI
{
    public partial class CustomerList : UserControl
    {
        public CustomerList()
        {
            InitializeComponent();
        }

        private void CustomerList_Load(object sender, EventArgs e)
        {
            SetupColumns();
            loadCustomerList();
        }
        public void loadCustomerList()
        {
            
            SystemUserService systemUserService = new SystemUserService();
            List<SystemUser> systemUsers = systemUserService.SelectSystemUserByWhere(su => su.Type == BHConstant.USER_TYPE_ID3);
            if (systemUsers != null)
            {
                cmbSaler.DataSource = systemUsers;

                cmbSaler.DisplayMember = "FullName";
                cmbSaler.ValueMember = "Id";

            }
            CustomerService customerService = new CustomerService();
            List<Customer> customers = customerService.GetCustomers();
            setUpDataGrid(customers);
        }
        private void setUpDataGrid(List<Customer> customers)
        {
            if (customers != null)
            {
                int index = 0;
                var query = from customer in customers

                            select new
                            {
                                CustomerName = customer.CustomerName,
                                Address = customer.Address,
                                BankAcc = customer.BankAcc,
                                BankName = customer.BankName,
                                ContactPerson = customer.ContactPerson,
                                ContactPersonEmail = customer.ContactPersonEmail,
                                ContactPersonPhone = customer.ContactPersonPhone,
                                CustCode = customer.CustCode,
                                Description = customer.Description,
                                Email = customer.Email,
                                Employee = (customer.Employee != null)?customer.Employee.FullName: "",
                                Fax = customer.Fax,
                                Phone = customer.Phone,
                                SalerId = customer.SalerId,
                                Status = customer.Status,
                                Id = customer.Id,
                                Index = ++index
                            };
                dgvProductList.DataSource = query.ToList();

            }
        }
        private void SetupColumns()
        {
            dgvProductList.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn indexColumn = new DataGridViewTextBoxColumn();
            indexColumn.Width = 30;
            indexColumn.DataPropertyName = "Index";
            indexColumn.HeaderText = "STT";
            indexColumn.ValueType = typeof(string);
            indexColumn.Frozen = true;
            dgvProductList.Columns.Add(indexColumn);

            DataGridViewTextBoxColumn productNameColumn = new DataGridViewTextBoxColumn();
            productNameColumn.Width = 100;
            productNameColumn.DataPropertyName = "CustomerName";
            productNameColumn.HeaderText = "Tên khách hàng";
            productNameColumn.ValueType = typeof(string);
            productNameColumn.Frozen = true;
            dgvProductList.Columns.Add(productNameColumn);



            DataGridViewTextBoxColumn addressColumn = new DataGridViewTextBoxColumn();
            addressColumn.DataPropertyName = "Address";
            addressColumn.Width = 100;
            addressColumn.HeaderText = "Địa chỉ";
            addressColumn.Frozen = true;
            addressColumn.ValueType = typeof(string);
            dgvProductList.Columns.Add(addressColumn);

            DataGridViewTextBoxColumn emailColumn = new DataGridViewTextBoxColumn();
            emailColumn.DataPropertyName = "Email";
            emailColumn.Width = 100;
            emailColumn.HeaderText = "Email";
            emailColumn.Frozen = true;
            emailColumn.ValueType = typeof(string);

            dgvProductList.Columns.Add(emailColumn);

            DataGridViewTextBoxColumn faxColumn = new DataGridViewTextBoxColumn();
            faxColumn.DataPropertyName = "Fax";
            faxColumn.Width = 100;
            faxColumn.HeaderText = "Fax";
            faxColumn.Frozen = true;
            faxColumn.ValueType = typeof(string);

            dgvProductList.Columns.Add(faxColumn);

            DataGridViewTextBoxColumn employeeColumn = new DataGridViewTextBoxColumn();
            employeeColumn.DataPropertyName = "Employee";
            employeeColumn.Width = 100;
            employeeColumn.HeaderText = "Nhân viên phụ trách";
            employeeColumn.Frozen = true;
            employeeColumn.ValueType = typeof(string);
            dgvProductList.Columns.Add(employeeColumn);

            DataGridViewImageColumn deleteButton = new DataGridViewImageColumn();
            deleteButton.Image = Properties.Resources.erase;
            deleteButton.Width = 70;
            deleteButton.HeaderText = "Xóa";
            deleteButton.ReadOnly = true;
            deleteButton.ImageLayout = DataGridViewImageCellLayout.Normal;

            DataGridViewTextBoxColumn descriptionColumn = new DataGridViewTextBoxColumn();
            descriptionColumn.DataPropertyName = "Description";
            descriptionColumn.Width = 150;//dgvProductList.Width - productNameColumn.Width - addressColumn.Width - emailColumn.Width - faxColumn.Width - employeeColumn.Width;
            descriptionColumn.HeaderText = "Mô tả khách hàng";
            descriptionColumn.Frozen = true;
            descriptionColumn.ValueType = typeof(string);

            dgvProductList.Columns.Add(descriptionColumn);
            dgvProductList.Columns.Add(deleteButton);
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = dgvProductList.SelectedRows;

            foreach (DataGridViewRow dgv in selectedRows)
            {

                CustomerService customerService = new CustomerService();
                int id = ObjectHelper.GetValueFromAnonymousType<int>(dgv.DataBoundItem, "Id");

                if (!customerService.DeleteCustomer(id))
                {
                    MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                    break;
                }

            }
            loadCustomerList();
        }

        private void dgvProductList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AddCustomer frmAddCustomer = new AddCustomer();
            DataGridViewRow currentRow = dgvProductList.Rows[e.RowIndex];

            int id = ObjectHelper.GetValueFromAnonymousType<int>(currentRow.DataBoundItem, "Id");
            frmAddCustomer.loadDataForEditCustomer(id);

            frmAddCustomer.CallFromUserControll = this;
            frmAddCustomer.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddCustomer frmAddCustomer = new AddCustomer();
            

            frmAddCustomer.CallFromUserControll = this;
            frmAddCustomer.ShowDialog();
        }

        private void dgvProductList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender is DataGridView)
            {
                DataGridViewCell cell = ((DataGridView)sender).CurrentCell;
                if (cell.ColumnIndex == ((DataGridView)sender).ColumnCount - 1)
                {
                    DialogResult result = MessageBox.Show("Bạn có muốn xóa khách hàng này?",
                    "Xoá khách hàng này",
                     MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        DataGridViewRow currentRow = dgvProductList.Rows[e.RowIndex];

                        CustomerService customerService = new CustomerService();
                        int id = ObjectHelper.GetValueFromAnonymousType<int>(currentRow.DataBoundItem, "Id");

                        if (!customerService.DeleteCustomer(id))
                        {
                            MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                            
                        }
                        loadCustomerList();
                    }

                }

            }
        }
    }
}
