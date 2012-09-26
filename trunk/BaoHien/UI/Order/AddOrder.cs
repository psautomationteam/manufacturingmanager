using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaoHien.UI.Base;
using DAL;
using BaoHien.Services.Orders;
using BaoHien.Services.Customers;
using BaoHien.Common;
using BaoHien.Services.Products;
using DAL.Helper;
using BaoHien.Services.OrderDetails;
using BaoHien.Services.BaseAttributes;
using BaoHien.Services.ProductAttributes;
using BaoHien.Model;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using CoolPrintPreview;
using System.Reflection;
using Word = Microsoft.Office.Interop.Word;
using BaoHien.Services.ProductInStocks;
using Microsoft.Office.Interop.Word;
using System.Globalization;

namespace BaoHien.UI
{
    public partial class AddOrder : BaseForm
    {
        bool isUpdating = false;
        Order order;
        BindingList<Customer> customers;
        BindingList<OrderDetail> orderDetails;
        BindingList<Product> products;
        BindingList<BaseAttribute> baseAttributesAtRow;
        BindingList<ProductionRequestDetailModel> originalProductions;

        private System.IO.Stream streamToPrint;
        Image MyImage;
        string streamType;
        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        private static extern bool BitBlt

        (

            IntPtr hdcDest, // handle to destination DC

            int nXDest, // x-coord of destination upper-left corner

            int nYDest, // y-coord of destination upper-left corner

            int nWidth, // width of destination rectangle

            int nHeight, // height of destination rectangle

            IntPtr hdcSrc, // handle to source DC

            int nXSrc, // x-coordinate of source upper-left corner

            int nYSrc, // y-coordinate of source upper-left corner

            System.Int32 dwRop // raster operation code

        );
        public AddOrder()
        {
            InitializeComponent();
        }
        private bool saveData()
        {
            
            if (validator1.Validate())
            {
                DialogResult dialogResult = MessageBox.Show("Bạn sẽ không thể chỉnh sửa sau khi lưu!Bạn muốn lưu?", "Xác nhận", MessageBoxButtons.YesNo);
                if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                {
                   
                    ProductInStockService pis = new ProductInStockService();
                    double discount = 0;
                    Double.TryParse(txtDiscount.Text, out discount);
                    DateTime createdDate = DateTime.Now;
                    if (!DateTime.TryParse(txtCreatedDate.Text, out createdDate))
                    {
                        createdDate = DateTime.Now;
                    };

                    double vat = 0;
                    Double.TryParse(txtVAT.Text, out vat);
                    int userId = 0;
                    if (BaoHien.Common.Global.CurrentUser != null)
                    {
                        userId = BaoHien.Common.Global.CurrentUser.Id;
                    }
                    else
                    {
                        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                        return false;
                    }
                    if (order != null)//update
                    {
                        order.CustId = (int)cbxCustomer.SelectedValue;
                        order.Discount = discount;
                        order.Note = txtNote.Text;
                        order.VAT = vat;
                        order.OrderCode = txtOrderCode.Text;
                        OrderService orderService = new OrderService();
                        bool result = orderService.UpdateOrder(order);
                        if (!result)
                        {
                            MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                            return false;
                        }
                        else
                        {
                            OrderDetailService orderDetailService = new OrderDetailService();
                            foreach (OrderDetail od in orderDetails)
                            {
                                if (od.ProductId > 0 && od.AttributeId > 0)
                                {
                                    if (od.Id == 0)
                                    {
                                        od.OrderId = order.Id;
                                        result = orderDetailService.AddOrderDetail(od);

                                        //Save in Production In Stock
                                        ProductInStock productInStock = new ProductInStock();
                                        List<ProductInStock> lstProductInStock = pis.SelectProductByWhere(pt => pt.ProductId == od.ProductId && pt.AttributeId == od.AttributeId);
                                        productInStock.AttributeId = od.AttributeId;
                                        productInStock.ProductId = od.ProductId;
                                        productInStock.LatestUpdate = DateTime.Now;
                                        productInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_OUTPUT;

                                        productInStock.NumberOfInput = lstProductInStock.Last<ProductInStock>().NumberOfInput;
                                        productInStock.NumberOfOutput = lstProductInStock.Last<ProductInStock>().NumberOfOutput + od.NumberUnit;
                                        productInStock.NumberOfItem = (int)lstProductInStock.Last<ProductInStock>().NumberOfItem - od.NumberUnit;

                                        pis.AddProductInStock(productInStock);
                                    }
                                    else
                                    {
                                        ProductionRequestDetailModel original = new ProductionRequestDetailModel();
                                        original = originalProductions.Where(p => p.Id == od.Id).ToList().FirstOrDefault();
                                        result = orderDetailService.UpdateOrderDetail(od);

                                        //Update so luong
                                        if (original != null)
                                        {
                                            if (od.ProductId == original.ProductId && od.AttributeId == original.AttributeId && od.NumberUnit != original.NumberUnit)
                                            {
                                                ProductInStock productInStock = new ProductInStock();
                                                List<ProductInStock> lstProductInStock = pis.SelectProductByWhere(pt => pt.ProductId == od.ProductId && pt.AttributeId == od.AttributeId);

                                                productInStock.AttributeId = od.AttributeId;
                                                productInStock.ProductId = od.ProductId;
                                                productInStock.LatestUpdate = DateTime.Now;
                                                productInStock.NumberOfInput = lstProductInStock.Last<ProductInStock>().NumberOfInput;
                                                productInStock.NumberOfOutput = lstProductInStock.Last<ProductInStock>().NumberOfOutput - original.NumberUnit + od.NumberUnit;
                                                productInStock.NumberOfItem = lstProductInStock.Last<ProductInStock>().NumberOfItem + original.NumberUnit - od.NumberUnit;
                                                productInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_OUTPUT;
                                                pis.UpdateProductInStock(productInStock);

                                            }

                                            //Sua chi tiet phieu
                                            else if (od.ProductId != original.ProductId || od.AttributeId != original.AttributeId)
                                            {
                                                //Tao moi
                                                List<ProductInStock> lstNewProduct = pis.SelectProductByWhere(pt => pt.ProductId == od.ProductId && pt.AttributeId == od.AttributeId);
                                                ProductInStock newProductInStock = new ProductInStock();
                                                newProductInStock.AttributeId = od.AttributeId;
                                                newProductInStock.ProductId = od.ProductId;
                                                newProductInStock.LatestUpdate = DateTime.Now;
                                                newProductInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_OUTPUT;

                                                newProductInStock.NumberOfInput = lstNewProduct.Last<ProductInStock>().NumberOfInput;
                                                newProductInStock.NumberOfOutput = lstNewProduct.Last<ProductInStock>().NumberOfOutput + od.NumberUnit;
                                                newProductInStock.NumberOfItem += lstNewProduct.Last<ProductInStock>().NumberOfItem - od.NumberUnit;

                                                pis.AddProductInStock(newProductInStock);

                                                //Xoa cu
                                                List<ProductInStock> lstOldProduct = pis.SelectProductByWhere(pt => pt.ProductId == original.ProductId && pt.AttributeId == original.AttributeId);
                                                ProductInStock oldProductInStock = new ProductInStock();
                                                oldProductInStock.AttributeId = original.AttributeId;
                                                oldProductInStock.ProductId = original.ProductId;
                                                oldProductInStock.LatestUpdate = DateTime.Now;
                                                oldProductInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_EDIT;

                                                newProductInStock.NumberOfInput = lstOldProduct.Last<ProductInStock>().NumberOfInput;
                                                newProductInStock.NumberOfOutput = lstOldProduct.Last<ProductInStock>().NumberOfOutput - original.NumberUnit;
                                                newProductInStock.NumberOfItem += lstOldProduct.Last<ProductInStock>().NumberOfItem + original.NumberUnit;

                                                pis.AddProductInStock(newProductInStock);
                                            }
                                        }

                                    }

                                    if (!result)
                                        break;
                                }
                            }
                            if (!result)
                            {
                                MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                                return false;
                            }
                            else
                            {
                                MessageBox.Show("Sản phẩm đã được cập nhật thành công");
                            }
                            if (this.CallFromUserControll != null && this.CallFromUserControll is OrderList)
                            {
                                ((OrderList)this.CallFromUserControll).loadOrderList();
                            }

                            disableForm();
                        }
                    }
                    else//add new
                    {
                        if (cbxCustomer.SelectedValue == null)
                        {
                            MessageBox.Show("Bạn cần có một khách hành cho phiếu này!");
                            return false;
                        }
                        order = new Order
                        {
                            CustId = cbxCustomer.SelectedValue != null ? (int)cbxCustomer.SelectedValue : 0,
                            Discount = discount,
                            Note = txtNote.Text,
                            VAT = vat,
                            OrderCode = txtOrderCode.Text,
                            CreatedDate = createdDate,
                            CreateBy = userId


                        };
                        OrderService orderService = new OrderService();
                        bool result = orderService.AddOrder(order);
                        long newOrderId = BaoHienRepository.GetMaxId<Order>();
                        OrderDetailService orderDetailService = new OrderDetailService();
                        foreach (OrderDetail od in orderDetails)
                        {
                            if (od.ProductId > 0)
                            {
                                od.OrderId = (int)newOrderId;
                                bool ret = orderDetailService.AddOrderDetail(od);

                                //Save in Production In Stock
                                ProductInStock productInStock = new ProductInStock();
                                List<ProductInStock> lstProductInStock = pis.SelectProductByWhere(pt => pt.ProductId == od.ProductId && pt.AttributeId == od.AttributeId);
                                productInStock.AttributeId = od.AttributeId;
                                productInStock.ProductId = od.ProductId;
                                productInStock.LatestUpdate = DateTime.Now;
                                productInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_OUTPUT;
                                if (lstProductInStock.Count > 0)
                                {
                                    productInStock.NumberOfInput = lstProductInStock.Last<ProductInStock>().NumberOfInput;
                                    productInStock.NumberOfOutput = lstProductInStock.Last<ProductInStock>().NumberOfOutput + od.NumberUnit;
                                    productInStock.NumberOfItem = (int)lstProductInStock.Last<ProductInStock>().NumberOfItem - od.NumberUnit;
                                }

                                pis.AddProductInStock(productInStock);

                                if (!ret)
                                {
                                    MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                                    return false;
                                }
                            }
                        }


                        if (result)
                        {
                            MessageBox.Show("Sản phẩm đã được tạo thành công");
                            //((OrderList)this.CallFromUserControll).loadOrderList();
                            this.Close();
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                            return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            saveData();
        }
        private void loadSomeData()
        {
            if(customers == null)
            {
                CustomerService customerService = new CustomerService();
                customers = new BindingList<Customer>(customerService.GetCustomers());
            }
            if (customers != null)
            {


                cbxCustomer.DataSource = customers;

                cbxCustomer.DisplayMember = "CustomerName";
                cbxCustomer.ValueMember = "Id";
                if (customers.Count == 0)
                {
                    cbxCustomer.Enabled = false;
                }
            }
            
            ProductService productService = new ProductService();
            products = new BindingList<Product>(productService.GetProducts());
            BaseAttributeService baseAttributeService = new BaseAttributeService();
            baseAttributesAtRow = new BindingList<BaseAttribute>(baseAttributeService.GetBaseAttributes());
            if (order != null)
            {
                if (customers != null)
                {
                    cbxCustomer.SelectedIndex = customers.ToList().FindIndex(cus => cus.Id == order.CustId);
                }
                txtDiscount.Text = order.Discount.HasValue ? order.Discount.Value.ToString() : "";
                txtNote.Text = order.Note;
                txtVAT.Text = order.VAT.HasValue ? order.VAT.Value.ToString() : "";
                txtOrderCode.Text = order.OrderCode;
                txtCreatedDate.Text = order.CreatedDate.ToShortDateString();

            }
            else
            {
                txtOrderCode.Text = RandomGeneration.GeneratingCode(BHConstant.PREFIX_FOR_ORDER);
                txtCreatedDate.Text = DateTime.Now.ToShortDateString();
            }
        }
        public void updateProductionRequestDetailCells()
        {
            if (isUpdating && order != null && orderDetails.Count < dgwOrderDetails.RowCount)
            {

                for (int i = 0; i < orderDetails.Count; i++)
                {
                    for (int j = 0; j < 2 && j < dgwOrderDetails.ColumnCount; j++)
                    {
                        if (dgwOrderDetails.Rows[i].Cells[j] is DataGridViewComboBoxCell)
                        {
                            if (j == 0)
                            {
                                DataGridViewComboBoxCell pkgBoxCell = (DataGridViewComboBoxCell)dgwOrderDetails.Rows[i].Cells[j];
                                pkgBoxCell.Value = orderDetails[i].ProductId;
                            }
                            if (j == 1)
                            {
                                DataGridViewComboBoxCell pkgBoxCell = (DataGridViewComboBoxCell)dgwOrderDetails.Rows[i].Cells[j];
                                pkgBoxCell.Value = orderDetails[i].AttributeId;
                            }
                        }
                    }
                }



            }
            
        }
        public void loadDataForEditOrder(int orderId)
        {
            disableForm();
            
            isUpdating = true;
            this.Text = "Chỉnh sửa  đơn hàng này";
            this.btnSave.Text = "Cập nhật";
            OrderService orderService = new OrderService();
            order = orderService.GetOrder(orderId);
            if (order != null)
            {
                if (orderDetails == null)
                {
                    OrderDetailService orderDetailService = new OrderDetailService();

                    orderDetails = new BindingList<OrderDetail>(orderDetailService.SelectOrderDetailByWhere(o => o.OrderId == order.Id));

                }
            }
            
        }
        private void AddOrder_Load(object sender, EventArgs e)
        {
            loadSomeData();
            SetupColumns();
            updateProductionRequestDetailCells();
            calculateTotal();
            
        }
        private void SetupColumns()
        {
            dgwOrderDetails.AutoGenerateColumns = false;

            if (orderDetails == null)
            {
                orderDetails = new BindingList<OrderDetail>();


            }

            var query = from orderDetail in orderDetails

                        select new ProductionRequestDetailModel
                        {
                            ProductId = orderDetail.ProductId,
                            AttributeId = orderDetail.AttributeId,
                            NumberUnit = orderDetail.NumberUnit,

                           
                            Price = orderDetail.Price,
                            Note = orderDetail.Note,
                            Total = (double)orderDetail.Price * orderDetail.NumberUnit
                        };

            originalProductions = new BindingList<ProductionRequestDetailModel>(query.ToList());
            dgwOrderDetails.DataSource = new BindingList<ProductionRequestDetailModel>(query.ToList());
            if(btnSave.Enabled)
                dgwOrderDetails.ReadOnly = false;
            else
                dgwOrderDetails.ReadOnly = true;
            DataGridViewComboBoxColumn productColumn = new DataGridViewComboBoxColumn();
            productColumn.Width = 150;
            productColumn.AutoComplete = false;

            productColumn.HeaderText = "Sản phẩm";
            productColumn.DataSource = products;
            productColumn.DisplayMember = "ProductName";
            //productColumn.Frozen = true;
            productColumn.ValueMember = "Id";

            dgwOrderDetails.Columns.Add(productColumn);

            DataGridViewComboBoxColumn productAttributeColumn = new DataGridViewComboBoxColumn();
            productAttributeColumn.Width = 150;
            productAttributeColumn.HeaderText = "Thuộc tính sản phẩm";

            productAttributeColumn.DataSource = baseAttributesAtRow;

            productAttributeColumn.DisplayMember = "AttributeName";
            //productColumn.Frozen = true;
            productAttributeColumn.ValueMember = "Id";

            dgwOrderDetails.Columns.Add(productAttributeColumn);



            DataGridViewTextBoxColumn numberUnitColumn = new DataGridViewTextBoxColumn();

            numberUnitColumn.Width = 100;
            numberUnitColumn.DataPropertyName = "NumberUnit";
            numberUnitColumn.HeaderText = "Số lượng";
            //numberUnitColumn.Frozen = true;
            //numberUnitColumn.ValueType = typeof(int);
            dgwOrderDetails.Columns.Add(numberUnitColumn);

            DataGridViewTextBoxColumn priceColumn = new DataGridViewTextBoxColumn();

            priceColumn.Width = 100;
            priceColumn.DataPropertyName = "Price";
            priceColumn.HeaderText = "Giá";
            //numberUnitColumn.Frozen = true;
            //priceColumn.ValueType = typeof(int);
            dgwOrderDetails.Columns.Add(priceColumn);

            DataGridViewTextBoxColumn totalColumn = new DataGridViewTextBoxColumn();

            totalColumn.Width = 100;
            totalColumn.DataPropertyName = "Total";
            totalColumn.HeaderText = "Tổng";
            totalColumn.ReadOnly = true;
            //numberUnitColumn.Frozen = true;
            //totalColumn.ValueType = typeof(int);
            dgwOrderDetails.Columns.Add(totalColumn);

            DataGridViewTextBoxColumn noteColumn = new DataGridViewTextBoxColumn();

            noteColumn.DataPropertyName = "Note";
            noteColumn.Width = 100;
            noteColumn.HeaderText = "Ghi chú";
            //numberUnitColumn.Frozen = true;
            noteColumn.ValueType = typeof(string);
            dgwOrderDetails.Columns.Add(noteColumn);

            DataGridViewImageColumn deleteButton = new DataGridViewImageColumn();
            deleteButton.DataPropertyName = "DeleteButton";
            deleteButton.Image = Properties.Resources.erase;
            deleteButton.Width = 100;
            deleteButton.HeaderText = "Xóa";
            deleteButton.ReadOnly = true;
            deleteButton.ImageLayout = DataGridViewImageCellLayout.Normal;
        }
        

        private void dgwOrderDetails_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //MessageBox.Show("Có lỗi nhập liệu xảy ra,vui lòng kiểm tra lại!");
        }

        private void dgwOrderDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (orderDetails == null)
            {
                orderDetails = new BindingList<OrderDetail>();
            }
            if (orderDetails.Count < dgwOrderDetails.RowCount)
            {
                OrderDetail orderDetail = new OrderDetail();
                orderDetails.Add(orderDetail);
            }
            if (dgv.CurrentCell.Value != null)
            {
                if (e.ColumnIndex == 0)
                {
                    
                    orderDetails[e.RowIndex].ProductId = (int)dgv.CurrentCell.Value;
                    ProductAttributeService productAttributeService = new ProductAttributeService();
                    List<ProductAttribute> productAttributes = productAttributeService.SelectProductAttributeByWhere(ba => ba.Id == orderDetails[e.RowIndex].ProductId);
                    baseAttributesAtRow = new BindingList<BaseAttribute>();
                    foreach(ProductAttribute pa in productAttributes)
                    {
                        baseAttributesAtRow.Add(pa.BaseAttribute);
                    }
                    DataGridViewComboBoxCell currentCell = (DataGridViewComboBoxCell)dgwOrderDetails.Rows[e.RowIndex].Cells[1];
                    currentCell.DataSource = baseAttributesAtRow;
                    if (baseAttributesAtRow.Count > e.RowIndex && baseAttributesAtRow.Count > 0)
                    {
                        orderDetails[e.RowIndex].AttributeId = baseAttributesAtRow[0].Id;
                        currentCell.Value = baseAttributesAtRow[0].Id;
                    }
                    

                }
                else if (e.ColumnIndex == 1)
                {
                    orderDetails[e.RowIndex].AttributeId = (int)dgv.CurrentCell.Value;
                }
                else if (e.ColumnIndex == 2)
                {
                    orderDetails[e.RowIndex].NumberUnit = (int)dgv.CurrentCell.Value;
                }
                else if (e.ColumnIndex == 3)
                {
                    orderDetails[e.RowIndex].Price = (double)dgv.CurrentCell.Value;
                }
                else if (e.ColumnIndex == 4)
                {
                    if (dgv.CurrentCell.Value != null)
                    {
                        orderDetails[e.RowIndex].Cost = (double)dgv.CurrentCell.Value;
                    }
                    
                }
                else if (e.ColumnIndex == 5)
                {
                    orderDetails[e.RowIndex].Note = (string)dgv.CurrentCell.Value;
                }
            }
            calculateTotal();
           
        }

        private void calculateTotal()
        {
            double totalNoTax = 0.0;
            double totalWithTax = 0.0;
            double discount = 0;
            double.TryParse(txtDiscount.Text, out discount);
            bool hasValue = false;

            for (int i = 0; i < dgwOrderDetails.RowCount; i++)
            {
                if (dgwOrderDetails.ColumnCount > 4)
                {
                    if (dgwOrderDetails.Rows[i].Cells[2].Value != null && dgwOrderDetails.Rows[i].Cells[3].Value != null)
                    {
                        dgwOrderDetails.Rows[i].Cells[4].Value = (int)dgwOrderDetails.Rows[i].Cells[2].Value * (double)dgwOrderDetails.Rows[i].Cells[3].Value;
                        hasValue = true;
                        totalNoTax += (double)dgwOrderDetails.Rows[i].Cells[4].Value;
                    }
                }
            }
            if (hasValue)
            {
                double vat = 0.0;
                double.TryParse(txtVAT.Text, out vat);
                totalWithTax = totalNoTax + vat - discount;
                lblSubTotal.Text = totalNoTax.ToString();
                lblGrantTotal.Text = totalWithTax.ToString();
            }
        }

        private void dgwOrderDetails_KeyUp(object sender, KeyEventArgs e)
        {
            if (sender is KeyPressAwareDataGridView)
            {
                KeyPressAwareDataGridView dgv = (KeyPressAwareDataGridView)sender;

                
            }
        }

        private void dgwOrderDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            if (dgwOrderDetails.CurrentCell.ColumnIndex == 0)
            {
                var source = new AutoCompleteStringCollection();
                String[] stringArray = Array.ConvertAll<Product, String>(products.ToArray(), delegate(Product row) { return (String)row.ProductName; });
                source.AddRange(stringArray);

                ComboBox prodCode = e.Control as ComboBox;
                if (prodCode != null)
                {
                    prodCode.DropDownStyle = ComboBoxStyle.DropDown;
                    prodCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    prodCode.AutoCompleteCustomSource = source;
                    prodCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    prodCode.MaxDropDownItems = 5;

                }
                //this.validator1.SetType(prodCode, Itboy.Components.ValidationType.Required);
            }
            else if (dgwOrderDetails.CurrentCell.ColumnIndex == 1)
            {
                if (baseAttributesAtRow != null)
                {
                    var source = new AutoCompleteStringCollection();
                    String[] stringArray = Array.ConvertAll<BaseAttribute, String>(baseAttributesAtRow.ToArray(), delegate(BaseAttribute row) { return (String)row.AttributeName; });
                    source.AddRange(stringArray);

                    ComboBox prodCode = e.Control as ComboBox;
                    if (prodCode != null)
                    {
                        prodCode.DropDownStyle = ComboBoxStyle.DropDown;
                        prodCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        prodCode.AutoCompleteCustomSource = source;
                        prodCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        prodCode.MaxDropDownItems = 5;

                    }
                    //this.validator1.SetType(prodCode, Itboy.Components.ValidationType.Required);
                }

            }
            else if (dgwOrderDetails.CurrentCell.ColumnIndex == 2)
            {
                TextBox numberOfUnit = e.Control as TextBox;
                this.validator1.SetRegularExpression(numberOfUnit, BHConstant.REGULAR_EXPRESSION_FOR_NUMBER);
                this.validator1.SetType(numberOfUnit, Itboy.Components.ValidationType.RegularExpression);
            }
            else if (dgwOrderDetails.CurrentCell.ColumnIndex == 3)
            {
                TextBox price = e.Control as TextBox;
                this.validator1.SetRegularExpression(price, BHConstant.REGULAR_EXPRESSION_FOR_CURRENCY);
                this.validator1.SetType(price, Itboy.Components.ValidationType.RegularExpression);
            }
            else if (dgwOrderDetails.CurrentCell.ColumnIndex == 4)
            {
                TextBox total = e.Control as TextBox;
                this.validator1.SetRegularExpression(total, BHConstant.REGULAR_EXPRESSION_FOR_CURRENCY);
                this.validator1.SetType(total, Itboy.Components.ValidationType.RegularExpression);
            }
            else
            {
                if (e.Control is TextBox)
                {
                    TextBox other = e.Control as TextBox;
                    this.validator1.SetType(other, Itboy.Components.ValidationType.None);
                }
            }
            

        }

        private void dgwOrderDetails_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void CaptureScreen()
        {
            Graphics g1 = this.CreateGraphics();

            MyImage = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height, g1);

            Graphics g2 = Graphics.FromImage(MyImage);

            IntPtr dc1 = g1.GetHdc();

            IntPtr dc2 = g2.GetHdc();

            BitBlt(dc2, 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height, dc1, 0, 0, 13369376);

            g1.ReleaseHdc(dc1);

            g2.ReleaseHdc(dc2);

            //MyImage.Save(@"c:\PrintPage.jpg", ImageFormat.Jpeg);
        }
        private void btnPrintOrder_Click(object sender, EventArgs e)
        {
            if (btnSave.Enabled)
            {
                if (saveData())
                {
                    printOrder();
                };
            }
            
            
            //CaptureScreen();
            //using (var dlg = new CoolPrintPreviewDialog())
            //{
            //    dlg.Document = this.printDoc;
            //    dlg.ShowDialog(this);
            //}
           
            
        }
        public void StartPrint(Stream streamToPrint, string streamType)
        {

            this.printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);

            this.streamToPrint = streamToPrint;

            this.streamType = streamType;

            System.Windows.Forms.PrintDialog PrintDialog1 = new PrintDialog();

            PrintDialog1.AllowSomePages = true;

            PrintDialog1.ShowHelp = true;

            PrintDialog1.Document = printDoc;

            DialogResult result = PrintDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {

                printDoc.Print();

                //docToPrint.Print();

            }
        }
        private void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            //System.Drawing.Image image = System.Drawing.Image.FromStream(this.streamToPrint);
            //;
            //int x = e.MarginBounds.X;

            //int y = e.MarginBounds.Y;

            //int width = image.Width;

            //int height = image.Height;

            //if ((width / e.MarginBounds.Width) > (height / e.MarginBounds.Height))
            //{

            //    width = e.MarginBounds.Width;

            //    height = image.Height * e.MarginBounds.Width / image.Width;

            //}

            //else
            //{

            //    height = e.MarginBounds.Height;

            //    width = image.Width * e.MarginBounds.Height / image.Height;

            //}

            //System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(x, y, width, height);

            //e.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel);
            e.Graphics.DrawImage(MyImage, 0, 0);
            MyImage.Save(@"c:\PrintPage.jpg", ImageFormat.Jpeg);
        }

        private void btnPrintXK_Click(object sender, EventArgs e)
        {
            printForStock();
        }
        private void printForStock()
        {
            object oMissing = System.Reflection.Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.
            Microsoft.Office.Interop.Word._Application oWord;
            Word._Document oDoc;
            oWord = new Word.Application();
            oWord.Visible = true;
            oDoc = oWord.Documents.Add(ref oMissing, ref oMissing,
                ref oMissing, ref oMissing);
            //Insert a paragraph at the beginning of the document.
            Word.Paragraph oPara1;
            oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara1.Range.Text = "PHIẾU XUẤT KHO";
            oPara1.Range.Font.Bold = 1;
            oPara1.Range.Font.Size = 30;
            oPara1.Range.Font.Name = "Times New Roman";
            oPara1.Format.SpaceAfter = 1;    //24 pt spacing after paragraph.
            oPara1.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            Word.Table oTableForHeader2;
            Word.Range ForHeader2 = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTableForHeader2 = oDoc.Tables.Add(ForHeader2, 1, 1, ref oMissing, ref oMissing);
            oTableForHeader2.Range.ParagraphFormat.SpaceAfter = 1;
            oTableForHeader2.Range.Font.Size = 8;
            oTableForHeader2.Range.Font.Name = "Times New Roman";

            Word.Paragraph oPara6;
            oPara6 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara6.Range.Text = "(SẼ BỔ XUNG HÓA ĐƠN TÀI CHÍNH VÀ THU THÊM THUẾ GTGT 10% SAU)";
            oPara6.Range.Font.Bold = 0;
            oPara6.Range.Font.Size = 14;
            oPara6.Range.Font.Name = "Times New Roman";
            oPara6.Format.SpaceAfter = 24;    //24 pt spacing after paragraph.
            oPara6.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            oPara6.Range.Font.Italic = 1;


            //Word.Paragraph oPara8;
            //oPara8 = oDoc.Content.Paragraphs.Add(ref oMissing);
            //oPara8.Range.Text = "";
            //oPara8.Range.Font.Bold = 0;
            //oPara8.Range.Font.Size = 8;
            //oPara8.Range.Font.Name = "Times New Roman";
            //oPara8.Format.SpaceAfter = 1;    //24 pt spacing after paragraph.
            //oPara8.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            //oPara8.Range.Font.Italic = 1;

            Word.Table oTableForCustomerInfo;
            Word.Range ForCustomerInfo = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTableForCustomerInfo = oDoc.Tables.Add(ForCustomerInfo, 4, 4, ref oMissing, ref oMissing);
            oTableForCustomerInfo.Range.ParagraphFormat.SpaceAfter = 1;
            oTableForCustomerInfo.Range.Font.Name = "Times New Roman";
            oTableForCustomerInfo.Range.Font.Size = 13;
            
            oTableForCustomerInfo.Cell(1, 1).Range.Text = "Khách hàng:";
            oTableForCustomerInfo.Cell(1, 1).Range.Bold = 1;
            oTableForCustomerInfo.Cell(1, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            if (cbxCustomer.SelectedValue != null)
            {
                oTableForCustomerInfo.Cell(1, 2).Width = 400;
                oTableForCustomerInfo.Cell(1, 2).Range.Text = customers.Where(cus => cus.Id == (int)cbxCustomer.SelectedValue).FirstOrDefault().CustomerName;
                oTableForCustomerInfo.Cell(1, 2).Range.Bold = 0;
                oTableForCustomerInfo.Cell(1, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            }

            
            oTableForCustomerInfo.Cell(2, 1).Range.Text = "Địa chỉ:";
            oTableForCustomerInfo.Cell(2, 1).Range.Bold = 1;
            oTableForCustomerInfo.Cell(2, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            if (cbxCustomer.SelectedValue != null)
            {
                oTableForCustomerInfo.Cell(2, 2).Width = 400;
                oTableForCustomerInfo.Cell(2, 2).Range.Text = customers.Where(cus => cus.Id == (int)cbxCustomer.SelectedValue).FirstOrDefault().Address;
                oTableForCustomerInfo.Cell(2, 2).Range.Bold = 0;
                oTableForCustomerInfo.Cell(2, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            }


            oTableForCustomerInfo.Cell(3, 1).Range.Text = "Lý do xuất kho:";
            oTableForCustomerInfo.Cell(3, 1).Range.Bold = 1;
            oTableForCustomerInfo.Cell(3, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            oTableForCustomerInfo.Cell(3, 2).Width = 400;
            oTableForCustomerInfo.Cell(3, 2).Range.Text = "";
            oTableForCustomerInfo.Cell(3, 2).Range.Bold = 0;
            oTableForCustomerInfo.Cell(3, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            oTableForCustomerInfo.Cell(4, 1).Range.Text = "Xuất tại kho:";
            oTableForCustomerInfo.Cell(4, 1).Range.Bold = 1;
            oTableForCustomerInfo.Cell(4, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            oTableForCustomerInfo.Cell(4, 2).Width = 400;
            oTableForCustomerInfo.Cell(4, 2).Range.Text = "";
            oTableForCustomerInfo.Cell(4, 2).Range.Bold = 0;
            oTableForCustomerInfo.Cell(4, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            

            Word.Paragraph oPara2;
            oPara2 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara2.Range.Font.Name = "Times New Roman";
            oPara2.Range.Font.Bold = 1;
            oPara2.Range.Font.Size = 8;
            oPara2.Format.SpaceAfter = 1;    //24 pt spacing after paragraph.
            oPara2.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;


            //Insert a table, fill it with data, and make the first row
            //bold and italic.
            Word.Table oTable;
            Word.Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTable = oDoc.Tables.Add(wrdRng, dgwOrderDetails.RowCount + 1, 4, ref oMissing, ref oMissing);
            oTable.Range.ParagraphFormat.SpaceAfter = 6;
            oTable.Borders.Enable = 1;
            oTable.Range.Font.Size = 12;
            oTable.Range.Font.Bold = 1;
            oTable.Range.Font.Name = "Times New Roman";
            int r, c;


            for (r = 1; r <= dgwOrderDetails.RowCount; r++)
                for (c = 1; c <= dgwOrderDetails.ColumnCount; c++)
                {
                    if (r == 1)
                    {
                        if (c == 1)
                            oTable.Cell(r, c).Range.Text = "STT";
                        else if (c == 2)
                            oTable.Cell(r, c).Range.Text = "Tên hàng";
                        else if (c == 3)
                            oTable.Cell(r, c).Range.Text = "Quy cách";
                        else if (c == 4)
                            oTable.Cell(r, c).Range.Text = "Số lượng";
                        //else if (c == 5)
                        //    oTable.Cell(r, c).Range.Text = "Ghi chú";
                        
                    }
                    else
                    {
                        if (c == 1)
                        {
                            oTable.Cell(r, c).Range.Text = (r - 1).ToString();
                        }
                        else if (c == 2)
                        {
                            oTable.Cell(r, c).Range.Text = products.ToList().Where(p => p.Id == orderDetails[r - 2].ProductId).FirstOrDefault().ProductName;
                        }
                        else if (c == 3)
                        {
                            oTable.Cell(r, c).Range.Text = baseAttributesAtRow.ToList().Where(a => a.Id == (int)dgwOrderDetails.Rows[r - 2].Cells[c - 2].Value).FirstOrDefault().AttributeName;
                        }
                        else if (c == 4)
                        {
                            oTable.Cell(r, c).Range.Text = ((int)dgwOrderDetails.Rows[r - 2].Cells[c - 2].Value).ToString();
                        }
                        //else if (c == 5)
                        //{
                        //    oTable.Cell(r, c).Range.Text = (string)dgwOrderDetails.Rows[r - 2].Cells[dgwOrderDetails.ColumnCount - 1].Value;
                        //}
                        

                    }


                }

            oTable.Rows[1].Range.Font.Italic = 1;
            oTable.Rows[1].Range.Font.Bold = 0;


            Word.Paragraph oPara3;
            oPara3 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara3.Range.Font.Name = "Times New Roman";
            oPara3.Range.Font.Bold = 1;
            oPara3.Range.Font.Size = 8;
            oPara3.Format.SpaceAfter = 1;    //24 pt spacing after paragraph.
            oPara3.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            Word.Table oTableForNote;
            Word.Range wrdRngForNote = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTableForNote = oDoc.Tables.Add(wrdRngForNote, 2, 2, ref oMissing, ref oMissing);
            oTableForNote.Range.ParagraphFormat.SpaceAfter = 6;
            oTableForNote.Borders.Enable = 0;
            oTableForNote.Range.Font.Size = 12;
            oTableForNote.Range.Font.Name = "Times New Roman";

            oTableForNote.Cell(1, 1).Width = 50;
            oTableForNote.Cell(1, 1).Range.Text = "Ghi chú";
            oTableForNote.Cell(1, 1).Range.Bold = 0;
            oTableForNote.Cell(1, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            oTableForNote.Cell(1, 2).Width = 400;
            oTableForNote.Cell(1, 2).Range.Text = "..................................................................................................................................................................................................................................................................";
            oTableForNote.Cell(1, 2).Range.Bold = 0;
            oTableForNote.Cell(1, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

           
            //oTableForNote.Cell(2, 2).Width = 400;
            oTableForNote.Cell(2, 2).Range.Text = "Xuất ngày      tháng      năm        ";
            oTableForNote.Cell(2, 2).Range.Bold = 0;
            oTableForNote.Cell(2, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;

            oTableForNote.Rows[1].Range.Font.Italic = 1;
            oTableForNote.Rows[2].Range.Font.Italic = 1;

            

            Word.Paragraph oPara4;
            oPara4 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara1.Range.Text = "";
            oPara4.Range.Font.Name = "Times New Roman";
            oPara4.Range.Font.Bold = 0;
            oPara4.Range.Font.Size = 8;
            oPara4.Format.SpaceAfter = 1;    //24 pt spacing after paragraph.
            oPara4.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
            

            Word.Table oTableForFooter;
            Word.Range wrdRngForFooter = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTableForFooter = oDoc.Tables.Add(wrdRngForFooter, 1, 4, ref oMissing, ref oMissing);
            oTableForFooter.Range.ParagraphFormat.SpaceAfter = 6;
            oTableForFooter.Borders.Enable = 0;
            oTableForFooter.Range.Font.Size = 12;
            oTableForFooter.Range.Font.Name = "Times New Roman";

            oTableForFooter.Cell(1, 1).Width = 120;
            oTableForFooter.Cell(1, 1).Range.Text = "NGƯỜI LẬP PHIẾU\n (Ký,họ tên)";
            oTableForFooter.Cell(1, 1).Range.Bold = 0;
            oTableForFooter.Cell(1, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            oTableForFooter.Cell(1, 2).Width = 110;
            oTableForFooter.Cell(1, 2).Range.Text = "NGƯỜI NHẬN\n (Ký,họ tên)";
            oTableForFooter.Cell(1, 2).Range.Bold = 0;
            oTableForFooter.Cell(1, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            oTableForFooter.Cell(1, 3).Width = 110;
            oTableForFooter.Cell(1, 3).Range.Text = "NGƯỜI GIAO\n (Ký,họ tên)";
            oTableForFooter.Cell(1, 3).Range.Bold = 0;
            oTableForFooter.Cell(1, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            oTableForFooter.Cell(1, 4).Width = 150;
            oTableForFooter.Cell(1, 4).Range.Text = "THỦ TRƯỞNG ĐƠN VỊ\n (Ký,họ tên)";
            oTableForFooter.Cell(1, 4).Range.Bold = 0;
            oTableForFooter.Cell(1, 4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            oTableForFooter.Rows[1].Range.Font.Italic = 0;

            Word.Paragraph oPara5;
            oPara5 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara5.Range.Font.Name = "Times New Roman";
            oPara5.Range.Font.Bold = 1;
            oPara5.Range.Font.Size = 8;
            oPara5.Format.SpaceAfter = 1;    //24 pt spacing after paragraph.
            oPara5.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            oDoc.PrintPreview();
        }
        private void printOrder()
        {
            double totalNoTax = 0.0;
            for (int i = 0; i < dgwOrderDetails.RowCount; i++)
            {
                if (dgwOrderDetails.ColumnCount > 4)
                {
                    if (dgwOrderDetails.Rows[i].Cells[2].Value != null && dgwOrderDetails.Rows[i].Cells[3].Value != null)
                    {
                        dgwOrderDetails.Rows[i].Cells[4].Value = (int)dgwOrderDetails.Rows[i].Cells[2].Value * (double)dgwOrderDetails.Rows[i].Cells[3].Value;
                        
                        totalNoTax += (double)dgwOrderDetails.Rows[i].Cells[4].Value;
                    }
                }
            }

            object oMissing = System.Reflection.Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.
            Microsoft.Office.Interop.Word._Application oWord;
            Word._Document oDoc;
            oWord = new Word.Application();
            oWord.Visible = true;
            oDoc = oWord.Documents.Add(ref oMissing, ref oMissing,
                ref oMissing, ref oMissing);
            
            Word.Table oTableForHeader;
            Word.Range ForHeader = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTableForHeader = oDoc.Tables.Add(ForHeader, 4, 4, ref oMissing, ref oMissing);
            oTableForHeader.Range.ParagraphFormat.SpaceAfter = 6;
            oTableForHeader.Range.Font.Size = 12;
            oTableForHeader.Range.Font.Name = "Times New Roman";
            oTableForHeader.Cell(1, 1).Range.Text = BHConstant.COMPANY_NAME;
            oTableForHeader.Cell(1, 1).Range.Bold = 0;
            oTableForHeader.Cell(1, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            oTableForHeader.Cell(1, 1).Width = 125;
            oTableForHeader.Cell(1, 2).Width = 175;

            oTableForHeader.Cell(1, 3).Range.Text = "Mã phiếu:";
            oTableForHeader.Cell(1, 3).Range.Bold = 1;
            oTableForHeader.Cell(1, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
            oTableForHeader.Cell(1, 4).Range.Text = txtOrderCode.Text != null ? txtOrderCode.Text : "không tồn tại";
            oTableForHeader.Cell(1, 4).Range.Bold = 0;
            oTableForHeader.Cell(1, 4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            oTableForHeader.Cell(2, 1).Width = 75;
            oTableForHeader.Cell(2, 1).Range.Text = "Đ/C:";
            oTableForHeader.Cell(2, 1).Range.Bold = 0;
            oTableForHeader.Cell(2, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            oTableForHeader.Cell(2, 2).Width = 225;
            oTableForHeader.Cell(2, 2).Range.Text = BHConstant.COMPANY_ADDRESS;
            oTableForHeader.Cell(2, 2).Range.Bold = 0;
            oTableForHeader.Cell(2, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            oTableForHeader.Cell(2, 3).Range.Text = "Ngày lập:";
            oTableForHeader.Cell(2, 3).Range.Bold = 1;
            oTableForHeader.Cell(2, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;

            oTableForHeader.Cell(2, 4).Range.Text = txtCreatedDate.Text;
            oTableForHeader.Cell(2, 4).Range.Bold = 0;
            oTableForHeader.Cell(2, 4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            oTableForHeader.Cell(3, 1).Width = 75;
            oTableForHeader.Cell(3, 1).Range.Text = "Điện thoại:";
            oTableForHeader.Cell(3, 1).Range.Bold = 0;
            oTableForHeader.Cell(3, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            oTableForHeader.Cell(3, 2).Width = 225;
            oTableForHeader.Cell(3, 2).Range.Text = BHConstant.COMPANY_PHONE;
            oTableForHeader.Cell(3, 2).Range.Bold = 0;
            oTableForHeader.Cell(3, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            oTableForHeader.Cell(4, 1).Width = 75;
            oTableForHeader.Cell(4, 1).Range.Text = "Fax:";
            oTableForHeader.Cell(4, 1).Range.Bold = 0;
            oTableForHeader.Cell(4, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            oTableForHeader.Cell(4, 2).Width = 225;
            oTableForHeader.Cell(4, 2).Range.Text = BHConstant.COMPANY_FAX;
            oTableForHeader.Cell(4, 2).Range.Bold = 0;
            oTableForHeader.Cell(4, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;


            //Insert a paragraph at the beginning of the document.
            Word.Paragraph oPara1;
            oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara1.Range.Text = "PHIẾU BÁN HÀNG";
            oPara1.Range.Font.Bold = 1;
            oPara1.Range.Font.Name = "Times New Roman";
            oPara1.Range.Font.Size = 30;
            oPara1.Format.SpaceAfter = 1;    //24 pt spacing after paragraph.
            oPara1.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            oPara1.Range.InsertParagraphAfter();

            Word.Table oTableForCustomerInfo;
            Word.Range ForCustomerInfo = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTableForCustomerInfo = oDoc.Tables.Add(ForCustomerInfo, 3, 4, ref oMissing, ref oMissing);
            oTableForCustomerInfo.Range.ParagraphFormat.SpaceAfter = 1;
            oTableForCustomerInfo.Range.Font.Name = "Times New Roman";
            oTableForCustomerInfo.Range.Font.Size = 13;
            oTableForCustomerInfo.Cell(1, 1).Range.Text = "Khách hàng:";
            oTableForCustomerInfo.Cell(1, 1).Range.Bold = 1;
            oTableForCustomerInfo.Cell(1, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            if (cbxCustomer.SelectedValue != null)
            {
                oTableForCustomerInfo.Cell(1, 2).Width = 400;
                oTableForCustomerInfo.Cell(1, 2).Range.Text = customers.Where(cus => cus.Id == (int)cbxCustomer.SelectedValue).FirstOrDefault().CustomerName;
                oTableForCustomerInfo.Cell(1, 2).Range.Bold = 0;
                oTableForCustomerInfo.Cell(1, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            }

            oTableForCustomerInfo.Cell(2, 1).Range.Text = "Địa chỉ:";
            oTableForCustomerInfo.Cell(2, 1).Range.Bold = 1;
            oTableForCustomerInfo.Cell(2, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            if (cbxCustomer.SelectedValue != null)
            {
                oTableForCustomerInfo.Cell(2, 2).Width = 400;
                oTableForCustomerInfo.Cell(2, 2).Range.Text = customers.Where(cus => cus.Id == (int)cbxCustomer.SelectedValue).FirstOrDefault().Address;
                oTableForCustomerInfo.Cell(2, 2).Range.Bold = 0;
                oTableForCustomerInfo.Cell(2, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            }

           
            oTableForCustomerInfo.Cell(3, 1).Range.Text = "Ghi chú:";
            oTableForCustomerInfo.Cell(3, 1).Range.Bold = 1;
            oTableForCustomerInfo.Cell(3, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            oTableForCustomerInfo.Cell(3, 2).Width = 400;
            oTableForCustomerInfo.Cell(3, 2).Range.Text = txtNote.Text;
            oTableForCustomerInfo.Cell(3, 2).Range.Bold = 0;
            oTableForCustomerInfo.Cell(3, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            //Microsoft.Office.Interop.Word.InlineShape line3 = oDoc.Paragraphs.Last.Range.InlineShapes.AddHorizontalLineStandard(ref oMissing);
            //line3.Height = 1;
            //line3.HorizontalLineFormat.NoShade = true;
            //line3.Fill.ForeColor.RGB = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Transparent);

            Word.Paragraph oPara2;
            oPara2 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara2.Range.Font.Name = "Times New Roman";
            oPara2.Range.Font.Bold = 1;
            oPara2.Range.Font.Size = 8;
            oPara2.Format.SpaceAfter = 1;    //24 pt spacing after paragraph.
            oPara2.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            //oPara2.Range.InsertParagraphAfter();

            //Insert a table, fill it with data, and make the first row
            //bold and italic.
            Word.Table oTable;
            Word.Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTable = oDoc.Tables.Add(wrdRng, dgwOrderDetails.RowCount + 1, 6, ref oMissing, ref oMissing);
            oTable.Range.ParagraphFormat.SpaceAfter = 1;
            oTable.Borders.Enable = 1;
            oTable.Range.Font.Name = "Times New Roman";
            oTable.Range.Font.Size = 12;
            oTable.Range.Font.Bold = 1;
            oTable.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            int r, c;


            for (r = 1; r <= dgwOrderDetails.RowCount; r++)
                for (c = 1; c <= dgwOrderDetails.ColumnCount; c++)
                {
                    if (r == 1)
                    {
                        if(c == 1)
                            oTable.Cell(r, c).Range.Text = "STT";
                        else if(c == 2)
                            oTable.Cell(r, c).Range.Text = "Tên hàng";
                        else if (c == 3)
                            oTable.Cell(r, c).Range.Text = "Quy cách";
                        else if (c == 4)
                            oTable.Cell(r, c).Range.Text = "Số lượng";
                        else if (c == 5)
                            oTable.Cell(r, c).Range.Text = "Giá(VNĐ)";
                        else if (c == 6)
                            oTable.Cell(r, c).Range.Text = "Thành tiền(VNĐ)";
                        //oTable.Cell(r, c).Range.Text = dgwOrderDetails.Columns[c - 1].HeaderText;
                    }
                    else
                    {
                        if (c == 1)
                        {
                            oTable.Cell(r, c).Range.Text = (r - 1).ToString();
                        }
                        else if (c == 2)
                        {
                            oTable.Cell(r, c).Range.Text = products.ToList().Where(p => p.Id == orderDetails[r - 2].ProductId).FirstOrDefault().ProductName;
                        }
                        else if (c == 3)
                        {
                            oTable.Cell(r, c).Range.Text = baseAttributesAtRow.ToList().Where(a => a.Id == (int)dgwOrderDetails.Rows[r - 2].Cells[c - 2].Value).FirstOrDefault().AttributeName;
                        }
                        else if (c == 4)
                        {
                            oTable.Cell(r, c).Range.Text = ((int)dgwOrderDetails.Rows[r - 2].Cells[c - 2].Value).ToString();
                        }
                        else if (c == 5)
                        {
                            string tmp = ((double)dgwOrderDetails.Rows[r - 2].Cells[c - 2].Value).ToString();
                            oTable.Cell(r, c).Range.Text = BaoHien.Common.Global.convertToCurrency(tmp);
                        }
                        else if (c == 6)
                        {
                            string tmp = (((double)dgwOrderDetails.Rows[r - 2].Cells[c - 2].Value)).ToString();
                            oTable.Cell(r, c).Range.Text = BaoHien.Common.Global.convertToCurrency(tmp);
                        }
                        
                    }


                }
            
            oTable.Rows[1].Range.Font.Italic = 1;
            oTable.Rows[1].Range.Font.Bold = 0;

            Word.Paragraph oPara3;
            oPara3 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara3.Range.Font.Name = "Times New Roman";
            oPara3.Range.Font.Bold = 1;
            oPara3.Range.Font.Size = 8;
            oPara3.Format.SpaceAfter = 1;    //24 pt spacing after paragraph.
            oPara3.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            //oPara3.Range.InsertParagraphAfter();

            Word.Table oTable3;
            Word.Range wrdRng3 = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTable3 = oDoc.Tables.Add(wrdRng3, 5, 3, ref oMissing, ref oMissing);
            oTable3.Range.ParagraphFormat.SpaceAfter = 1;
            oTable3.Borders.Enable = 0;
            oTable3.Range.Font.Size = 12;
            oTable3.Range.Font.Name = "Times New Roman";

            oTable3.Cell(1, 1).Width = 200;
            oTable3.Cell(2, 1).Width = 200;
            oTable3.Cell(3, 1).Width = 200;
            oTable3.Cell(4, 1).Width = 200;
            oTable3.Cell(5, 1).Width = 200;

            oTable3.Cell(1, 2).Width = 125;
            oTable3.Cell(1, 2).Range.Text = "Giá trị hàng:";
            oTable3.Cell(1, 2).Range.Bold = 0;
            oTable3.Cell(1, 2).Range.Italic = 1;
            oTable3.Cell(1, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;

            //oTable3.Cell(1, 3).Width = 150;
            oTable3.Cell(1, 3).Range.Text = BaoHien.Common.Global.convertToCurrency(totalNoTax.ToString());
            oTable3.Cell(1, 3).Range.Bold = 0;
            oTable3.Cell(1, 3).Range.Italic = 0;
            oTable3.Cell(1, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;

            oTable3.Cell(2, 2).Width = 125;
            oTable3.Cell(2, 2).Range.Text = "VAT:";
            oTable3.Cell(2, 2).Range.Bold = 0;
            oTable3.Cell(2, 2).Range.Italic = 1;
            oTable3.Cell(2, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;

            //oTable3.Cell(2, 3).Width = 150;
            oTable3.Cell(2, 3).Range.Text = BaoHien.Common.Global.convertToCurrency(txtVAT.Text);
            oTable3.Cell(2, 3).Range.Bold = 0;
            oTable3.Cell(2, 3).Range.Italic = 0;
            oTable3.Cell(2, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;

            oTable3.Cell(3, 2).Width = 125;
            oTable3.Cell(3, 2).Range.Text = "Chiết khấu:";
            oTable3.Cell(3, 2).Range.Bold = 0;
            oTable3.Cell(3, 2).Range.Italic = 1;
            oTable3.Cell(3, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;

            //oTable3.Cell(3, 3).Width = 150;
            oTable3.Cell(3, 3).Range.Text = BaoHien.Common.Global.convertToCurrency(txtDiscount.Text);
            oTable3.Cell(3, 3).Range.Bold = 0;
            oTable3.Cell(3, 3).Range.Italic = 0;
            oTable3.Cell(3, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;

            oTable3.Cell(4, 2).Width = 125;
            oTable3.Cell(4, 2).Range.Text = "Tổng tiền:";
            oTable3.Cell(4, 2).Range.Bold = 0;
            oTable3.Cell(4, 2).Range.Italic = 1;
            oTable3.Cell(4, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;

            //oTable3.Cell(4, 3).Width = 150;
            oTable3.Cell(4, 3).Range.Text = BaoHien.Common.Global.convertToCurrency(lblGrantTotal.Text);
            oTable3.Cell(4, 3).Range.Bold = 1;
            oTable3.Cell(4, 3).Range.Italic = 0;
            oTable3.Cell(4, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;

            oTable3.Cell(5, 2).Width = 125;
            oTable3.Cell(5, 2).Range.Text = "Đơn vị:";
            oTable3.Cell(5, 2).Range.Bold = 0;
            oTable3.Cell(5, 2).Range.Italic = 1;
            oTable3.Cell(5, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;

            //oTable3.Cell(5, 3).Width = 150;
            oTable3.Cell(5, 3).Range.Text = "VNĐ";
            oTable3.Cell(5, 3).Range.Bold = 1;
            oTable3.Cell(5, 3).Range.Italic = 0;
            oTable3.Cell(5, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;

            Word.Paragraph oPara4;
            oPara4 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara4.Range.Font.Name = "Times New Roman";
            oPara4.Range.Font.Bold = 1;
            oPara4.Range.Font.Size = 8;
            oPara4.Format.SpaceAfter = 1;    //24 pt spacing after paragraph.
            oPara4.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            //oPara4.Range.InsertParagraphAfter();

            Word.Table oTableForFooter;
            Word.Range wrdRngForFooter = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTableForFooter = oDoc.Tables.Add(wrdRngForFooter, 1, 3, ref oMissing, ref oMissing);
            oTableForFooter.Range.ParagraphFormat.SpaceAfter = 1;
            oTableForFooter.Borders.Enable = 0;
            oTableForFooter.Range.Font.Size = 12;
            oTableForFooter.Range.Font.Name = "Times New Roman";

            oTableForFooter.Cell(1, 1).Range.Text = "Khách hàng";
            oTableForFooter.Cell(1, 1).Range.Bold = 0;
            oTableForFooter.Cell(1, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            oTableForFooter.Cell(1, 2).Range.Text = "Người lập phiếu";
            oTableForFooter.Cell(1, 2).Range.Bold = 0;
            oTableForFooter.Cell(1, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            
            oTableForFooter.Cell(1, 3).Range.Text = "Kế toán";
            oTableForFooter.Cell(1, 3).Range.Bold = 0;
            oTableForFooter.Cell(1, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            oTableForFooter.Rows[1].Range.Font.Italic = 1;

            Word.Paragraph oPara5;
            oPara5 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara5.Range.Font.Name = "Times New Roman";
            oPara5.Range.Font.Bold = 1;
            oPara5.Range.Font.Size = 8;
            oPara5.Format.SpaceAfter = 1;    //24 pt spacing after paragraph.
            oPara5.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            //oPara5.Range.InsertParagraphAfter();

            oDoc.PrintPreview();
            //oDoc.Close(ref oMissing, ref oMissing, ref oMissing);
        }
        private void InsertLine()
        {
            

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtVAT_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtVAT_Leave(object sender, EventArgs e)
        {
            if (txtVAT.Text != null)
            {
                
                

            }
        }
        private void disableForm()
        {
            txtCreatedDate.Enabled = false;
            txtDiscount.Enabled = false;
            txtNote.Enabled = false;
            txtOrderCode.Enabled = false;
            txtVAT.Enabled = false;
            cbxCustomer.Enabled = false;
            btnSave.Enabled = false;
            dgwOrderDetails.ReadOnly = true;
        }
        
    }
}
