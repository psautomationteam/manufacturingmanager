using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using BaoHien.Services.Orders;
using BaoHien.Services.Customers;
using DAL.Helper;
using BaoHien.Services.SystemUsers;
using BaoHien.Model;
using BaoHien.Common;
using BaoHien.Services.ProductLogs;
using BaoHien.Services.Employees;

namespace BaoHien.UI
{
    public partial class OrderList : UserControl
    {
        List<Customer> customers;
        List<SystemUser> systemUsers;
        public OrderList()
        {
            InitializeComponent();
        }

        private void OrderList_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Today.AddDays(-DateTime.Now.Day + 1);
            dtpFrom.CustomFormat = BHConstant.DATE_FORMAT;
            dtpTo.CustomFormat = BHConstant.DATE_FORMAT;
            loadOrderList();
            SetupColumns();
        }

        public void loadOrderList()
        {
            loadSomeData();
            OrderService orderService = new OrderService();
            List<Order> orders = orderService.GetOrders();
            setUpDataGrid(orders);            
        }

        private void loadSomeData()
        {
            CustomerService customerService = new CustomerService();
            customers = customerService.GetCustomers();
            customers.Add(new Customer() { Id = 0, CustomerName = "Tất cả" });
            customers = customers.OrderBy(x => x.Id).ToList();

            cbmCustomers.DataSource = customers;
            cbmCustomers.DisplayMember = "CustomerName";
            cbmCustomers.ValueMember = "Id";

            SystemUserService systemUserService = new SystemUserService();
            systemUsers = systemUserService.GetSystemUsers();
            systemUsers.Add(new SystemUser() { Id = 0, FullName = "Tất cả" });
            systemUsers = systemUsers.OrderBy(x => x.Id).ToList();

            cbmUsers.DataSource = systemUsers;
            cbmUsers.DisplayMember = "FullName";
            cbmUsers.ValueMember = "Id";
        }

        private void setUpDataGrid(List<Order> orders)
        {
            dgwOrderList.AutoGenerateColumns = false;
            if (orders != null)
            {
                int index = 0;
                var query = from order in orders
                            select new
                            {
                                Id = order.Id,
                                OrderCode = order.OrderCode,
                                Note = order.Note,
                                Total = Global.formatVNDCurrencyText(order.Total.ToString()),
                                TotalInCurrency = order.Total,
                                VAT = Global.formatVNDCurrencyText(order.VAT.ToString()),
                                CustomerName = order.Customer != null ? order.Customer.CustomerName : "",
                                CustomerCode = order.Customer != null ? order.Customer.CustCode : "",
                                Discount = Global.formatVNDCurrencyText(order.Discount.ToString()),
                                CreatedDate = order.CreatedDate.ToString(BHConstant.DATE_FORMAT),
                                CreateBy = order.SystemUser.FullName,
                                Index = ++index
                            };
                dgwOrderList.DataSource = query.ToList();
                lblTotalResult.Text = orders.Count.ToString();
                lbTotal.Text = Global.formatVNDCurrencyText(query.Select(x => x.TotalInCurrency).Sum().ToString());
            }
        }

        private void SetupColumns()
        {
            dgwOrderList.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn indexColumn = new DataGridViewTextBoxColumn();
            indexColumn.Width = 30;
            indexColumn.DataPropertyName = "Index";
            indexColumn.HeaderText = "STT";
            indexColumn.ValueType = typeof(string);
            dgwOrderList.Columns.Add(indexColumn);

            DataGridViewTextBoxColumn createdDateColumn = new DataGridViewTextBoxColumn();
            createdDateColumn.DataPropertyName = "CreatedDate";
            createdDateColumn.Width = 100;
            createdDateColumn.HeaderText = "Ngày tạo";
            createdDateColumn.ValueType = typeof(string);
            dgwOrderList.Columns.Add(createdDateColumn);

            DataGridViewTextBoxColumn orderCodeColumn = new DataGridViewTextBoxColumn();
            orderCodeColumn.Width = 100;
            orderCodeColumn.DataPropertyName = "OrderCode";
            orderCodeColumn.HeaderText = "Mã đặt hàng";
            orderCodeColumn.ValueType = typeof(string);
            dgwOrderList.Columns.Add(orderCodeColumn);

            DataGridViewTextBoxColumn customerNameColumn = new DataGridViewTextBoxColumn();
            customerNameColumn.DataPropertyName = "CustomerName";
            customerNameColumn.Width = 200;
            customerNameColumn.HeaderText = "Khách hàng";
            customerNameColumn.ValueType = typeof(string);
            dgwOrderList.Columns.Add(customerNameColumn);

            DataGridViewTextBoxColumn customerCodeColumn = new DataGridViewTextBoxColumn();
            customerCodeColumn.DataPropertyName = "CustomerCode";
            customerCodeColumn.Width = 150;
            customerCodeColumn.HeaderText = "Mã khách hàng";
            customerCodeColumn.ValueType = typeof(string);
            dgwOrderList.Columns.Add(customerCodeColumn);

            DataGridViewTextBoxColumn noteColumn = new DataGridViewTextBoxColumn();
            noteColumn.DataPropertyName = "Note";
            noteColumn.Width = 200;
            noteColumn.HeaderText = "Chú ý";
            noteColumn.ValueType = typeof(string);
            dgwOrderList.Columns.Add(noteColumn);

            DataGridViewTextBoxColumn VATColumn = new DataGridViewTextBoxColumn();
            VATColumn.DataPropertyName = "VAT";
            VATColumn.Width = 100;
            VATColumn.HeaderText = "VAT";
            VATColumn.ValueType = typeof(string);
            dgwOrderList.Columns.Add(VATColumn);

            DataGridViewTextBoxColumn discountColumn = new DataGridViewTextBoxColumn();
            discountColumn.DataPropertyName = "Discount";
            discountColumn.Width = 100;
            discountColumn.HeaderText = "Giảm";
            discountColumn.ValueType = typeof(string);
            dgwOrderList.Columns.Add(discountColumn);

            DataGridViewTextBoxColumn createByColumn  = new DataGridViewTextBoxColumn();
            createByColumn.DataPropertyName = "CreateBy";
            createByColumn.Width = 100;
            createByColumn.HeaderText = "Người tạo";
            createByColumn.ValueType = typeof(string);
            dgwOrderList.Columns.Add(createByColumn);

            DataGridViewTextBoxColumn totalColumn = new DataGridViewTextBoxColumn();
            totalColumn.DataPropertyName = "Total";
            totalColumn.Width = 100;
            totalColumn.HeaderText = "Tổng";
            totalColumn.ValueType = typeof(string);
            dgwOrderList.Columns.Add(totalColumn);
                        
            DataGridViewImageColumn deleteButton = new DataGridViewImageColumn();
            deleteButton.Image = Properties.Resources.erase;
            deleteButton.Width = 40;
            deleteButton.HeaderText = "Xóa";
            deleteButton.ImageLayout = DataGridViewImageCellLayout.Normal;
            dgwOrderList.Columns.Add(deleteButton);
        }

        private void dgwOrderList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender is DataGridView)
            {
                DataGridViewCell cell = ((DataGridView)sender).CurrentCell;
                if (cell.ColumnIndex == ((DataGridView)sender).ColumnCount - 1)
                {
                    if (Global.isAdmin())
                    {
                        DeleteOrder(e);
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng đăng nhập quyền Admin để xóa được phiếu bán hàng!", "Lỗi phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }

        private void dgwOrderList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AddOrder frmAddOrder = new AddOrder();
            DataGridViewRow currentRow = dgwOrderList.Rows[e.RowIndex];

            int id = ObjectHelper.GetValueFromAnonymousType<int>(currentRow.DataBoundItem, "Id");
            frmAddOrder.loadDataForEditOrder(id);

            frmAddOrder.CallFromUserControll = this;
            frmAddOrder.ShowDialog();
        }
        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            OrderSearchCriteria search = new OrderSearchCriteria
            {
                Code = string.IsNullOrEmpty(txtCode.Text) ? txtCode.Text : "",
                CreatedBy = (cbmUsers.SelectedValue != null && cbmUsers.SelectedIndex != 0) ? (int?)cbmUsers.SelectedValue : (int?)null,
                Customer = (cbmCustomers.SelectedValue != null && cbmCustomers.SelectedIndex != 0) ? (int?)cbmCustomers.SelectedValue : (int?)null,
                From = dtpFrom.Value != null ? dtpFrom.Value : (DateTime?)null,
                To = dtpTo.Value != null ? dtpTo.Value.AddDays(1).Date : (DateTime?)null,
            };
            OrderService service = new OrderService();
            List<Order> orders = service.SearchingOrder(search);
            setUpDataGrid(orders);
        }

        private void DeleteOrder(DataGridViewCellEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn muốn xóa đơn hàng này?", "Xoá đơn hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                DataGridViewRow currentRow = dgwOrderList.Rows[e.RowIndex];

                OrderService orderService = new OrderService();
                //Product mu = (Product)dgv.DataBoundItem;
                int id = ObjectHelper.GetValueFromAnonymousType<int>(currentRow.DataBoundItem, "Id");
                Order order = orderService.GetOrder(id);
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
                    Amount = order.Total,
                    AfterDebit = beforeDebit - order.Total,
                    CreatedDate = DateTime.Now
                };
                bool kq = cls.AddCustomerLog(cl);

                double totalCommission = 0.0;
                ProductLogService productLogService = new ProductLogService();
                foreach (OrderDetail od in order.OrderDetails)
                {
                    totalCommission += od.Commission;
                    ProductLog pl = productLogService.GetNewestProductUnitLog(od.ProductId, od.AttributeId, od.UnitId);
                    ProductLog plg = new ProductLog
                    {
                        AttributeId = od.AttributeId,
                        ProductId = od.ProductId,
                        UnitId = od.UnitId,
                        RecordCode = order.OrderCode,
                        BeforeNumber = pl.AfterNumber,
                        Amount = od.NumberUnit,
                        AfterNumber = pl.AfterNumber + od.NumberUnit,
                        CreatedDate = DateTime.Now
                    };
                    bool ret = productLogService.AddProductLog(plg);
                }

                int salerId = (int)order.Customer.SalerId;
                if (salerId > 0)
                {
                    EmployeeLogService els = new EmployeeLogService();
                    EmployeeLog el = els.GetNewestEmployeeLog(order.CreateBy);
                    EmployeeLog newel = new EmployeeLog
                    {
                        EmployeeId = salerId,
                        RecordCode = order.OrderCode,
                        BeforeNumber = el.AfterNumber,
                        Amount = totalCommission,
                        AfterNumber = el.AfterNumber - totalCommission,
                        CreatedDate = DateTime.Now
                    };
                    kq = els.AddEmployeeLog(newel);
                }

                if (!orderService.DeleteOrder(id) && kq)
                {
                    MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                }
                loadOrderList();
            }
        }
    }
}
