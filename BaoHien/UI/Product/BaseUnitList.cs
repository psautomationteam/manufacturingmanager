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
            loadMeasurementUnitList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
           DataGridViewSelectedRowCollection selectedRows =  dgvBaseUnitList.SelectedRows;
            
           foreach (DataGridViewRow dgv in selectedRows)
            {
               
               MeasurementUnitService measurementUnitService = new MeasurementUnitService();
               MeasurementUnit mu = (MeasurementUnit)dgv.DataBoundItem;
               
               if (!measurementUnitService.DeleteMeasurementUnit(mu.Id))
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
                dgvBaseUnitList.DataSource = measurementUnits;
            }
        }
    }
}
