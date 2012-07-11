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

namespace BaoHien.UI
{
    public partial class AddProductAttribute : Form
    {
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
            BaseAttribute baseAttribute = new BaseAttribute
            {
                
                AttributeName = txtName.Text,
                Description = txtDescription.Text,
                AttributeCode = txtCode.Text
            };
            BaseAttributeService baseAttributeService = new BaseAttributeService();
            bool result = baseAttributeService.AddBaseAttribute(baseAttribute);
            if (result)
            {
                MessageBox.Show("Attribute added successfully");
                this.Close();
            }
            else
            {
                MessageBox.Show("Opps! Something wrong!");
            }
        }
    }
}
