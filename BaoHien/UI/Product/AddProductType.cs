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
        ProductType productType;
        public AddProductType()
        {
            InitializeComponent();
        }

        private void AddProductType_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (validator1.Validate())
            {
                if (productType != null && productType.Id > 0)//edit
                {
                    productType.Description = txtDescription.Text;
                    productType.ProductName = txtName.Text;
                    productType.TypeCode = txtCode.Text;

                    ProductTypeService productTypeService = new ProductTypeService();
                    bool result = productTypeService.UpdateProductType(productType);
                    if (result)
                    {
                        MessageBox.Show("Loại sản phẩm đã được cập nhật vào hệ thống");
                        ((ucProductType)this.CallFromUserControll).loadProductTypeList();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                    }
                }
                else//add new
                {
                    productType = new ProductType
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
        public void loadDataForEditProductType(int productTypeId)
        {
            this.Text = "Chỉnh sửa loại sản phẩm này";
            this.btnAdd.Text = "Cập nhật";

            ProductTypeService productTypeService = new ProductTypeService();
           
           productType = productTypeService.GetProductType(productTypeId);
            if (productType != null)
            {
                txtDescription.Text = productType.Description;
                txtCode.Text = productType.TypeCode;
                txtName.Text = productType.ProductName;
            }
        }
    }
}
