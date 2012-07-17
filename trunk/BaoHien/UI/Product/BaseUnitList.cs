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
                var query = from measurementUnit in measurementUnits

                            select new
                            {
                                Name = measurementUnit.Name,
                                Description = measurementUnit.Description,
                                Id = measurementUnit.Id,

                            };
                dgvBaseUnitList.DataSource = query.ToList();

            }
        }
        private void SetupColumns()
        {
            dgvBaseUnitList.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.Width = 150;
            nameColumn.DataPropertyName = "Name";
            nameColumn.HeaderText = "Tên đơn vị tính";
            nameColumn.ValueType = typeof(string);
            nameColumn.Frozen = true;
            dgvBaseUnitList.Columns.Add(nameColumn);



            DataGridViewTextBoxColumn descriptionColumn = new DataGridViewTextBoxColumn();
            descriptionColumn.Width = dgvBaseUnitList.Width - nameColumn.Width;
            descriptionColumn.DataPropertyName = "Description";
            descriptionColumn.HeaderText = "Đặc tả";
            descriptionColumn.Frozen = true;
            descriptionColumn.ValueType = typeof(string);
            dgvBaseUnitList.Columns.Add(descriptionColumn);
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
    }
}
