using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DAL;
using BaoHien.Services.Products;
using BaoHien.Model;
using BaoHien.Services.BaseAttributes;
using BaoHien.Services.ProductLogs;
using BaoHien.Common;
using BaoHien.Services.MeasurementUnits;
using DAL.Helper;
using BaoHien.Services.Orders;
using BaoHien.Services.ProductionRequests;
using BaoHien.Services;
using System.Diagnostics;
using Microsoft.Win32;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing.Printing;
using System.IO;

namespace BaoHien.UI
{
    public partial class ProductAndMaterialReport : UserControl
    {
        List<ProductReport> productReports = new List<ProductReport>();
        List<ProductsReport> productsReports = new List<ProductsReport>();
        List<Product> products;
        List<BaseAttribute> attrs;
        List<ProductType> productTypes;
        List<MeasurementUnit> units;
        ProductLogService productLogService;
        int modeReport = 0;
        string textForPrint = "", textInfo;

        public ProductAndMaterialReport()
        {
            InitializeComponent();
            productLogService = new ProductLogService();
            LoadDataCombobox();
            dtpFrom.Value = DateTime.Today.AddDays(-DateTime.Now.Day + 1);
            dtpFrom.CustomFormat = BHConstant.DATE_FORMAT;
            dtpTo.CustomFormat = BHConstant.DATE_FORMAT;
        }
        
        void LoadReport()
        {
            DateTime dtFrom = dtpFrom.Value;
            DateTime dtTo = dtpTo.Value.AddDays(1).Date;
            int productTypeId = cbmProductTypes.SelectedValue == null ? 0 : (int)cbmProductTypes.SelectedValue;
            int productId = cbmProducts.SelectedValue == null ? 0 : (int)cbmProducts.SelectedValue;
            int attrId = cbmAttrs.SelectedValue == null ? 0 : (int)cbmAttrs.SelectedValue;
            int unitId = cbmUnits.SelectedValue == null ? 0 : (int)cbmUnits.SelectedValue;
            modeReport = 0;
            if (productTypeId > 0 && productId > 0 && attrId > 0 && unitId > 0)
            {
                modeReport = 1;
                textForPrint = textForPrint = "Từ ngày " + dtpFrom.Value.ToString(BHConstant.DATE_FORMAT) + " đến ngày " + dtpTo.Value.ToString(BHConstant.DATE_FORMAT); 
                textInfo = ((Product)cbmProducts.SelectedItem).ProductName + ((BaseAttribute)cbmAttrs.SelectedItem).AttributeName
                        + " - ĐVT: " + ((MeasurementUnit)cbmUnits.SelectedItem).Name;
                productReports = productLogService.GetReportsOfProductAttributeUnit(productId, attrId, unitId, dtFrom, dtTo);
                SetupColumnProductDetails(productReports);
            }
            else
            {
                textForPrint = "Từ ngày " + dtpFrom.Value.ToString(BHConstant.DATE_FORMAT) + " đến ngày " + dtpTo.Value.ToString(BHConstant.DATE_FORMAT); 
                productsReports = productLogService.GetReportsOfProducts(productTypeId, productId, attrId, unitId, dtFrom, dtTo);
                SetupColumnProducts(productsReports);
            }
        }

        void LoadDataCombobox()
        {
            ProductTypeService productTypeService = new ProductTypeService();
            ProductType pt = new ProductType
            {
                Id = 0,
                TypeName = "Tất cả"
            };
            productTypes = productTypeService.GetProductTypes();
            productTypes.Add(pt);
            productTypes = productTypes.OrderBy(pts => pts.Id).ToList();
            if (productTypes != null)
            {
                cbmProductTypes.DataSource = productTypes;
                cbmProductTypes.DisplayMember = "TypeName";
                cbmProductTypes.ValueMember = "Id";
            }
            LoadProducts(0);
        }

        private void SetupColumnProducts(List<ProductsReport> data)
        {
            dgwStockEntranceList.Columns.Clear();
            dgwStockEntranceList.AutoGenerateColumns = false;

            dgwStockEntranceList.Columns.Add(Global.CreateCell("ProductCode", "Mã SP", 80));
            dgwStockEntranceList.Columns.Add(Global.CreateCell("ProductName", "Tên và qui cách", 200));
            dgwStockEntranceList.Columns.Add(Global.CreateCell("Jampo", "Jampo", 50));
            dgwStockEntranceList.Columns.Add(Global.CreateCell("UnitName", "Đơn vị tính", 100));
            dgwStockEntranceList.Columns.Add(Global.CreateCellWithAlignment("FirstNumber", "Đầu kì", 100, DataGridViewContentAlignment.MiddleRight));
            dgwStockEntranceList.Columns.Add(Global.CreateCellWithAlignment("ImportNumber", "Nhập", 100, DataGridViewContentAlignment.MiddleRight));
            dgwStockEntranceList.Columns.Add(Global.CreateCellWithAlignment("ExportNumber", "Xuất", 100, DataGridViewContentAlignment.MiddleRight));
            dgwStockEntranceList.Columns.Add(Global.CreateCellWithAlignment("LastNumber", "Cuối kì", 100, DataGridViewContentAlignment.MiddleRight));

            dgwStockEntranceList.DataSource = data;
        }

        private void SetupColumnProductDetails(List<ProductReport> data)
        {
            dgwStockEntranceList.Columns.Clear();
            dgwStockEntranceList.AutoGenerateColumns = false;
            
            dgwStockEntranceList.Columns.Add(Global.CreateCell("UpdatedDateString", "Ngày", 100));
            dgwStockEntranceList.Columns.Add(Global.CreateCell("RecordCode", "Mã phiếu", 100));
            dgwStockEntranceList.Columns.Add(Global.CreateCellWithAlignment("Amount", "Số lượng", 120, DataGridViewContentAlignment.MiddleRight));
            dgwStockEntranceList.DataSource = data;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgwStockEntranceList.AutoGenerateColumns = false;
            LoadReport();
        }

        private void LoadProducts(int productTypeId)
        {
            if (productTypeId == 0)
            {
                cbmProducts.Enabled = false;
                cbmAttrs.Enabled = false;
                cbmUnits.Enabled = false;
            }
            else
            {
                cbmProducts.Enabled = true;
                ProductService productService = new ProductService();
                Product product = new Product
                {
                    ProductName = "Tất cả",
                    Id = 0
                };
                products = productService.GetProducts().Where(p => p.ProductType == productTypeId).ToList();
                products.Add(product);
                products = products.OrderBy(p => p.Id).ToList();
                if (products != null)
                {
                    cbmProducts.DataSource = products;
                    cbmProducts.DisplayMember = "ProductName";
                    cbmProducts.ValueMember = "Id";
                }
                LoadAttributes(0);
            }
        }

        private void LoadAttributes(int productId)
        {
            if (productId == 0)
            {
                cbmAttrs.Enabled = false;
                cbmUnits.Enabled = false;
            }
            else
            {
                cbmAttrs.Enabled = true;
                BaseAttributeService attrService = new BaseAttributeService();
                BaseAttribute ba = new BaseAttribute
                {
                    AttributeName = "Tất cả",
                    Id = 0
                };

                ProductService productService = new ProductService();
                Product p = productService.GetProduct(productId);
                List<ProductAttribute> pas = p.ProductAttributes.ToList();
                attrs = new List<BaseAttribute>();
                attrs.Add(ba);
                foreach (ProductAttribute pa in pas)
                {
                    attrs.Add(pa.BaseAttribute);
                }
                attrs = attrs.OrderBy(a => a.Id).ToList();
                if (attrs != null)
                {
                    cbmAttrs.DataSource = attrs;
                    cbmAttrs.DisplayMember = "AttributeName";
                    cbmAttrs.ValueMember = "Id";
                }
                LoadUnits(productId, 0);
            }
        }

        private void LoadUnits(int productId, int attrId)
        {
            if (attrId == 0)
            {
                cbmUnits.Enabled = false;
            }
            else
            {
                cbmUnits.Enabled = true;
                MeasurementUnitService unitService = new MeasurementUnitService();
                MeasurementUnit u = new MeasurementUnit
                {
                    Name = "Tất cả",
                    Id = 0
                };

                units = productLogService.GetUnitsOfProductAttribute(productId, attrId);
                units.Add(u);
                units = units.OrderBy(a => a.Id).ToList();
                if (units != null)
                {
                    cbmUnits.DataSource = units;
                    cbmUnits.DisplayMember = "Name";
                    cbmUnits.ValueMember = "Id";
                }
            }
        }

        private void cbmProductTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int productTypeId = productTypes[cbmProductTypes.SelectedIndex].Id;
            LoadProducts(productTypeId);
        }

        private void cbmProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            int productId = products[cbmProducts.SelectedIndex].Id;
            LoadAttributes(productId);
        }

        private void cbmAttrs_SelectedIndexChanged(object sender, EventArgs e)
        {
            int productId = products[cbmProducts.SelectedIndex].Id;
            int attrId = attrs[cbmAttrs.SelectedIndex].Id;
            LoadUnits(productId, attrId);
        }

        private void dgwStockEntranceList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (modeReport == 1)
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
                    case BHConstant.PREFIX_FOR_ENTRANCE:
                        {
                            EntranceStockService stockService = new EntranceStockService();
                            EntranceStock stock = stockService.GetEntranceStocks().Where(r => r.EntranceCode == RecordCode).FirstOrDefault();
                            if (stock != null)
                            {
                                AddEntranceStock frmAddEntranceStock = new AddEntranceStock();
                                frmAddEntranceStock.loadDataForEditEntranceStock(stock.Id);

                                frmAddEntranceStock.CallFromUserControll = this;
                                frmAddEntranceStock.ShowDialog();
                            }
                        } break;
                    case BHConstant.PREFIX_FOR_PRODUCTION:
                        {
                            ProductionRequestService requestService = new ProductionRequestService();
                            ProductionRequest request = requestService.GetProductionRequests().Where(r => r.ReqCode == RecordCode).FirstOrDefault();
                            if (request != null)
                            {
                                AddProductionRequest addProductionRequest = new AddProductionRequest();
                                addProductionRequest.loadDataForEditProductRequest(request.Id);

                                addProductionRequest.CallFromUserControll = this;
                                addProductionRequest.ShowDialog();
                            }
                        } break;
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if ((modeReport == 1 && productReports.Count > 0)
                || (modeReport != 1 && productsReports.Count > 0))
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
            
            PdfWriter docWriter = PdfWriter.GetInstance(doc, new FileStream(BHConstant.SAVE_IN_DIRECTORY + @"\Kho.pdf", FileMode.Create));
            PdfWriterEvents writerEvent;

            Image watermarkImage = Image.GetInstance(AppDomain.CurrentDomain.BaseDirectory + @"logo.png");
            watermarkImage.SetAbsolutePosition(doc.PageSize.Width / 2 - 70, 600);
            writerEvent = new PdfWriterEvents(watermarkImage);
            docWriter.PageEvent = writerEvent;

            doc.Open();

            doc.Add(FormatConfig.ParaRightBeforeHeader("In ngày : " + BaoHienRepository.GetBaoHienDBDataContext().GetSystemDate().ToString(BHConstant.DATETIME_FORMAT)));
            doc.Add(FormatConfig.ParaHeader("BÁO CÁO KHO VÀ THÀNH PHẨM"));

            doc.Add(FormatConfig.ParaRightBelowHeader("(" + textForPrint + ")"));
            if (modeReport != 1)
                doc.Add(ProductsTable());
            else
            {
                doc.Add(FormatConfig.ParaCommonInfo("Sản phẩm : ", textInfo));
                doc.Add(ProductDetailTable());
            }
            doc.Add(FormatConfig.ParaCommonInfo("Ghi chú : ", String.Concat(Enumerable.Repeat("...", 96))));

            doc.Close();
        }

        private PdfPTable ProductsTable()
        {
            PdfPTable table = FormatConfig.Table(9, new float[] { 0.5f, 1.3f, 2.5f, 0.7f, 1f, 1f, 1f, 1f, 1f });
            table.AddCell(FormatConfig.TableCellHeader("STT"));
            table.AddCell(FormatConfig.TableCellHeader("Mã SP"));            
            table.AddCell(FormatConfig.TableCellHeader("Tên và qui cách"));
            table.AddCell(FormatConfig.TableCellHeader("Jampo"));
            table.AddCell(FormatConfig.TableCellHeader("ĐVT"));
            table.AddCell(FormatConfig.TableCellHeader("Đầu kì"));
            table.AddCell(FormatConfig.TableCellHeader("Nhập"));
            table.AddCell(FormatConfig.TableCellHeader("Xuất"));
            table.AddCell(FormatConfig.TableCellHeader("Cuối kì"));

            foreach (ProductsReport item in productsReports)
            {
                if (string.IsNullOrEmpty(item.Index))
                {
                    table.AddCell(FormatConfig.TableCellBoldBody(item.ProductName, PdfPCell.ALIGN_LEFT, 9));
                }
                else
                {
                    table.AddCell(FormatConfig.TableCellBody(item.Index, PdfPCell.ALIGN_CENTER));
                    table.AddCell(FormatConfig.TableCellBody(item.ProductCode, PdfPCell.ALIGN_CENTER));
                    table.AddCell(FormatConfig.TableCellBody(item.ProductName, PdfPCell.ALIGN_CENTER));
                    table.AddCell(FormatConfig.TableCellBody(item.Jampo, PdfPCell.ALIGN_CENTER));
                    table.AddCell(FormatConfig.TableCellBody(item.UnitName, PdfPCell.ALIGN_CENTER));
                    table.AddCell(FormatConfig.TableCellBody(item.FirstNumber, PdfPCell.ALIGN_RIGHT));
                    table.AddCell(FormatConfig.TableCellBody(item.ImportNumber, PdfPCell.ALIGN_RIGHT));
                    table.AddCell(FormatConfig.TableCellBody(item.ExportNumber, PdfPCell.ALIGN_RIGHT));
                    table.AddCell(FormatConfig.TableCellBody(item.LastNumber, PdfPCell.ALIGN_RIGHT));
                }
            }
            return table;
        }

        private PdfPTable ProductDetailTable()
        {
            PdfPTable table = FormatConfig.Table(4, new float[] { 1f, 2.5f, 3.5f, 3f });
            table.AddCell(FormatConfig.TableCellHeader("STT"));
            table.AddCell(FormatConfig.TableCellHeader("Ngày"));
            table.AddCell(FormatConfig.TableCellHeader("Mã phiếu"));
            table.AddCell(FormatConfig.TableCellHeader("Số lượng"));

            int index = 0;
            foreach (ProductReport item in productReports)
            {
                table.AddCell(FormatConfig.TableCellBody((++index).ToString(), PdfPCell.ALIGN_CENTER));
                table.AddCell(FormatConfig.TableCellBody(item.UpdatedDateString, PdfPCell.ALIGN_CENTER));
                table.AddCell(FormatConfig.TableCellBody(item.RecordCode, PdfPCell.ALIGN_CENTER));
                table.AddCell(FormatConfig.TableCellBody(item.Amount, PdfPCell.ALIGN_RIGHT));
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
                string file1 = BHConstant.SAVE_IN_DIRECTORY + @"\Kho.pdf";

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

        private void dgwStockEntranceList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView gridView = sender as DataGridView;
            if (null != gridView)
            {
                gridView.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders);
                foreach (DataGridViewRow r in gridView.Rows)
                {
                    gridView.Rows[r.Index].HeaderCell.Value = (r.Index + 1).ToString();
                    if(modeReport == 1)
                    {
                        gridView.Rows[r.Index].DefaultCellStyle.BackColor = productReports[r.Index].Direction == BHConstant.DIRECTION_IN ?
                            System.Drawing.Color.White : System.Drawing.Color.Wheat;
                    }
                }
            }
        }
    }
}
