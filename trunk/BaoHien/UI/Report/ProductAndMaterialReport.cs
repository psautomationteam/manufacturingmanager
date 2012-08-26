using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using BaoHien.Services.Products;
using BaoHien.Model;

namespace BaoHien.UI
{
    public partial class ProductAndMaterialReport : UserControl
    {
        List<ProductType> productTypes;
        List<EntranceStockReport> entranceStockReports;
        public ProductAndMaterialReport()
        {
            InitializeComponent();
            loadProductType();
        }
        void loadProductType()
        {
            ProductTypeService productTypeService = new ProductTypeService();
            productTypes = productTypeService.GetProductTypes();
            if (productTypes != null)
            {


                cbmProductTypes.DataSource = productTypes;

                cbmProductTypes.DisplayMember = "ProductName";
                cbmProductTypes.ValueMember = "Id";

            }
        }
        
        private void SetupColumns()
        {
            dgwStockEntranceList.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn indexColumn = new DataGridViewTextBoxColumn();
            indexColumn.Width = 30;
            indexColumn.DataPropertyName = "Index";
            indexColumn.HeaderText = "STT";
            indexColumn.ValueType = typeof(string);
            indexColumn.Frozen = true;
            dgwStockEntranceList.Columns.Add(indexColumn);

            DataGridViewTextBoxColumn productCodeColumn = new DataGridViewTextBoxColumn();
            productCodeColumn.Width = 150;
            productCodeColumn.DataPropertyName = "ProductCode";
            productCodeColumn.HeaderText = "Mã sản phẩm";
            productCodeColumn.ValueType = typeof(string);
            productCodeColumn.Frozen = true;
            dgwStockEntranceList.Columns.Add(productCodeColumn);

            DataGridViewTextBoxColumn productNameColumn = new DataGridViewTextBoxColumn();
            productNameColumn.Width = 150;
            productNameColumn.DataPropertyName = "ProductName";
            productNameColumn.HeaderText = "Tên sản phẩm";
            productNameColumn.ValueType = typeof(string);
            productNameColumn.Frozen = true;
            dgwStockEntranceList.Columns.Add(productNameColumn);





            DataGridViewTextBoxColumn attributeNameColumn = new DataGridViewTextBoxColumn();
            attributeNameColumn.DataPropertyName = "AttributeName";
            attributeNameColumn.Width = 100;
            attributeNameColumn.HeaderText = "Quy cách";
            attributeNameColumn.Frozen = true;
            attributeNameColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(attributeNameColumn);

            DataGridViewTextBoxColumn baseUnitColumn = new DataGridViewTextBoxColumn();
            baseUnitColumn.DataPropertyName = "BaseUnit";
            baseUnitColumn.Width = 100;
            baseUnitColumn.HeaderText = "Đơn vị tính";
            baseUnitColumn.Frozen = true;
            baseUnitColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(baseUnitColumn);

            DataGridViewTextBoxColumn numberOfInputColumn = new DataGridViewTextBoxColumn();
            numberOfInputColumn.DataPropertyName = "NumberOfInput";
            numberOfInputColumn.Width = 100;
            numberOfInputColumn.HeaderText = "Nhập";
            numberOfInputColumn.Frozen = true;
            numberOfInputColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(numberOfInputColumn);

            DataGridViewTextBoxColumn numberOfOutputColumn = new DataGridViewTextBoxColumn();
            numberOfOutputColumn.DataPropertyName = "NumberOfOutput";
            numberOfOutputColumn.Width = 100;
            numberOfOutputColumn.HeaderText = "Xuất";
            numberOfOutputColumn.Frozen = true;
            numberOfOutputColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(numberOfOutputColumn);

            DataGridViewTextBoxColumn numberOfRemainingColumn = new DataGridViewTextBoxColumn();
            numberOfRemainingColumn.DataPropertyName = "NumberOfRemaining";
            numberOfRemainingColumn.Width = 100;
            numberOfRemainingColumn.HeaderText = "Số lượng tồn";
            numberOfRemainingColumn.Frozen = true;
            numberOfRemainingColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(numberOfOutputColumn);

            DataGridViewImageColumn deleteButton = new DataGridViewImageColumn();
            deleteButton.Image = Properties.Resources.erase;
            deleteButton.Width = 100;
            deleteButton.HeaderText = "Xóa";
            deleteButton.ReadOnly = true;
            deleteButton.ImageLayout = DataGridViewImageCellLayout.Normal;

            DataGridViewTextBoxColumn descriptionColumn = new DataGridViewTextBoxColumn();
            descriptionColumn.Width = 170;// dgvProductList.Width - productNameColumn.Width - typeCodeColumn.Width - deleteButton.Width;
            descriptionColumn.DataPropertyName = "Description";
            descriptionColumn.HeaderText = "Đặc tả";
            descriptionColumn.Frozen = true;
            descriptionColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(descriptionColumn);

            dgwStockEntranceList.Columns.Add(deleteButton);
        }
    }
}
