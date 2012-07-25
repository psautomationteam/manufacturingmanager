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

namespace BaoHien.UI
{
    public partial class AddOrder : BaseForm
    {
        Order order;
        List<Customer> customers;
        List<OrderDetail> orderDetails;
        List<Product> products;
        List<BaseAttribute> baseAttributesAtRow;
        public AddOrder()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
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
            if(Global.CurrentUser != null)
            {
                userId = Global.CurrentUser.Id;
            }else{
                MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                return;
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
                    return;
                }
                else
                {
                    MessageBox.Show("Sản phẩm đã được cập nhật thành công");
                    ((OrderList)this.CallFromUserControll).loadOrderList();
                    this.Close();
                }
            }
            else//add new
            {
                if (cbxCustomer.SelectedValue == null)
                {
                    MessageBox.Show("Bạn cần có một khách hành cho phiếu này!");
                    return;
                }
                order = new Order
                {
                    CustId = cbxCustomer.SelectedValue != null?(int)cbxCustomer.SelectedValue: 0,
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
                        od.Id = (int)newOrderId;
                        bool ret = orderDetailService.AddOrderDetail(od);
                        if (!ret)
                        {
                            MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                            return;
                        }
                    }
                }

            //    foreach (DataGridViewRow dgv in rows)
            //{

            //    ProductTypeService productTypeService = new ProductTypeService();
            //    int id = ObjectHelper.GetValueFromAnonymousType<int>(dgv.DataBoundItem, "Id");
            //    ProductService productService = new ProductService();
            //    List<Product> productList = productService.SelectProductByWhere(pt => pt.ProductType == id);
            //    bool deleteAllProductForThisType = true;
            //    foreach (Product p in productList)
            //    {
            //        if (!productService.DeleteProduct(p.Id))
            //        {
            //            deleteAllProductForThisType = false;
            //            MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
            //            break;
            //        }
            //    }
            //    if (!deleteAllProductForThisType || !productTypeService.DeleteProductType(id))
            //    {
            //        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
            //        break;
            //    }

            //}
                if (result)
                {
                    MessageBox.Show("Sản phẩm đã được tạo thành công");
                    //((OrderList)this.CallFromUserControll).loadOrderList();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                }
            }
        }
        private void loadSomeData()
        {
            if(customers == null)
            {
                CustomerService customerService = new CustomerService();
                customers = customerService.GetCustomers();
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
            if (orderDetails == null && order != null)
            {
                OrderDetailService orderService = new OrderDetailService();

                orderDetails = orderService.SelectOrderDetailByWhere(o => o.Id == order.Id);
            }
        }
        public void loadDataForEditOrder(int orderId)
        {
            this.Text = "Chỉnh sửa  đơn hàng này";
            this.btnSave.Text = "Cập nhật";
            

            OrderService orderService = new OrderService();

            order = orderService.GetOrder(orderId);
            loadSomeData();
            if (order != null)
            {
                if(customers != null)
                {
                    cbxCustomer.SelectedIndex = customers.FindIndex(cus => cus.Id == order.CustId);
                }
                txtDiscount.Text = order.Discount.HasValue?order.Discount.Value.ToString(): "";
                txtNote.Text = order.Note;
                txtVAT.Text = order.VAT.HasValue?order.VAT.Value.ToString():"";
                txtOrderCode.Text= order.OrderCode;
                txtCreatedDate.Text = order.CreatedDate.ToShortDateString();  
                    
            }
        }
        private void AddOrder_Load(object sender, EventArgs e)
        {
           
            SetupColumns();
            loadSomeData();
            calculateTotal();
        }
        private void SetupColumns()
        {
            bool isUpdating = false;
            dgwOrderDetails.AutoGenerateColumns = false;
            
            ProductService productService = new ProductService();
            if (products == null)
            {
                products = productService.GetProducts();
            }
            
            List<object> objects = new List<object>
            {
                new {
                    Product = "", 
                    AttributeName = "",
                    NumberUnit = 0,
                    Price = 0.0,
                    Tax = 0.0,
                    Note = "",
                    DeleteButton = ""
                }
            };
            if (orderDetails == null)
            {
                orderDetails = new List<OrderDetail>();
                OrderDetail orderDetail = new OrderDetail();
                orderDetails.Add(orderDetail);

                dgwOrderDetails.ReadOnly = false;
                dgwOrderDetails.AllowUserToAddRows = true;
                dgwOrderDetails.EditMode = DataGridViewEditMode.EditOnEnter;
            }
            else
            {
                
                var query = from orderDetail in orderDetails

                            select new
                            {
                                ProductName = orderDetail.Product.ProductName,
                                AttributeName = orderDetail.BaseAttribute != null ? orderDetail.BaseAttribute.AttributeName : "",
                                NumberUnit = orderDetail.NumberUnit,
                                Price = orderDetail.Price,
                                Note = orderDetail.Note,
                                Total = (double)orderDetail.Price * orderDetail.NumberUnit
                            };
                dgwOrderDetails.DataSource = query.ToList();
                isUpdating = true;
            }
            
           // dgwOrderDetails.DataSource = query.ToList();
            //dgwOrderDetails.DataSource = objects;
            if (isUpdating)
            {
                DataGridViewTextBoxColumn productColumn = new DataGridViewTextBoxColumn();
                productColumn.Width = 150;
                productColumn.DataPropertyName = "ProductName";

                productColumn.HeaderText = "Sản phẩm";


                dgwOrderDetails.Columns.Add(productColumn);
            }
            else
            {
                /*
                DataGridViewTextBoxColumn productColumn = new DataGridViewTextBoxColumn();
                productColumn.Width = 150;
                productColumn.DataPropertyName = "ProductName";

                productColumn.HeaderText = "Sản phẩm";


                dgwOrderDetails.Columns.Add(productColumn);*/
                
                DataGridViewComboBoxColumn productColumn = new DataGridViewComboBoxColumn();
                productColumn.Width = 150;
                productColumn.AutoComplete = false;
                
                productColumn.HeaderText = "Sản phẩm";
                productColumn.DataSource = products;
                productColumn.DisplayMember = "ProductName";
                //productColumn.Frozen = true;
                productColumn.ValueMember = "Id";

                dgwOrderDetails.Columns.Add(productColumn);
            }
            if (isUpdating)
            {

                DataGridViewTextBoxColumn productAttributeColumn = new DataGridViewTextBoxColumn();

                productAttributeColumn.Width = 150;
                productAttributeColumn.DataPropertyName = "AttributeName";

                productAttributeColumn.HeaderText = "Thuộc tính sản phẩm";


                dgwOrderDetails.Columns.Add(productAttributeColumn);
            }
            else
            {
                List<BaseAttribute> baseAttributes = new List<BaseAttribute>();
                DataGridViewComboBoxColumn productAttributeColumn = new DataGridViewComboBoxColumn();
                productAttributeColumn.Width = 150;
                productAttributeColumn.HeaderText = "Thuộc tính sản phẩm";

                productAttributeColumn.DataSource = baseAttributes;

                productAttributeColumn.DisplayMember = "AttributeName";
                //productColumn.Frozen = true;
                productAttributeColumn.ValueMember = "Id";

                dgwOrderDetails.Columns.Add(productAttributeColumn);
            }

            

            DataGridViewTextBoxColumn numberUnitColumn = new DataGridViewTextBoxColumn();
            
            numberUnitColumn.Width = 100;
            if (isUpdating)
            {
                numberUnitColumn.DataPropertyName = "NumberUnit";
            }
            //numberUnitColumn.DataPropertyName = "NumberUnit";
            numberUnitColumn.HeaderText = "Số lượng";
            //numberUnitColumn.Frozen = true;
            numberUnitColumn.ValueType = typeof(int);
            dgwOrderDetails.Columns.Add(numberUnitColumn);

            DataGridViewTextBoxColumn priceColumn = new DataGridViewTextBoxColumn();
            //priceColumn.DataPropertyName = "Price";
            if (isUpdating)
            {
                priceColumn.DataPropertyName = "Price";
            }
            priceColumn.Width = 100;
            priceColumn.HeaderText = "Giá";
           // numberUnitColumn.Frozen = true;
            priceColumn.ValueType = typeof(double);
            dgwOrderDetails.Columns.Add(priceColumn);

            DataGridViewTextBoxColumn totalColumn = new DataGridViewTextBoxColumn();
            //priceColumn.DataPropertyName = "Price";
            if (isUpdating)
            {
                totalColumn.DataPropertyName = "Total";
            }
            totalColumn.Width = 100;
            totalColumn.HeaderText = "Thành Tiền";
            // numberUnitColumn.Frozen = true;
            totalColumn.ValueType = typeof(double);
            dgwOrderDetails.Columns.Add(totalColumn);

            DataGridViewTextBoxColumn noteColumn = new DataGridViewTextBoxColumn();
            if (isUpdating)
            {
                noteColumn.DataPropertyName = "Note";
            }
            //noteColumn.DataPropertyName = "Note";
            noteColumn.Width = 100;
            noteColumn.HeaderText = "Ghi chú";
            //numberUnitColumn.Frozen = true;
            noteColumn.ValueType = typeof(string);
            dgwOrderDetails.Columns.Add(noteColumn);

            //DataGridViewImageColumn deleteButton = new DataGridViewImageColumn();
            //deleteButton.DataPropertyName = "DeleteButton";
            //deleteButton.Image = Properties.Resources.erase;
            //deleteButton.Width = 100;
            //deleteButton.HeaderText = "Xóa";
            //deleteButton.ReadOnly = true;
            //deleteButton.ImageLayout = DataGridViewImageCellLayout.Normal;



            //dgwOrderDetails.Columns.Add(deleteButton);
        }

        private void dgwOrderDetails_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Có lỗi nhập liệu xảy ra,vui lòng kiểm tra lại!");
        }

        private void dgwOrderDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (orderDetails == null)
            {
                orderDetails = new List<OrderDetail>();
            }
            if (orderDetails.Count < dgwOrderDetails.RowCount)
            {
                OrderDetail orderDetail = new OrderDetail
                {
                    //ProductId = (int)dgwOrderDetails.Rows[dgwOrderDetails.RowCount - 1].Cells[0].Value,
                    //NumberUnit = (int)dgwOrderDetails.Rows[dgwOrderDetails.RowCount - 1].Cells[1].Value,
                    //Price = (double)dgwOrderDetails.Rows[dgwOrderDetails.RowCount - 1].Cells[2].Value,
                    //Tax = (double)dgwOrderDetails.Rows[dgwOrderDetails.RowCount - 1].Cells[3].Value,
                    //Note = (string)dgwOrderDetails.Rows[dgwOrderDetails.RowCount - 1].Cells[4].Value,
                };
                orderDetails.Add(orderDetail);
            }
            if (dgv.CurrentCell.Value != null)
            {
                if (e.ColumnIndex == 0)
                {
                    
                    orderDetails[e.RowIndex].ProductId = (int)dgv.CurrentCell.Value;
                    ProductAttributeService productAttributeService = new ProductAttributeService();
                    List<ProductAttribute> productAttributes = productAttributeService.SelectProductAttributeByWhere(ba => ba.Id == orderDetails[e.RowIndex].ProductId);
                    baseAttributesAtRow = new List<BaseAttribute>();
                    foreach(ProductAttribute pa in productAttributes)
                    {
                        baseAttributesAtRow.Add(pa.BaseAttribute);
                    }
                    DataGridViewComboBoxCell currentCell = (DataGridViewComboBoxCell)dgwOrderDetails.Rows[e.RowIndex].Cells[1];
                    if (baseAttributesAtRow.Count > 0)
                    {
                        currentCell.DataSource = baseAttributesAtRow;
                        currentCell.Value = baseAttributesAtRow[0].Id;
                    }
                    
                    //currentCell.DisplayMember = "AttributeName";
                    //currentCell.ValueMember = "Id";
                    //currentCell.

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
                }
                
            }
            

        }

        private void dgwOrderDetails_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (orderDetails == null)
            {
                orderDetails = new List<OrderDetail>();
            }
            if (orderDetails.Count < dgwOrderDetails.RowCount)
            {
                OrderDetail orderDetail = new OrderDetail
                {
                    //ProductId = (int)dgwOrderDetails.Rows[dgwOrderDetails.RowCount - 1].Cells[0].Value,
                    //NumberUnit = (int)dgwOrderDetails.Rows[dgwOrderDetails.RowCount - 1].Cells[1].Value,
                    //Price = (double)dgwOrderDetails.Rows[dgwOrderDetails.RowCount - 1].Cells[2].Value,
                    //Tax = (double)dgwOrderDetails.Rows[dgwOrderDetails.RowCount - 1].Cells[3].Value,
                    //Note = (string)dgwOrderDetails.Rows[dgwOrderDetails.RowCount - 1].Cells[4].Value,
                };
                orderDetails.Add(orderDetail);
            }
            if (dgv.CurrentCell.Value != null)
            {
                if (e.ColumnIndex == 0)
                {

                    orderDetails[e.RowIndex].ProductId = (int)dgv.CurrentCell.Value;
                    ProductAttributeService productAttributeService = new ProductAttributeService();
                    List<ProductAttribute> productAttributes = productAttributeService.SelectProductAttributeByWhere(ba => ba.Id == orderDetails[e.RowIndex].ProductId);
                    List<BaseAttribute> baseAttributesAtRow = new List<BaseAttribute>();
                    foreach (ProductAttribute pa in productAttributes)
                    {
                        baseAttributesAtRow.Add(pa.BaseAttribute);
                    }
                    DataGridViewComboBoxCell currentCell = (DataGridViewComboBoxCell)dgwOrderDetails.Rows[e.RowIndex].Cells[1];
                    if (baseAttributesAtRow.Count > 0)
                    {
                        currentCell.DataSource = baseAttributesAtRow;
                        currentCell.Value = baseAttributesAtRow[0].Id;
                    }

                    //currentCell.DisplayMember = "AttributeName";
                    //currentCell.ValueMember = "Id";
                    //currentCell.

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

        
    }
}
