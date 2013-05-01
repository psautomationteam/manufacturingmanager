using System;
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
            customers = customerService.GetCustomers();
            customers.Add(new Customer() { Id = 0, CustomerName = "Tất cả" });
            customers = customers.OrderBy(x => x.Id).ToList();

            cbmCustomers.DataSource = customers;
            cbmCustomers.DisplayMember = "CustomerName";
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
                int index = 0;
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
                                CreateBy = bill.SystemUser.FullName,
                                CreatedDate = bill.CreatedDate.ToString(BHConstant.DATE_FORMAT),
                                Index = ++index
                            };
                dgwBillingList.DataSource = query.ToList();
                lblTotal.Text = Global.formatVNDCurrencyText(query.Select(x => x.TotalInCurrency).Sum().ToString());
                productionRequestInTotal.Text = query.Count().ToString();
            }
        }

        private void SetupColumns()
        {
            dgwBillingList.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn indexColumn = new DataGridViewTextBoxColumn();
            indexColumn.Width = 30;
            indexColumn.DataPropertyName = "Index";
            indexColumn.HeaderText = "STT";
            indexColumn.ValueType = typeof(string);
            dgwBillingList.Columns.Add(indexColumn);

            DataGridViewTextBoxColumn createdDateColumn = new DataGridViewTextBoxColumn();
            createdDateColumn.DataPropertyName = "CreatedDate";
            createdDateColumn.Width = 100;
            createdDateColumn.HeaderText = "Ngày tạo";
            createdDateColumn.ValueType = typeof(string);
            dgwBillingList.Columns.Add(createdDateColumn);

            DataGridViewTextBoxColumn billCodeColumn = new DataGridViewTextBoxColumn();
            billCodeColumn.Width = 100;
            billCodeColumn.DataPropertyName = "BillCode";
            billCodeColumn.HeaderText = "Mã đặt hàng";
            billCodeColumn.ValueType = typeof(string);
            dgwBillingList.Columns.Add(billCodeColumn);

            DataGridViewTextBoxColumn customerNameColumn = new DataGridViewTextBoxColumn();
            customerNameColumn.DataPropertyName = "CustomerName";
            customerNameColumn.Width = 200;
            customerNameColumn.HeaderText = "Khách hàng";
            customerNameColumn.ValueType = typeof(string);
            dgwBillingList.Columns.Add(customerNameColumn);

            DataGridViewTextBoxColumn customerCodeColumn = new DataGridViewTextBoxColumn();
            customerCodeColumn.DataPropertyName = "CustomerCode";
            customerCodeColumn.Width = 150;
            customerCodeColumn.HeaderText = "Mã khách hàng";
            customerCodeColumn.ValueType = typeof(string);
            dgwBillingList.Columns.Add(customerCodeColumn);

            DataGridViewTextBoxColumn createByColumn = new DataGridViewTextBoxColumn();
            createByColumn.DataPropertyName = "CreateBy";
            createByColumn.Width = 100;
            createByColumn.HeaderText = "Người tạo";
            createByColumn.ValueType = typeof(string);
            dgwBillingList.Columns.Add(createByColumn);

            DataGridViewTextBoxColumn totalColumn = new DataGridViewTextBoxColumn();
            totalColumn.DataPropertyName = "Total";
            totalColumn.Width = 100;
            totalColumn.HeaderText = "Tổng";
            totalColumn.ValueType = typeof(string);
            dgwBillingList.Columns.Add(totalColumn);

            DataGridViewTextBoxColumn noteColumn = new DataGridViewTextBoxColumn();
            noteColumn.DataPropertyName = "Note";
            noteColumn.Width = 300;
            noteColumn.HeaderText = "Chú ý";
            noteColumn.ValueType = typeof(string);
            dgwBillingList.Columns.Add(noteColumn);

            DataGridViewImageColumn deleteButton = new DataGridViewImageColumn();
            deleteButton.Image = Properties.Resources.erase;
            deleteButton.Width = 40;
            deleteButton.HeaderText = "Xóa";
            deleteButton.ImageLayout = DataGridViewImageCellLayout.Normal;
            dgwBillingList.Columns.Add(deleteButton);
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
                Code = txtCode.Text,
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
                        //Product mu = (Product)dgv.DataBoundItem;
                        int id = ObjectHelper.GetValueFromAnonymousType<int>(currentRow.DataBoundItem, "Id");
                        Bill bill = billService.GetBill(id);
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
                            AfterDebit = beforeDebit + bill.Amount,
                            CreatedDate = DateTime.Now
                        };
                        bool kq = cls.AddCustomerLog(cl);
                        if (!billService.DeleteBill(id) && kq)
                        {
                            MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");

                        }
                        loadBillList();
                    }

                }

            }
        }
    }
}
