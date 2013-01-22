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
using BaoHien.Services.ProductInStocks;
using System.Globalization;
using PDF = iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System.Diagnostics;
using BaoHien.Services.ProductLogs;
using BaoHien.Services.Employees;
using BaoHien.Services.MeasurementUnits;

namespace BaoHien.UI
{
    public partial class AddOrder : BaseForm
    {
        bool isUpdating = false;
        Order order;
        ProductLogService productLogService;
        MeasurementUnitService unitService;
        BindingList<MeasurementUnit> units;
        BindingList<Customer> customers;
        BindingList<OrderDetail> orderDetails;
        BindingList<ProductAttributeModel> productAttrs;
        BindingList<ProductionRequestDetailModel> originalProductions;
        double totalWithTax = 0.0, totalCommission = 0.0;

        // Cell position
        const int ProductAttrCell = 0, NumberUnitCell = 1, UnitCell = 2, PriceCell = 3, CommissionCell = 4, TotalCell = 5, NoteCell = 6;

        public AddOrder()
        {
            InitializeComponent();
        }

        private bool saveData()
        {
            calculateTotal();
            if (validator1.Validate())
            {
                DialogResult dialogResult = MessageBox.Show("Bạn sẽ không thể chỉnh sửa sau khi lưu!Bạn muốn lưu?", "Xác nhận", MessageBoxButtons.YesNo);
                if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                {
                    double discount = 0;
                    Double.TryParse(txtDiscount.WorkingText, out discount);
                    DateTime createdDate = DateTime.Now;
                    if (!DateTime.TryParse(txtCreatedDate.Text, out createdDate))
                    {
                        createdDate = DateTime.Now;
                    };

                    double vat = 0;
                    Double.TryParse(txtVAT.WorkingText, out vat);
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
                        return true;

                        #region Fix Update

                        //order.CustId = (int)cbxCustomer.SelectedValue;
                        //order.Discount = discount;
                        //order.Note = txtNote.Text;
                        //order.VAT = vat;
                        //order.OrderCode = txtOrderCode.Text;
                        //order.Total = totalWithTax;
                        //OrderService orderService = new OrderService();
                        //bool result = orderService.UpdateOrder(order);
                        //if (!result)
                        //{
                        //    MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                        //    return false;
                        //}
                        //else
                        //{
                        //    OrderDetailService orderDetailService = new OrderDetailService();
                        //    foreach (OrderDetail od in orderDetails)
                        //    {
                        //        if (od.ProductId > 0 && od.AttributeId > 0)
                        //        {
                        //            if (od.Id == 0)
                        //            {
                        //                od.OrderId = order.Id;
                        //                result = orderDetailService.AddOrderDetail(od);

                        //                //Save in Production In Stock
                        //                ProductInStock productInStock = new ProductInStock();
                        //                List<ProductInStock> lstProductInStock = pis.SelectProductByWhere(pt => pt.ProductId == od.ProductId && pt.AttributeId == od.AttributeId);
                        //                productInStock.AttributeId = od.AttributeId;
                        //                productInStock.ProductId = od.ProductId;
                        //                productInStock.LatestUpdate = DateTime.Now;
                        //                productInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_OUTPUT;

                        //                productInStock.NumberOfInput = lstProductInStock.Last<ProductInStock>().NumberOfInput;
                        //                productInStock.NumberOfOutput = lstProductInStock.Last<ProductInStock>().NumberOfOutput + od.NumberUnit;
                        //                productInStock.NumberOfItem = (int)lstProductInStock.Last<ProductInStock>().NumberOfItem - od.NumberUnit;

                        //                pis.AddProductInStock(productInStock);
                        //            }
                        //            else
                        //            {
                        //                ProductionRequestDetailModel original = new ProductionRequestDetailModel();
                        //                original = originalProductions.Where(p => p.Id == od.Id).ToList().FirstOrDefault();
                        //                result = orderDetailService.UpdateOrderDetail(od);

                        //                //Update so luong
                        //                if (original != null)
                        //                {
                        //                    if (od.ProductId == original.ProductId && od.AttributeId == original.AttributeId && od.NumberUnit != original.NumberUnit)
                        //                    {
                        //                        ProductInStock productInStock = new ProductInStock();
                        //                        List<ProductInStock> lstProductInStock = pis.SelectProductByWhere(pt => pt.ProductId == od.ProductId && pt.AttributeId == od.AttributeId);

                        //                        productInStock.AttributeId = od.AttributeId;
                        //                        productInStock.ProductId = od.ProductId;
                        //                        productInStock.LatestUpdate = DateTime.Now;
                        //                        productInStock.NumberOfInput = lstProductInStock.Last<ProductInStock>().NumberOfInput;
                        //                        productInStock.NumberOfOutput = lstProductInStock.Last<ProductInStock>().NumberOfOutput - original.NumberUnit + od.NumberUnit;
                        //                        productInStock.NumberOfItem = lstProductInStock.Last<ProductInStock>().NumberOfItem + original.NumberUnit - od.NumberUnit;
                        //                        productInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_OUTPUT;
                        //                        pis.UpdateProductInStock(productInStock);

                        //                    }

                        //                    //Sua chi tiet phieu
                        //                    else if (od.ProductId != original.ProductId || od.AttributeId != original.AttributeId)
                        //                    {
                        //                        //Tao moi
                        //                        List<ProductInStock> lstNewProduct = pis.SelectProductByWhere(pt => pt.ProductId == od.ProductId && pt.AttributeId == od.AttributeId);
                        //                        ProductInStock newProductInStock = new ProductInStock();
                        //                        newProductInStock.AttributeId = od.AttributeId;
                        //                        newProductInStock.ProductId = od.ProductId;
                        //                        newProductInStock.LatestUpdate = DateTime.Now;
                        //                        newProductInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_OUTPUT;

                        //                        newProductInStock.NumberOfInput = lstNewProduct.Last<ProductInStock>().NumberOfInput;
                        //                        newProductInStock.NumberOfOutput = lstNewProduct.Last<ProductInStock>().NumberOfOutput + od.NumberUnit;
                        //                        newProductInStock.NumberOfItem += lstNewProduct.Last<ProductInStock>().NumberOfItem - od.NumberUnit;

                        //                        pis.AddProductInStock(newProductInStock);

                        //                        //Xoa cu
                        //                        List<ProductInStock> lstOldProduct = pis.SelectProductByWhere(pt => pt.ProductId == original.ProductId && pt.AttributeId == original.AttributeId);
                        //                        ProductInStock oldProductInStock = new ProductInStock();
                        //                        oldProductInStock.AttributeId = original.AttributeId;
                        //                        oldProductInStock.ProductId = original.ProductId;
                        //                        oldProductInStock.LatestUpdate = DateTime.Now;
                        //                        oldProductInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_EDIT;

                        //                        newProductInStock.NumberOfInput = lstOldProduct.Last<ProductInStock>().NumberOfInput;
                        //                        newProductInStock.NumberOfOutput = lstOldProduct.Last<ProductInStock>().NumberOfOutput - original.NumberUnit;
                        //                        newProductInStock.NumberOfItem += lstOldProduct.Last<ProductInStock>().NumberOfItem + original.NumberUnit;

                        //                        pis.AddProductInStock(newProductInStock);
                        //                    }
                        //                }

                        //            }

                        //            if (!result)
                        //                break;
                        //        }
                        //    }
                        //    if (!result)
                        //    {
                        //        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                        //        return false;
                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show("Phiếu bán hàng đã được cập nhật thành công");
                        //    }
                        //    if (this.CallFromUserControll != null && this.CallFromUserControll is OrderList)
                        //    {
                        //        ((OrderList)this.CallFromUserControll).loadOrderList();
                        //    }

                        //    disableForm();
                        //}

                        #endregion
                    }
                    else//add new
                    {
                        if (cbxCustomer.SelectedValue == null)
                        {
                            MessageBox.Show("Bạn cần có một khách hàng cho phiếu này!");
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
                            CreateBy = userId,
                            Reason = txtReason.Text,
                            WareHouse = txtWare.Text,
                            Total = totalWithTax
                        };
                        OrderService orderService = new OrderService();
                        bool result = orderService.AddOrder(order);
                        long newOrderId = BaoHienRepository.GetMaxId<Order>();
                        OrderDetailService orderDetailService = new OrderDetailService();
                        foreach (OrderDetail od in orderDetails)
                        {
                            if (od.ProductId > 0 && od.AttributeId > 0)
                            {
                                od.OrderId = (int)newOrderId;
                                bool ret = orderDetailService.AddOrderDetail(od);
                                totalCommission += od.Commission;

                                //Save in Production Log
                                ProductLog pl = productLogService.GetNewestProductUnitLog(od.ProductId, od.AttributeId, od.UnitId);
                                ProductLog plg = new ProductLog
                                {
                                    AttributeId = od.AttributeId,
                                    ProductId = od.ProductId,
                                    UnitId = od.UnitId,
                                    RecordCode = order.OrderCode,
                                    BeforeNumber = pl.AfterNumber,
                                    Amount = od.NumberUnit,
                                    AfterNumber = pl.AfterNumber - od.NumberUnit,
                                    CreatedDate = DateTime.Now
                                };
                                ret = productLogService.AddProductLog(plg);
                                if (!ret)
                                {
                                    MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                                    return false;
                                }
                            }
                        }

                        CustomerLogService cls = new CustomerLogService();
                        CustomerLog newest = cls.GetNewestCustomerLog(order.CustId);
                        double beforeDebit = 0.0;
                        if (newest != null)
                        {
                            beforeDebit = newest.AfterDebit;
                        }
                        CustomerLog cl = new CustomerLog
                        {
                            CustomerId = order.CustId,
                            RecordCode = order.OrderCode,
                            BeforeDebit = beforeDebit,
                            Amount = totalWithTax,
                            AfterDebit = beforeDebit + totalWithTax,
                            CreatedDate = DateTime.Now
                        };
                        result = cls.AddCustomerLog(cl);

                        int salerId = (int)order.Customer.SalerId;
                        if(salerId > 0)
                        {
                            EmployeeLogService els = new EmployeeLogService();
                            EmployeeLog el = els.GetNewestEmployeeLog(order.CreateBy);
                            EmployeeLog newel = new EmployeeLog
                            {
                                EmployeeId = salerId,
                                RecordCode = order.OrderCode,
                                BeforeNumber = el.AfterNumber,
                                Amount = totalCommission,
                                AfterNumber = el.AfterNumber + totalCommission,
                                CreatedDate = DateTime.Now
                            };
                            result = els.AddEmployeeLog(newel);                            
                        }

                        if (result)
                        {
                            MessageBox.Show("Phiếu bán hàng đã được tạo thành công");
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
            ProductAttributeService productAttrService = new ProductAttributeService();
            productAttrs = new BindingList<ProductAttributeModel>(productAttrService.GetProductAndAttribute());
            units = new BindingList<MeasurementUnit>(unitService.GetMeasurementUnits());
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

                txtVAT.Text = Global.formatVNDCurrencyText(txtVAT.Text);
                txtDiscount.Text = Global.formatVNDCurrencyText(txtDiscount.Text);
                txtWare.Text = order.WareHouse;
                txtReason.Text = order.Reason;
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
                ProductAttributeService productAttrService = new ProductAttributeService();
                for (int i = 0; i < orderDetails.Count; i++)
                {
                    if (dgwOrderDetails.Rows[i].Cells[ProductAttrCell] is DataGridViewComboBoxCell)
                    {
                        DataGridViewComboBoxCell pkgBoxCell = (DataGridViewComboBoxCell)dgwOrderDetails.Rows[i].Cells[ProductAttrCell];
                        try
                        {
                            pkgBoxCell.Value = productAttrService.GetProductAttribute(orderDetails[i].ProductId, orderDetails[i].AttributeId).Id;
                        }
                        catch { }
                    }
                    // Unit
                    if (dgwOrderDetails.Rows[i].Cells[UnitCell] is DataGridViewComboBoxCell)
                    {
                        DataGridViewComboBoxCell pkgBoxCell = (DataGridViewComboBoxCell)dgwOrderDetails.Rows[i].Cells[UnitCell];
                        try
                        {
                            pkgBoxCell.Value = orderDetails[i].UnitId;
                        }
                        catch { }
                    }
                }
            }            
        }

        public void loadDataForEditOrder(int orderId)
        {
            disableForm();
            isUpdating = true;
            this.Text = "Chỉnh sửa đơn hàng này";
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
            unitService = new MeasurementUnitService();
            productLogService = new ProductLogService();
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
                            Total = (double)orderDetail.Price * orderDetail.NumberUnit,
                            Commission = orderDetail.Commission,
                        };

            originalProductions = new BindingList<ProductionRequestDetailModel>(query.ToList());
            dgwOrderDetails.DataSource = new BindingList<ProductionRequestDetailModel>(query.ToList());
            if(btnSave.Enabled)
                dgwOrderDetails.ReadOnly = false;
            else
                dgwOrderDetails.ReadOnly = true;

            DataGridViewComboBoxColumn productColumn = new DataGridViewComboBoxColumn();
            productColumn.Width = 200;
            productColumn.AutoComplete = false;
            productColumn.HeaderText = "Sản phẩm";
            productColumn.DataSource = productAttrs;
            productColumn.DisplayMember = "ProductAttribute";
            //productColumn.Frozen = true;
            productColumn.ValueMember = "Id";
            dgwOrderDetails.Columns.Add(productColumn);
            
            DataGridViewTextBoxColumn numberUnitColumn = new DataGridViewTextBoxColumn();
            numberUnitColumn.Width = 100;
            numberUnitColumn.DataPropertyName = "NumberUnit";
            numberUnitColumn.HeaderText = "Số lượng";
            //numberUnitColumn.Frozen = true;
            //numberUnitColumn.ValueType = typeof(int);
            dgwOrderDetails.Columns.Add(numberUnitColumn);

            DataGridViewComboBoxColumn unitColumn = new DataGridViewComboBoxColumn();
            unitColumn.Width = 140;
            productColumn.AutoComplete = false;
            unitColumn.HeaderText = "Đơn vị tính";
            unitColumn.DataSource = units;
            unitColumn.DisplayMember = "Name";
            unitColumn.ValueMember = "Id";
            dgwOrderDetails.Columns.Add(unitColumn);

            DataGridViewTextBoxColumn priceColumn = new DataGridViewTextBoxColumn();
            priceColumn.Width = 100;
            priceColumn.DataPropertyName = "Price";
            priceColumn.HeaderText = "Giá";
            //numberUnitColumn.Frozen = true;
            //priceColumn.ValueType = typeof(int);
            dgwOrderDetails.Columns.Add(priceColumn);

            DataGridViewTextBoxColumn commissionColumn = new DataGridViewTextBoxColumn();
            commissionColumn.Width = 100;
            commissionColumn.DataPropertyName = "Commission";
            commissionColumn.HeaderText = "Hoa hồng";
            //commissionColumn.ValueType = typeof(int);
            dgwOrderDetails.Columns.Add(commissionColumn);

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
            noteColumn.Width = 150;
            noteColumn.HeaderText = "Ghi chú";
            //numberUnitColumn.Frozen = true;
            noteColumn.ValueType = typeof(string);
            dgwOrderDetails.Columns.Add(noteColumn);
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
                switch (e.ColumnIndex)
                {
                    case ProductAttrCell:
                        {
                            ProductAttributeService productAttrService = new ProductAttributeService();
                            ProductAttribute pa = productAttrService.GetProductAttribute((int)dgv.CurrentCell.Value);
                            if (pa != null)
                            {
                                orderDetails[e.RowIndex].ProductId = pa.ProductId;
                                orderDetails[e.RowIndex].AttributeId = pa.AttributeId;
                            }
                        } break;
                    case PriceCell:
                        {
                            orderDetails[e.RowIndex].Price = (double)dgv.CurrentCell.Value;
                        } break;
                    case CommissionCell:
                        {
                            orderDetails[e.RowIndex].Commission = double.Parse(dgv.CurrentCell.Value.ToString());//(double)dgv.CurrentCell.Value;
                        } break;
                    case NoteCell:
                        {
                            orderDetails[e.RowIndex].Note = (string)dgv.CurrentCell.Value;
                        } break;
                    default:
                        break;
                }
            }
            calculateNumberUnit(dgv, e.RowIndex, e.ColumnIndex);
            calculateTotal();
            orderDetails[e.RowIndex].Cost = (int)dgwOrderDetails.Rows[e.RowIndex].Cells[NumberUnitCell].Value
                * (double)dgwOrderDetails.Rows[e.RowIndex].Cells[PriceCell].Value;
        }

        private void calculateNumberUnit(DataGridView dgv, int rowIndex, int colIndex)
        {
            if (dgv.Rows[rowIndex].Cells[UnitCell].Value != null &&
                dgv.Rows[rowIndex].Cells[NumberUnitCell].Value != null &&
                (colIndex == UnitCell || colIndex == NumberUnitCell || colIndex == ProductAttrCell))
            {
                ProductLog pl = productLogService.GetNewestProductUnitLog(
                    orderDetails[rowIndex].ProductId,
                    orderDetails[rowIndex].AttributeId,
                    (int)dgv.Rows[rowIndex].Cells[UnitCell].Value);
                if (pl.Id == 0)
                {
                    MessageBox.Show("Sản phẩm với đơn vị tính này hiện chưa có trong kho.");
                    dgv.Rows[rowIndex].Cells[NumberUnitCell].Value = 0;
                    orderDetails[rowIndex].NumberUnit = 0;
                }
                else
                {
                    if (pl.AfterNumber <= 0)
                    {
                        MessageBox.Show("Số lượng sản phẩm trong kho đã hết.");
                        dgv.Rows[rowIndex].Cells[NumberUnitCell].Value = 0;
                        orderDetails[rowIndex].NumberUnit = 0;
                    }
                    else if (pl.AfterNumber < (int)dgv.Rows[rowIndex].Cells[NumberUnitCell].Value)
                    {
                        MessageBox.Show("Số lượng sản phẩm trong kho còn lại là : " + pl.AfterNumber);
                        dgv.Rows[rowIndex].Cells[NumberUnitCell].Value = pl.AfterNumber;
                        orderDetails[rowIndex].NumberUnit = (int)pl.AfterNumber;
                    }
                    else
                    {
                        orderDetails[rowIndex].UnitId = (int)dgv.Rows[rowIndex].Cells[UnitCell].Value;
                        orderDetails[rowIndex].NumberUnit = (int)dgv.Rows[rowIndex].Cells[NumberUnitCell].Value;
                    }
                }
            }
        }

        private void calculateTotal()
        {
            double totalNoTax = 0.0;
            totalWithTax = 0.0;
            bool hasValue = false;

            for (int i = 0; i < orderDetails.Count; i++)
            {
                if (orderDetails[i].ProductId != 0 && orderDetails[i].AttributeId != 0 && orderDetails[i].UnitId != 0)
                {
                    // Calculate total at Column 1 w 3 = 5
                    dgwOrderDetails.Rows[i].Cells[TotalCell].Value = (int)dgwOrderDetails.Rows[i].Cells[NumberUnitCell].Value
                        * (double)dgwOrderDetails.Rows[i].Cells[PriceCell].Value;
                    hasValue = true;
                    totalNoTax += (double)dgwOrderDetails.Rows[i].Cells[TotalCell].Value;
                }
            }
            if (hasValue)
            {
                double vat = 0.0;
                double discount = 0.0;
                string tVAT = (order != null && order.VAT != null) ? order.VAT.ToString() : 
                    string.IsNullOrEmpty(txtVAT.WorkingText) ? txtVAT.Text : txtVAT.WorkingText;
                string tDiscount = (order != null && order.Discount != null) ? order.Discount.ToString() : 
                    string.IsNullOrEmpty(txtDiscount.WorkingText) ? txtDiscount.Text : txtDiscount.WorkingText;
                double.TryParse(tVAT, out vat);
                double.TryParse(tDiscount, out discount);
                totalWithTax = totalNoTax + vat - discount;
                lblSubTotal.Text = Global.formatVNDCurrencyText(totalNoTax.ToString());
                lblGrantTotal.Text = Global.formatVNDCurrencyText(totalWithTax.ToString());
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
            switch (dgwOrderDetails.CurrentCell.ColumnIndex)
            {
                case ProductAttrCell:
                    {
                        var source = new AutoCompleteStringCollection();
                        String[] stringArray = Array.ConvertAll<ProductAttributeModel, String>(productAttrs.ToArray(), delegate(ProductAttributeModel row) { return (String)row.ProductAttribute; });
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
                    } break;
                case NumberUnitCell:
                    {
                        TextBox numberOfUnit = e.Control as TextBox;
                        this.validator1.SetRegularExpression(numberOfUnit, BHConstant.REGULAR_EXPRESSION_FOR_NUMBER);
                        this.validator1.SetType(numberOfUnit, Itboy.Components.ValidationType.RegularExpression);
                    } break;
                case UnitCell:
                    {
                        var source = new AutoCompleteStringCollection();
                        String[] stringArray = Array.ConvertAll<MeasurementUnit, String>(units.ToArray(), delegate(MeasurementUnit row) { return (String)row.Name; });
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
                    } break;
                case PriceCell:
                    {
                        TextBox price = e.Control as TextBox;
                        this.validator1.SetRegularExpression(price, BHConstant.REGULAR_EXPRESSION_FOR_CURRENCY);
                        this.validator1.SetType(price, Itboy.Components.ValidationType.RegularExpression);
                    } break;
                case CommissionCell:
                    {
                        TextBox commision = e.Control as TextBox;
                        this.validator1.SetRegularExpression(commision, BHConstant.REGULAR_EXPRESSION_FOR_CURRENCY);
                        this.validator1.SetType(commision, Itboy.Components.ValidationType.RegularExpression);
                    } break;
                case TotalCell:
                    {
                        TextBox total = e.Control as TextBox;
                        this.validator1.SetRegularExpression(total, BHConstant.REGULAR_EXPRESSION_FOR_CURRENCY);
                        this.validator1.SetType(total, Itboy.Components.ValidationType.RegularExpression);
                    } break;
                case NoteCell:
                    {
                        if (e.Control is TextBox)
                        {
                            TextBox other = e.Control as TextBox;
                            this.validator1.SetType(other, Itboy.Components.ValidationType.None);
                        }
                    } break;
            }
        }

        private void dgwOrderDetails_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        
        private void btnPrintOrder_Click(object sender, EventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            pd.PrinterSettings = new PrinterSettings();
            if (DialogResult.OK == pd.ShowDialog(this))
            {
                this.Cursor = Cursors.AppStarting;
                if (btnSave.Enabled)
                {
                    if (saveData())
                    {
                        Print(pd.PrinterSettings.PrinterName);
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Hệ thống xảy ra lỗi, vui lòng thử lại sau !");
                    }
                }
                else
                {
                    Print(pd.PrinterSettings.PrinterName);
                }
            }
        }
        
        private void printForStock()
        {
            Global.checkDirSaveFile();
            var doc = new PDF.Document();
            PdfWriter.GetInstance(doc, new FileStream(BHConstant.SAVE_IN_DIRECTORY + @"\XKho.pdf", FileMode.Create));
            doc.Open();

            doc.Add(FormatConfig.ParaRightBeforeHeader("Mã số phiếu : " + getOrderCode()));
            doc.Add(FormatConfig.ParaHeader("PHIẾU XUẤT KHO"));

            doc.Add(FormatConfig.ParaCommonInfo("Tên khách hàng : ", getCustomerName()));
            doc.Add(FormatConfig.ParaCommonInfo("Địa chỉ : ", getCustomerAddress()));
            doc.Add(FormatConfig.ParaCommonInfo("Điện thoại : ", getCustomerPhone()));
            doc.Add(FormatConfig.ParaCommonInfo("Lý do xuất kho : ", txtReason.Text));
            doc.Add(FormatConfig.ParaCommonInfo("Xuất tại kho : ", txtWare.Text));

            PDF.pdf.PdfPTable table = FormatConfig.Table(6, new float[] { 0.5f, 3.5f, 1.5f, 1f, 1f, 2.5f });
            table.AddCell(FormatConfig.TableCellHeader("STT"));
            table.AddCell(FormatConfig.TableCellHeader("Tên sản phẩm"));
            table.AddCell(FormatConfig.TableCellHeader("Mã sản phẩm"));
            table.AddCell(FormatConfig.TableCellHeader("Đơn vị"));
            table.AddCell(FormatConfig.TableCellHeader("Số lượng"));
            table.AddCell(FormatConfig.TableCellHeader("Ghi chú"));

            for (int i = 0; i < orderDetails.Count; i++)
            {
                if (orderDetails[i].ProductId != 0 && orderDetails[i].AttributeId != 0 && orderDetails[i].UnitId != 0)
                {
                    table.AddCell(FormatConfig.TableCellBody((i + 1).ToString(), PdfPCell.ALIGN_CENTER));
                    table.AddCell(FormatConfig.TableCellBody(
                                        getProductName(orderDetails[i].ProductId, orderDetails[i].AttributeId),
                                        PdfPCell.ALIGN_LEFT));
                    table.AddCell(FormatConfig.TableCellBody(
                                        getProductCode(orderDetails[i].ProductId),
                                        PdfPCell.ALIGN_LEFT));
                    table.AddCell(FormatConfig.TableCellBody(
                                        getUnitName(orderDetails[i].UnitId),
                                        PdfPCell.ALIGN_LEFT));
                    table.AddCell(FormatConfig.TableCellBody(
                                        orderDetails[i].NumberUnit.ToString(),
                                        PdfPCell.ALIGN_CENTER));
                    table.AddCell(FormatConfig.TableCellBody(
                                        string.IsNullOrEmpty(orderDetails[i].Note) ? "" : orderDetails[i].Note.ToString(),
                                        PdfPCell.ALIGN_LEFT));
                }
            }

            doc.Add(table);

            doc.Add(FormatConfig.ParaCommonInfo("Ghi chú : ", string.IsNullOrEmpty(txtNote.Text) ?
                String.Concat(Enumerable.Repeat("...", 96)) :  txtNote.Text));

            DateTime date = DateTime.Now;
            doc.Add(FormatConfig.ParaRightBeforeHeader(string.Format("Ngày {0} tháng {1} năm {2}", date.Day, date.Month, date.Year)));
            PDF.pdf.PdfPTable table2 = FormatConfig.Table(4, new float[] { 2.5f, 2f, 2f, 3.5f });
            table2.AddCell(FormatConfig.TableCellHeaderCommon("NGƯỜI LẬP PHIẾU", PdfPCell.NO_BORDER));
            table2.AddCell(FormatConfig.TableCellHeaderCommon("NGƯỜI NHẬN", PdfPCell.NO_BORDER));
            table2.AddCell(FormatConfig.TableCellHeaderCommon("NGƯỜI GIAO", PdfPCell.NO_BORDER));
            table2.AddCell(FormatConfig.TableCellHeaderCommon("THỦ TRƯỞNG ĐƠN VỊ", PdfPCell.NO_BORDER));
            table2.AddCell(FormatConfig.TableCellBodyCustom("(Ký, họ tên)", PdfPCell.ALIGN_CENTER, FontConfig.SmallItalicFont));
            table2.AddCell(FormatConfig.TableCellBodyCustom("(Ký, họ tên)", PdfPCell.ALIGN_CENTER, FontConfig.SmallItalicFont));
            table2.AddCell(FormatConfig.TableCellBodyCustom("(Ký, họ tên)", PdfPCell.ALIGN_CENTER, FontConfig.SmallItalicFont));
            table2.AddCell(FormatConfig.TableCellBodyCustom("(Ký, họ tên, đóng dấu)", PdfPCell.ALIGN_CENTER, FontConfig.ItalicFont));
            doc.Add(table2);

            doc.Close();
        }

        private void printOrder()
        {
            Global.checkDirSaveFile();
            var doc = new PDF.Document();
            PdfWriter.GetInstance(doc, new FileStream(BHConstant.SAVE_IN_DIRECTORY + @"\BHang.pdf", FileMode.Create));
            doc.Open();

            doc.Add(FormatConfig.ParaRightBeforeHeader("Mã số phiếu : " + getOrderCode()));
            doc.Add(FormatConfig.ParaHeader("PHIẾU XUẤT KHO"));
            doc.Add(FormatConfig.ParaRightBelowHeader("(SẼ BỔ SUNG HÓA ĐƠN TÀI CHÍNH VÀ THU THÊM THUẾ GTGT 10% SAU)"));

            doc.Add(FormatConfig.ParaCommonInfo("Tên khách hàng : ", getCustomerName()));
            doc.Add(FormatConfig.ParaCommonInfo("Địa chỉ : ", getCustomerAddress()));
            doc.Add(FormatConfig.ParaCommonInfo("Điện thoại : ", getCustomerPhone()));
            doc.Add(FormatConfig.ParaCommonInfo("Lý do xuất kho : ", txtReason.Text));
            doc.Add(FormatConfig.ParaCommonInfo("Xuất tại kho : ", txtWare.Text));

            PDF.pdf.PdfPTable table = FormatConfig.Table(7, new float[] { 0.5f, 3f, 1.5f, 1f, 1f, 1.5f, 1.5f });
            table.AddCell(FormatConfig.TableCellHeader("STT"));
            table.AddCell(FormatConfig.TableCellHeader("Tên sản phẩm"));
            table.AddCell(FormatConfig.TableCellHeader("Mã sản phẩm"));
            table.AddCell(FormatConfig.TableCellHeader("Đơn vị"));
            table.AddCell(FormatConfig.TableCellHeader("Số lượng"));
            table.AddCell(FormatConfig.TableCellHeader("Giá (VND)"));
            table.AddCell(FormatConfig.TableCellHeader("Thành tiền (VND)"));

            for (int i = 0; i < orderDetails.Count; i++)
            {
                if (orderDetails[i].ProductId != 0 && orderDetails[i].AttributeId != 0)
                {
                    table.AddCell(FormatConfig.TableCellBody((i + 1).ToString(), PdfPCell.ALIGN_CENTER));
                    table.AddCell(FormatConfig.TableCellBody(
                                        getProductName(orderDetails[i].ProductId, orderDetails[i].AttributeId),
                                        PdfPCell.ALIGN_LEFT));
                    table.AddCell(FormatConfig.TableCellBody(
                                        getProductCode(orderDetails[i].ProductId),
                                        PdfPCell.ALIGN_LEFT));
                    table.AddCell(FormatConfig.TableCellBody(
                                        getUnitName(orderDetails[i].UnitId),
                                        PdfPCell.ALIGN_LEFT));
                    table.AddCell(FormatConfig.TableCellBody(
                                        orderDetails[i].NumberUnit.ToString(),
                                        PdfPCell.ALIGN_CENTER));
                    table.AddCell(FormatConfig.TableCellBody(
                                        Global.convertToCurrency(orderDetails[i].Price.ToString()),
                                        PdfPCell.ALIGN_RIGHT));
                    table.AddCell(FormatConfig.TableCellBody(
                                        Global.convertToCurrency(orderDetails[i].Cost.ToString()),
                                        PdfPCell.ALIGN_RIGHT));
                }
            }

            table.AddCell(FormatConfig.TableCellBoldBody("VAT", PdfPCell.ALIGN_RIGHT, 6));
            table.AddCell(FormatConfig.TableCellBody(
                Global.convertToCurrency(txtVAT.WorkingText),
                PdfPCell.ALIGN_RIGHT));

            table.AddCell(FormatConfig.TableCellBoldBody("Khấu chi", PdfPCell.ALIGN_RIGHT, 6));
            table.AddCell(FormatConfig.TableCellBody(
                Global.convertToCurrency(txtDiscount.WorkingText),
                PdfPCell.ALIGN_RIGHT));

            table.AddCell(FormatConfig.TableCellBoldBody("Tổng", PdfPCell.ALIGN_RIGHT, 6));
            table.AddCell(FormatConfig.TableCellBody(
                Global.convertToCurrency(totalWithTax.ToString()),
                PdfPCell.ALIGN_RIGHT));

            doc.Add(table);

            doc.Add(FormatConfig.ParaCommonInfo("Cộng thành tiền (viết bằng chữ) : ", Global.convertCurrencyToText(totalWithTax.ToString())));

            doc.Add(FormatConfig.ParaCommonInfo("Ghi chú : ", string.IsNullOrEmpty(txtNote.Text) ?
                String.Concat(Enumerable.Repeat("...", 96)) : txtNote.Text));

            DateTime date = DateTime.Now;
            doc.Add(FormatConfig.ParaRightBeforeHeader(string.Format("Ngày {0} tháng {1} năm {2}", date.Day, date.Month, date.Year)));
            PDF.pdf.PdfPTable table2 = FormatConfig.Table(4, new float[] { 2.5f, 2f, 2f, 3.5f });
            table2.AddCell(FormatConfig.TableCellHeaderCommon("NGƯỜI LẬP PHIẾU", PdfPCell.NO_BORDER));
            table2.AddCell(FormatConfig.TableCellHeaderCommon("NGƯỜI NHẬN", PdfPCell.NO_BORDER));
            table2.AddCell(FormatConfig.TableCellHeaderCommon("NGƯỜI GIAO", PdfPCell.NO_BORDER));
            table2.AddCell(FormatConfig.TableCellHeaderCommon("THỦ TRƯỞNG ĐƠN VỊ", PdfPCell.NO_BORDER));
            table2.AddCell(FormatConfig.TableCellBodyCustom("(Ký, họ tên)", PdfPCell.ALIGN_CENTER, FontConfig.SmallItalicFont));
            table2.AddCell(FormatConfig.TableCellBodyCustom("(Ký, họ tên)", PdfPCell.ALIGN_CENTER, FontConfig.SmallItalicFont));
            table2.AddCell(FormatConfig.TableCellBodyCustom("(Ký, họ tên)", PdfPCell.ALIGN_CENTER, FontConfig.SmallItalicFont));
            table2.AddCell(FormatConfig.TableCellBodyCustom("(Ký, họ tên, đóng dấu)", PdfPCell.ALIGN_CENTER, FontConfig.ItalicFont));
            doc.Add(table2);

            doc.Close();
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
            txtWare.Enabled = false;
            txtReason.Enabled = false;
        }
        
        private void txtDiscount_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDiscount.Text))
            {
                calculateTotal();
            }
        }

        private string getOrderCode()
        {
            return txtOrderCode.Text;
        }

        private string getCustomerName()
        {
            string result = string.Empty;
            if (cbxCustomer.SelectedValue != null)
                result = customers.Where(cus => cus.Id == (int)cbxCustomer.SelectedValue).FirstOrDefault().CustomerName;
            return result;
        }

        private string getCustomerAddress()
        {
            string result = string.Empty;
            if (cbxCustomer.SelectedValue != null)
                result = customers.Where(cus => cus.Id == (int)cbxCustomer.SelectedValue).FirstOrDefault().Address;
            return result;
        }

        private string getCustomerPhone()
        {
            string result = string.Empty;
            if (cbxCustomer.SelectedValue != null)
                result = customers.Where(cus => cus.Id == (int)cbxCustomer.SelectedValue).FirstOrDefault().Phone;
            return result;
        }

        private string getProductName(int productID, int attrID)
        { 
            string result = string.Empty;
            ProductAttributeModel pad = productAttrs.Where(p => p.ProductId == productID && p.AttributeId == attrID).FirstOrDefault();
            if (pad != null)
                result = pad.ProductAttribute;
            return result;
        }

        private string getProductCode(int productID)
        {
            string result = string.Empty;
            ProductService productService = new ProductService();
            Product p = productService.GetProduct(productID);
            if (p != null)
                result = p.ProductCode;
            return result;
        }

        private string getUnitName(int unitID)
        {
            string result = string.Empty;
            MeasurementUnit u = units.Where(un => un.Id == unitID).FirstOrDefault();
            if (u != null)
                result = u.Name;
            return result;
        }

        private void txtVAT_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtVAT.Text))
            {
                calculateTotal();
            }
        }

        private void Print(string printerName)
        {
            printOrder();
            printForStock();
            // Print the file to the printer.
            var adobe = Registry.LocalMachine.OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Windows").OpenSubKey("CurrentVersion").OpenSubKey("App Paths").OpenSubKey("AcroRd32.exe");
            var path = adobe.GetValue("");
            string file1 = BHConstant.SAVE_IN_DIRECTORY + @"\BHang.pdf";
            string file2 = BHConstant.SAVE_IN_DIRECTORY + @"\XKho.pdf";
            string parameter1 = String.Format(@"/n /t ""{0}"" ""{1}""", file1, printerName);
            string parameter2 = String.Format(@"/n /t ""{0}"" ""{1}""", file2, printerName);
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo(path.ToString(), parameter1);
                processStartInfo.CreateNoWindow = true;
                processStartInfo.UseShellExecute = false;
                Process process = Process.Start(processStartInfo);
                process.WaitForInputIdle(20 * 1000);
                process.Kill();

                processStartInfo = new ProcessStartInfo(path.ToString(), parameter2);
                processStartInfo.CreateNoWindow = true;
                processStartInfo.UseShellExecute = false;
                process = Process.Start(processStartInfo);
                process.WaitForInputIdle(20 * 1000);
                process.Kill();
            }
            catch
            {
                MessageBox.Show("Không thể kết nối máy in!");
            }
            this.Cursor = Cursors.Default;
        }
    }
}
