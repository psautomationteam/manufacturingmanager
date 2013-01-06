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

        private void setUpDataGrid(List<CustomerLog> customerLogs)
        {
            int index = 0;
            var query = from customerLog in customerLogs
                        select new ArrearReportModel
                        {
                            Date = customerLog.CreatedDate.ToString(BHConstant.DATETIME_FORMAT),
                            Amount = Global.formatVNDCurrencyText((customerLog.BeforeDebit - customerLog.AfterDebit).ToString()),
                            RecordCode = customerLog.RecordCode,
                            Index = ++index
                        };
            dgwStockEntranceList.DataSource = query.ToList();
            SetupColumnOneCustomer();
            setColorRow(3);
        }

        private void setUpDataGridAllCustomers(List<ArrearReportModel> arrearReportModel)
        {
            dgwStockEntranceList.DataSource = arrearReportModel;
            SetupColumnAllCustomers();
            setColorRow(4);
        }

        void LoadReport()
        {
            if (cbmCustomers.SelectedValue != null)
            {
                int customerId = (int)cbmCustomers.SelectedValue;
                if (customerId != 0)
                {
                    CustomerLogService customerLogService = new CustomerLogService();
                    List<CustomerLog> customerLogs = customerLogService.GetLogsOfCustomer(customerId, dtpFrom.Value, dtpTo.Value.AddDays(1));
                    setUpDataGrid(customerLogs);
                }
                else
                {
                    CustomerService customerService = new CustomerService();
                    var customers = customerService.GetCustomers();
                    CustomerLogService customerLogService = new CustomerLogService();
                    List<ArrearReportModel> arrearReportModel = new List<ArrearReportModel>();
                    int index = 0;
                    foreach (Customer ct in customers)
                    {
                        CustomerLog ctl = customerLogService.GetNewestCustomerLog(ct.Id);
                        ArrearReportModel arrear = new ArrearReportModel { 
                            CustomerName = ct.CustomerName,
                            Date = ctl.CreatedDate.ToString(BHConstant.DATETIME_FORMAT),
                            Amount = Global.formatVNDCurrencyText(ctl.AfterDebit.ToString()),
                            RecordCode = ctl.RecordCode,
                            Index = ++index
                        };
                        arrearReportModel.Add(arrear);
                    }
                    setUpDataGridAllCustomers(arrearReportModel);
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
            amountColumn.DataPropertyName = "Amount";
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
    }
}
