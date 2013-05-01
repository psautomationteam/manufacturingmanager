using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using BaoHien.Services.SystemUsers;
using BaoHien.Services;
using DAL.Helper;
using BaoHien.Model;
using BaoHien.Services.ProductLogs;
using BaoHien.Common;

namespace BaoHien.UI
{
    public partial class StockEntranceList : UserControl
    {
        List<EntranceStock> entranceStocks;
        List<SystemUser> systemUsers;

        public StockEntranceList()
        {
            InitializeComponent();
        }

        private void loadSomeData()
        {
            SystemUserService systemUserService = new SystemUserService();
            systemUsers = systemUserService.GetSystemUsers();
            systemUsers.Add(new SystemUser() { Id = 0, FullName = "Tất cả" });
            systemUsers = systemUsers.OrderBy(x => x.Id).ToList();

            cbmUsers.DataSource = systemUsers;
            cbmUsers.DisplayMember = "FullName";
            cbmUsers.ValueMember = "Id";
        }

        private void setUpDataGrid(List<EntranceStock> entranceStocks)
        {
            dgwStockEntranceList.AutoGenerateColumns = false;
            if (entranceStocks != null)
            {
                int index = 0;
                var query = from entranceStock in entranceStocks
                            select new
                            {
                                Id = entranceStock.Id,
                                Note = entranceStock.Note,
                                EntranceCode = entranceStock.EntranceCode,
                                EntrancedDate = entranceStock.EntrancedDate.ToString(BHConstant.DATE_FORMAT),
                                EntrancedBy = entranceStock.SystemUser.FullName,
                                Index = ++index
                            };
                dgwStockEntranceList.DataSource = query.ToList();
                productionRequestInTotal.Text = query.Count().ToString();
            }
        }

        private void SetupColumns()
        {
            dgwStockEntranceList.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn indexColumn = new DataGridViewTextBoxColumn();
            indexColumn.Width = 30;
            indexColumn.DataPropertyName = "Index";
            indexColumn.HeaderText = "STT";
            indexColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(indexColumn);

            DataGridViewTextBoxColumn entrancedDateColumn = new DataGridViewTextBoxColumn();
            entrancedDateColumn.DataPropertyName = "EntrancedDate";
            entrancedDateColumn.Width = 100;
            entrancedDateColumn.HeaderText = "Ngày tạo";
            entrancedDateColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(entrancedDateColumn);

            DataGridViewTextBoxColumn entranceCodeColumn = new DataGridViewTextBoxColumn();
            entranceCodeColumn.Width = 100;
            entranceCodeColumn.DataPropertyName = "EntranceCode";
            entranceCodeColumn.HeaderText = "Mã sản phẩm";
            entranceCodeColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(entranceCodeColumn);

            DataGridViewTextBoxColumn entrancedByColumn = new DataGridViewTextBoxColumn();
            entrancedByColumn.Width = 200;
            entrancedByColumn.DataPropertyName = "EntrancedBy";
            entrancedByColumn.HeaderText = "Người tạo";
            entrancedByColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(entrancedByColumn);

            DataGridViewTextBoxColumn noteColumn = new DataGridViewTextBoxColumn();
            noteColumn.DataPropertyName = "Note";
            noteColumn.Width = 400;
            noteColumn.HeaderText = "Chú ý";
            noteColumn.ValueType = typeof(string);
            dgwStockEntranceList.Columns.Add(noteColumn);

            DataGridViewImageColumn deleteButton = new DataGridViewImageColumn();
            deleteButton.Image = Properties.Resources.erase;
            deleteButton.Width = 40;
            deleteButton.HeaderText = "Xóa";
            deleteButton.ReadOnly = true;
            deleteButton.ImageLayout = DataGridViewImageCellLayout.Normal;
            dgwStockEntranceList.Columns.Add(deleteButton);
        }

        public void loadEntranceStockList()
        {
            loadSomeData();
            EntranceStockService entranceStockService = new EntranceStockService();
            List<EntranceStock> entranceStocks = entranceStockService.GetEntranceStocks();
            setUpDataGrid(entranceStocks);
        }

        private void StockEntranceList_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Today.AddDays(-DateTime.Now.Day + 1);
            dtpFrom.CustomFormat = BHConstant.DATE_FORMAT;
            dtpTo.CustomFormat = BHConstant.DATE_FORMAT;
            loadEntranceStockList();
            SetupColumns();
        }

        private void dgwStockEntranceList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender is DataGridView)
            {
                DataGridViewCell cell = ((DataGridView)sender).CurrentCell;
                if (cell.ColumnIndex == ((DataGridView)sender).ColumnCount - 1)
                {
                    DialogResult result = MessageBox.Show("Bạn có muốn xóa sản phẩm này?",
                    "Xoá sản phẩm",
                     MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        DataGridViewRow currentRow = dgwStockEntranceList.Rows[e.RowIndex];

                        EntranceStockService entranceStockService = new EntranceStockService();
                        //Product mu = (Product)dgv.DataBoundItem;
                        int id = ObjectHelper.GetValueFromAnonymousType<int>(currentRow.DataBoundItem, "Id");
                        EntranceStock es = entranceStockService.GetEntranceStock(id);

                        ProductLogService productLogService = new ProductLogService();
                        foreach (EntranceStockDetail esd in es.EntranceStockDetails)
                        {
                            ProductLog pl = productLogService.GetNewestProductUnitLog(esd.ProductId, esd.AttributeId, esd.UnitId);
                            ProductLog plg = new ProductLog
                            {
                                AttributeId = esd.AttributeId,
                                ProductId = esd.ProductId,
                                UnitId = esd.UnitId,
                                RecordCode = es.EntranceCode,
                                BeforeNumber = pl.AfterNumber,
                                Amount = esd.NumberUnit,
                                AfterNumber = (pl.AfterNumber - esd.NumberUnit) > 0 ? (pl.AfterNumber - esd.NumberUnit) : 0,
                                CreatedDate = DateTime.Now
                            };
                            bool ret = productLogService.AddProductLog(plg);
                        }
                        if (!entranceStockService.DeleteEntranceStock(id))
                        {
                            MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");

                        }
                        loadEntranceStockList();
                    }

                }

            }
        }

        private void dgwStockEntranceList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AddEntranceStock frmAddEntranceStock = new AddEntranceStock();
            DataGridViewRow currentRow = dgwStockEntranceList.Rows[e.RowIndex];

            int id = ObjectHelper.GetValueFromAnonymousType<int>(currentRow.DataBoundItem, "Id");
            frmAddEntranceStock.loadDataForEditEntranceStock(id);

            frmAddEntranceStock.CallFromUserControll = this;
            frmAddEntranceStock.ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            EntranceStockSearchCriteria entranceStockSearchCriteria = new EntranceStockSearchCriteria
            {
                Code = txtCode.Text != null ? txtCode.Text : "",
                CreatedBy = (cbmUsers.SelectedValue != null && cbmUsers.SelectedIndex != 0) ? (int?)cbmUsers.SelectedValue : (int?)null,
                From = dtpFrom.Value != null ? dtpFrom.Value : (DateTime?)null,
                To = dtpTo.Value != null ? dtpTo.Value.AddDays(1).Date : (DateTime?)null,
            };
            EntranceStockService entranceStockService = new EntranceStockService();
            List<EntranceStock> entranceStocks = entranceStockService.SearchingEntranceStock(entranceStockSearchCriteria);
            if (entranceStocks == null)
            {
                entranceStocks = new List<EntranceStock>();
            }
            setUpDataGrid(entranceStocks);
        }
    }
}
