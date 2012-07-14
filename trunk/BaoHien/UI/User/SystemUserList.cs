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
            SetupColumns();
            loadSystemUserList();
        }
        public void loadSystemUserList()
        {
           
            SystemUserService systemUserService = new SystemUserService();
            List<SystemUser> systemUsers = systemUserService.GetSystemUsers();
            if (systemUsers != null)
            {
                dgvUserList.DataSource = systemUsers;
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
                                username = user.username,
                                FullName = user.FullName,
                                Type = getNameForTypeUser(user.Type),
                                Id = user.Id,
                                Status = user.Status
                                
                            };
                dgvUserList.DataSource = query.ToList();
                
            }
        }
        private void SetupColumns()
        {
            dgvUserList.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn productNameColumn = new DataGridViewTextBoxColumn();
            productNameColumn.Width = 150;
            productNameColumn.DataPropertyName = "username";
            productNameColumn.HeaderText = "Tên đăng nhập";
            productNameColumn.ValueType = typeof(string);
            productNameColumn.Frozen = true;
            dgvUserList.Columns.Add(productNameColumn);



            DataGridViewTextBoxColumn typeCodeColumn = new DataGridViewTextBoxColumn();
            typeCodeColumn.DataPropertyName = "FullName";
            typeCodeColumn.Width = 150;
            typeCodeColumn.HeaderText = "Tên đầy đủ";
            typeCodeColumn.Frozen = true;
            typeCodeColumn.ValueType = typeof(string);
            dgvUserList.Columns.Add(typeCodeColumn);

            DataGridViewTextBoxColumn descriptionColumn = new DataGridViewTextBoxColumn();
            descriptionColumn.Width = dgvUserList.Width - productNameColumn.Width - typeCodeColumn.Width;
            descriptionColumn.DataPropertyName = "Type";
            descriptionColumn.HeaderText = "Kiểu người dùng";
            descriptionColumn.Frozen = true;
            descriptionColumn.ValueType = typeof(string);
            dgvUserList.Columns.Add(descriptionColumn);
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
                    MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                    break;
                }

            }
            loadSystemUserList();
        }
    }
}
