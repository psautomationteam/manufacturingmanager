﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using BaoHien.Services.Customers;
using BaoHien.Services.SystemUsers;
using BaoHien.Services.Bills;
using DAL.Helper;
using BaoHien.Model;
using BaoHien.Common;

namespace BaoHien.UI
{
    public partial class BillList : UserControl
    {
        List<Customer> customers;
        List<SystemUser> systemUsers;

        public BillList()
        {
            InitializeComponent();
        }

        public void loadBillList()
        {
            loadSomeData();
            BillService billService = new BillService();
            List<Bill> bills = billService.GetBills();
            setUpDataGrid(bills);
        }

        private void loadSomeData()
        {
            CustomerService customerService = new CustomerService();
            customers = customerService.GetCustomers().OrderBy(x => x.CustCode).ToList();
            customers.Insert(0, new Customer() { Id = 0, CustomerName = "Tất cả", CustCode = "Tất cả" });

            cbmCustomers.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbmCustomers.AutoCompleteSource = AutoCompleteSource.ListItems;
            cbmCustomers.DataSource = customers;
            cbmCustomers.DisplayMember = "CustCode";
            cbmCustomers.ValueMember = "Id";

            SystemUserService systemUserService = new SystemUserService();
            systemUsers = systemUserService.GetSystemUsers();
            systemUsers.Add(new SystemUser() { Id = 0, FullName = "Tất cả" });
            systemUsers = systemUsers.OrderBy(x => x.Id).ToList();

            cbmUsers.DataSource = systemUsers;
            cbmUsers.DisplayMember = "FullName";
            cbmUsers.ValueMember = "Id";
        }

        private void setUpDataGrid(List<Bill> bills)
        {
            dgwBillingList.AutoGenerateColumns = false;
            if (bills != null)
            {
                var query = from bill in bills
                            select new
                            {
                                Id = bill.Id,
                                BillCode = bill.BillCode,
                                CustomerName = bill.Customer.CustomerName,
                                CustomerCode = bill.Customer.CustCode,
                                Total = Global.formatVNDCurrencyText(bill.Amount.ToString()),
                                TotalInCurrency = bill.Amount,
                                Note = bill.Note,
                                UserId = bill.SystemUser.FullName,
                                CreatedDate = bill.CreatedDate.ToString(BHConstant.DATE_FORMAT)
                            };
                dgwBillingList.DataSource = query.ToList();
                lblTotal.Text = Global.formatVNDCurrencyText(query.Select(x => x.TotalInCurrency).Sum().ToString());
                productionRequestInTotal.Text = query.Count().ToString();
            }
        }

        private void SetupColumns()
        {
            dgwBillingList.AutoGenerateColumns = false;
            
            dgwBillingList.Columns.Add(Global.CreateCell("CreatedDate", "Ngày", 100));
            dgwBillingList.Columns.Add(Global.CreateCell("BillCode", "Mã hóa đơn", 100));
            dgwBillingList.Columns.Add(Global.CreateCell("CustomerName", "Khách hàng", 200));
            dgwBillingList.Columns.Add(Global.CreateCell("CustomerCode", "Mã khách hàng", 150));
            dgwBillingList.Columns.Add(Global.CreateCell("UserId", "Người tạo", 150));
            dgwBillingList.Columns.Add(Global.CreateCellWithAlignment("Total", "Tổng", 100, DataGridViewContentAlignment.MiddleRight));
            dgwBillingList.Columns.Add(Global.CreateCell("Note", "Ghi chú", 150));
            dgwBillingList.Columns.Add(Global.CreateCellDeleteAction());
        }

        private void BillList_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Today.AddDays(-DateTime.Now.Day + 1);
            dtpFrom.CustomFormat = BHConstant.DATE_FORMAT;
            dtpTo.CustomFormat = BHConstant.DATE_FORMAT;
            loadBillList();
            SetupColumns();
        }

        private void dgwBillingList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AddBill frmAddBill = new AddBill();
            DataGridViewRow currentRow = dgwBillingList.Rows[e.RowIndex];

            int id = ObjectHelper.GetValueFromAnonymousType<int>(currentRow.DataBoundItem, "Id");
            frmAddBill.loadDataForEditBill(id);

            frmAddBill.CallFromUserControll = this;
            frmAddBill.ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BillSearchCriteria billSearchCriteria = new BillSearchCriteria
            {
                Code = txtCode.Text.ToLower(),
                CreatedBy = (cbmUsers.SelectedValue != null && cbmUsers.SelectedIndex != 0) ? (int?)cbmUsers.SelectedValue : (int?)null,
                CustId = (cbmCustomers.SelectedValue != null && cbmCustomers.SelectedIndex != 0) ? (int?)cbmCustomers.SelectedValue : (int?)null,
                From = dtpFrom.Value != null ? dtpFrom.Value : (DateTime?)null,
                To = dtpTo.Value != null ? dtpTo.Value.AddDays(1).Date : (DateTime?)null,
            };
            BillService billService = new BillService();
            List<Bill> bills = billService.SearchingBill(billSearchCriteria);
            if (bills == null)
            {
                bills = new List<Bill>();
            }
            setUpDataGrid(bills);
        }

        private void dgwBillingList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender is DataGridView)
            {
                DataGridViewCell cell = ((DataGridView)sender).CurrentCell;
                if (cell.ColumnIndex == ((DataGridView)sender).ColumnCount - 1)
                {
                    DialogResult result = MessageBox.Show("Bạn có muốn xóa phiếu thanh toán?",
                    "Xoá phiếu thanh toán",
                     MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        DataGridViewRow currentRow = dgwBillingList.Rows[e.RowIndex];

                        BillService billService = new BillService();
                        int id = ObjectHelper.GetValueFromAnonymousType<int>(currentRow.DataBoundItem, "Id");
                        Bill bill = billService.GetBill(id);
                        CustomerLogService cls = new CustomerLogService();
                        CustomerLog cl = cls.SelectCustomerLogByWhere(x => x.RecordCode == bill.BillCode).FirstOrDefault();
                        bool kq = true;
                        if(cl != null)
                            cls.DeleteCustomerLog(cl.Id);

                        if (!billService.DeleteBill(id) && kq)
                        {
                            MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        loadBillList();
                    }

                }

            }
        }

        private void cbmCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbmCustomers.SelectedValue != null)
            {
                Customer cm = null;
                if (cbmCustomers.SelectedValue is Customer)
                    cm = (Customer)cbmCustomers.SelectedValue;
                else
                    cm = customers.Where(x => x.Id == (int)cbmCustomers.SelectedValue).FirstOrDefault();
                if (cm != null)
                {
                    lbCustomerName.Text = cm.CustomerName;
                }
            }
        }

        private void dgwBillingList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
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

        private void cbmCustomers_KeyDown(object sender, KeyEventArgs e)
        {
            Global.DisableDropDownWhenSuggesting(cbmCustomers);
        }
    }
}
