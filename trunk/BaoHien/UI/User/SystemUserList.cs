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
            if (!Global.isAdmin())
            {
                btnAdd.Visible = btnDelete.Visible = false;
            }
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
                int index = 0;
                var query = from user in systemUsers

                            select new
                            {
                                username = user.username,
                                FullName = user.FullName,
                                Type = getNameForTypeUser(user.Type),
                                Id = user.Id,
                                Status = user.Status == Constant.ACTIVE_PROPERTY_VALUE?true:false,
                                Index = ++index
                            };
                dgvUserList.DataSource = query.ToList();
                
            }
        }

        private void SetupColumns()
        {
            dgvUserList.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn indexColumn = new DataGridViewTextBoxColumn();
            indexColumn.Width = 30;
            indexColumn.DataPropertyName = "Index";
            indexColumn.HeaderText = "STT";
            indexColumn.ValueType = typeof(string);
            //indexColumn.Frozen = true;
            dgvUserList.Columns.Add(indexColumn);

            DataGridViewTextBoxColumn productNameColumn = new DataGridViewTextBoxColumn();
            productNameColumn.Width = 150;
            productNameColumn.DataPropertyName = "username";
            productNameColumn.HeaderText = "Tên đăng nhập";
            productNameColumn.ValueType = typeof(string);
            //productNameColumn.Frozen = true;
            dgvUserList.Columns.Add(productNameColumn);

            DataGridViewTextBoxColumn typeCodeColumn = new DataGridViewTextBoxColumn();
            typeCodeColumn.DataPropertyName = "FullName";
            typeCodeColumn.Width = 150;
            typeCodeColumn.HeaderText = "Tên đầy đủ";
            //typeCodeColumn.Frozen = true;
            typeCodeColumn.ValueType = typeof(string);
            dgvUserList.Columns.Add(typeCodeColumn);
            
            DataGridViewTextBoxColumn descriptionColumn = new DataGridViewTextBoxColumn();
            descriptionColumn.Width = 320;// dgvUserList.Width - productNameColumn.Width - typeCodeColumn.Width;
            descriptionColumn.DataPropertyName = "Type";
            descriptionColumn.HeaderText = "Kiểu người dùng";
            //descriptionColumn.Frozen = true;
            descriptionColumn.ValueType = typeof(string);
            dgvUserList.Columns.Add(descriptionColumn);

            //DataGridViewCheckBoxColumn deleteButton = new DataGridViewCheckBoxColumn();
            //deleteButton.DataPropertyName = "Status";
            //deleteButton.Width = 100;
            //deleteButton.HeaderText = "Kích hoạt/Bỏ";
            //deleteButton.FalseValue = false;
            //deleteButton.TrueValue = true;
            //deleteButton.ValueType = typeof(bool);
            //dgvUserList.Columns.Add(deleteButton);
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

        private void dgvUserList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (sender is DataGridView)
            //{
            //    DataGridViewCell cell = ((DataGridView)sender).CurrentCell;
            //    if (cell.ColumnIndex == ((DataGridView)sender).ColumnCount - 1)
            //    {
            //        DataGridViewCheckBoxCell checkBoxcell = (DataGridViewCheckBoxCell)cell;
            //        DataGridViewRow currentRow = dgvUserList.Rows[e.RowIndex];

            //        SystemUserService systemUserService = new SystemUserService();
            //        int id = ObjectHelper.GetValueFromAnonymousType<int>(currentRow.DataBoundItem, "Id");
            //        SystemUser user = systemUserService.GetSystemUser(id);
            //        if (checkBoxcell.Value != null && checkBoxcell.Value.ToString().Equals(bool.TrueString))
            //        {
            //            user.Status = Constant.ACTIVE_PROPERTY_VALUE;
            //        }
            //        else
            //        {
            //            user.Status = Constant.DEACTIVE_PROPERTY_VALUE;
            //        }
            //        bool result = systemUserService.UpdateSystemUser(user);
            //        if (!result)
            //        {
            //            MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
            //        }
            //        else
            //        {
            //            MessageBox.Show("Thực hiện thành công!");
            //            loadSystemUserList();
            //        }
            //    }

            //}
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
    }
}
