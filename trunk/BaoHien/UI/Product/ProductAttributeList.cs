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
using DAL.Helper;
using BaoHien.Common;

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
            SetupColumns();
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
                int id = ObjectHelper.GetValueFromAnonymousType<int>(dgv.DataBoundItem, "Id");
                if (!productAttributeService.DeleteBaseAttribute(id))
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
                setUpDataGrid(baseAttributes);

            }
        }

        private void setUpDataGrid(List<BaseAttribute> baseAttributes)
        {
            if (baseAttributes != null)
            {
                int index = 0;
                var query = from baseAttribute in baseAttributes

                            select new
                            {
                                AttributeName = baseAttribute.AttributeName,
                                AttributeCode = baseAttribute.AttributeCode,
                                Description = baseAttribute.Description,
                                Id = baseAttribute.Id,
                                Index = ++index
                            };
                dgvProductAttributeList.DataSource = query.ToList();

            }
        }

        private void SetupColumns()
        {
            dgvProductAttributeList.AutoGenerateColumns = false;

            dgvProductAttributeList.Columns.Add(Global.CreateCell("Index", "STT", 30));
            dgvProductAttributeList.Columns.Add(Global.CreateCell("AttributeCode", "Mã Quy cách", 100));
            dgvProductAttributeList.Columns.Add(Global.CreateCell("AttributeName", "Tên Quy cách", 150));
            dgvProductAttributeList.Columns.Add(Global.CreateCell("Description", "Đặc tả", 400));
            dgvProductAttributeList.Columns.Add(Global.CreateCellDeleteAction());
        }

        private void dgvProductAttributeList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AddProductAttribute frmAddProductType = new AddProductAttribute();
            DataGridViewRow currentRow = dgvProductAttributeList.Rows[e.RowIndex];

            int id = ObjectHelper.GetValueFromAnonymousType<int>(currentRow.DataBoundItem, "Id");
            frmAddProductType.loadDataForEditProductAttribute(id);

            frmAddProductType.CallFromUserControll = this;
            frmAddProductType.ShowDialog();
        }

        private void dgvProductAttributeList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender is DataGridView)
            {
                DataGridViewCell cell = ((DataGridView)sender).CurrentCell;
                if (cell.ColumnIndex == ((DataGridView)sender).ColumnCount - 1)
                {
                    DialogResult result = MessageBox.Show("Bạn có muốn xóa Quy cách sản phẩm này?",
                    "Xoá Quy cách sản phẩm này",
                     MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        DataGridViewRow currentRow = dgvProductAttributeList.Rows[e.RowIndex];
                        BaseAttributeService baseAttributeService = new BaseAttributeService();
                        int id = ObjectHelper.GetValueFromAnonymousType<int>(currentRow.DataBoundItem, "Id");
                        ProductAttributeService productAttributeService = new ProductAttributeService();
                        List<ProductAttribute> productList = productAttributeService.SelectProductAttributeByWhere(x => x.AttributeId == id);
                        if (productList.Count > 0)
                        {
                            MessageBox.Show("Quy cách sản phẩm này đang được sử dụng. Không xóa được!");
                        }
                        else
                        {
                            if (!baseAttributeService.DeleteBaseAttribute(id))
                            {
                                MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                            }
                            loadProductAttributeList();
                        }
                    }

                }

            }
        }
    }
}
