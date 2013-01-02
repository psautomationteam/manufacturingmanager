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
using BaoHien.Services.MaterialInStocks;
using BaoHien.Services.Customers;
using BaoHien.Common;

namespace BaoHien.UI
{
    public partial class ArrearReport : UserControl
    {
        List<Customer> customers;
        List<EntranceStockReport> entranceStockReports;

        public ArrearReport()
        {
            InitializeComponent();
            LoadCustomers();
            dtpFrom.Value = DateTime.Today.AddMonths(-1);
            dtpFrom.CustomFormat = "dd/MM/yyyy";
            dtpTo.CustomFormat = "dd/MM/yyyy";
        }

        private void setUpDataGrid(List<CustomerLog> customerLogs)
        {
            int index = 0;
            var query = from customerLog in customerLogs
                        select new ArrearReportModel
                        {
                            Date = customerLog.CreatedDate.ToString("dd/MM/yyyy HH:mm:ss"),
                            Amount = Global.formatVNDCurrencyText((customerLog.BeforeDebit - customerLog.AfterDebit).ToString()),
                            RecordCode = customerLog.RecordCode,
                            Index = ++index
                        };
            dgwStockEntranceList.DataSource = query.ToList();
        }

        void LoadReport()
        {
            if (cbmCustomers.SelectedValue != null)
            {
                int customerId = (int)cbmCustomers.SelectedValue;
                if (customerId != 0)
                {
                    CustomerLogService customerLogService = new CustomerLogService();
                    List<CustomerLog> customerLogs = customerLogService.GetLogsOfCustomer(customerId, dtpFrom.Value, dtpTo.Value);
                    setUpDataGrid(customerLogs);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn loại sản phẩm");
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
        
        private void SetupColumns()
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
            //productCodeColumn.Frozen = true;
            dgwStockEntranceList.Columns.Add(recordCodeColumn);

            DataGridViewTextBoxColumn dateColumn = new DataGridViewTextBoxColumn();
            dateColumn.Width = 150;
            dateColumn.DataPropertyName = "Date";
            dateColumn.HeaderText = "Ngày";
            dateColumn.ValueType = typeof(string);
            //productNameColumn.Frozen = true;
            dgwStockEntranceList.Columns.Add(dateColumn);
            
            DataGridViewTextBoxColumn amountColumn = new DataGridViewTextBoxColumn();
            amountColumn.DataPropertyName = "Amount";
            amountColumn.Width = 150;
            amountColumn.HeaderText = "Số tiền";
            //attributeNameColumn.Frozen = true;
            amountColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(amountColumn);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgwStockEntranceList.AutoGenerateColumns = false;
            LoadReport();
            SetupColumns();
        }
    }
}
