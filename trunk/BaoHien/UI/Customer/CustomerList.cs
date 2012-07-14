using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaoHien.Services.SystemUsers;
using DAL;
using BaoHien.Common;

namespace BaoHien.UI
{
    public partial class CustomerList : UserControl
    {
        public CustomerList()
        {
            InitializeComponent();
        }

        private void CustomerList_Load(object sender, EventArgs e)
        {
            SystemUserService systemUserService = new SystemUserService();
            List<SystemUser> systemUsers = systemUserService.SelectSystemUserByWhere(su => su.Type == BHConstant.USER_TYPE_ID3);
            if (systemUsers != null)
            {
                cmbSaler.DataSource = systemUsers;

                cmbSaler.DisplayMember = "FullName";
                cmbSaler.ValueMember = "Id";

            }

            
        }
    }
}
