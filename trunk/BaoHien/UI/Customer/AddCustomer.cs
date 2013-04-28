using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaoHien.UI.Base;
using BaoHien.Services.Customers;
using DAL;
using BaoHien.Services.SystemUsers;
using BaoHien.Services.Employees;

namespace BaoHien.UI
{
    public partial class AddCustomer : BaseForm
    {
        Customer customer;
        List<Employee> salers;
        List<Customer> customers;
        bool preventClosing = false;

        public AddCustomer()
        {
            InitializeComponent();
        }

        public void loadDataForEditCustomer(int customerId)
        {
            this.Text = "Chỉnh sửa thông tin khách hàng";
            this.btnAdd.Text = "Cập nhật";            
            CustomerService customerService = new CustomerService();
            customer = customerService.GetCustomer(customerId);
            loadSomeData();
            if (customer != null)
            {
                txtDescription.Text = customer.Description;
                txtCode.Text = customer.CustCode;
                txtName.Text = customer.CustomerName;
                txtAddress.Text = customer.Address;
                txtBankAcc.Text = customer.BankAcc;
                txtBankName.Text = customer.BankName;
                txtContactPersonPhone.Text = customer.ContactPersonPhone;
                txtContactPersonEmail.Text = customer.ContactPersonEmail;
                txtContactPersonName.Text = customer.ContactPerson;
                txtEmail.Text = customer.Email;
                txtFax.Text = customer.Fax;
                txtPhoneNumber.Text = customer.Phone;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (validator1.Validate() && ValidateData())
            {
                if (customer != null && customer.Id > 0)
                {
                    customer.Description = txtDescription.Text;
                    customer.CustCode = txtCode.Text;
                    customer.Address = txtAddress.Text;
                    customer.BankAcc = txtBankAcc.Text;
                    customer.BankName = txtBankName.Text;
                    customer.ContactPersonEmail = txtContactPersonEmail.Text;
                    customer.ContactPerson = txtContactPersonName.Text;
                    customer.ContactPersonPhone = txtContactPersonPhone.Text;
                    customer.Email = txtEmail.Text;
                    customer.Fax = txtFax.Text;
                    customer.Phone = txtPhoneNumber.Text;
                    customer.CustomerName = txtName.Text;
                    customer.FavoriteProduct = txtFavoriteProduct.Text;
                    if (cmbSaler.SelectedValue != null)
                    {
                        Employee emp = salers.Single(x => x.Id == (int)cmbSaler.SelectedValue);
                        customer.Employee = emp;
                    }
                    CustomerService customerService = new CustomerService();
                    bool result = customerService.UpdateCustomer(customer);
                    if (result)
                    {
                        MessageBox.Show("Khách hàng được cập nhật thành công");
                        if (this.CallFromUserControll != null && this.CallFromUserControll is CustomerList)
                        {
                            ((CustomerList)this.CallFromUserControll).loadCustomerList();
                        }

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                    }
                }
                else
                {
                    customer = new Customer
                    {
                        Description = txtDescription.Text,
                        CustCode = txtCode.Text,
                        Address = txtAddress.Text,
                        BankAcc = txtBankAcc.Text,
                        BankName = txtBankName.Text,
                        ContactPerson = txtContactPersonName.Text,
                        ContactPersonEmail = txtContactPersonEmail.Text,
                        ContactPersonPhone = txtContactPersonPhone.Text,
                        Email = txtEmail.Text,
                        Fax = txtFax.Text,
                        Phone = txtPhoneNumber.Text,
                        CustomerName = txtName.Text,
                        SalerId = cmbSaler.SelectedValue != null ? (int?)cmbSaler.SelectedValue : (int?)null,
                        FavoriteProduct = txtFavoriteProduct.Text
                    };
                    CustomerService customerService = new CustomerService();
                    bool result = customerService.AddCustomer(customer);
                    if (result)
                    {
                        MessageBox.Show("Khách hàng được tạo thành công");
                        if (this.CallFromUserControll != null && this.CallFromUserControll is CustomerList)
                        {
                            ((CustomerList)this.CallFromUserControll).loadCustomerList();
                        }
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                    }
                }
            }
            else
                preventClosing = true;
        }

        private void loadSomeData()
        {            
            if (salers == null)
            {
                EmployeeService empls = new EmployeeService();
                salers = empls.GetEmployees();
            }
            if (salers != null)
            {
                cmbSaler.DataSource = salers;
                cmbSaler.DisplayMember = "FullName";
                cmbSaler.ValueMember = "Id";
                if (customer != null)
                {
                    cmbSaler.SelectedIndex = salers.FindIndex(su => su.Id == customer.SalerId);
                }
            }
            if (customers == null)
            {
                CustomerService cus = new CustomerService();
                customers = cus.GetCustomers();
            }
        }

        private void AddCustomer_Load(object sender, EventArgs e)
        {
            loadSomeData();
        }

        private bool ValidateData()
        { 
            bool result = true;
            if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtCode.Text))
            {
                Customer cm = customers.Where(x => x.CustCode == txtCode.Text || x.CustomerName == txtName.Text).FirstOrDefault();
                if (cm != null)
                {
                    DialogResult dl = MessageBox.Show("Trùng tên hoặc Mã khách hàng. Bạn có muốn tiếp tục?", "Cảnh báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dl == DialogResult.No)
                        result = false;
                }
            }
            return result;
        }

        private void AddCustomer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (preventClosing)
            {
                e.Cancel = true;
                preventClosing = false;
            }
        }
    }
}
