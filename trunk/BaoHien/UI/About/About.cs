using System;
using System.Windows.Forms;
using BaoHien.Common;
using BaoHien.Services.Employees;
using DAL;
using System.Collections.Generic;
using BaoHien.Services.Orders;

namespace BaoHien.UI
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            lbVersion.Text = BHConstant.BUILD_VERSION;
            lbReleaseDate.Text = BHConstant.BUILD_RELEASE_DATE;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbAbout_DoubleClick(object sender, EventArgs e)
        {
            EmployeeLogService els = new EmployeeLogService();
            List<EmployeeLog> el_list = els.GetEmployeeLogs();
            OrderService os = new OrderService();
        }
    }
}
