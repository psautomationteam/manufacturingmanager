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
        
        void LoadReport()
        {
            lbTotal.Text = "(VND) 0";
            if (cbmCustomers.SelectedValue != null)
            {
                int customerId = (int)cbmCustomers.SelectedValue;
                if (customerId != 0)
                {
                    double total = 0.0;
                    CustomerLogService customerLogService = new CustomerLogService();
                    List<CustomerReport> arrearReportModels = customerLogService.GetReportsOfCustomer(customerId, dtpFrom.Value, 
                        dtpTo.Value.AddDays(1).Date, ref total);
                    dgwStockEntranceList.DataSource = arrearReportModels;
                    SetupColumnOneCustomer();
                    lbTotal.Text = Global.formatVNDCurrencyText(total.ToString());
                }
                else
                {
                    double total = 0.0;
                    CustomerLogService customerLogService = new CustomerLogService();
                    List<CustomersReport> arrearReportModels = customerLogService.GetReportsOfCustomers(dtpFrom.Value, dtpTo.Value.AddDays(1).Date, ref total);
                    dgwStockEntranceList.DataSource = arrearReportModels;
                    SetupColumnAllCustomers();
                    setColorRow(4);
                    lbTotal.Text = Global.formatVNDCurrencyText(total.ToString());
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
            dgwStockEntranceList.Columns.Add(indexColumn);

            DataGridViewTextBoxColumn dateColumn = new DataGridViewTextBoxColumn();
            dateColumn.DefaultCellStyle.Format = BHConstant.DATETIME_FORMAT;
            dateColumn.Width = 100;
            dateColumn.DataPropertyName = "Date";
            dateColumn.HeaderText = "Ngày";
            dateColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(dateColumn);
            
            DataGridViewTextBoxColumn productNameColumn = new DataGridViewTextBoxColumn();
            productNameColumn.Width = 150;
            productNameColumn.DataPropertyName = "ProductName";
            productNameColumn.HeaderText = "Mặt hàng";
            productNameColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(productNameColumn);

            DataGridViewTextBoxColumn AttrNameColumn = new DataGridViewTextBoxColumn();
            AttrNameColumn.Width = 100;
            AttrNameColumn.DataPropertyName = "AttrName";
            AttrNameColumn.HeaderText = "Quy cách";
            AttrNameColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(AttrNameColumn);

            DataGridViewTextBoxColumn numberColumn = new DataGridViewTextBoxColumn();
            numberColumn.Width = 80;
            numberColumn.DataPropertyName = "Number";
            numberColumn.HeaderText = "Số lượng";
            numberColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(numberColumn);

            DataGridViewTextBoxColumn UnitColumn = new DataGridViewTextBoxColumn();
            UnitColumn.Width = 100;
            UnitColumn.DataPropertyName = "Unit";
            UnitColumn.HeaderText = "ĐVT";
            UnitColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(UnitColumn);

            DataGridViewTextBoxColumn costColumn = new DataGridViewTextBoxColumn();
            costColumn.Width = 100;
            costColumn.DataPropertyName = "Cost";
            costColumn.HeaderText = "Đơn giá";
            costColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(costColumn);
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
            dgwStockEntranceList.Columns.Add(indexColumn);

            DataGridViewTextBoxColumn dateColumn = new DataGridViewTextBoxColumn();
            dateColumn.DefaultCellStyle.Format = BHConstant.DATETIME_FORMAT;
            dateColumn.Width = 100;
            dateColumn.DataPropertyName = "Date";
            dateColumn.HeaderText = "Ngày";
            dateColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(dateColumn);

            DataGridViewTextBoxColumn customerNameColumn = new DataGridViewTextBoxColumn();
            customerNameColumn.Width = 150;
            customerNameColumn.DataPropertyName = "CustomerName";
            customerNameColumn.HeaderText = "Tên khách hàng";
            customerNameColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(customerNameColumn);

            DataGridViewTextBoxColumn customerCodeColumn = new DataGridViewTextBoxColumn();
            customerCodeColumn.Width = 150;
            customerCodeColumn.DataPropertyName = "CustomerCode";
            customerCodeColumn.HeaderText = "Mã khách hàng";
            customerCodeColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(customerCodeColumn);

            DataGridViewTextBoxColumn recordCodeColumn = new DataGridViewTextBoxColumn();
            recordCodeColumn.Width = 100;
            recordCodeColumn.DataPropertyName = "RecordCode";
            recordCodeColumn.HeaderText = "Mã phiếu";
            recordCodeColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(recordCodeColumn);

            DataGridViewTextBoxColumn amountColumn = new DataGridViewTextBoxColumn();
            amountColumn.DataPropertyName = "Amount";
            amountColumn.Width = 150;
            amountColumn.HeaderText = "Số tiền";
            amountColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(amountColumn);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgwStockEntranceList.AutoGenerateColumns = false;
            LoadReport();
        }
        
        private void setColorRow(int followValueColumn)
        {
            foreach (DataGridViewRow row in dgwStockEntranceList.Rows)
            {
                if (row.Cells[followValueColumn].Value.ToString().Contains("BH"))
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
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
