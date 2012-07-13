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
using BaoHien.UI.Base;

namespace BaoHien.UI
{
    public partial class AddProductType : BaseForm
    {
        public AddProductType()
        {
            InitializeComponent();
        }

        private void AddProductType_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductType productType = new ProductType
            {

                Description = txtDescription.Text,
                ProductName = txtName.Text,
                TypeCode = txtCode.Text
            };
            ProductTypeService productTypeService = new ProductTypeService();
            bool result = productTypeService.AddProductType(productType);
            if (result)
            {
                MessageBox.Show("Loại sản phẩm đã được thêm mới vào hệ thống");
                ((ucProductType)this.CallFromUserControll).loadProductTypeList();
                this.Close();
            }
            else
            {
                MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
            }
        }        
    }
}
