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
using BaoHien.Services.MeasurementUnits;
using BaoHien.Model;
using DAL.Helper;
using BaoHien.Common;
using BaoHien.Services.ProductLogs;
namespace BaoHien.UI
{
    public partial class ProductList : UserControl
    {
        public ProductList()
        {
            InitializeComponent();
        }

        private void ProductList_Load(object sender, EventArgs e)
        {
            SetupColumns();
            loadProductList(); 
        }

        private void searchProduct()
        {
            ProductSearchCriteria productSearchCriteria = new ProductSearchCriteria()
            {
                ProductCode = string.IsNullOrEmpty(txtCode.Text) ? "" : txtCode.Text.ToLower(),
                ProductName = string.IsNullOrEmpty(txtName.Text) ? "" : txtName.Text.ToLower(),
                ProductTypeId = cbProductTypes.SelectedValue != null && cbProductTypes.SelectedIndex > 0 ? (int)cbProductTypes.SelectedValue : (int?)null
            };

            ProductService productService = new ProductService();
            List<Product> products = productService.SearchingProduct(productSearchCriteria);
            if (products != null)
            {
                setUpDataGrid(products);
                lblTotalResult.Text = products.Count.ToString();
            }
        }

        private void setUpDataGrid(List<Product> products)
        {
            if (products != null)
            {
                var query = from product in products
                            select new
                            {
                                ProductName = product.ProductName,
                                ProductCode = product.ProductCode,
                                Description = product.Description,
                                Id = product.Id,
                                Status = product.Status,
                                ProductType = (product.ProductType1 != null) ? product.ProductType1.TypeName : ""
                            };
                dgvProductList.DataSource = query.ToList();
                lblTotalResult.Text = products.Count.ToString();
            }
        }

        public void loadProductList()
        {            
            ProductService productService = new ProductService();
            List<Product> products = productService.GetProducts().OrderBy(x => x.ProductCode).ToList();
            setUpDataGrid(products);
            ProductTypeService productTypeService = new ProductTypeService();
            List<ProductType> productTypes = productTypeService.GetProductTypes();
            productTypes.Add(new ProductType() { Id = 0, TypeName = "Tất cả" });
            productTypes = productTypes.OrderBy(x => x.Id).ToList();
            if (productTypes != null)
            {
                cbProductTypes.DataSource = productTypes;
                cbProductTypes.DisplayMember = "TypeName";
                cbProductTypes.ValueMember = "Id";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddProduct frmAddProduct = new AddProduct();
            frmAddProduct.CallFromUserControll = this;
            frmAddProduct.ShowDialog();
        }
        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            searchProduct();
        }

        private void SetupColumns()
        {
            dgvProductList.AutoGenerateColumns = false;

            dgvProductList.Columns.Add(Global.CreateCell("ProductCode", "Mã sản phẩm", 100));
            dgvProductList.Columns.Add(Global.CreateCell("ProductName", "Tên sản phẩm", 150));
            dgvProductList.Columns.Add(Global.CreateCell("ProductType", "Loại sản phẩm", 170));
            dgvProductList.Columns.Add(Global.CreateCell("Description", "Đặc tả", 170));
            dgvProductList.Columns.Add(Global.CreateCellDeleteAction());
        }

        private void dgvProductList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AddProduct frmAddProductType = new AddProduct();
            DataGridViewRow currentRow = dgvProductList.Rows[e.RowIndex];

            int id = ObjectHelper.GetValueFromAnonymousType<int>(currentRow.DataBoundItem, "Id");
            frmAddProductType.loadDataForEditProduct(id);

            frmAddProductType.CallFromUserControll = this;
            frmAddProductType.ShowDialog();
        }

        private void dgvProductList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender is DataGridView)
            {
                DataGridViewCell cell = ((DataGridView)sender).CurrentCell;
                if (cell.ColumnIndex == ((DataGridView)sender).ColumnCount - 1)
                {
                    DialogResult result = MessageBox.Show("Bạn có muốn xóa sản phẩm này?",
                    "Xoá sản phẩm này",
                     MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        DataGridViewRow currentRow = dgvProductList.Rows[e.RowIndex];

                        ProductService productService = new ProductService();
                        int id = ObjectHelper.GetValueFromAnonymousType<int>(currentRow.DataBoundItem, "Id");
                        ProductLogService productLogService = new ProductLogService();
                        ProductLog log = productLogService.GetProductLogs().Where(p => p.ProductId == id && p.Status == BHConstant.ACTIVE_STATUS).FirstOrDefault();
                        if (log == null)
                        {
                            if (!productService.DeleteProduct(id))
                            {
                                MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Sản phẩm này còn trong kho nên không thể xóa được!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        loadProductList();
                    }

                }

            }
        }

        private void dgvProductList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
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
