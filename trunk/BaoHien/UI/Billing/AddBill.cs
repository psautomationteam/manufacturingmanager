using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using BaoHien.Common;
using BaoHien.Services.Bills;
using BaoHien.Services.Customers;
using BaoHien.Services.SystemUsers;
using BaoHien.UI.Base;

namespace BaoHien.UI
{
    public partial class AddBill : BaseForm
    {        
        Bill bill;
        List<Customer> customers;

        public AddBill()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loadSomeData()
        {
            if (customers == null)
            {
                CustomerService customerService = new CustomerService();
                customers = customerService.GetCustomers();               
            }
            if (customers != null)
            {
                cbxCustomer.DataSource = customers;
                cbxCustomer.DisplayMember = "CustomerName";
                cbxCustomer.ValueMember = "Id";
            }
            if (bill != null)
            {
                txtAmount.Text = Global.formatVNDCurrencyText(bill.Amount.ToString());
                txtAmount.Enabled = false;
                txtCreatedDate.Text = bill.CreatedDate.ToShortDateString();
                txtNote.Text = bill.Note;
                txtOrderCode.Text = bill.BillCode;
                cbxCustomer.SelectedValue = bill.CustId;
                cbxCustomer.Enabled = false;
                btnSave.Text = "OK";
            }
            else
            {
                txtCreatedDate.Text = DateTime.Now.ToShortDateString();
                txtOrderCode.Text = RandomGeneration.GeneratingCode(BHConstant.PREFIX_FOR_BILLING);
            }
            txtCreatedDate.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (validator1.Validate())
            {
                BillService billService = new BillService();
                if (bill == null)
                {
                    double amount = 0;
                    string amountStr = string.IsNullOrEmpty(txtAmount.WorkingText) ? txtAmount.Text : txtAmount.WorkingText;
                    double.TryParse(amountStr, out amount);
                    int userId = 0;
                    if (Global.CurrentUser != null)
                    {
                        userId = Global.CurrentUser.Id;
                    }
                    else
                    {
                        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                        return;
                    }

                    bill = new Bill
                    {
                        BillCode = txtOrderCode.Text,
                        Note = txtNote.Text,
                        CreatedDate = DateTime.Now,
                        Amount = amount,
                        CustId = cbxCustomer.SelectedValue != null ? (int)cbxCustomer.SelectedValue : 0,
                        UserId = userId
                    };

                    bool result = billService.AddBill(bill);
                    CustomerLogService cls = new CustomerLogService();
                    CustomerLog newest = cls.GetNewestCustomerLog(bill.CustId);
                    double beforeDebit = 0.0;
                    if (newest != null)
                    {
                        beforeDebit = newest.AfterDebit;
                    }
                    CustomerLog cl = new CustomerLog
                    {
                        CustomerId = bill.CustId,
                        RecordCode = bill.BillCode,
                        BeforeDebit = beforeDebit,
                        Amount = bill.Amount,
                        AfterDebit = beforeDebit - bill.Amount,
                        CreatedDate = DateTime.Now
                    };
                    result = cls.AddCustomerLog(cl);
                    if (result)
                    {
                        MessageBox.Show("Phiếu thanh toán đã được thêm!");
                    }
                    else
                    {
                        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                        return;
                    }
                }
                else
                {
                    //double amount = 0;
                    //double.TryParse(txtAmount.Text, out amount);
                    //int userId = 0;
                    //if (Global.CurrentUser != null)
                    //{
                    //    userId = Global.CurrentUser.Id;
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                    //    return;
                    //}
                    //bill.BillCode = txtOrderCode.Text;
                    //bill.Note = txtNote.Text;
                    //bill.Amount = amount;
                    //bill.CustId = cbxCustomer.SelectedValue != null ? (int)cbxCustomer.SelectedValue : 0;
                    //bill.UserId = userId;
                    //bool result = billService.UpdateBill(bill);
                    //if (result)
                    //{
                    //    MessageBox.Show("Phiếu thanh toán đã được cập nhật!");
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                    //    return;
                    //}
                }
                if (this.CallFromUserControll != null && this.CallFromUserControll is BillList)
                {
                    ((BillList)this.CallFromUserControll).loadBillList();
                }

                this.Close();
            }
            
        }

        private void AddBill_Load(object sender, EventArgs e)
        {
            loadSomeData();
        }

        public void loadDataForEditBill(int billId)
        {
            BillService billService = new BillService();
            bill = billService.GetBill(billId);            
        }
    }
}
