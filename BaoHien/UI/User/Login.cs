using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using BaoHien.Services.SystemUsers;
using BaoHien.Common;

namespace BaoHien.UI
{
    public partial class Login : Form
    {
        private const string DUMMY_USERNAME = "baohien";
        private const string DUMMY_PASSWORD = "baohien123A";//for demo purpose only, will encode MD5 for this field
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == DUMMY_USERNAME && txtPassword.Text == DUMMY_PASSWORD)
            {
                SystemUserService systemUserService = new SystemUserService();
                //SystemUser user = systemUserService.GetSystemUsers().Single(u => (u.username == DUMMY_USERNAME) && (u.password == DUMMY_PASSWORD));
                SystemUser user = systemUserService.GetSystemUser(1);
                this.Hide();
                Main main = new Main();
                Global.CurrentUser = user;
                main.ShowDialog();
                this.Close();
            }
            else
            {
                lblErrorMessage.Text = "(*) Tên đăng nhập không đúng hoặc mật mã sai. Vui long thử lại.";
                txtPassword.Text = "";
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
                
    }
}
