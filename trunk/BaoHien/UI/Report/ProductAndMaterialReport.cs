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
using BaoHien.Services.MaterialInStocks;

namespace BaoHien.UI
{
    public partial class ProductAndMaterialReport : UserControl
    {
        List<ProductType> productTypes;
        List<EntranceStockReport> entranceStockReports;
        public ProductAndMaterialReport()
        {
            InitializeComponent();
            LoadProductType();
            
        }
        private void setUpDataGrid(List<MaterialInStock> materialInStocks)
        {
            int index = 0;
            var query = from materialInStock in materialInStocks
                        select new EntranceStockReport
                        {
                            ProductCode = materialInStock.Product.ProductCode,
                            ProductName = materialInStock.Product.ProductName,
                            AttributeName = materialInStock.BaseAttribute.AttributeName,
                            NumberOfInput = (materialInStock.NumberOfInput != null) ? (int)materialInStock.NumberOfInput : 0,
                            NumberOfOutput = (materialInStock.NumberOfOutput != null) ? (int)materialInStock.NumberOfOutput : 0,
                            NumberOfRemaining = ((materialInStock.NumberOfInput != null) ? (int)materialInStock.NumberOfInput : 0) - ((materialInStock.NumberOfOutput != null) ? (int)materialInStock.NumberOfOutput : 0),
                            BaseUnitName = (materialInStock.Product.MeasurementUnit != null) ? materialInStock.Product.MeasurementUnit.Name : "",
                            Index = index++
                        };
            dgwStockEntranceList.DataSource = query.ToList();
        }
        void LoadReport()
        {
            if (chkbSelectAll.Checked)
            {
                
                MaterialInStockService materialInStockService = new MaterialInStockService();
                List<MaterialInStock> materialInStocks = materialInStockService.GetMaterialInStocks();
                materialInStocks = materialInStocks.Where(mis => mis.LatestUpdate.CompareTo(dtpFrom.Value) > 0).ToList();
                setUpDataGrid(materialInStocks);
            }
            else
            {
                if (cbmProductTypes.SelectedValue != null)
                {
                    int productTyeId = (int)cbmProductTypes.SelectedValue;
                    MaterialInStockService materialInStockService = new MaterialInStockService();
                    List<MaterialInStock> materialInStocks = materialInStockService.GetMaterialInStocks();
                    materialInStocks = materialInStocks.Where(mis => mis.Product.ProductType == productTyeId && mis.LatestUpdate.CompareTo(dtpFrom.Value) > 0).ToList();
                    setUpDataGrid(materialInStocks);
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn loại sản phẩm");
                }
            }
            
            
            
        }
        void LoadProductType()
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
            dgwStockEntranceList.Columns.Clear();
            dgwStockEntranceList.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn indexColumn = new DataGridViewTextBoxColumn();
            indexColumn.Width = 30;
            indexColumn.DataPropertyName = "Index";
            indexColumn.HeaderText = "STT";
            indexColumn.ValueType = typeof(string);
            //indexColumn.Frozen = true;
            dgwStockEntranceList.Columns.Add(indexColumn);

            DataGridViewTextBoxColumn productCodeColumn = new DataGridViewTextBoxColumn();
            productCodeColumn.Width = 150;
            productCodeColumn.DataPropertyName = "ProductCode";
            productCodeColumn.HeaderText = "Mã sản phẩm";
            productCodeColumn.ValueType = typeof(string);
            //productCodeColumn.Frozen = true;
            dgwStockEntranceList.Columns.Add(productCodeColumn);

            DataGridViewTextBoxColumn productNameColumn = new DataGridViewTextBoxColumn();
            productNameColumn.Width = 150;
            productNameColumn.DataPropertyName = "ProductName";
            productNameColumn.HeaderText = "Tên sản phẩm";
            productNameColumn.ValueType = typeof(string);
            //productNameColumn.Frozen = true;
            dgwStockEntranceList.Columns.Add(productNameColumn);





            DataGridViewTextBoxColumn attributeNameColumn = new DataGridViewTextBoxColumn();
            attributeNameColumn.DataPropertyName = "AttributeName";
            attributeNameColumn.Width = 100;
            attributeNameColumn.HeaderText = "Quy cách";
            //attributeNameColumn.Frozen = true;
            attributeNameColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(attributeNameColumn);

            DataGridViewTextBoxColumn baseUnitColumn = new DataGridViewTextBoxColumn();
            baseUnitColumn.DataPropertyName = "BaseUnit";
            baseUnitColumn.Width = 100;
            baseUnitColumn.HeaderText = "Đơn vị tính";
            //baseUnitColumn.Frozen = true;
            baseUnitColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(baseUnitColumn);

            DataGridViewTextBoxColumn numberOfInputColumn = new DataGridViewTextBoxColumn();
            numberOfInputColumn.DataPropertyName = "NumberOfInput";
            numberOfInputColumn.Width = 100;
            numberOfInputColumn.HeaderText = "Nhập";
            //numberOfInputColumn.Frozen = true;
            numberOfInputColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(numberOfInputColumn);

            DataGridViewTextBoxColumn numberOfOutputColumn = new DataGridViewTextBoxColumn();
            numberOfOutputColumn.DataPropertyName = "NumberOfOutput";
            numberOfOutputColumn.Width = 100;
            numberOfOutputColumn.HeaderText = "Xuất";
            //numberOfOutputColumn.Frozen = true;
            numberOfOutputColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(numberOfOutputColumn);

            DataGridViewTextBoxColumn numberOfRemainingColumn = new DataGridViewTextBoxColumn();
            numberOfRemainingColumn.DataPropertyName = "NumberOfRemaining";
            numberOfRemainingColumn.Width = 100;
            numberOfRemainingColumn.HeaderText = "Số lượng tồn";
            //numberOfRemainingColumn.Frozen = true;
            numberOfRemainingColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(numberOfRemainingColumn);

            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgwStockEntranceList.AutoGenerateColumns = false;
            LoadReport();
            SetupColumns();
        }
    }
}
