using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using BaoHien.Services.ProductAttributes;
using BaoHien.Services.BaseAttributes;
using BaoHien.UI.Base;

namespace BaoHien.UI
{
    public partial class AddProductAttribute : BaseForm
    {
        BaseAttribute baseAttribute;
        public AddProductAttribute()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (validator1.Validate())
            {
                if (baseAttribute != null && baseAttribute.Id > 0)
                {
                    baseAttribute.Description = txtDescription.Text;
                    baseAttribute.AttributeName = txtName.Text;
                    baseAttribute.AttributeCode = txtCode.Text;

                    BaseAttributeService baseAttributeService = new BaseAttributeService();
                    bool result = baseAttributeService.UpdateBaseAttribute(baseAttribute);
                    if (result)
                    {
                        MessageBox.Show("Loại quy cách đã được cập nhật vào hệ thống");
                        ((ProductAttributeList)this.CallFromUserControll).loadProductAttributeList();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                    }
                }
                else
                {
                    baseAttribute = new BaseAttribute
                    {

                        AttributeName = txtName.Text,
                        Description = txtDescription.Text,
                        AttributeCode = txtCode.Text
                    };
                    BaseAttributeService baseAttributeService = new BaseAttributeService();
                    bool result = baseAttributeService.AddBaseAttribute(baseAttribute);
                    if (result)
                    {
                        MessageBox.Show("Quy cách được tạo thành công");
                        ((ProductAttributeList)this.CallFromUserControll).loadProductAttributeList();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                    }
                }
            }
        }
        public void loadDataForEditProductAttribute(int productAttributeId)
        {
            this.Text = "Chỉnh sửa quy cách này";
            this.btnSave.Text = "Cập nhật";

            BaseAttributeService baseAttributeService = new BaseAttributeService();

            baseAttribute = baseAttributeService.GetBaseAttribute(productAttributeId);
            if (baseAttribute != null)
            {
                txtDescription.Text = baseAttribute.Description;
                txtCode.Text = baseAttribute.AttributeCode;
                txtName.Text = baseAttribute.AttributeName;
            }
        }
    }
}
