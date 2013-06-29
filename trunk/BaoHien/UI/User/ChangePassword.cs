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

namespace BaoHien.UI
{
    public partial class ChangePassword : Form
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (validator1.Validate())
            {
                if (txtNewPass.Text != txtConfirmPass.Text)
                {
                    MessageBox.Show("Mật khẩu mới không khớp với nhau!");
                    txtNewPass.Text = "";
                    txtCurrentPass.Text = "";
                }
                else
                {
                    SystemUserService systemUserService = new SystemUserService();
                    SystemUser user = systemUserService.GetSystemUsers().Where(u => u.Username == Global.CurrentUser.Username && u.Password == txtCurrentPass.Text).FirstOrDefault();
                    if (user != null)
                    {
                        user.Password = txtNewPass.Text;
                        bool result = systemUserService.UpdateSystemUser(user);
                        if (result)
                        { 
                            MessageBox.Show("Mật khẩu đã được thay đổi thành công!");
                        }
                        else
                        {
                            MessageBox.Show("Hệ thống đang có lỗi. Vui lòng thử lại sau!");
                        }
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu hiện tại không đúng!");
                        txtNewPass.Text = "";
                        txtCurrentPass.Text = "";
                    }
                }
            }
        }
    }
}
