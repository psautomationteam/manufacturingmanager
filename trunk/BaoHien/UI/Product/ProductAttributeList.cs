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
            frmProductAttribute.CallFromUserControll = this;
            frmProductAttribute.ShowDialog();
        }

        private void ProductAttributeList_Load(object sender, EventArgs e)
        {
            loadProductAttributeList();
        }

        private void dgvProductAttributeList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = dgvProductAttributeList.SelectedRows;

            foreach (DataGridViewRow dgv in selectedRows)
            {

                BaseAttributeService productAttributeService = new BaseAttributeService();
                BaseAttribute mu = (BaseAttribute)dgv.DataBoundItem;

                if (!productAttributeService.DeleteBaseAttribute(mu.Id))
                {
                    MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                    break;
                }

            }
            loadProductAttributeList();
        }
        public void loadProductAttributeList()
        {
            BaseAttributeService baseAttributeService = new BaseAttributeService();
            List<BaseAttribute> baseAttributes = baseAttributeService.GetBaseAttributes();
            if (baseAttributes != null)
            {
                dgvProductAttributeList.DataSource = baseAttributes;

            }
        }
    }
}
