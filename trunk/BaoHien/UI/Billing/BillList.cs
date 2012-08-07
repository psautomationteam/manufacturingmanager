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
            if (customers == null)
            {
                CustomerService customerService = new CustomerService();
                customers = customerService.GetCustomers();
            }
            if (customers != null)
            {


                cmbCustomer.DataSource = customers;

                cmbCustomer.DisplayMember = "CustomerName";
                cmbCustomer.ValueMember = "Id";

            }
            if (systemUsers == null)
            {
                SystemUserService systemUserService = new SystemUserService();
                systemUsers = systemUserService.GetSystemUsers();
            }
            if (systemUsers != null)
            {


                cbmUsers.DataSource = systemUsers;

                cbmUsers.DisplayMember = "FullName";
                cbmUsers.ValueMember = "Id";

            }

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
                                Total = bill.Amount,
                                Note = bill.Note,
                                CreateBy = bill.SystemUser.FullName,

                                CreatedDate = bill.CreatedDate.ToShortDateString(),
                                
                                Index = index++
                            };
                dgwBillingList.DataSource = query.ToList();

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
            indexColumn.Frozen = true;
            dgwBillingList.Columns.Add(indexColumn);
            DataGridViewTextBoxColumn billCodeColumn = new DataGridViewTextBoxColumn();
            billCodeColumn.Width = 100;
            billCodeColumn.DataPropertyName = "BillCode";
            billCodeColumn.HeaderText = "Mã đặt hàng";
            billCodeColumn.ValueType = typeof(string);
            billCodeColumn.Frozen = true;
            dgwBillingList.Columns.Add(billCodeColumn);



            DataGridViewTextBoxColumn customerNameColumn = new DataGridViewTextBoxColumn();
            customerNameColumn.DataPropertyName = "CustomerName";
            customerNameColumn.Width = 200;
            customerNameColumn.HeaderText = "Khách hàng";
            customerNameColumn.Frozen = true;
            customerNameColumn.ValueType = typeof(string);
            dgwBillingList.Columns.Add(customerNameColumn);

            

            

           


            DataGridViewTextBoxColumn createByColumn = new DataGridViewTextBoxColumn();
            createByColumn.DataPropertyName = "CreateBy";
            createByColumn.Width = 100;
            createByColumn.HeaderText = "Người tạo";
            createByColumn.Frozen = true;
            createByColumn.ValueType = typeof(string);
            dgwBillingList.Columns.Add(createByColumn);

            DataGridViewTextBoxColumn createdDateColumn = new DataGridViewTextBoxColumn();
            createdDateColumn.DataPropertyName = "CreatedDate";
            createdDateColumn.Width = 100;
            createdDateColumn.HeaderText = "Ngày tạo";
            createdDateColumn.Frozen = true;
            createdDateColumn.ValueType = typeof(string);
            dgwBillingList.Columns.Add(createdDateColumn);

            DataGridViewTextBoxColumn totalColumn = new DataGridViewTextBoxColumn();
            totalColumn.DataPropertyName = "Total";
            totalColumn.Width = 50;
            totalColumn.HeaderText = "Tổng";
            totalColumn.Frozen = true;
            totalColumn.ValueType = typeof(string);
            dgwBillingList.Columns.Add(totalColumn);

            DataGridViewTextBoxColumn noteColumn = new DataGridViewTextBoxColumn();
            noteColumn.DataPropertyName = "Note";
            noteColumn.Width = 300;
            noteColumn.HeaderText = "Chú ý";
            noteColumn.Frozen = true;
            noteColumn.ValueType = typeof(string);
            dgwBillingList.Columns.Add(noteColumn);

            DataGridViewImageColumn deleteButton = new DataGridViewImageColumn();
            deleteButton.Image = Properties.Resources.erase;
            deleteButton.Width = 100;
            deleteButton.HeaderText = "Xóa";
            deleteButton.ReadOnly = true;
            deleteButton.ImageLayout = DataGridViewImageCellLayout.Normal;



            dgwBillingList.Columns.Add(deleteButton);
        }

        private void BillList_Load(object sender, EventArgs e)
        {
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
                CreatedBy = cbmUsers.SelectedValue != null && (int)cbmUsers.SelectedValue > 0 ? (int?)cbmUsers.SelectedValue : (int?)null,
                CustId = cmbCustomer.SelectedValue != null && (int)cmbCustomer.SelectedValue > 0 ? (int?)cmbCustomer.SelectedValue : (int?)null,
                From = dtpFrom.Value != null ? dtpFrom.Value : (DateTime?)null,
                To = dtpTo.Value != null ? dtpTo.Value : (DateTime?)null,
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
                        if (!billService.DeleteBill(id))
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
