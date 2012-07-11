using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using BaoHien.Services.Products;

namespace BaoHien.UI
{
    public partial class AddProductType : Form
    {
        public AddProductType()
        {
            InitializeComponent();
        }

        private void AddProductType_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductType productType = new ProductType
            {

                Description = txtDescription.Text,
                ProductName = txtName.Text,
                TypeCode = txtCode.Text
            };
            ProductTypeService productTypeService = new ProductTypeService();
            bool result = productTypeService.AddProductType(productType);
            if (result)
            {
                MessageBox.Show("Product Type added successfully");
                this.Close();
            }
            else
            {
                MessageBox.Show("Opps! Something wrong!");
            }
        }        
    }
}
