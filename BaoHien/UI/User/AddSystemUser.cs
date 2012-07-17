using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaoHien.Services.SystemUsers;
using DAL;
using BaoHien.Common;
using BaoHien.UI.Base;

namespace BaoHien.UI
{
    public partial class AddSystemUser : BaseForm
    {
        SystemUser systemUser;
        public AddSystemUser()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            short userType = 0;
            string o = cbUserType.Text;
            if (o != null && o != "" && o != " ")
            {
                if (o.Contains(BHConstant.USER_TYPE_NAME1))
                {
                    userType = BHConstant.USER_TYPE_ID1;

                }
                else if (o.Contains(BHConstant.USER_TYPE_NAME2))
                {
                    userType = BHConstant.USER_TYPE_ID2;
                }
                else if (o.Contains(BHConstant.USER_TYPE_NAME3))
                {
                    userType = BHConstant.USER_TYPE_ID3;
                }
            }
            SystemUser systemUser = new SystemUser
            {
                FullName = txtName.Text,
                password = txtPass.Text,
                Type = userType,
                username = txtCode.Text
            };
            SystemUserService systemUserService = new SystemUserService();
            if (systemUserService.AddPSystemUser(systemUser))
            {
                MessageBox.Show("Người dùng đã được tạo thành công");
                ((SystemUserList)this.CallFromUserControll).loadSystemUserList();
                this.Close();
            }
            else
            {
                MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
