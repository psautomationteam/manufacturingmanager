using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaoHien.Properties;
using DAL.Helper;
using BaoHien.Common;
using BaoHien.Services.SystemUsers;
using DAL;

namespace BaoHien.UI
{
    public partial class DBConfiguration : Form
    {
        public DBConfiguration()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BaoHienRepository.testDBConnection(txtIP.Text, BHConstant.DATABASE_NAME, txtUsername.Text, txtPass.Text))
            {

                SettingManager.UpdateSetting(txtIP.Text, BHConstant.DATABASE_NAME, txtUsername.Text, txtPass.Text);
                BaoHienRepository.ResetDBDataContext();
                MessageBox.Show("Cơ sở dữ liệu đã được chuyển");
                this.Hide();
                if (Global.CurrentUser == null)
                {
                    MessageBox.Show("Cơ sở dữ liệu đã được chuyển");
                    SettingManager.UpdateSetting(txtIP.Text, BHConstant.DATABASE_NAME, txtUsername.Text, txtPass.Text);
                    
                    //Application.Run(new Login());
                    Login frmLogin = new Login();
                    frmLogin.ShowDialog();
                    
                }
                else
                {
                    SystemUserService systemUserService = new SystemUserService();
                    SystemUser user = systemUserService.GetSystemUsers().Single(u => (u.username == Global.CurrentUser.username) && (u.password == Global.CurrentUser.password));
                    if (user == null)
                    {
                        MessageBox.Show("Cần đăng nhập lại!");
                        
                        //Application.Run(new Login());
                        Login frmLogin = new Login();
                        frmLogin.ShowDialog();
                        
                    }
                    else
                    {
                        Global.CurrentUser = user;
                        
                    }
                   
                    
                }

                this.Close();
               
            }
            else
            {
                
                MessageBox.Show("Không thể kết nối cơ sở dữ liệu");
               
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (BaoHienRepository.testDBConnection(txtIP.Text, BHConstant.DATABASE_NAME, txtUsername.Text, txtPass.Text))
            {
                MessageBox.Show("Cơ sở dữ liệu kết nối thành công!");
            }
            else
            {
                MessageBox.Show("Không thể kết nối cơ sở dữ liệu");
            }

        }
    }
}
