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
using DAL.Helper;

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
            dgvUserList.AutoGenerateColumns = false;
            loadSystemUserList();
            SetupColumns();
        }

        public void loadSystemUserList()
        {
           
            SystemUserService systemUserService = new SystemUserService();
            List<SystemUser> systemUsers = systemUserService.GetSystemUsers();
            if (systemUsers != null)
            {
                //dgvUserList.DataSource = systemUsers;
                setUpDataGrid(systemUsers);
            }
            
        }

        private string getNameForTypeUser(short userTypeId)
        {
            switch (userTypeId)
            {
                case BHConstant.USER_TYPE_ID1:
                    return BHConstant.USER_TYPE_NAME1;
                case BHConstant.USER_TYPE_ID2:
                    return BHConstant.USER_TYPE_NAME2;
                case BHConstant.USER_TYPE_ID3:
                    return BHConstant.USER_TYPE_NAME3;
            }
            return "";
        }

        private void setUpDataGrid(List<SystemUser> systemUsers)
        {
            if (systemUsers != null)
            {
                var query = from user in systemUsers
                            select new
                            {
                                Username = user.Username,
                                FullName = user.FullName,
                                Type = getNameForTypeUser(user.Type),
                                Id = user.Id,
                                Status = user.Status == Constant.ACTIVE_PROPERTY_VALUE?true:false,
                            };
                dgvUserList.DataSource = query.ToList();
                lblTotalResult.Text = systemUsers.Count.ToString();
            }
        }

        private void SetupColumns()
        {
            dgvUserList.AutoGenerateColumns = false;

            dgvUserList.Columns.Add(Global.CreateCell("Username", "Tên đăng nhập", 200));
            dgvUserList.Columns.Add(Global.CreateCell("FullName", "Tên đầy đủ", 200));
            dgvUserList.Columns.Add(Global.CreateCell("Type", "Kiểu người dùng", 200));
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = dgvUserList.SelectedRows;
            foreach (DataGridViewRow dgv in selectedRows)
            {
                SystemUserService systemUserService = new SystemUserService();
                int id = ObjectHelper.GetValueFromAnonymousType<int>(dgv.DataBoundItem, "Id");
                if (!systemUserService.DeleteSystemUser(id))
                {
                    MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
            }
            loadSystemUserList();
        }

        private void dgvUserList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Global.isAdmin())
            {
                AddSystemUser frmAddSystemUser = new AddSystemUser();
                DataGridViewRow currentRow = dgvUserList.Rows[e.RowIndex];

                int id = ObjectHelper.GetValueFromAnonymousType<int>(currentRow.DataBoundItem, "Id");
                frmAddSystemUser.loadDataForEditSystemUser(id);

                frmAddSystemUser.CallFromUserControll = this;
                frmAddSystemUser.ShowDialog();
            }
            else
            {
                MessageBox.Show("Tài khoản quản trị mới xem được thông tin người dùng!");
            }
        }

        private void dgvUserList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView gridView = sender as DataGridView;
            if (null != gridView)
            {
                gridView.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders);
                foreach (DataGridViewRow r in gridView.Rows)
                {
                    gridView.Rows[r.Index].HeaderCell.Value = (r.Index + 1).ToString();
                }
            }
        }
    }
}
