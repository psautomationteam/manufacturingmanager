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
            txtIP.Text = BHConstant.INIT_IP;
            txtPort.Text = BHConstant.INIT_PORT;
            txtNet.Text = BHConstant.INIT_NETWORK_LIBRARY;
            txtDataName.Text = BHConstant.INIT_DATABASE_NAME;
            txtUsername.Text = BHConstant.INIT_USER_ID;
            txtPass.Text = BHConstant.INIT_PW;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BaoHienRepository.testDBConnection(txtIP.Text, txtPort.Text, txtNet.Text, txtDataName.Text,
                txtUsername.Text, txtPass.Text))
            {

                SettingManager.UpdateRegistry(txtIP.Text, txtPort.Text, txtNet.Text, txtDataName.Text,
                    txtUsername.Text, txtPass.Text);
                BaoHienRepository.ResetDBDataContext();
                MessageBox.Show("Cơ sở dữ liệu đã được chuyển");
                this.Hide();
                SystemUser user = Global.CurrentUser;
                if (user != null)
                {
                    SystemUserService systemUserService = new SystemUserService();
                    user = systemUserService.GetSystemUsers().Single(u => (u.Username == Global.CurrentUser.Username) && (u.Password == Global.CurrentUser.Password));
                }
                if (user == null)
                {
                    Login frmLogin = new Login();
                    frmLogin.ShowDialog();
                }
                else
                {
                    Global.CurrentUser = user;
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
            if (BaoHienRepository.testDBConnection(txtIP.Text, txtPort.Text, txtNet.Text, txtDataName.Text,
                txtUsername.Text, txtPass.Text))
            {
                MessageBox.Show("Cơ sở dữ liệu kết nối thành công!");
            }
            else
            {
                MessageBox.Show("Không thể kết nối cơ sở dữ liệu");
            }

        }

        private void DBConfiguration_Load(object sender, EventArgs e)
        {
            ModifyRegistry reg = new ModifyRegistry();

            txtIP.Text = String.IsNullOrEmpty(reg.Read("DBServerName")) ? BHConstant.INIT_IP : reg.Read("DBServerName");
            txtPort.Text = String.IsNullOrEmpty(reg.Read("Port")) ? BHConstant.INIT_PORT : reg.Read("Port");
            txtNet.Text = String.IsNullOrEmpty(reg.Read("NetworkLibrary")) ? BHConstant.INIT_NETWORK_LIBRARY : reg.Read("NetworkLibrary");
            txtDataName.Text = String.IsNullOrEmpty(reg.Read("DatabaseName")) ? BHConstant.INIT_DATABASE_NAME : reg.Read("DatabaseName");
            txtUsername.Text = String.IsNullOrEmpty(reg.Read("DatabaseUserID")) ? BHConstant.INIT_USER_ID : reg.Read("DatabaseUserID");
            txtPass.Text = String.IsNullOrEmpty(reg.Read("DatabasePwd")) ? BHConstant.INIT_PW : reg.Read("DatabasePwd");
        }
    }
}
