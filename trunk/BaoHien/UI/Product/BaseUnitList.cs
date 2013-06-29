using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaoHien.Services.MeasurementUnits;
using DAL;
using DAL.Helper;
using BaoHien.Services.ProductLogs;
using BaoHien.Common;

namespace BaoHien.UI
{
    public partial class BaseUnitList : UserControl
    {

        public BaseUnitList()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddMeasurementUnit frmAddUnit = new AddMeasurementUnit();
            frmAddUnit.CallFromUserControll = this;
            frmAddUnit.ShowDialog();
        }

        private void BaseUnitList_Load(object sender, EventArgs e)
        {
            SetupColumns();
            loadMeasurementUnitList();
        }
        
        public void loadMeasurementUnitList()
        {
            MeasurementUnitService measurementUnitService = new MeasurementUnitService();
            List<MeasurementUnit> measurementUnits = measurementUnitService.GetMeasurementUnits();
            if (measurementUnits != null)
            {
                setUpDataGrid(measurementUnits);
            }
        }

        private void setUpDataGrid(List<MeasurementUnit> measurementUnits)
        {
            if (measurementUnits != null)
            {
                dgvBaseUnitList.DataSource = measurementUnits.ToList();
                lblTotalResult.Text = measurementUnits.Count.ToString();
            }
        }

        private void SetupColumns()
        {
            dgvBaseUnitList.AutoGenerateColumns = false;

            dgvBaseUnitList.Columns.Add(Global.CreateCell("Name", "Tên đơn vị tính", 220));
            dgvBaseUnitList.Columns.Add(Global.CreateCell("Description", "Đặc tả", 400));
            dgvBaseUnitList.Columns.Add(Global.CreateCellDeleteAction());
        }

        private void dgvBaseUnitList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AddMeasurementUnit frmAddMeasurementUnit = new AddMeasurementUnit();
            DataGridViewRow currentRow = dgvBaseUnitList.Rows[e.RowIndex];

            int id = ObjectHelper.GetValueFromAnonymousType<int>(currentRow.DataBoundItem, "Id");
            frmAddMeasurementUnit.loadDataForEditMeasurementUnit(id);

            frmAddMeasurementUnit.CallFromUserControll = this;
            frmAddMeasurementUnit.ShowDialog();
        }

        private void dgvBaseUnitList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender is DataGridView)
            {
                DataGridViewCell cell = ((DataGridView)sender).CurrentCell;
                if (cell.ColumnIndex == ((DataGridView)sender).ColumnCount - 1)
                {
                    DialogResult result = MessageBox.Show("Bạn có muốn xóa đơn vị tính này?",
                    "Xoá đơn vị tính này",
                     MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        DataGridViewRow currentRow = dgvBaseUnitList.Rows[e.RowIndex];

                        MeasurementUnitService measurementUnitService = new MeasurementUnitService();
                        int id = ObjectHelper.GetValueFromAnonymousType<int>(currentRow.DataBoundItem, "Id");
                        ProductLogService productLogService = new ProductLogService();
                        ProductLog log = productLogService.GetProductLogs().Where(p => p.UnitId == id).FirstOrDefault();
                        if (log == null)
                        {
                            if (!measurementUnitService.DeleteMeasurementUnit(id))
                            {
                                MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            loadMeasurementUnitList();
                        }
                        else
                        {
                            MessageBox.Show("Đơn vị tính này đang được sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                }

            }
        }

        private void dgvBaseUnitList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
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
