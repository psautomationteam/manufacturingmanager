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
                var query = from baseAttribute in baseAttributes

                            select new
                            {
                                AttributeName = baseAttribute.AttributeName,
                                AttributeCode = baseAttribute.AttributeCode,
                                Description = baseAttribute.Description,
                                Id = baseAttribute.Id,

                            };
                dgvProductAttributeList.DataSource = query.ToList();

            }
        }
        private void SetupColumns()
        {
            dgvProductAttributeList.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn attributeNameColumn = new DataGridViewTextBoxColumn();
            attributeNameColumn.Width = 150;
            attributeNameColumn.DataPropertyName = "AttributeName";
            attributeNameColumn.HeaderText = "Tên thuộc tính";
            attributeNameColumn.ValueType = typeof(string);
            attributeNameColumn.Frozen = true;
            dgvProductAttributeList.Columns.Add(attributeNameColumn);



            DataGridViewTextBoxColumn attributeCodeColumn = new DataGridViewTextBoxColumn();
            attributeCodeColumn.DataPropertyName = "AttributeCode";
            attributeCodeColumn.Width = 150;
            attributeCodeColumn.HeaderText = "Mã Thuộc tính";
            attributeCodeColumn.Frozen = true;
            attributeCodeColumn.ValueType = typeof(string);
            dgvProductAttributeList.Columns.Add(attributeCodeColumn);

            DataGridViewTextBoxColumn descriptionColumn = new DataGridViewTextBoxColumn();
            descriptionColumn.Width = dgvProductAttributeList.Width - attributeNameColumn.Width - attributeCodeColumn.Width;
            descriptionColumn.DataPropertyName = "Description";
            descriptionColumn.HeaderText = "Đặc tả";
            descriptionColumn.Frozen = true;
            descriptionColumn.ValueType = typeof(string);
            dgvProductAttributeList.Columns.Add(descriptionColumn);
        }
    }
}
