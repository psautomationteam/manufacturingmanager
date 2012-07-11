using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaoHien.Services.ProductAttributes;
using DAL;

namespace BaoHien.UI
{
    public partial class ProductAttributeList : UserControl
    {
        public ProductAttributeList()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddProductAttribute frmProductAttribute = new AddProductAttribute();
            frmProductAttribute.ShowDialog();
        }

        private void ProductAttributeList_Load(object sender, EventArgs e)
        {
            ProductAttributeService productAttributeService = new ProductAttributeService();
            List<ProductAttribute> productAttributes = productAttributeService.GetProductAttributes();
            if (productAttributes != null)
            {
                dgvProductAttributeList.DataSource = productAttributes;
                
            }
        }
    }
}
