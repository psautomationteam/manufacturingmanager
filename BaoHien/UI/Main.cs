using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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

        #endregion 

#region toolbar action
        private void tsbAddProduct_Click(object sender, EventArgs e)
        {
            AddProduct frmAddProduct = new AddProduct();
            frmAddProduct.ShowDialog();

        }

#endregion

        

        

        

       

    }
}
