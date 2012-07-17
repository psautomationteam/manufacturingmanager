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

namespace BaoHien.UI
{
    public partial class AddCustomer : BaseForm
    {
        Customer customer;
        public AddCustomer()
        {
            InitializeComponent();
        }
        public void loadDataForEditCustomer(int customerId)
        {
            this.Text = "Chỉnh sửa thuộc tính phẩm này";
            this.btnAdd.Text = "Cập nhật";

            CustomerService customerService = new CustomerService();

            customer = customerService.GetCustomer(customerId);
            if (customer != null)
            {
                txtDescription.Text = customer.Description;
                txtCode.Text = customer.CustCode;
                txtName.Text = customer.CustomerName;
                txtAddress.Text = customer.Address;
                txtBankAcc.Text = customer.BankAcc;
                txtBankName.Text = customer.BankName;
                txtContactPersonEmail.Text = customer.ContactPersonEmail;
                txtContactPersonName.Text = customer.ContactPersonPhone;
                txtEmail.Text = customer.Email;
                txtFax.Text = customer.Fax;
                txtPhoneNumber.Text = customer.Phone;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (customer != null && customer.Id > 0)
            {
                customer.Description = txtDescription.Text;
                customer.CustCode = txtCode.Text;
                customer.Address = txtAddress.Text;
                customer.BankAcc = txtBankAcc.Text;
                customer.BankName = txtBankName.Text;
                customer.ContactPersonEmail = txtContactPersonEmail.Text;
                customer.ContactPersonPhone = txtContactPersonName.Text;
                customer.Email = txtEmail.Text;
                customer.Fax = txtFax.Text;
                customer.Phone = txtPhoneNumber.Text;
                customer.CustomerName = txtName.Text;
                customer.SalerId = cmbSaler.SelectedValue != null ? (int?)cmbSaler.SelectedValue : null;
                CustomerService customerService = new CustomerService();
                bool result = customerService.UpdateCustomer(customer);
                if (result)
                {
                    MessageBox.Show("Khách hàng được cập nhật thành công");
                    ((CustomerList)this.CallFromUserControll).loadCustomerList();
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
                    ContactPersonEmail = txtContactPersonEmail.Text,
                    ContactPersonPhone = txtContactPersonName.Text,
                    Email = txtEmail.Text,
                    Fax = txtFax.Text,
                    Phone = txtPhoneNumber.Text,
                    CustomerName = txtName.Text
                };
                CustomerService customerService = new CustomerService();
                bool result = customerService.AddCustomer(customer);
                if (result)
                {
                    MessageBox.Show("Khách hàng được tạo thành công");
                    ((CustomerList)this.CallFromUserControll).loadCustomerList();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                }
            }
        }
    }
}
