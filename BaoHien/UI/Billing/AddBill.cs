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
using BaoHien.Services.Seeds;
using DAL.Helper;

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
                cbxCustomer.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbxCustomer.AutoCompleteSource = AutoCompleteSource.ListItems;
                cbxCustomer.DataSource = customers;
                cbxCustomer.DisplayMember = "CustCode";
                cbxCustomer.ValueMember = "Id";
            }
            if (bill != null)
            {
                txtAmount.Text = Global.formatVNDCurrencyText(bill.Amount.ToString());
                txtAmount.Enabled = false;
                txtCreatedDate.Text = bill.CreatedDate.ToString(BHConstant.DATE_FORMAT);
                txtNote.Text = bill.Note;
                txtOrderCode.Text = bill.BillCode;
                cbxCustomer.SelectedValue = bill.CustId;
                cbxCustomer.Enabled = false;
                btnSave.Text = "OK";
            }
            else
            {
                txtCreatedDate.Text = BaoHienRepository.GetBaoHienDBDataContext().GetSystemDate().ToString(BHConstant.DATE_FORMAT);
                txtOrderCode.Text = Global.GetTempSeedID(BHConstant.PREFIX_FOR_BILLING);
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
                    DateTime systime = BaoHienRepository.GetBaoHienDBDataContext().GetSystemDate();
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
                    
                    SeedService ss = new SeedService();
                    bill = new Bill
                    {
                        BillCode = ss.AddSeedID(BHConstant.PREFIX_FOR_BILLING),
                        Note = txtNote.Text,
                        CreatedDate = systime,
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
                        CreatedDate = systime
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

        private void cbxCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxCustomer.SelectedValue != null)
            {
                Customer cm = null;
                if (cbxCustomer.SelectedValue is Customer)
                    cm = (Customer)cbxCustomer.SelectedValue;
                else
                    cm = customers.Where(x => x.Id == (int)cbxCustomer.SelectedValue).FirstOrDefault();
                if (cm != null)
                {
                    lbCustomerName.Text = cm.CustomerName;
                }
            }
        }
    }
}
