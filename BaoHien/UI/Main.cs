﻿using System;
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
        private int modeClose = 0;

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
            modeClose = 1;
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
        
        private void menuDBConfig_Click(object sender, EventArgs e)
        {
            DBConfiguration frm = new DBConfiguration();
            pnlMain.Controls.Clear();
            frm.ShowDialog();
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
            pnlMain.Controls.Clear();
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

        private void menuArrear_Click(object sender, EventArgs e)
        {
            ArrearReport arrearReport = new ArrearReport();
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(arrearReport);
        }

        private void menuCommission_Click(object sender, EventArgs e)
        {
            CommissionReport commisionReport = new CommissionReport();
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(commisionReport);
        }

        private void menuAbout_Click(object sender, EventArgs e)
        {
            About frm = new About();
            pnlMain.Controls.Clear();
            frm.ShowDialog();
        }

        #endregion 

        #region toolbar action

        private void tsbAddProduct_Click(object sender, EventArgs e)
        {
            AddProduct frmAddProduct = new AddProduct();
            pnlMain.Controls.Clear();
            frmAddProduct.ShowDialog();
        }

        private void tsbAddCustomer_Click(object sender, EventArgs e)
        {
            AddCustomer frmAddCustomer = new AddCustomer();
            pnlMain.Controls.Clear();
            frmAddCustomer.ShowDialog();
        }

        private void tsbAddOrder_Click(object sender, EventArgs e)
        {
            AddOrder frmAddOrder = new AddOrder();
            pnlMain.Controls.Clear();
            try
            {
                frmAddOrder.ShowDialog();
            }
            catch { }
        }

        private void tsbAddProductionRequest_Click(object sender, EventArgs e)
        {
            AddProductionRequest frmRequest = new AddProductionRequest();
            pnlMain.Controls.Clear();
            frmRequest.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddBill frm = new AddBill();
            pnlMain.Controls.Clear();
            frm.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            AddEntranceStock frm = new AddEntranceStock();
            pnlMain.Controls.Clear();
            frm.ShowDialog();
        }

        private void menuCleaner_Click(object sender, EventArgs e)
        {
            Cleaner.Cleaner frm = new Cleaner.Cleaner();
            pnlMain.Controls.Clear();
            frm.ShowDialog();
        }

        #endregion

        private void Main_Load(object sender, EventArgs e)
        {
            if (!Global.isAdmin())
            {
                menuSystemUser.Visible = false;
                menuCleaner.Visible = false;
            }
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (modeClose == 0)
                Application.Exit();
        }
    }
}
