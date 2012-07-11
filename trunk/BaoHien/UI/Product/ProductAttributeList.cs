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
using BaoHien.Services.BaseAttributes;

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
            BaseAttributeService baseAttributeService = new BaseAttributeService();
            List<BaseAttribute> baseAttributes = baseAttributeService.GetBaseAttributes();
            if (baseAttributes != null)
            {
                dgvProductAttributeList.DataSource = baseAttributes;
                
            }
        }

        private void dgvProductAttributeList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }
    }
}
