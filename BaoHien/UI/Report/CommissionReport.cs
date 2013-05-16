using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DAL;
using BaoHien.Services.Employees;
using BaoHien.Common;
using BaoHien.Model;
using DAL.Helper;
using BaoHien.Services.Orders;
using System.Drawing.Printing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;

namespace BaoHien.UI
{
    public partial class CommissionReport : UserControl
    {
        List<Employee> employees;
        List<EmployeesReport> employees_reports = new List<EmployeesReport>();
        EmployeeLogService employeeLogService;
        List<EmployeeReport> employee_reports = new List<EmployeeReport>();
        string textForPrint = "", textEmployee = "";
        int modeReport = 0;

        public CommissionReport()
        {
            employeeLogService = new EmployeeLogService();
            InitializeComponent();
            LoadEmployees();
            dtpFrom.Value = DateTime.Today.AddDays(-DateTime.Now.Day + 1);
            dtpFrom.CustomFormat = BHConstant.DATE_FORMAT;
            dtpTo.CustomFormat = BHConstant.DATE_FORMAT;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgwEmployeeCommissionList.AutoGenerateColumns = false;
            LoadReport();
        }

        void LoadEmployees()
        {
            EmployeeService employeeService = new EmployeeService();
            employees = employeeService.GetEmployees();
            Employee e = new Employee
            {
                Id = 0,
                FullName = "Tất cả"
            };
            employees.Add(e);
            employees = employees.OrderBy(el => el.Id).ToList();
            if (employees != null)
            {
                cbmEmployees.DataSource = employees;
                cbmEmployees.DisplayMember = "FullName";
                cbmEmployees.ValueMember = "Id";
            }
        }

        void LoadReport()
        {
            btnExportExcel.Visible = false;
            modeReport = 0;
            lbTotal.Text = "(VND) 0";
            if (cbmEmployees.SelectedValue != null)
            {
                int employeeId = (int)cbmEmployees.SelectedValue;
                if (employeeId == 0)
                {
                    double total = 0.0;
                    textForPrint = "Từ ngày " + dtpFrom.Value.ToString(BHConstant.DATE_FORMAT) + " đến ngày " + dtpTo.Value.ToString(BHConstant.DATE_FORMAT); 
                    employees_reports = employeeLogService.GetReportsOfEmployees(dtpFrom.Value, dtpTo.Value.AddDays(1).Date, ref total);
                    dgwEmployeeCommissionList.DataSource = employees_reports;
                    SetupDataGrid();
                    lbTotal.Text = Global.formatVNDCurrencyText(total.ToString());
                }
                else
                {
                    modeReport = 1;
                    btnExportExcel.Visible = true;
                    textForPrint = "Từ ngày " + dtpFrom.Value.ToString(BHConstant.DATE_FORMAT) + " đến ngày " + dtpTo.Value.ToString(BHConstant.DATE_FORMAT); 
                    textEmployee = ((Employee)cbmEmployees.SelectedItem).FullName;
                    double total = 0.0;
                    employee_reports = employeeLogService.GetReportsOfEmployee(employeeId, dtpFrom.Value, 
                        dtpTo.Value.AddDays(1).Date, ref total);
                    dgwEmployeeCommissionList.DataSource = employee_reports;
                    SetupDataGridDetail();
                    lbTotal.Text = Global.formatVNDCurrencyText(total.ToString());
                }
            }
        }

        private void cbmEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void SetupDataGrid()
        {
            dgwEmployeeCommissionList.Columns.Clear();
            dgwEmployeeCommissionList.AutoGenerateColumns = false;

            dgwEmployeeCommissionList.Columns.Add(Global.CreateCell("Index", "STT", 30));
            dgwEmployeeCommissionList.Columns.Add(Global.CreateCell("CreatedDate", "Ngày", 100));
            dgwEmployeeCommissionList.Columns.Add(Global.CreateCell("EmployeeName", "Nhân viên", 150));
            dgwEmployeeCommissionList.Columns.Add(Global.CreateCell("RecordCode", "Mã phiếu", 150));
            dgwEmployeeCommissionList.Columns.Add(Global.CreateCellWithAlignment("Amount", "Số tiền", 150, DataGridViewContentAlignment.MiddleRight));
        }

        void SetupDataGridDetail()
        {
            dgwEmployeeCommissionList.Columns.Clear();
            dgwEmployeeCommissionList.AutoGenerateColumns = false;

            dgwEmployeeCommissionList.Columns.Add(Global.CreateCell("Index", "STT", 30));
            dgwEmployeeCommissionList.Columns.Add(Global.CreateCell("Date", "Ngày", 100));
            dgwEmployeeCommissionList.Columns.Add(Global.CreateCell("CustomerName", "Tên khách hàng", 150));
            dgwEmployeeCommissionList.Columns.Add(Global.CreateCell("ProductName", "Mặt hàng", 150));
            dgwEmployeeCommissionList.Columns.Add(Global.CreateCell("AttrName", "Quy cách", 150));
            dgwEmployeeCommissionList.Columns.Add(Global.CreateCellWithAlignment("Number", "Số lượng", 80, DataGridViewContentAlignment.MiddleRight));
            dgwEmployeeCommissionList.Columns.Add(Global.CreateCellWithAlignment("Unit", "ĐVT", 100, DataGridViewContentAlignment.MiddleRight));
            dgwEmployeeCommissionList.Columns.Add(Global.CreateCellWithAlignment("Cost", "Đơn giá", 100, DataGridViewContentAlignment.MiddleRight));
            dgwEmployeeCommissionList.Columns.Add(Global.CreateCellWithAlignment("Commission", "Hoa hồng", 100, DataGridViewContentAlignment.MiddleRight));
        }

        private void dgwEmployeeCommissionList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow currentRow = dgwEmployeeCommissionList.Rows[e.RowIndex];
            string RecordCode = ObjectHelper.GetValueFromAnonymousType<string>(currentRow.DataBoundItem, "RecordCode");
            OrderService orderService = new OrderService();
            Order order = orderService.GetOrders().Where(o => o.OrderCode == RecordCode).FirstOrDefault();
            if (order != null)
            {
                AddOrder frmAddOrder = new AddOrder();
                frmAddOrder.loadDataForEditOrder(order.Id);

                frmAddOrder.CallFromUserControll = this;
                frmAddOrder.ShowDialog();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if ((modeReport == 1 && employee_reports.Count > 0)
                || (modeReport != 1 && employees_reports.Count > 0))
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
            PdfWriter docWriter = PdfWriter.GetInstance(doc, new FileStream(BHConstant.SAVE_IN_DIRECTORY + @"\HHong.pdf", FileMode.Create));
            PdfWriterEvents writerEvent;

            Image watermarkImage = Image.GetInstance(AppDomain.CurrentDomain.BaseDirectory + @"logo.png");
            watermarkImage.SetAbsolutePosition(doc.PageSize.Width / 2 - 70, 600);
            writerEvent = new PdfWriterEvents(watermarkImage);
            docWriter.PageEvent = writerEvent;

            doc.Open();

            doc.Add(FormatConfig.ParaRightBeforeHeader("In ngày : " + BaoHienRepository.GetBaoHienDBDataContext().GetSystemDate().ToString(BHConstant.DATETIME_FORMAT)));
            doc.Add(FormatConfig.ParaHeader("BÁO CÁO HOA HỒNG"));

            doc.Add(FormatConfig.ParaRightBelowHeader("(" + textForPrint + ")"));
            if (modeReport != 1)
            {
                doc.Add(EmployeesTable());
            }
            else
            {
                doc.Add(FormatConfig.ParaCommonInfo("Người bán hàng : ", textEmployee));
                doc.Add(EmployeeDetailTable());
            }

            doc.Add(FormatConfig.ParaCommonInfo("Ghi chú : ", String.Concat(Enumerable.Repeat("...", 96))));

            doc.Close();
        }

        private PdfPTable EmployeesTable()
        {
            PdfPTable table = FormatConfig.Table(5, new float[] { 1f, 2f, 2.5f, 2.5f, 2f });
            table.AddCell(FormatConfig.TableCellHeader("STT"));
            table.AddCell(FormatConfig.TableCellHeader("Ngày"));
            table.AddCell(FormatConfig.TableCellHeader("Nhân viên"));
            table.AddCell(FormatConfig.TableCellHeader("Mã phiếu"));
            table.AddCell(FormatConfig.TableCellHeader("Số tiền"));

            foreach (EmployeesReport item in employees_reports)
            {
                table.AddCell(FormatConfig.TableCellBody(item.Index.ToString(), PdfPCell.ALIGN_CENTER));
                table.AddCell(FormatConfig.TableCellBody(item.CreatedDate, PdfPCell.ALIGN_CENTER));
                table.AddCell(FormatConfig.TableCellBody(item.EmployeeName, PdfPCell.ALIGN_CENTER));
                table.AddCell(FormatConfig.TableCellBody(item.RecordCode, PdfPCell.ALIGN_CENTER));
                table.AddCell(FormatConfig.TableCellBody(item.Amount, PdfPCell.ALIGN_RIGHT));
            }
            return table;
        }

        private PdfPTable EmployeeDetailTable()
        {
            PdfPTable table = FormatConfig.Table(9, new float[] { 0.5f, 1.3f, 1.5f, 1.3f, 1.4f, 1f, 1f, 1f, 1f });
            table.AddCell(FormatConfig.TableCellHeader("STT"));
            table.AddCell(FormatConfig.TableCellHeader("Ngày"));
            table.AddCell(FormatConfig.TableCellHeader("Tên khách hàng"));
            table.AddCell(FormatConfig.TableCellHeader("Mặt hàng"));
            table.AddCell(FormatConfig.TableCellHeader("Quy cách"));
            table.AddCell(FormatConfig.TableCellHeader("Số lượng"));
            table.AddCell(FormatConfig.TableCellHeader("ĐVT"));
            table.AddCell(FormatConfig.TableCellHeader("Tổng giá"));
            table.AddCell(FormatConfig.TableCellHeader("Hoa hồng"));

            foreach (EmployeeReport item in employee_reports)
            {
                if (string.IsNullOrEmpty(item.Index))
                {
                    table.AddCell(FormatConfig.TableCellBoldBody(item.CustomerName, PdfPCell.ALIGN_LEFT, 9));
                }
                else
                {
                    table.AddCell(FormatConfig.TableCellBody(item.Index, PdfPCell.ALIGN_CENTER));
                    table.AddCell(FormatConfig.TableCellBody(item.Date, PdfPCell.ALIGN_CENTER));
                    table.AddCell(FormatConfig.TableCellBody(item.CustomerName, PdfPCell.ALIGN_CENTER));
                    table.AddCell(FormatConfig.TableCellBody(item.ProductName, PdfPCell.ALIGN_CENTER));
                    table.AddCell(FormatConfig.TableCellBody(item.AttrName, PdfPCell.ALIGN_CENTER));
                    table.AddCell(FormatConfig.TableCellBody(item.Number, PdfPCell.ALIGN_RIGHT));
                    table.AddCell(FormatConfig.TableCellBody(item.Unit, PdfPCell.ALIGN_CENTER));
                    table.AddCell(FormatConfig.TableCellBody(item.Cost, PdfPCell.ALIGN_RIGHT));
                    table.AddCell(FormatConfig.TableCellBody(item.Commission, PdfPCell.ALIGN_RIGHT));
                }
            }

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
                string file1 = BHConstant.SAVE_IN_DIRECTORY + @"\HHong.pdf";

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

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Chọn đường dẫn lưu file ...";
            sfd.DefaultExt = "xls";
            sfd.Filter = "Excel 2007 (.xlsx)|.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string filepath = sfd.FileName;
                bool result = Global.ExportDataGridViewToXLS(filepath, dgwEmployeeCommissionList);
                if (result)
                    MessageBox.Show("Đã lưu thành công!", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Hệ thống có lỗi, chưa lưu được!", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
