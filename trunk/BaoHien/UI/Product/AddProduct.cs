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


namespace BaoHien.UI
{
    public partial class AddProduct : BaseForm
    {
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
            
            Product product = new Product
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
            double price =0;
            Double.TryParse( txtPrice.Text,out price);
            Price newPrice = new Price
            {
                Id = (int)newProductId,
                Price1 = price,
                UpdatedDate = DateTime.Now
            };
            result = priceService.AddPrice(newPrice);
            if (result)
            {
                MessageBox.Show("Sản phẩm đã được tạo thành công");
                ((ProductList)this.CallFromUserControll).loadProductList();
                this.Close();
            }
            else
            {
                MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
            }
        }

        private void AddProduct_Load(object sender, EventArgs e)
        {
            MeasurementUnitService measurementUnitService = new MeasurementUnitService();
            List<MeasurementUnit> measurementUnits = measurementUnitService.GetMeasurementUnits();
            
            if (measurementUnits != null)
            {

                cmbUnit.DataSource = measurementUnits;

                cmbUnit.DisplayMember = "Name";
                cmbUnit.ValueMember = "Id";

            }
            ProductTypeService productTypeService = new ProductTypeService();
            List<ProductType> productTypes = productTypeService.GetProductTypes();
            if (productTypes != null)
            {

                cmbType.DataSource = productTypes;

                cmbType.DisplayMember = "ProductName";
                cmbType.ValueMember = "Id";

            }
            BaseAttributeService baseAttributeService = new BaseAttributeService();
            List<BaseAttribute> baseAttributes = baseAttributeService.GetBaseAttributes();
            if (baseAttributes != null)
            {
                dgvBaseAttributes.DataSource = baseAttributes;
                

               

            }
            
        }
    }
}
