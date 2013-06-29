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
using BaoHien.Common;

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
                setUpDataGrid(productTypes);
                //dgvProductTypeList.DataSource = productTypes;
            }
        }

        private void setUpDataGrid(List<ProductType> productTypes)
        {
            if (productTypes != null)
            {
                dgvProductTypeList.DataSource = productTypes.ToList();
                lblTotalResult.Text = productTypes.Count.ToString();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            searchProductType();
        }

        private void searchProductType()
        {
            ProductTypeSearchCriteria producTypeSearchCriteria = new ProductTypeSearchCriteria
            {
                ProductTypeCode = string.IsNullOrEmpty(txtCode.Text) ? "" : txtCode.Text.ToLower(),
                ProductTypeName = string.IsNullOrEmpty(txtName.Text) ? "" : txtName.Text.ToLower()
            };

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
            dgvProductTypeList.Columns.Add(Global.CreateCell("TypeCode", "Mã LSP", 100));
            dgvProductTypeList.Columns.Add(Global.CreateCell("TypeName", "Tên LSP", 150));
            dgvProductTypeList.Columns.Add(Global.CreateCell("Description", "Đặc tả", 300));
            dgvProductTypeList.Columns.Add(Global.CreateCellDeleteAction());
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

        private void dgvProductTypeList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender is DataGridView)
            {
                DataGridViewCell cell = ((DataGridView)sender).CurrentCell;
                if (cell.ColumnIndex == ((DataGridView)sender).ColumnCount - 1)
                {
                    DialogResult result = MessageBox.Show("Bạn có muốn xóa loại sản phẩm này?",
                    "Xoá loại sản phẩm này",
                     MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        DataGridViewRow currentRow = dgvProductTypeList.Rows[e.RowIndex];

                        int id = ObjectHelper.GetValueFromAnonymousType<int>(currentRow.DataBoundItem, "Id");
                        ProductService productService = new ProductService();
                        ProductTypeService producTypeService = new ProductTypeService();
                        List<Product> productList = productService.SelectProductByWhere(pt => pt.ProductType == id);
                        if (productList.Count > 0)
                        {
                            MessageBox.Show("Loại này hiện có nhiều sản phẩm. Không xóa được!");
                        }
                        else
                        {
                            if (!producTypeService.DeleteProductType(id))
                            {
                                MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            loadProductTypeList();
                        }
                    }
                    
                }
                
            }
            
        }

        private void dgvProductTypeList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView gridView = sender as DataGridView;
            if (null != gridView)
            {
                gridView.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders);
                foreach (DataGridViewRow r in gridView.Rows)
                {
                    gridView.Rows[r.Index].HeaderCell.Value = (r.Index + 1).ToString();
                }
            }
        }
    }
}
