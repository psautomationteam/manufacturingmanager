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

            DataGridViewTextBoxColumn indexColumn = new DataGridViewTextBoxColumn();
            indexColumn.Width = 30;
            indexColumn.DataPropertyName = "Index";
            indexColumn.HeaderText = "STT";
            indexColumn.ValueType = typeof(string);
            indexColumn.Frozen = true;
            dgvProductAttributeList.Columns.Add(indexColumn);

            DataGridViewTextBoxColumn attributeNameColumn = new DataGridViewTextBoxColumn();
            attributeNameColumn.Width = 150;
            attributeNameColumn.DataPropertyName = "AttributeName";
            attributeNameColumn.HeaderText = "Tên thuộc tính";
            attributeNameColumn.ValueType = typeof(string);
            attributeNameColumn.Frozen = true;
            dgvProductAttributeList.Columns.Add(attributeNameColumn);



            DataGridViewTextBoxColumn attributeCodeColumn = new DataGridViewTextBoxColumn();
            attributeCodeColumn.DataPropertyName = "AttributeCode";
            attributeCodeColumn.Width = 120;
            attributeCodeColumn.HeaderText = "Mã Thuộc tính";
            attributeCodeColumn.Frozen = true;
            attributeCodeColumn.ValueType = typeof(string);
            dgvProductAttributeList.Columns.Add(attributeCodeColumn);

            DataGridViewImageColumn deleteButton = new DataGridViewImageColumn();
            deleteButton.Image = Properties.Resources.erase;
            deleteButton.Width = 100;
            deleteButton.HeaderText = "Xóa";
            deleteButton.ReadOnly = true;
            deleteButton.ImageLayout = DataGridViewImageCellLayout.Normal;

            DataGridViewTextBoxColumn descriptionColumn = new DataGridViewTextBoxColumn();
            descriptionColumn.Width = 350;// dgvProductAttributeList.Width - attributeNameColumn.Width - attributeCodeColumn.Width;
            descriptionColumn.DataPropertyName = "Description";
            descriptionColumn.HeaderText = "Đặc tả";
            descriptionColumn.Frozen = true;
            descriptionColumn.ValueType = typeof(string);
            dgvProductAttributeList.Columns.Add(descriptionColumn);

            dgvProductAttributeList.Columns.Add(deleteButton);
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
                    DialogResult result = MessageBox.Show("Bạn có muốn xóa thuộc tính sản phẩm này?",
                    "Xoá thuộc tính sản phẩm này",
                     MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        DataGridViewRow currentRow = dgvProductAttributeList.Rows[e.RowIndex];

                        BaseAttributeService productAttributeService = new BaseAttributeService();
                        int id = ObjectHelper.GetValueFromAnonymousType<int>(currentRow.DataBoundItem, "Id");
                        if (!productAttributeService.DeleteBaseAttribute(id))
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
