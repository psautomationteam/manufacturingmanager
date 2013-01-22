using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using BaoHien.Services.Products;
using BaoHien.Model;
using BaoHien.Services.Customers;
using BaoHien.Common;
using DAL.Helper;
using BaoHien.Services.Orders;
using BaoHien.Services.Bills;

namespace BaoHien.UI
{
    public partial class ArrearReport : UserControl
    {
        List<Customer> customers;

        public ArrearReport()
        {
            InitializeComponent();
            LoadCustomers();
            dtpFrom.Value = DateTime.Today.AddMonths(-1);
            dtpFrom.CustomFormat = BHConstant.DATE_FORMAT;
            dtpTo.CustomFormat = BHConstant.DATE_FORMAT;
        }
        
        private void setUpDataGrid(List<ArrearReportModel> arrearReportModel, int colColor)
        {
            dgwStockEntranceList.DataSource = arrearReportModel;
            if (colColor == 3)
            {
                SetupColumnOneCustomer();
                ArrearReportModel last = arrearReportModel.OrderByDescending(a => a.ID).FirstOrDefault();
                if (last != null)
                {
                    lbTotal.Text = last.AfterDebit;
                }
            }
            else
            {
                SetupColumnAllCustomers();
                double total = arrearReportModel.Sum(a => a.AfterDebitNumber);
                lbTotal.Text = Global.formatVNDCurrencyText(total.ToString());
            }
            setColorRow(colColor);
        }

        void LoadReport()
        {
            lbTotal.Text = "(VND) 0";
            if (cbmCustomers.SelectedValue != null)
            {
                int customerId = (int)cbmCustomers.SelectedValue;
                if (customerId != 0)
                {
                    CustomerLogService customerLogService = new CustomerLogService();
                    List<ArrearReportModel> arrearReportModels = customerLogService.GetReportsOfCustomer(customerId, dtpFrom.Value, dtpTo.Value.AddDays(1));
                    setUpDataGrid(arrearReportModels, 3);
                }
                else
                {
                    CustomerLogService customerLogService = new CustomerLogService();
                    List<ArrearReportModel> arrearReportModels = customerLogService.GetReportsOfCustomers(dtpFrom.Value, dtpTo.Value.AddDays(1));
                    setUpDataGrid(arrearReportModels, 4);
                }
            }
            else
            {
                MessageBox.Show("Không đủ thông tin để lập báo cáo!");
            }
        }

        void LoadCustomers()
        {
            CustomerService customerService = new CustomerService();
            customers = customerService.GetCustomers();
            Customer ctm = new Customer { 
                Id = 0,
                CustomerName = "Tất cả"
            };
            customers.Add(ctm);
            customers = customers.OrderBy(ct => ct.Id).ToList();
            if (customers != null)
            {
                cbmCustomers.DataSource = customers;
                cbmCustomers.DisplayMember = "CustomerName";
                cbmCustomers.ValueMember = "Id";
            }
        }
        
        private void SetupColumnOneCustomer()
        {
            dgwStockEntranceList.Columns.Clear();
            dgwStockEntranceList.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn indexColumn = new DataGridViewTextBoxColumn();
            indexColumn.Width = 30;
            indexColumn.DataPropertyName = "Index";
            indexColumn.HeaderText = "STT";
            indexColumn.ValueType = typeof(string);
            //indexColumn.Frozen = true;
            dgwStockEntranceList.Columns.Add(indexColumn);

            DataGridViewTextBoxColumn recordCodeColumn = new DataGridViewTextBoxColumn();
            recordCodeColumn.Width = 150;
            recordCodeColumn.DataPropertyName = "RecordCode";
            recordCodeColumn.HeaderText = "Mã phiếu";
            recordCodeColumn.ValueType = typeof(string);
            //recordCodeColumn.Frozen = true;
            dgwStockEntranceList.Columns.Add(recordCodeColumn);

            DataGridViewTextBoxColumn dateColumn = new DataGridViewTextBoxColumn();
            dateColumn.DefaultCellStyle.Format = BHConstant.DATETIME_FORMAT;
            dateColumn.Width = 150;
            dateColumn.DataPropertyName = "Date";
            dateColumn.HeaderText = "Ngày";
            dateColumn.ValueType = typeof(string);
            //dateColumn.Frozen = true;
            dgwStockEntranceList.Columns.Add(dateColumn);
            
            DataGridViewTextBoxColumn amountColumn = new DataGridViewTextBoxColumn();
            amountColumn.DataPropertyName = "Amount";
            amountColumn.Width = 150;
            amountColumn.HeaderText = "Số tiền";
            //amountColumn.Frozen = true;
            amountColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(amountColumn);
        }

        private void SetupColumnAllCustomers()
        {
            dgwStockEntranceList.Columns.Clear();
            dgwStockEntranceList.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn indexColumn = new DataGridViewTextBoxColumn();
            indexColumn.Width = 30;
            indexColumn.DataPropertyName = "Index";
            indexColumn.HeaderText = "STT";
            indexColumn.ValueType = typeof(string);
            //indexColumn.Frozen = true;
            dgwStockEntranceList.Columns.Add(indexColumn);

            DataGridViewTextBoxColumn customerNameColumn = new DataGridViewTextBoxColumn();
            customerNameColumn.Width = 150;
            customerNameColumn.DataPropertyName = "CustomerName";
            customerNameColumn.HeaderText = "Tên khách hàng";
            customerNameColumn.ValueType = typeof(string);
            //customerNameColumn.Frozen = true;
            dgwStockEntranceList.Columns.Add(customerNameColumn);

            DataGridViewTextBoxColumn recordCodeColumn = new DataGridViewTextBoxColumn();
            recordCodeColumn.Width = 150;
            recordCodeColumn.DataPropertyName = "RecordCode";
            recordCodeColumn.HeaderText = "Mã phiếu (giao dịch cuối)";
            recordCodeColumn.ValueType = typeof(string);
            //recordCodeColumn.Frozen = true;
            dgwStockEntranceList.Columns.Add(recordCodeColumn);

            DataGridViewTextBoxColumn dateColumn = new DataGridViewTextBoxColumn();
            dateColumn.DefaultCellStyle.Format = BHConstant.DATETIME_FORMAT;
            dateColumn.Width = 150;
            dateColumn.DataPropertyName = "Date";
            dateColumn.HeaderText = "Ngày";
            dateColumn.ValueType = typeof(string);
            //dateColumn.Frozen = true;
            dgwStockEntranceList.Columns.Add(dateColumn);

            DataGridViewTextBoxColumn amountColumn = new DataGridViewTextBoxColumn();
            amountColumn.DataPropertyName = "AfterDebit";
            amountColumn.Width = 150;
            amountColumn.HeaderText = "Số tiền";
            //amountColumn.Frozen = true;
            amountColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(amountColumn);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgwStockEntranceList.AutoGenerateColumns = false;
            LoadReport();
        }

        private void cbmCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedIndex == 0)
            {
                dtpFrom.Enabled = false;
                dtpTo.Enabled = false;
            }
            else
            {
                dtpFrom.Enabled = true;
                dtpTo.Enabled = true;
            }
        }

        private void setColorRow(int followValueColumn)
        {
            foreach (DataGridViewRow row in dgwStockEntranceList.Rows)
            {
                row.DefaultCellStyle.ForeColor = Color.White;
                if (row.Cells[followValueColumn].Value.ToString().Contains("-"))
                {
                    row.DefaultCellStyle.BackColor = Color.Blue;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        private void dgwStockEntranceList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow currentRow = dgwStockEntranceList.Rows[e.RowIndex];
            string RecordCode = ObjectHelper.GetValueFromAnonymousType<string>(currentRow.DataBoundItem, "RecordCode");
            string prefix = RecordCode.Substring(0, 2);
            switch (prefix)
            {
                case BHConstant.PREFIX_FOR_ORDER:
                    {
                        OrderService orderService = new OrderService();
                        Order order = orderService.GetOrders().Where(o => o.OrderCode == RecordCode).FirstOrDefault();
                        if (order != null)
                        {
                            AddOrder frmAddOrder = new AddOrder();
                            frmAddOrder.loadDataForEditOrder(order.Id);

                            frmAddOrder.CallFromUserControll = this;
                            frmAddOrder.ShowDialog();
                        }
                    } break;
                case BHConstant.PREFIX_FOR_BILLING:
                    {
                        BillService billService = new BillService();
                        Bill bill = billService.GetBills().Where(b => b.BillCode == RecordCode).FirstOrDefault();
                        if (bill != null)
                        {
                            AddBill frmAddBill = new AddBill();
                            frmAddBill.loadDataForEditBill(bill.Id);

                            frmAddBill.CallFromUserControll = this;
                            frmAddBill.ShowDialog();
                        }
                    } break;
            }
        }
    }
}
