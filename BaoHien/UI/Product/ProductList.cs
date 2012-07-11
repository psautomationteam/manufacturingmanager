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
            frmAddProduct.ShowDialog();
        }
    }
}
