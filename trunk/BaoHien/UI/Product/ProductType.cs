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

namespace BaoHien.UI
{
    public partial class ucProductType : UserControl
    {
        public ucProductType()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddProductType frmAddProductType = new AddProductType();
            frmAddProductType.ShowDialog();
        }

        private void ucProductType_Load(object sender, EventArgs e)
        {
            ProductTypeService productTypeService = new ProductTypeService();
            List<ProductType> productTypes = productTypeService.GetProductTypes();
            if (productTypes != null)
            {
                
                dgvProductTypeList.DataSource = productTypes;


            }
        }      
       
    }
}
