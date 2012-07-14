using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaoHien.Services.Products;
using DAL;
using BaoHien.Services.MeasurementUnits;
using BaoHien.Model;
using DAL.Helper;
namespace BaoHien.UI
{
    public partial class ProductList : UserControl
    {
        public ProductList()
        {
            InitializeComponent();
        }

        private void ProductList_Load(object sender, EventArgs e)
        {
            SetupColumns();
            loadProductList(); 
        }
        private void searchProduct()
        {
            ProductSearchCriteria productSearchCriteria = new ProductSearchCriteria();
            if (txtCode.Text != null && txtCode.Text != "" && txtCode.Text != " ")
            {
                productSearchCriteria.ProductCode = txtCode.Text;
            }
            if (txtName.Text != null && txtName.Text != "" && txtName.Text != " ")
            {
                productSearchCriteria.ProductName = txtName.Text;
            }

            if (cbProductTypes.SelectedValue != null && (int)cbProductTypes.SelectedValue > 0)
            {
                productSearchCriteria.ProductTypeId = (int?)cbProductTypes.SelectedValue;
            }
            if (cbPurchaseStatus.SelectedValue != null && (int)cbPurchaseStatus.SelectedValue > 0)
            {
                productSearchCriteria.PurchaseStatus = (int?)cbPurchaseStatus.SelectedValue;
            }
            
            ProductService productService = new ProductService();
            List<Product> products = productService.SearchingProduct(productSearchCriteria);
            if (products != null)
            {
                setUpDataGrid(products);
                lblTotalResult.Text = products.Count.ToString();
            }
        }
        private void setUpDataGrid(List<Product> products)
        {
            if (products != null)
            {
                var query = from product in products

                            select new
                            {
                                ProductName = product.ProductName,
                                ProductCode = product.ProductCode,
                                Description = product.Description,
                                Id = product.Id,
                                Status = product.Status,
                                ProductType = product.ProductType1.ProductName,
                                BaseUnit = product.MeasurementUnit.Name
                            };
                dgvProductList.DataSource = query.ToList();
                lblTotalResult.Text = products.Count.ToString();
            }
        }
        public void loadProductList()
        {
            
            ProductService productService = new ProductService();
            List<Product> products = productService.GetProducts();
            setUpDataGrid(products);
            ProductTypeService productTypeService = new ProductTypeService();
            List<ProductType> productTypes = productTypeService.GetProductTypes();
            if (productTypes != null)
            {
                
                
                cbProductTypes.DataSource = productTypes;

                cbProductTypes.DisplayMember = "ProductName";
                cbProductTypes.ValueMember = "Id";

            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddProduct frmAddProduct = new AddProduct();
            frmAddProduct.CallFromUserControll = this;
            frmAddProduct.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = dgvProductList.SelectedRows;

            foreach (DataGridViewRow dgv in selectedRows)
            {

                ProductService productService = new ProductService();
                //Product mu = (Product)dgv.DataBoundItem;
                int id = ObjectHelper.GetValueFromAnonymousType<int>(dgv.DataBoundItem, "Id");
                if (!productService.DeleteProduct(id))
                {
                    MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                    break;
                }

            }
            loadProductList();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            searchProduct();
        }
        private void SetupColumns()
        {
            dgvProductList.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn productNameColumn = new DataGridViewTextBoxColumn();
            productNameColumn.Width = 150;
            productNameColumn.DataPropertyName = "ProductName";
            productNameColumn.HeaderText = "Tên sản phẩm";
            productNameColumn.ValueType = typeof(string);
            productNameColumn.Frozen = true;
            dgvProductList.Columns.Add(productNameColumn);



            DataGridViewTextBoxColumn typeCodeColumn = new DataGridViewTextBoxColumn();
            typeCodeColumn.DataPropertyName = "ProductCode";
            typeCodeColumn.Width = 150;
            typeCodeColumn.HeaderText = "Mã sản phẩm";
            typeCodeColumn.Frozen = true;
            typeCodeColumn.ValueType = typeof(string);
            dgvProductList.Columns.Add(typeCodeColumn);

            DataGridViewTextBoxColumn productTypeColumn = new DataGridViewTextBoxColumn();
            productTypeColumn.DataPropertyName = "ProductType";
            productTypeColumn.Width = 150;
            productTypeColumn.HeaderText = "Loại sản phẩm";
            productTypeColumn.Frozen = true;
            productTypeColumn.ValueType = typeof(string);
            dgvProductList.Columns.Add(productTypeColumn);

            DataGridViewTextBoxColumn baseUnitColumn = new DataGridViewTextBoxColumn();
            baseUnitColumn.DataPropertyName = "BaseUnit";
            baseUnitColumn.Width = 150;
            baseUnitColumn.HeaderText = "Đơn vị tính";
            baseUnitColumn.Frozen = true;
            baseUnitColumn.ValueType = typeof(string);
            dgvProductList.Columns.Add(baseUnitColumn);

            DataGridViewTextBoxColumn descriptionColumn = new DataGridViewTextBoxColumn();
            descriptionColumn.Width = dgvProductList.Width - productNameColumn.Width - typeCodeColumn.Width;
            descriptionColumn.DataPropertyName = "Description";
            descriptionColumn.HeaderText = "Đặc tả";
            descriptionColumn.Frozen = true;
            descriptionColumn.ValueType = typeof(string);
            dgvProductList.Columns.Add(descriptionColumn);
        }
    }
}
