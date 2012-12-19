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
            if (validator1.Validate())
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
                if (systemUser != null)
                {
                    systemUser.FullName = txtName.Text;
                    if (txtPass.Text != "" && txtConfirmPass.Text != "" && txtPass.Text.Equals(txtConfirmPass.Text))
                    {
                        systemUser.password = txtPass.Text;
                    }

                    systemUser.Type = userType;

                    SystemUserService systemUserService = new SystemUserService();
                    if (systemUserService.UpdateSystemUser(systemUser))
                    {
                        MessageBox.Show("Người dùng đã được cập nhật thành công");
                        ((SystemUserList)this.CallFromUserControll).loadSystemUserList();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                    }
                }
                else
                {
                    systemUser = new SystemUser
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

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public void loadDataForEditSystemUser(int systemUserId)
        {
            this.Text = "Chỉnh sửa người dùng này";
            this.btnAdd.Text = "Cập nhật";

            SystemUserService systemUserService = new SystemUserService();

            systemUser = systemUserService.GetSystemUser(systemUserId);

            short userType = systemUser.Type;
            string o = cbUserType.Text;
            if (userType == BHConstant.USER_TYPE_ID1)
            {
                cbUserType.Text = BHConstant.USER_TYPE_NAME1;
            }
            else if (userType == BHConstant.USER_TYPE_ID2)
            {
                cbUserType.Text = BHConstant.USER_TYPE_NAME2;
            }
            else if (userType == BHConstant.USER_TYPE_ID3)
            {
                cbUserType.Text = BHConstant.USER_TYPE_NAME3;
            }

            if (systemUser != null)
            {
                txtName.Text = systemUser.FullName ;
                txtCode.Text = systemUser.username;
                txtCode.Enabled = false;
                txtConfirmPass.Text = "";
                txtPass.Text = "";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
