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
using BaoHien.Services.Prices;
using DAL.Helper;
using BaoHien.Services.BaseAttributes;
using BaoHien.UI.Base;
using BaoHien.Services.ProductAttributes;


namespace BaoHien.UI
{
    public partial class AddProduct : BaseForm
    {
        Product product;
        List<MeasurementUnit> measurementUnits;
        List<ProductType> productTypes;
        List<BaseAttribute> baseAttributes;
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
            if (product != null)//update
            {
                product.Description = txtDescription.Text;
                product.ProductName = txtName.Text;
                //product.BaseUnit = cmbUnit.SelectedValue != null ? (int)cmbUnit.SelectedValue : (int?)null;
                product.ProductCode = txtCode.Text;
                //product.ProductType = (int)cmbType.SelectedValue;
                if (cmbUnit.SelectedValue != null)
                {
                    MeasurementUnit refMeasurementUnit = measurementUnits.Single(mu => mu.Id == (int)cmbUnit.SelectedValue);
                    product.MeasurementUnit = refMeasurementUnit;
                }
                ProductType refProductType = productTypes.Single(pt => pt.Id == (int)cmbType.SelectedValue);
                product.ProductType1 = refProductType;
                ProductService productService = new ProductService();
                bool result = productService.UpdateProduct(product);
                if (result)
                {
                    MessageBox.Show("Sản phẩm đã được cập nhật thành công");
                    ((ProductList)this.CallFromUserControll).loadProductList();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                }
            }
            else//add new
            {
                product = new Product
                {

                    Description = txtDescription.Text,
                    ProductName = txtName.Text,
                    BaseUnit = cmbUnit.SelectedValue != null ? (int)cmbUnit.SelectedValue : (int?)null,
                    ProductCode = txtCode.Text,
                    ProductType = (int)cmbType.SelectedValue,

                };
                ProductService productService = new ProductService();
                bool result = productService.AddProduct(product);

                long newProductId = BaoHienRepository.GetMaxId<Product>();
                PriceService priceService = new PriceService();
                double price = 0;
                Double.TryParse(txtPrice.Text, out price);
                Price newPrice = new Price
                {
                    Id = (int)newProductId,
                    Price1 = price,
                    UpdatedDate = DateTime.Now
                };
                result = priceService.AddPrice(newPrice);

                DataGridViewRowCollection selectedRows = dgvBaseAttributes.Rows;
                ProductAttributeService productAttributeService = new ProductAttributeService();
                foreach (DataGridViewRow dgv in selectedRows)
                {
                    DataGridViewCheckBoxCell checkbox = (DataGridViewCheckBoxCell)dgv.Cells[0];
                    if (checkbox.Value != null && checkbox.Value.ToString().Equals(bool.TrueString) && dgv.DataBoundItem != null)
                    {
                        ProductAttribute productAttribute = new ProductAttribute
                        {
                            AttributeId = ((BaseAttribute)dgv.DataBoundItem).Id,
                            Id = (int)newProductId
                        };
                        result = productAttributeService.AddProductAttribute(productAttribute);
                        if (!result)
                        {
                            MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                            return;
                        }
                    }
                    else
                    {
                        if (dgv.DataBoundItem == null)
                        {
                            result = false;
                            break;
                        }
                        
                    }

                    

                }

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
                    MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                }
            }
            
        }

        private void AddProduct_Load(object sender, EventArgs e)
        {
            loadSomeData();
            SetupColumns();
            
            
            
        }
        private void loadSomeData()
        {
            if (measurementUnits == null)
            {
                MeasurementUnitService measurementUnitService = new MeasurementUnitService();
                measurementUnits = measurementUnitService.GetMeasurementUnits();

            }
            
            if (measurementUnits != null)
            {

                cmbUnit.DataSource = measurementUnits;

                cmbUnit.DisplayMember = "Name";
                cmbUnit.ValueMember = "Id";

            }
            if (productTypes == null)
            {
                ProductTypeService productTypeService = new ProductTypeService();
                productTypes = productTypeService.GetProductTypes();
            }
            
            if (productTypes != null)
            {

                cmbType.DataSource = productTypes;

                cmbType.DisplayMember = "ProductName";
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
            this.Text = "Chỉnh sửa  sản phẩm này";
            this.btnAdd.Text = "Cập nhật";

            loadSomeData();
            ProductService productTypeService = new ProductService();

            product = productTypeService.GetProduct(productId);
            if (product != null)
            {
                if (measurementUnits != null && product.BaseUnit.HasValue)
                {
                    cmbUnit.SelectedIndex = measurementUnits.FindIndex(mu => mu.Id == product.BaseUnit);
                }
                if (productTypes != null)
                {
                    cmbType.SelectedIndex = productTypes.FindIndex(p => p.Id == product.ProductType);
                }
                 
                txtDescription.Text = product.Description;
                txtCode.Text = product.ProductCode;
                txtName.Text = product.ProductName;
            }
        }

        private void chkAuto_CheckedChanged(object sender, EventArgs e)
        {
            txtCode.Enabled = !chkAuto.Checked;
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
            checkboxColumn.HeaderText = "STT";
            checkboxColumn.ValueType = typeof(string);
            //checkboxColumn.Frozen = true;
            dgvBaseAttributes.Columns.Add(checkboxColumn);

            DataGridViewTextBoxColumn attributeNameColumn = new DataGridViewTextBoxColumn();
            attributeNameColumn.Width = 150;
            attributeNameColumn.DataPropertyName = "AttributeName";
            attributeNameColumn.HeaderText = "Tên thuộc tính";
            attributeNameColumn.ValueType = typeof(string);
            //attributeNameColumn.Frozen = true;
            dgvBaseAttributes.Columns.Add(attributeNameColumn);



            DataGridViewTextBoxColumn attributeCodeColumn = new DataGridViewTextBoxColumn();
            attributeCodeColumn.DataPropertyName = "AttributeCode";
            attributeCodeColumn.Width = 120;
            attributeCodeColumn.HeaderText = "Mã Thuộc tính";
            //attributeCodeColumn.Frozen = true;
            attributeCodeColumn.ValueType = typeof(string);
            dgvBaseAttributes.Columns.Add(attributeCodeColumn);

            DataGridViewImageColumn deleteButton = new DataGridViewImageColumn();
            deleteButton.Image = Properties.Resources.erase;
            deleteButton.Width = 100;
            deleteButton.HeaderText = "Xóa";
            //deleteButton.ReadOnly = true;
            deleteButton.ImageLayout = DataGridViewImageCellLayout.Normal;

            DataGridViewTextBoxColumn descriptionColumn = new DataGridViewTextBoxColumn();
            descriptionColumn.Width = 350;// dgvProductAttributeList.Width - attributeNameColumn.Width - attributeCodeColumn.Width;
            descriptionColumn.DataPropertyName = "Description";
            descriptionColumn.HeaderText = "Đặc tả";
            //descriptionColumn.Frozen = true;
            descriptionColumn.ValueType = typeof(string);
            dgvBaseAttributes.Columns.Add(descriptionColumn);

            dgvBaseAttributes.Columns.Add(deleteButton);

        }

        private void dgwOrderDetails_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Có lỗi nhập liệu xảy ra,vui lòng kiểm tra lại!");
        }
    }
}
