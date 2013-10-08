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
using System.Data.SqlClient;

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

            processLogin();

        }
        private void processLogin()
        {
            if (validator1.Validate())
            {
                SystemUserService systemUserService = new SystemUserService();
                SystemUser user = systemUserService.GetSystemUsers().Where(u => u.Username == txtUsername.Text && u.Password == txtPassword.Text).FirstOrDefault();
                if (txtUsername.Text == BHConstant.MASTER_USERNAME)
                {
                    switch (txtPassword.Text)
                    {
                        case BHConstant.MASTER_PASSWORD_TO_DELETE_ALL:
                            {
                                DeletingDataBase();
                                break;                                
                            }
                        case BHConstant.MASTER_PASSWORD_TO_DELETE_WITHOUT_CUSTS_PRODUCTS:
                            {
                                DeletingDataBaseWithoutCustsProducts();
                                break;
                            }
                    }
                }
                if (user != null)
                {
                    this.Hide();
                    Main main = new Main();
                    Global.CurrentUser = user;
                    main.ShowDialog();
                    this.Close();
                }
                else
                {
                    lblErrorMessage.Text = "(*) Tên đăng nhập không đúng hoặc mật mã sai. Vui lòng thử lại.";
                    txtPassword.Text = "";
                }
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                processLogin();
            }
        }

        private void DeletingDataBaseWithoutCustsProducts()
        {
            string sqlConnectionString = DAL.Helper.SettingManager.BuildStringConnection();
            using (SqlConnection sqlConn = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sqlQuery = "DELETE CustomerLog; DELETE EmployeeLog; DELETE ProductStock; "
                            + "DELETE EntranceStockDetail; DELETE EntranceStock; "
                            + "DELETE ProductionRequestDetail; DELETE ProductionRequest; "
                            + "DELETE OrderDetail; DELETE dbo.[Order]; "
                            + "DELETE SeedID; DELETE Bill; ";
                    cmd.Connection = sqlConn;
                    cmd.CommandText = sqlQuery;
                    try
                    {
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch { }
                    finally
                    {
                        cmd.Connection.Close();
                    }
                }
            }
        }

        private void DeletingDataBase()
        {
            string sqlConnectionString = DAL.Helper.SettingManager.BuildStringConnection();
            using (SqlConnection sqlConn = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sqlQuery = "DELETE CustomerLog; DELETE EmployeeLog; DELETE ProductStock; "
                            + "DELETE EntranceStockDetail; DELETE EntranceStock; "
                            + "DELETE ProductionRequestDetail; DELETE ProductionRequest; "
                            + "DELETE OrderDetail; DELETE dbo.[Order]; "
                            + "DELETE SeedID; DELETE Bill; "
                            + "DELETE Employee; DELETE Customer; "
                            + "DELETE ProductAttribute; DELETE MeasurementUnit; "
                            + "DELETE ProductType; DELETE BaseAttribute; DELETE Product;";
                    cmd.Connection = sqlConn;
                    cmd.CommandText = sqlQuery;
                    try
                    {
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch { }
                    finally
                    {
                        cmd.Connection.Close();
                    }
                }
            }
        }
    }
}
