using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using BaoHien.Services.Products;
using BaoHien.Services.MeasurementUnits;
using DAL.Helper;
using BaoHien.Services.BaseAttributes;
using BaoHien.UI.Base;
using BaoHien.Services.ProductAttributes;
using BaoHien.Common;
using BaoHien.Services.ProductLogs;


namespace BaoHien.UI
{
    public partial class AddProduct : BaseForm
    {
        Product product;
        List<Product> products;
        List<MeasurementUnit> measurementUnits;
        List<ProductType> productTypes;
        List<BaseAttribute> baseAttributes;
        int mode = 0; // default "New status"
        List<int> oldAttr;
        ProductLogService productLogService;

        public AddProduct()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (validator1.Validate())
            {
                if (product != null)//update
                {
                    product.Description = txtDescription.Text;
                    product.ProductName = txtName.Text;
                    //product.BaseUnit = cmbUnit.SelectedValue != null ? (int)cmbUnit.SelectedValue : (int?)null;
                    product.ProductCode = txtCode.Text;
                    //product.ProductType = (int)cmbType.SelectedValue;

                    ProductType refProductType = productTypes.Single(pt => pt.Id == (int)cmbType.SelectedValue);
                    product.ProductType1 = refProductType;
                    ProductService productService = new ProductService();
                    bool result = productService.UpdateProduct(product);
                    result = UpdateAttribute(product.Id);
                    if (result)
                    {
                        MessageBox.Show("Sản phẩm đã được cập nhật thành công");
                        ((ProductList)this.CallFromUserControll).loadProductList();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else//add new
                {
                    product = new Product
                    {
                        Description = txtDescription.Text,
                        ProductName = txtName.Text,
                        ProductCode = txtCode.Text,
                        ProductType = (int)cmbType.SelectedValue
                    };
                    ProductService productService = new ProductService();
                    bool result = productService.AddProduct(product);

                    long newProductId = BaoHienRepository.GetMaxId<Product>();

                    result = UpdateAttribute(newProductId);
                    if (result)
                    {
                        MessageBox.Show("Sản phẩm đã được tạo thành công");
                        if (this.CallFromUserControll != null && this.CallFromUserControll is ProductList)
                        {
                            ((ProductList)this.CallFromUserControll).loadProductList();
                        }
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void AddProduct_Load(object sender, EventArgs e)
        {
            ProductService productService = new ProductService();
            products = productService.GetProducts();

            productLogService = new ProductLogService();
            dgvBaseAttributes.AutoGenerateColumns = false;
            loadSomeData();
            SetupColumns();

            oldAttr = new List<int>();
            if (mode == 1) // Load data grid
            {
                List<ProductAttribute> pas = product.ProductAttributes.ToList<ProductAttribute>();
                foreach (ProductAttribute pa in pas)
                {
                    oldAttr.Add(pa.AttributeId);
                    foreach (DataGridViewRow dgv in dgvBaseAttributes.Rows)
                    {
                        DataGridViewCheckBoxCell checkbox = (DataGridViewCheckBoxCell)dgv.Cells[0];
                        if ((BaseAttribute)dgv.DataBoundItem == pa.BaseAttribute)
                        {
                            checkbox.Value = 1;
                            if (productLogService.GetNewestProductLog(product.Id, pa.AttributeId) != null)
                            {
                                checkbox.FlatStyle = FlatStyle.Flat;
                                checkbox.Style.ForeColor = Color.DarkGray;
                                checkbox.ReadOnly = true;
                                checkbox.ToolTipText = "Thuộc tính của sản phẩm đang được sử dụng";
                            }
                            break;
                        }
                    }
                }
            }

            var product_codes = new AutoCompleteStringCollection();
            product_codes.AddRange(products.Select(x => x.ProductCode).ToArray());
            txtCode.AutoCompleteCustomSource = product_codes;
            txtCode.AutoCompleteMode = AutoCompleteMode.Append;
            txtCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void loadSomeData()
        {
            if (productTypes == null)
            {
                ProductTypeService productTypeService = new ProductTypeService();
                productTypes = productTypeService.GetProductTypes();
            }
            
            if (productTypes != null)
            {
                cmbType.DataSource = productTypes;
                cmbType.DisplayMember = "TypeName";
                cmbType.ValueMember = "Id";
            }

            BaseAttributeService baseAttributeService = new BaseAttributeService();
            if (baseAttributes == null)
            {
                baseAttributes = baseAttributeService.GetBaseAttributes();
            }      
        }

        public void loadDataForEditProduct(int productId)
        {
            this.Text = "Chỉnh sửa sản phẩm này";
            this.btnAdd.Text = "Cập nhật";
            
            ProductService productTypeService = new ProductService();
            product = productTypeService.GetProduct(productId);
            loadSomeData();
            if (product != null)
            {
                mode = 1; // Edit mode
                if (productTypes != null)
                {
                    cmbType.SelectedIndex = productTypes.FindIndex(p => p.Id == product.ProductType);
                }
                //dgvBaseAttributes.Enabled = false;
                txtDescription.Text = product.Description;
                txtCode.Text = product.ProductCode;
                txtName.Text = product.ProductName;                
            }
        }

        private void chkAuto_CheckedChanged(object sender, EventArgs e)
        {
            txtCode.Enabled = !chkAuto.Checked;
            if (!txtCode.Enabled)
            {
                int max_id = product == null ? BaoHienRepository.GetMaxId<Product>() + 1 : product.Id;
                string id = BHConstant.PREFIX_FOR_PRODUCT;
                if (max_id.ToString().Length < BHConstant.MAX_ID)
                    id += String.Concat(Enumerable.Repeat("0", BHConstant.MAX_ID - max_id.ToString().Length));
                id += max_id.ToString();
                txtCode.Text = id;
            }
        }

        private void SetupColumns()
        {
            dgvBaseAttributes.AutoGenerateColumns = false;
            if (baseAttributes != null)
            {
                dgvBaseAttributes.DataSource = baseAttributes;
            }
            DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.Width = 30;
            checkboxColumn.HeaderText = "";
            checkboxColumn.ValueType = typeof(string);
            //checkboxColumn.Frozen = true;
            dgvBaseAttributes.Columns.Add(checkboxColumn);

            DataGridViewTextBoxColumn attributeNameColumn = new DataGridViewTextBoxColumn();
            attributeNameColumn.Width = 150;
            attributeNameColumn.DataPropertyName = "AttributeName";
            attributeNameColumn.HeaderText = "Tên Quy cách";
            attributeNameColumn.ValueType = typeof(string);
            //attributeNameColumn.Frozen = true;
            dgvBaseAttributes.Columns.Add(attributeNameColumn);

            DataGridViewTextBoxColumn attributeCodeColumn = new DataGridViewTextBoxColumn();
            attributeCodeColumn.DataPropertyName = "AttributeCode";
            attributeCodeColumn.Width = 120;
            attributeCodeColumn.HeaderText = "Mã Quy cách";
            //attributeCodeColumn.Frozen = true;
            attributeCodeColumn.ValueType = typeof(string);
            dgvBaseAttributes.Columns.Add(attributeCodeColumn);

            DataGridViewTextBoxColumn descriptionColumn = new DataGridViewTextBoxColumn();
            descriptionColumn.Width = 350;// dgvProductAttributeList.Width - attributeNameColumn.Width - attributeCodeColumn.Width;
            descriptionColumn.DataPropertyName = "Description";
            descriptionColumn.HeaderText = "Đặc tả";
            //descriptionColumn.Frozen = true;
            descriptionColumn.ValueType = typeof(string);
            dgvBaseAttributes.Columns.Add(descriptionColumn);
        }

        private void dgwOrderDetails_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Có lỗi nhập liệu xảy ra,vui lòng kiểm tra lại!");
        }

        private bool UpdateAttribute(long newProductId)
        {
            DataGridViewRowCollection selectedRows = dgvBaseAttributes.Rows;
            ProductAttributeService productAttributeService = new ProductAttributeService();

            bool result = true;
            List<int> newAttr = new List<int>();
            List<int> addAttr, removeAttr;

            foreach (DataGridViewRow dgv in selectedRows)
            {
                DataGridViewCheckBoxCell checkbox = (DataGridViewCheckBoxCell)dgv.Cells[0];
                if (checkbox.Value != null &&
                    (checkbox.Value.ToString().Equals(bool.TrueString) || checkbox.Value.ToString().Equals("1")))
                {
                    newAttr.Add(((BaseAttribute)dgv.DataBoundItem).Id);
                }
            }

            addAttr = newAttr.Except(oldAttr).ToList();
            removeAttr = oldAttr.Except(newAttr).ToList();

            foreach (int add in addAttr)
            {
                ProductAttribute pa = new ProductAttribute()
                {
                    ProductId = (int)newProductId,
                    AttributeId = add
                };
                result = productAttributeService.AddProductAttribute(pa);
            }

            foreach (int remove in removeAttr)
            {
                int removeId = -1;
                using (BaoHienDBDataContext context = new BaoHienDBDataContext(SettingManager.BuildStringConnection()))
                {
                    ProductAttribute pa = context.ProductAttributes
                        .Where(p => p.AttributeId == remove && p.ProductId == (int)newProductId).SingleOrDefault();
                    if (pa != null)
                        removeId = pa.Id;
                }
                if (removeId != -1)
                    result = productAttributeService.DeleteProductAttribute(removeId);
            }
            BaoHienRepository.ResetDBDataContext();
            return result;
        }

        private void txtCode_MouseLeave(object sender, EventArgs e)
        {
            string code = txtCode.Text;
            if (!string.IsNullOrEmpty(code))
            {
                Product p = null;
                if (product != null)
                    p = products.Where(x => x.ProductCode == code && x.Id != product.Id).FirstOrDefault();
                else
                    p = products.Where(x => x.ProductCode == code).FirstOrDefault();
                if (p != null)
                {
                    if (MessageBox.Show("Sản phẩm với mã này hiện đang tồn tại, bạn có muốn chỉnh sửa lại sản phẩm này không ?", "Thông báo ...", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                        == System.Windows.Forms.DialogResult.Yes)
                    {
                        loadDataForEditProduct(p.Id);
                        AddProduct_Load(null, null);
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
        }

        private void dgvBaseAttributes_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
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
