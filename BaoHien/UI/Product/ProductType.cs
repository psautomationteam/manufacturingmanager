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
using BaoHien.Model;
using DAL.Helper;

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
            frmAddProductType.CallFromUserControll = this;
            frmAddProductType.ShowDialog();
        }

        private void ucProductType_Load(object sender, EventArgs e)
        {
            SetupColumns();
            loadProductTypeList();
        }
        public void loadProductTypeList()
        {
            
            ProductTypeService productTypeService = new ProductTypeService();
            List<ProductType> productTypes = productTypeService.GetProductTypes();
            if (productTypes != null)
            {

                dgvProductTypeList.DataSource = productTypes;


            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = dgvProductTypeList.SelectedRows;

            foreach (DataGridViewRow dgv in selectedRows)
            {

                ProductTypeService productTypeService = new ProductTypeService();
                int id = ObjectHelper.GetValueFromAnonymousType<int>(dgv.DataBoundItem, "Id");
                ProductService productService = new ProductService();
                List<Product> productList = productService.SelectProductByWhere(pt => pt.ProductType == id);
                bool deleteAllProductForThisType = true;
                foreach (Product p in productList)
                {
                    if (!productService.DeleteProduct(p.Id))
                    {
                        deleteAllProductForThisType = false;
                        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                        break;
                    }
                }
                if (!deleteAllProductForThisType || !productTypeService.DeleteProductType(id))
                {
                    MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                    break;
                }

            }
            loadProductTypeList();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            searchProductType();
        }
        private void searchProductType()
        {
            ProductTypeSearchCriteria producTypeSearchCriteria = new ProductTypeSearchCriteria();
            if (txtCode.Text != null && txtCode.Text != "" && txtCode.Text != " ")
            {
                producTypeSearchCriteria.ProductTypeCode = txtCode.Text;
            }
            if (txtName.Text != null && txtName.Text != "" && txtName.Text != " ")
            {
                producTypeSearchCriteria.ProductTypeName = txtName.Text;
            }



            ProductTypeService producTypeService = new ProductTypeService();
            List<ProductType> productTypes = producTypeService.SearchingProductType(producTypeSearchCriteria);
            if (productTypes != null)
            {
                
                dgvProductTypeList.DataSource = productTypes;
                lblTotalResult.Text = productTypes.Count.ToString();
            }
        }
        private void SetupColumns()
        {
            dgvProductTypeList.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn productNameColumn = new DataGridViewTextBoxColumn();
            productNameColumn.Width = 150;
            productNameColumn.DataPropertyName = "ProductName";
            productNameColumn.HeaderText = "Tên loại sản phẩm";
            productNameColumn.ValueType = typeof(string);
            productNameColumn.Frozen = true;
            dgvProductTypeList.Columns.Add(productNameColumn);

            

            DataGridViewTextBoxColumn typeCodeColumn = new DataGridViewTextBoxColumn();
            typeCodeColumn.DataPropertyName = "TypeCode";
            typeCodeColumn.Width = 150;
            typeCodeColumn.HeaderText = "Mã loại sản phẩm";
            typeCodeColumn.Frozen = true;
            typeCodeColumn.ValueType = typeof(string);
            dgvProductTypeList.Columns.Add(typeCodeColumn);

            DataGridViewTextBoxColumn descriptionColumn = new DataGridViewTextBoxColumn();
            descriptionColumn.Width = dgvProductTypeList.Width - productNameColumn.Width - typeCodeColumn.Width;
            descriptionColumn.DataPropertyName = "Description";
            descriptionColumn.HeaderText = "Đặc tả";
            descriptionColumn.Frozen = true;
            descriptionColumn.ValueType = typeof(string);
            dgvProductTypeList.Columns.Add(descriptionColumn);
        }

        

        private void dgvProductTypeList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AddProductType frmAddProductType = new AddProductType();
            DataGridViewRow currentRow = dgvProductTypeList.Rows[e.RowIndex];

            int id = ObjectHelper.GetValueFromAnonymousType<int>(currentRow.DataBoundItem, "Id");
            frmAddProductType.loadDataForEditProductType(id);
            
            frmAddProductType.CallFromUserControll = this;
            frmAddProductType.ShowDialog();
        }
    }
}
