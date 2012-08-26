using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using BaoHien.Common;
using BaoHien.Services.SystemUsers;
using BaoHien.Properties;

namespace BaoHien.UI
{
    public partial class Main : Form
    {
        
        public Main()
        {
            InitializeComponent();
        }

        #region Menu Navigation

        private void menuProductType_Click(object sender, EventArgs e)
        {
            ucProductType productType = new ucProductType();
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(productType);
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form main = new Login();
            main.ShowDialog();
            this.Close();
        }

        private void menuProduct_Click(object sender, EventArgs e)
        {
            ProductList productList = new ProductList();
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(productList);
        }


        private void menuMeasurementUnit_Click(object sender, EventArgs e)
        {
            BaseUnitList ucBaseUnitList = new BaseUnitList();
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(ucBaseUnitList);
        }

        private void menuAttribute_Click(object sender, EventArgs e)
        {
            ProductAttributeList ucAttributeList = new ProductAttributeList();
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(ucAttributeList);
        }

        private void menuCustomer_Click(object sender, EventArgs e)
        {
            CustomerList ucCustomerlist = new CustomerList();
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(ucCustomerlist);
        }

        private void menuSystemUser_Click(object sender, EventArgs e)
        {
            SystemUserList ucSystemUser = new SystemUserList();
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(ucSystemUser);
        }

        private void menuChangePass_Click(object sender, EventArgs e)
        {
            ChangePassword frmChangePass = new ChangePassword();
            frmChangePass.ShowDialog();

        }

        private void menuEmployee_Click(object sender, EventArgs e)
        {
            EmployeeList ucEmLst = new EmployeeList();
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(ucEmLst);
        }

        private void menuOrder_Click(object sender, EventArgs e)
        {
            OrderList ucOrderList = new OrderList();
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(ucOrderList);
        }

        private void menuProductionRequest_Click(object sender, EventArgs e)
        {
            ProductionRequestList ucRequestList = new ProductionRequestList();
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(ucRequestList);
        }

        private void menuStockIn_Click(object sender, EventArgs e)
        {
            StockEntranceList ucStockList = new StockEntranceList();
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(ucStockList);
        }

        private void menuBilling_Click(object sender, EventArgs e)
        {
            BillList ucBillList = new BillList();
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(ucBillList);
        }

        private void menuInventory_Click(object sender, EventArgs e)
        {
            ProductAndMaterialReport ucReport = new ProductAndMaterialReport();
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(ucReport);
        }
        #endregion 

#region toolbar action
        private void tsbAddProduct_Click(object sender, EventArgs e)
        {
            AddProduct frmAddProduct = new AddProduct();
            frmAddProduct.ShowDialog();

        }

        private void tsbAddCustomer_Click(object sender, EventArgs e)
        {
            AddCustomer frmAddCustomer = new AddCustomer();
            frmAddCustomer.ShowDialog();
        }

        private void tsbAddOrder_Click(object sender, EventArgs e)
        {
            AddOrder frmAddOrder = new AddOrder();
            frmAddOrder.ShowDialog();
        }

        private void tsbAddProductionRequest_Click(object sender, EventArgs e)
        {
            AddProductionRequest frmRequest = new AddProductionRequest();
            frmRequest.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddBill frm = new AddBill();
            frm.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            AddEntranceStock frm = new AddEntranceStock();
            frm.ShowDialog();
        }

#endregion

        private void Main_Load(object sender, EventArgs e)
        {
            Settings.Default.FirstRun = 1;
            Settings.Default.Save();
            if (Settings.Default.FirstRun == 1)
            {
                DBConfiguration dBConfiguration = new DBConfiguration();
                dBConfiguration.Show();
            }
        }

        private void menuDBConfig_Click(object sender, EventArgs e)
        {
            DBConfiguration frm = new DBConfiguration();
            frm.ShowDialog();
        }

      
        

       
        

       

       
        

        

        

        

        

        

        

        

        

        

        

        

       

    }
}
