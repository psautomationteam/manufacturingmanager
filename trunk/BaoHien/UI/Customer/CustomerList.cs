using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DAL;
using BaoHien.Common;
using BaoHien.Services.Customers;
using DAL.Helper;
using BaoHien.Services.Employees;
using BaoHien.Model;
using System.Drawing.Printing;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;

namespace BaoHien.UI
{
    public partial class CustomerList : UserControl
    {
        List<Customer> customers;

        public CustomerList()
        {
            InitializeComponent();
        }

        private void CustomerList_Load(object sender, EventArgs e)
        {
            SetupColumns();
            loadCustomerList();
            if (Global.isAdmin())
                btnPrint.Visible = true;
        }

        public void loadCustomerList()
        {
            EmployeeService service = new EmployeeService();
            List<Employee> salers = service.GetEmployees();
            salers.Add(new Employee() { Id = 0, FullName = "Tất cả" });
            salers = salers.OrderBy(x => x.Id).ToList();
            cmbSaler.DataSource = salers;
            cmbSaler.DisplayMember = "FullName";
            cmbSaler.ValueMember = "Id";

            CustomerService customerService = new CustomerService();
            customers = customerService.GetCustomers();
            setUpDataGrid(customers);
        }

        private void setUpDataGrid(List<Customer> customers)
        {
            if (customers != null)
            {
                int index = 0;
                var query = from customer in customers
                            select new
                            {
                                CustomerName = customer.CustomerName,
                                CustCode = customer.CustCode,
                                CustomerPhone = customer.Phone,
                                CustomerPersonName = customer.ContactPerson,
                                CustomerPersonPhone = customer.ContactPersonPhone,
                                FavoriteProduct = customer.FavoriteProduct,
                                Employee = (customer.Employee != null) ? customer.Employee.FullName : "",
                                Id = customer.Id,
                                Index = ++index
                            };
                dgvProductList.DataSource = query.ToList();
                lblTotalResult.Text = customers.Count.ToString();
            }
        }

        private void SetupColumns()
        {
            dgvProductList.AutoGenerateColumns = false;

            dgvProductList.Columns.Add(Global.CreateCell("CustomerName", "Khách hàng", 200));
            dgvProductList.Columns.Add(Global.CreateCell("CustCode", "Mã khách hàng", 150));
            dgvProductList.Columns.Add(Global.CreateCell("CustomerPhone", "SĐT Cty", 100));
            dgvProductList.Columns.Add(Global.CreateCell("CustomerPersonName", "Tên người liên lạc", 150));
            dgvProductList.Columns.Add(Global.CreateCell("CustomerPersonPhone", "SĐT người liên lạc", 150));
            dgvProductList.Columns.Add(Global.CreateCell("FavoriteProduct", "Dòng sản phẩm", 150));
            dgvProductList.Columns.Add(Global.CreateCell("Employee", "Nhân viên phụ trách", 150));
            dgvProductList.Columns.Add(Global.CreateCellDeleteAction());        
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = dgvProductList.SelectedRows;

            foreach (DataGridViewRow dgv in selectedRows)
            {

                CustomerService customerService = new CustomerService();
                int id = ObjectHelper.GetValueFromAnonymousType<int>(dgv.DataBoundItem, "Id");

                if (!customerService.DeleteCustomer(id))
                {
                    MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }

            }
            loadCustomerList();
        }

        private void dgvProductList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AddCustomer frmAddCustomer = new AddCustomer();
            DataGridViewRow currentRow = dgvProductList.Rows[e.RowIndex];

            int id = ObjectHelper.GetValueFromAnonymousType<int>(currentRow.DataBoundItem, "Id");
            frmAddCustomer.loadDataForEditCustomer(id);

            frmAddCustomer.CallFromUserControll = this;
            frmAddCustomer.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddCustomer frmAddCustomer = new AddCustomer();
            

            frmAddCustomer.CallFromUserControll = this;
            frmAddCustomer.ShowDialog();
        }

        private void dgvProductList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender is DataGridView)
            {
                DataGridViewCell cell = ((DataGridView)sender).CurrentCell;
                if (cell.ColumnIndex == ((DataGridView)sender).ColumnCount - 1)
                {
                    DialogResult result = MessageBox.Show("Bạn có muốn xóa khách hàng này?",
                    "Xoá khách hàng này",
                     MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        DataGridViewRow currentRow = dgvProductList.Rows[e.RowIndex];

                        CustomerService customerService = new CustomerService();
                        int id = ObjectHelper.GetValueFromAnonymousType<int>(currentRow.DataBoundItem, "Id");

                        if (!customerService.DeleteCustomer(id))
                        {
                            MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            
                        }
                        loadCustomerList();
                    }

                }

            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            CustomerSearchCriteria search = new CustomerSearchCriteria
            {
                Code = string.IsNullOrEmpty(txtCode.Text) ? "" : txtCode.Text.ToLower() ,
                Phone = string.IsNullOrEmpty(txtPhone.Text) ? "" : txtPhone.Text.ToLower(),
                Name = string.IsNullOrEmpty(txtName.Text) ? "" : txtName.Text.ToLower(),
                Saler = (cmbSaler.SelectedValue != null && (int)cmbSaler.SelectedValue > 0) ? (int?)cmbSaler.SelectedValue : (int?)null,
                FavorProduct = string.IsNullOrEmpty(txtFavoriteProduct.Text) ? "" : txtFavoriteProduct.Text.ToLower()
            };
            CustomerService service = new CustomerService();
            customers = service.SearchingCustomer(search);
            setUpDataGrid(customers);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (Global.isAdmin())
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
                MessageBox.Show("Chỉ Admin mới in được danh sách khách hàng!", "Lỗi thông báo");
            }
        }

        private void ExportFile()
        {
            Global.checkDirSaveFile();
            var doc = new Document(PageSize.A4, 20, 20, 10, 10);
            PdfWriter docWriter = PdfWriter.GetInstance(doc, new FileStream(BHConstant.SAVE_IN_DIRECTORY + @"\KHang.pdf", FileMode.Create));
            PdfWriterEvents writerEvent;

            Image watermarkImage = Image.GetInstance(AppDomain.CurrentDomain.BaseDirectory + @"logo.png");
            watermarkImage.SetAbsolutePosition(doc.PageSize.Width / 2 - 70, 550);
            writerEvent = new PdfWriterEvents(watermarkImage);
            docWriter.PageEvent = writerEvent;

            doc.Open();

            doc.Add(FormatConfig.ParaRightBeforeHeader("In ngày : " + BaoHienRepository.GetBaoHienDBDataContext().GetSystemDate().ToString(BHConstant.DATETIME_FORMAT)));
            doc.Add(FormatConfig.ParaHeader("DANH SÁCH KHÁCH HÀNG"));

            PdfPTable table = FormatConfig.Table(7, new float[] { 0.5f, 2f, 1.3f, 1.3f, 2f, 1.6f, 1.3f });
            table.AddCell(FormatConfig.TableCellHeader("STT"));
            table.AddCell(FormatConfig.TableCellHeader("Tên khách hàng"));
            table.AddCell(FormatConfig.TableCellHeader("Mã KH"));
            table.AddCell(FormatConfig.TableCellHeader("SĐT Cty"));
            table.AddCell(FormatConfig.TableCellHeader("Địa chỉ"));
            table.AddCell(FormatConfig.TableCellHeader("Người liên lạc"));
            table.AddCell(FormatConfig.TableCellHeader("SĐT"));

            for (int i = 0; i < customers.Count; i++)
            {
                table.AddCell(FormatConfig.TableCellBody((i + 1).ToString(), PdfPCell.ALIGN_CENTER));
                table.AddCell(FormatConfig.TableCellBody(customers[i].CustomerName, PdfPCell.ALIGN_LEFT));
                table.AddCell(FormatConfig.TableCellBody(customers[i].CustCode, PdfPCell.ALIGN_LEFT));
                table.AddCell(FormatConfig.TableCellBody(customers[i].Phone, PdfPCell.ALIGN_LEFT));
                table.AddCell(FormatConfig.TableCellBody(customers[i].Address, PdfPCell.ALIGN_LEFT));
                table.AddCell(FormatConfig.TableCellBody(customers[i].ContactPerson, PdfPCell.ALIGN_LEFT));
                table.AddCell(FormatConfig.TableCellBody(customers[i].ContactPersonPhone, PdfPCell.ALIGN_LEFT));
            }
            doc.Add(table);
            doc.Add(FormatConfig.ParaCommonInfo("Ghi chú : ", String.Concat(Enumerable.Repeat("...", 96))));

            doc.Close();
        }

        private void Print(string printerName)
        {
            ExportFile();
            // Print the file to the printer.
            try
            {
                string foxit = Registry.LocalMachine.OpenSubKey("Software").OpenSubKey("Foxit Software").OpenSubKey("Foxit Reader")
                    .GetValue("InnoSetupUpdatePath").ToString().Replace("unins000", "Foxit Reader");
                string file1 = BHConstant.SAVE_IN_DIRECTORY + @"\KHang.pdf";

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

        private void dgvProductList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
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
    }
}
