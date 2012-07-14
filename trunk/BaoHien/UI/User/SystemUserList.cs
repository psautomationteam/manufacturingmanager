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
            frmAddUser.CallFromUserControll = this;
            frmAddUser.ShowDialog();
        }

        private void SystemUserList_Load(object sender, EventArgs e)
        {
            loadSystemUserList();
        }
        public void loadSystemUserList()
        {
            SystemUserService systemUserService = new SystemUserService();
            List<SystemUser> systemUsers = systemUserService.GetSystemUsers();
            if (systemUsers != null)
            {
                dgvUserList.DataSource = systemUsers;
                
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = dgvUserList.SelectedRows;

            foreach (DataGridViewRow dgv in selectedRows)
            {

                SystemUserService systemUserService = new SystemUserService();
                SystemUser mu = (SystemUser)dgv.DataBoundItem;
                if (!systemUserService.DeleteSystemUser(mu.Id))
                {
                    MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                    break;
                }

            }
            loadSystemUserList();
        }
    }
}
