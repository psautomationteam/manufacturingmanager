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
                                Employee = customer.Employee.FirstName + " " + customer.Employee.MiddleName + " " + customer.Employee.LastName,
                                Fax = customer.Fax,
                                Phone = customer.Phone,
                                SalerId = customer.SalerId,
                                Status = customer.Status,
                                Id = customer.Id,
                            };
                dgvProductList.DataSource = query.ToList();

            }
        }
        private void SetupColumns()
        {
            dgvProductList.AutoGenerateColumns = false;
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

            DataGridViewTextBoxColumn descriptionColumn = new DataGridViewTextBoxColumn();
            descriptionColumn.DataPropertyName = "Description";
            descriptionColumn.Width = dgvProductList.Width - productNameColumn.Width - addressColumn.Width - emailColumn.Width - faxColumn.Width - employeeColumn.Width;
            descriptionColumn.HeaderText = "Mô tả khách hàng";
            descriptionColumn.Frozen = true;
            descriptionColumn.ValueType = typeof(string);

            dgvProductList.Columns.Add(descriptionColumn);

           
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
    }
}
