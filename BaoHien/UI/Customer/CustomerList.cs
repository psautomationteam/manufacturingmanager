using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaoHien.Services.SystemUsers;
using DAL;
using BaoHien.Common;
using BaoHien.Services.Customers;
using DAL.Helper;
using BaoHien.Services.Employees;
using BaoHien.Model;

namespace BaoHien.UI
{
    public partial class CustomerList : UserControl
    {
        public CustomerList()
        {
            InitializeComponent();
        }

        private void CustomerList_Load(object sender, EventArgs e)
        {
            SetupColumns();
            loadCustomerList();
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
            List<Customer> customers = customerService.GetCustomers();
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

            DataGridViewTextBoxColumn indexColumn = new DataGridViewTextBoxColumn();
            indexColumn.Width = 30;
            indexColumn.DataPropertyName = "Index";
            indexColumn.HeaderText = "STT";
            indexColumn.ValueType = typeof(string);
            dgvProductList.Columns.Add(indexColumn);

            DataGridViewTextBoxColumn customerNameColumn = new DataGridViewTextBoxColumn();
            customerNameColumn.Width = 150;
            customerNameColumn.DataPropertyName = "CustomerName";
            customerNameColumn.HeaderText = "Tên khách hàng";
            customerNameColumn.ValueType = typeof(string);
            dgvProductList.Columns.Add(customerNameColumn);

            DataGridViewTextBoxColumn customerCodeColumn = new DataGridViewTextBoxColumn();
            customerCodeColumn.Width = 150;
            customerCodeColumn.DataPropertyName = "CustCode";
            customerCodeColumn.HeaderText = "Mã khách hàng";
            customerCodeColumn.ValueType = typeof(string);
            dgvProductList.Columns.Add(customerCodeColumn);

            DataGridViewTextBoxColumn customerPhoneColumn = new DataGridViewTextBoxColumn();
            customerPhoneColumn.Width = 100;
            customerPhoneColumn.DataPropertyName = "CustomerPhone";
            customerPhoneColumn.HeaderText = "SĐT Cty";
            customerPhoneColumn.ValueType = typeof(string);
            dgvProductList.Columns.Add(customerPhoneColumn);

            DataGridViewTextBoxColumn customerPersonNameColumn = new DataGridViewTextBoxColumn();
            customerPersonNameColumn.DataPropertyName = "CustomerPersonName";
            customerPersonNameColumn.Width = 150;
            customerPersonNameColumn.HeaderText = "Tên người liên lạc";
            customerPersonNameColumn.ValueType = typeof(string);
            dgvProductList.Columns.Add(customerPersonNameColumn);

            DataGridViewTextBoxColumn customerPersonPhoneColumn = new DataGridViewTextBoxColumn();
            customerPersonPhoneColumn.DataPropertyName = "CustomerPersonPhone";
            customerPersonPhoneColumn.Width = 150;
            customerPersonPhoneColumn.HeaderText = "SĐT người liên lạc";
            customerPersonPhoneColumn.ValueType = typeof(string);
            dgvProductList.Columns.Add(customerPersonPhoneColumn);
            
            DataGridViewTextBoxColumn favorProductColumn = new DataGridViewTextBoxColumn();
            favorProductColumn.DataPropertyName = "FavoriteProduct";
            favorProductColumn.Width = 150;
            favorProductColumn.HeaderText = "Dòng sản phẩm";
            favorProductColumn.ValueType = typeof(string);
            dgvProductList.Columns.Add(favorProductColumn);

            DataGridViewTextBoxColumn employeeColumn = new DataGridViewTextBoxColumn();
            employeeColumn.DataPropertyName = "Employee";
            employeeColumn.Width = 150;
            employeeColumn.HeaderText = "Nhân viên phụ trách";
            employeeColumn.ValueType = typeof(string);
            dgvProductList.Columns.Add(employeeColumn);

            DataGridViewImageColumn deleteButton = new DataGridViewImageColumn();
            deleteButton.Image = Properties.Resources.erase;
            deleteButton.Width = 40;
            deleteButton.HeaderText = "Xóa";
            deleteButton.ImageLayout = DataGridViewImageCellLayout.Normal;
            dgvProductList.Columns.Add(deleteButton);           
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
                    MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
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
                            MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                            
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
                Code = string.IsNullOrEmpty(txtCode.Text) ? "" : txtCode.Text ,
                Phone = string.IsNullOrEmpty(txtPhone.Text) ? "" : txtPhone.Text,
                Name = string.IsNullOrEmpty(txtName.Text) ? "" : txtName.Text,
                Saler = (cmbSaler.SelectedValue != null && (int)cmbSaler.SelectedValue > 0) ? (int?)cmbSaler.SelectedValue : (int?)null,
                FavorProduct = string.IsNullOrEmpty(txtFavoriteProduct.Text) ? "" : txtFavoriteProduct.Text
            };
            CustomerService service = new CustomerService();
            List<Customer> customers = service.SearchingCustomer(search);
            setUpDataGrid(customers);
        }
    }
}
