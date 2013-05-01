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
using BaoHien.Services.ProductAttributes;
using BaoHien.Services.BaseAttributes;
using BaoHien.Services.ProductLogs;
using BaoHien.Common;
using BaoHien.Services.MeasurementUnits;
using DAL.Helper;
using BaoHien.Services.Orders;
using BaoHien.Services.ProductionRequests;
using BaoHien.Services;

namespace BaoHien.UI
{
    public partial class ProductAndMaterialReport : UserControl
    {
        List<ProductReport> productReports;
        List<Product> products;
        List<BaseAttribute> attrs;
        List<ProductType> productTypes;
        List<MeasurementUnit> units;
        ProductLogService productLogService;
        int modeReport = 0;

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
            if (productTypeId == 0)
            {
                productReports = productLogService.GetReportsOfProducts(dtFrom, dtTo);
                SetupColumnProducts(productReports);
            }
            else
            {
                if (productId == 0)
                {
                    productReports = productLogService.GetReportsOfProducts(productTypeId, dtFrom, dtTo);
                    SetupColumnProducts(productReports);
                }
                else
                {
                    if (attrId == 0)
                    {
                        productReports = productLogService.GetReportsOfProduct(productId, dtFrom, dtTo);
                        SetupColumnProducts(productReports);
                    }
                    else
                    {
                        if (unitId == 0)
                        {
                            productReports = productLogService.GetReportsOfProductAttribute(productId, attrId, dtFrom, dtTo);
                            SetupColumnProducts(productReports);
                        }
                        else
                        {
                            modeReport = 1;
                            productReports = productLogService.GetReportsOfProductAttributeUnit(productId, attrId, unitId, dtFrom, dtTo);
                            SetupColumnProductDetails(productReports);
                        }
                    }
                }
            }
        }

        void LoadDataCombobox()
        {
            ProductTypeService productTypeService = new ProductTypeService();
            ProductType pt = new ProductType
            {
                Id = 0,
                ProductName = "Tất cả"
            };
            productTypes = productTypeService.GetProductTypes();
            productTypes.Add(pt);
            productTypes = productTypes.OrderBy(pts => pts.Id).ToList();
            if (productTypes != null)
            {
                cbmProductTypes.DataSource = productTypes;
                cbmProductTypes.DisplayMember = "ProductName";
                cbmProductTypes.ValueMember = "Id";
            }
            LoadProducts(0);
        }

        private void SetupColumnProducts(List<ProductReport> productReports)
        {
            dgwStockEntranceList.DataSource = productReports;
            dgwStockEntranceList.Columns.Clear();
            dgwStockEntranceList.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn indexColumn = new DataGridViewTextBoxColumn();
            indexColumn.Width = 30;
            indexColumn.DataPropertyName = "Index";
            indexColumn.HeaderText = "STT";
            indexColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(indexColumn);

            DataGridViewTextBoxColumn createdDateColumn = new DataGridViewTextBoxColumn();
            createdDateColumn.DefaultCellStyle.Format = BHConstant.DATETIME_FORMAT;
            createdDateColumn.DataPropertyName = "CreatedDate";
            createdDateColumn.Width = 150;
            createdDateColumn.HeaderText = "Tồn tính đến ngày";
            createdDateColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(createdDateColumn);

            DataGridViewTextBoxColumn productCodeColumn = new DataGridViewTextBoxColumn();
            productCodeColumn.Width = 150;
            productCodeColumn.DataPropertyName = "ProductCode";
            productCodeColumn.HeaderText = "Mã sản phẩm";
            productCodeColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(productCodeColumn);

            DataGridViewTextBoxColumn productNameColumn = new DataGridViewTextBoxColumn();
            productNameColumn.Width = 150;
            productNameColumn.DataPropertyName = "ProductName";
            productNameColumn.HeaderText = "Tên sản phẩm";
            productNameColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(productNameColumn);

            DataGridViewTextBoxColumn quantityColumn = new DataGridViewTextBoxColumn();
            quantityColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            quantityColumn.DataPropertyName = "Quantity";
            quantityColumn.Width = 100;
            quantityColumn.HeaderText = "Số lượng tồn";
            quantityColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(quantityColumn);

            DataGridViewTextBoxColumn unitColumn = new DataGridViewTextBoxColumn();
            unitColumn.DataPropertyName = "UnitName";
            unitColumn.Width = 100;
            unitColumn.HeaderText = "Đơn vị tính";
            unitColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(unitColumn);         
        }
        
        private void SetupColumnProductDetails(List<ProductReport> productReports)
        {
            dgwStockEntranceList.DataSource = productReports;
            dgwStockEntranceList.Columns.Clear();
            dgwStockEntranceList.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn indexColumn = new DataGridViewTextBoxColumn();
            indexColumn.Width = 30;
            indexColumn.DataPropertyName = "Index";
            indexColumn.HeaderText = "STT";
            indexColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(indexColumn);

            DataGridViewTextBoxColumn createdDateColumn = new DataGridViewTextBoxColumn();
            createdDateColumn.DefaultCellStyle.Format = BHConstant.DATETIME_FORMAT;
            createdDateColumn.DataPropertyName = "CreatedDate";
            createdDateColumn.Width = 150;
            createdDateColumn.HeaderText = "Ngày";
            createdDateColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(createdDateColumn);

            DataGridViewTextBoxColumn recordCodeColumn = new DataGridViewTextBoxColumn();
            recordCodeColumn.Width = 150;
            recordCodeColumn.DataPropertyName = "RecordCode";
            recordCodeColumn.HeaderText = "Mã phiếu";
            recordCodeColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(recordCodeColumn);

            DataGridViewTextBoxColumn beforeNumberColumn = new DataGridViewTextBoxColumn();
            beforeNumberColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            beforeNumberColumn.Width = 120;
            beforeNumberColumn.DataPropertyName = "BeforeNumber";
            beforeNumberColumn.HeaderText = "Số lượng đầu";
            beforeNumberColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(beforeNumberColumn);

            DataGridViewTextBoxColumn amountColumn = new DataGridViewTextBoxColumn();
            amountColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            amountColumn.DataPropertyName = "Amount";
            amountColumn.Width = 120;
            amountColumn.HeaderText = "Số lượng cập nhật";
            amountColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(amountColumn);

            DataGridViewTextBoxColumn afterNumberColumn = new DataGridViewTextBoxColumn();
            afterNumberColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            afterNumberColumn.DataPropertyName = "AfterNumber";
            afterNumberColumn.Width = 120;
            afterNumberColumn.HeaderText = "Số lượng cuối";
            afterNumberColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(afterNumberColumn);            
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
    }
}
