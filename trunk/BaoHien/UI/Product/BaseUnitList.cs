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
                int index = 0;
                var query = from measurementUnit in measurementUnits

                            select new
                            {
                                Name = measurementUnit.Name,
                                Description = measurementUnit.Description,
                                Id = measurementUnit.Id,
                                Index = index++
                            };
                dgvBaseUnitList.DataSource = query.ToList();

            }
        }
        private void SetupColumns()
        {
            dgvBaseUnitList.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn indexColumn = new DataGridViewTextBoxColumn();
            indexColumn.Width = 30;
            indexColumn.DataPropertyName = "Index";
            indexColumn.HeaderText = "STT";
            indexColumn.ValueType = typeof(string);
            indexColumn.Frozen = true;
            dgvBaseUnitList.Columns.Add(indexColumn);

            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.Width = 220;
            nameColumn.DataPropertyName = "Name";
            nameColumn.HeaderText = "Tên đơn vị tính";
            nameColumn.ValueType = typeof(string);
            nameColumn.Frozen = true;
            dgvBaseUnitList.Columns.Add(nameColumn);

            DataGridViewImageColumn deleteButton = new DataGridViewImageColumn();
            deleteButton.Image = Properties.Resources.erase;
            deleteButton.Width = 100;
            deleteButton.HeaderText = "Xóa";
            deleteButton.ReadOnly = true;
            deleteButton.ImageLayout = DataGridViewImageCellLayout.Normal;

            DataGridViewTextBoxColumn descriptionColumn = new DataGridViewTextBoxColumn();
            descriptionColumn.Width = 400;// dgvBaseUnitList.Width - nameColumn.Width - deleteButton.Width;
            descriptionColumn.DataPropertyName = "Description";
            descriptionColumn.HeaderText = "Đặc tả";
            descriptionColumn.Frozen = true;
            descriptionColumn.ValueType = typeof(string);

            dgvBaseUnitList.Columns.Add(descriptionColumn);
            dgvBaseUnitList.Columns.Add(deleteButton);
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
                    DialogResult result = MessageBox.Show("Bạn có muốn xóa loại sản phẩm này?",
                    "Xoá loại sản phẩm này",
                     MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        DataGridViewRow currentRow = dgvBaseUnitList.Rows[e.RowIndex];

                        MeasurementUnitService measurementUnitService = new MeasurementUnitService();
                        int id = ObjectHelper.GetValueFromAnonymousType<int>(currentRow.DataBoundItem, "Id");
                        if (!measurementUnitService.DeleteMeasurementUnit(id))
                        {
                            MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                           
                        }
                        loadMeasurementUnitList();
                    }

                }

            }
        }
    }
}
