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

        private void btnDelete_Click(object sender, EventArgs e)
        {
           DataGridViewSelectedRowCollection selectedRows =  dgvBaseUnitList.SelectedRows;
            
           foreach (DataGridViewRow dgv in selectedRows)
            {
               
               MeasurementUnitService measurementUnitService = new MeasurementUnitService();
               int id = ObjectHelper.GetValueFromAnonymousType<int>(dgv.DataBoundItem, "Id");
               if (!measurementUnitService.DeleteMeasurementUnit(id))
               {
                   MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                   break;
               }

            }
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
                int index = 0;
                var query = from measurementUnit in measurementUnits

                            select new
                            {
                                Name = measurementUnit.Name,
                                Description = measurementUnit.Description,
                                Id = measurementUnit.Id,
                                Index = ++index
                            };
                dgvBaseUnitList.DataSource = query.ToList();

            }
        }

        private void SetupColumns()
        {
            dgvBaseUnitList.AutoGenerateColumns = false;

            dgvBaseUnitList.Columns.Add(Global.CreateCell("Index", "STT", 30));
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
                                MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                            }
                            loadMeasurementUnitList();
                        }
                        else
                        {
                            MessageBox.Show("Đơn vị này đang được sử dụng!");
                        }
                    }

                }

            }
        }
    }
}
