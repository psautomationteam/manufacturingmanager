using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using BaoHien.Services.ProductAttributes;

namespace BaoHien.UI
{
    public partial class AddProductAttribute : Form
    {
        public AddProductAttribute()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ProductAttribute productAttribute = new ProductAttribute
            {

            };
            ProductAttributeService productAttributeService = new ProductAttributeService();
            bool result = productAttributeService.AddProductAttribute(productAttribute);
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
