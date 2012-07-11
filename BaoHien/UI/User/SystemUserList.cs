using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaoHien.UI
{
    public partial class SystemUserList : UserControl
    {
        public SystemUserList()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddSystemUser frmAddUser = new AddSystemUser();
            frmAddUser.ShowDialog();
        }
    }
}
