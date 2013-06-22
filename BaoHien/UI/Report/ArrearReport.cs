using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Microsoft.Win32;
using System.Diagnostics;
using iTextSharp.text.pdf;
using System.Drawing.Printing;
using iTextSharp.text;
using System.IO;

namespace BaoHien.UI
{
    public partial class ArrearReport : UserControl
    {
        List<Customer> customers;
        int modeReport = 0;
        List<CustomerReport> customerReports = new List<CustomerReport>();
        List<CustomersReport> customersReports = new List<CustomersReport>();
        string textForPrint = "";
        Customer customerPrint;

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
            modeReport = 0;
            lbTotal.Text = "(VND) 0";
            if (cbmCustomers.SelectedValue != null)
            {
                int customerId = (int)cbmCustomers.SelectedValue;
                if (customerId != 0)
                {
                    modeReport = 1;
                    textForPrint = "Từ ngày " + dtpFrom.Value.ToString(BHConstant.DATE_FORMAT) + " đến ngày " + dtpTo.Value.ToString(BHConstant.DATE_FORMAT); 
                    customerPrint = ((Customer)cbmCustomers.SelectedItem);
                    double total = 0.0;
                    CustomerLogService customerLogService = new CustomerLogService();
                    customerReports = customerLogService.GetReportsOfCustomer(customerId, dtpFrom.Value, 
                        dtpTo.Value.AddDays(1).Date, ref total);
                    dgwStockEntranceList.DataSource = customerReports;
                    SetupColumnOneCustomer();
                    lbTotal.Text = Global.formatVNDCurrencyText(total.ToString());
                }
                else
                {
                    double total = 0.0;
                    textForPrint = "Từ ngày " + dtpFrom.Value.ToString(BHConstant.DATE_FORMAT) + " đến ngày " + dtpTo.Value.ToString(BHConstant.DATE_FORMAT);
                    CustomerLogService customerLogService = new CustomerLogService();
                    customersReports = customerLogService.GetReportsOfCustomers(dtpFrom.Value, dtpTo.Value.AddDays(1).Date, ref total);
                    dgwStockEntranceList.DataSource = customersReports;
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
                CustomerName = "Tất cả",
                CustCode = "Tất cả"
            };
            customers.Add(ctm);
            customers = customers.OrderBy(ct => ct.Id).ToList();

            cbmCustomers.AutoCompleteMode = AutoCompleteMode.Append;
            cbmCustomers.AutoCompleteSource = AutoCompleteSource.ListItems;
            cbmCustomers.DataSource = customers;
            cbmCustomers.DisplayMember = "CustCode";
            cbmCustomers.ValueMember = "Id";
        }
        
        private void SetupColumnOneCustomer()
        {
            dgwStockEntranceList.Columns.Clear();
            dgwStockEntranceList.AutoGenerateColumns = false;

            dgwStockEntranceList.Columns.Add(Global.CreateCell("Index", "STT", 30));
            dgwStockEntranceList.Columns.Add(Global.CreateCell("Date", "Ngày", 100));
            dgwStockEntranceList.Columns.Add(Global.CreateCell("ProductName", "Mặt hàng", 150));
            dgwStockEntranceList.Columns.Add(Global.CreateCell("AttrName", "Quy cách", 100));
            dgwStockEntranceList.Columns.Add(Global.CreateCellWithAlignment("Number", "Số lượng", 80, DataGridViewContentAlignment.MiddleRight));
            dgwStockEntranceList.Columns.Add(Global.CreateCellWithAlignment("Unit", "ĐVT", 100, DataGridViewContentAlignment.MiddleRight));
            dgwStockEntranceList.Columns.Add(Global.CreateCellWithAlignment("Cost", "Tổng giá", 100, DataGridViewContentAlignment.MiddleRight));
        }

        private void SetupColumnAllCustomers()
        {
            dgwStockEntranceList.Columns.Clear();
            dgwStockEntranceList.AutoGenerateColumns = false;

            dgwStockEntranceList.Columns.Add(Global.CreateCell("Index", "STT", 30));
            dgwStockEntranceList.Columns.Add(Global.CreateCell("Date", "Ngày", 100));
            dgwStockEntranceList.Columns.Add(Global.CreateCell("CustomerName", "Tên khách hàng", 150));
            dgwStockEntranceList.Columns.Add(Global.CreateCell("CustomerCode", "Mã khách hàng", 150));
            dgwStockEntranceList.Columns.Add(Global.CreateCell("RecordCode", "Mã phiếu", 150));
            dgwStockEntranceList.Columns.Add(Global.CreateCellWithAlignment("Amount", "Số tiền", 80, DataGridViewContentAlignment.MiddleRight));
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
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if ((modeReport == 1 && customerReports.Count > 0)
                || (modeReport != 1 && customersReports.Count > 0))
            {
                PrintDialog pd = new PrintDialog();
                pd.PrinterSettings = new PrinterSettings();
                if (DialogResult.OK == pd.ShowDialog(this))
                {
                    this.Cursor = Cursors.AppStarting;
                    Print(pd.PrinterSettings.PrinterName);
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để in ấn!", "Lỗi hệ thống");
            }
        }

        private void ExportFile()
        {
            Global.checkDirSaveFile();
            var doc = new Document(PageSize.A4, 20, 20, 10, 10);
            PdfWriter docWriter = PdfWriter.GetInstance(doc, new FileStream(BHConstant.SAVE_IN_DIRECTORY + @"\CongNo.pdf", FileMode.Create));
            PdfWriterEvents writerEvent;

            Image watermarkImage = Image.GetInstance(AppDomain.CurrentDomain.BaseDirectory + @"logo.png");
            watermarkImage.SetAbsolutePosition(doc.PageSize.Width / 2 - 70, 500);
            writerEvent = new PdfWriterEvents(watermarkImage);
            docWriter.PageEvent = writerEvent;

            doc.Open();
            if (modeReport == 1)
            {
                doc.Add(FormatConfig.ParaRightBeforeHeaderRight(BHConstant.COMPANY_NAME));
                doc.Add(FormatConfig.ParaRightBeforeHeaderRight(BHConstant.COMPANY_ADDRESS));
                doc.Add(FormatConfig.ParaRightBeforeHeaderRight("ĐT: " + BHConstant.COMPANY_PHONE + " Fax: " + BHConstant.COMPANY_FAX));
            }

            doc.Add(FormatConfig.ParaRightBeforeHeader("In ngày : " + BaoHienRepository.GetBaoHienDBDataContext().GetSystemDate().ToString(BHConstant.DATETIME_FORMAT)));
            doc.Add(FormatConfig.ParaHeader("BÁO CÁO CÔNG NỢ"));

            doc.Add(FormatConfig.ParaRightBelowHeader("(" + textForPrint + ")"));
            if (modeReport != 1)
                doc.Add(CustomersTable());
            else
            {
                doc.Add(FormatConfig.ParaCommonInfo("Kính gửi : ", customerPrint.CustomerName));
                doc.Add(FormatConfig.ParaCommonInfo("Địa chỉ : ", customerPrint.Address));
                doc.Add(FormatConfig.ParaCommonInfo("ĐT : ", customerPrint.Phone + " Fax: " + customerPrint.Fax));
                doc.Add(CustomerDetailTable());
            }
            doc.Add(FormatConfig.ParaCommonInfo("", "Quý khách hàng kiểm tra và đối chiếu, nếu có thắc mắc vui lòng liên hệ số điện thoại trên."));
            doc.Add(FormatConfig.ParaCommonInfo("", "Chân thành cảm ơn sự hợp tác của quí khách."));
            doc.Add(FormatConfig.ParaCommonInfo("Ghi chú : ", String.Concat(Enumerable.Repeat("...", 96))));

            doc.Close();
        }

        private PdfPTable CustomersTable()
        {
            PdfPTable table = FormatConfig.Table(6, new float[] { 0.5f, 1.5f, 2.5f, 2.5f, 2f, 1f });
            table.AddCell(FormatConfig.TableCellHeader("STT"));
            table.AddCell(FormatConfig.TableCellHeader("Ngày"));
            table.AddCell(FormatConfig.TableCellHeader("Tên khách hàng"));
            table.AddCell(FormatConfig.TableCellHeader("Mã khách hàng"));
            table.AddCell(FormatConfig.TableCellHeader("Mã phiếu"));
            table.AddCell(FormatConfig.TableCellHeader("Số tiền"));

            foreach (CustomersReport item in customersReports)
            {
                table.AddCell(FormatConfig.TableCellBody(item.Index.ToString(), PdfPCell.ALIGN_CENTER));
                table.AddCell(FormatConfig.TableCellBody(item.Date, PdfPCell.ALIGN_CENTER));
                table.AddCell(FormatConfig.TableCellBody(item.CustomerName, PdfPCell.ALIGN_CENTER));
                table.AddCell(FormatConfig.TableCellBody(item.CustomerCode, PdfPCell.ALIGN_CENTER));
                table.AddCell(FormatConfig.TableCellBody(item.RecordCode, PdfPCell.ALIGN_CENTER));
                table.AddCell(FormatConfig.TableCellBody(item.Amount, PdfPCell.ALIGN_RIGHT));
            }
            table.AddCell(FormatConfig.TableCellBoldBody("Tổng", PdfPCell.ALIGN_RIGHT, 5));
            table.AddCell(FormatConfig.TableCellBody(lbTotal.Text.Split(' ')[2], PdfPCell.ALIGN_RIGHT));
            return table;
        }

        private PdfPTable CustomerDetailTable()
        {
            PdfPTable table = FormatConfig.Table(7, new float[] { 0.5f, 1.5f, 2f, 2f, 1.2f, 1.3f, 1.5f });
            table.AddCell(FormatConfig.TableCellHeader("STT"));
            table.AddCell(FormatConfig.TableCellHeader("Ngày"));
            table.AddCell(FormatConfig.TableCellHeader("Mặt hàng"));
            table.AddCell(FormatConfig.TableCellHeader("Quy cách"));
            table.AddCell(FormatConfig.TableCellHeader("Số lượng"));
            table.AddCell(FormatConfig.TableCellHeader("ĐVT"));
            table.AddCell(FormatConfig.TableCellHeader("Tổng giá"));

            foreach (CustomerReport item in customerReports)
            {
                if (string.IsNullOrEmpty(item.Index))
                {
                    table.AddCell(FormatConfig.TableCellBoldBody(item.Date, PdfPCell.ALIGN_LEFT, 7));
                }
                else
                {
                    table.AddCell(FormatConfig.TableCellBody(item.Index, PdfPCell.ALIGN_CENTER));
                    table.AddCell(FormatConfig.TableCellBody(item.Date, PdfPCell.ALIGN_CENTER));
                    table.AddCell(FormatConfig.TableCellBody(item.ProductName, PdfPCell.ALIGN_CENTER));
                    table.AddCell(FormatConfig.TableCellBody(item.AttrName, PdfPCell.ALIGN_CENTER));
                    table.AddCell(FormatConfig.TableCellBody(item.Number, PdfPCell.ALIGN_RIGHT));
                    table.AddCell(FormatConfig.TableCellBody(item.Unit, PdfPCell.ALIGN_CENTER));
                    table.AddCell(FormatConfig.TableCellBody(item.Cost, PdfPCell.ALIGN_RIGHT));
                }
            }
            table.AddCell(FormatConfig.TableCellBoldBody("Tổng", PdfPCell.ALIGN_RIGHT, 6));
            table.AddCell(FormatConfig.TableCellBody(lbTotal.Text.Split(' ')[2], PdfPCell.ALIGN_RIGHT));
            return table;
        }

        private void Print(string printerName)
        {
            ExportFile();
            // Print the file to the printer.
            try
            {
                string foxit = Registry.LocalMachine.OpenSubKey("Software").OpenSubKey("Foxit Software").OpenSubKey("Foxit Reader")
                    .GetValue("InnoSetupUpdatePath").ToString().Replace("unins000", "Foxit Reader");
                string file1 = BHConstant.SAVE_IN_DIRECTORY + @"\CongNo.pdf";

                Process pdf_print1 = new Process();
                pdf_print1.StartInfo.FileName = foxit;
                pdf_print1.StartInfo.Arguments = string.Format(@"-t ""{0}"" ""{1}""", file1, printerName);
                pdf_print1.StartInfo.CreateNoWindow = true;
                pdf_print1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                pdf_print1.Start();
                pdf_print1.WaitForExit(20000);
                if (!pdf_print1.HasExited)
                {
                    pdf_print1.Kill();
                    pdf_print1.Dispose();
                    MessageBox.Show("Không thể in danh sách khách hàng lúc này!");
                }
            }
            catch
            {
                MessageBox.Show("Không thể kết nối máy in!");
            }
            this.Cursor = Cursors.Default;
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
    }
}
