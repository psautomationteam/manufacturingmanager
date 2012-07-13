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
                dgvProductList.DataSource = products;
                lblTotalResult.Text = products.Count.ToString();
            }
        }
        public void loadProductList()
        {
            ProductService productService = new ProductService();
            List<Product> products = productService.GetProducts();
            if (products != null)
            {
                dgvProductList.DataSource = products;
                lblTotalResult.Text = products.Count.ToString();
            }
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
                Product mu = (Product)dgv.DataBoundItem;
                if (!productService.DeleteProduct(mu.Id))
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
    }
}
