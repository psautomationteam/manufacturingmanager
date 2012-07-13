using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using BaoHien.Services.MeasurementUnits;
using BaoHien.UI.Base;

namespace BaoHien.UI
{
    public partial class AddMeasurementUnit : BaseForm
    {
        public AddMeasurementUnit()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MeasurementUnit productType = new MeasurementUnit
            {
                
                Description = txtDescription.Text,
                Name = txtName.Text,
                UnitCode = txtCode.Text
            };
            MeasurementUnitService measurementUnitService = new MeasurementUnitService();
            bool result = measurementUnitService.AddMeasurementUnit(productType);
            if (result)
            {
                MessageBox.Show("Đơn vị tính đã được thêm mới vào hệ thống");
                ((BaseUnitList)this.CallFromUserControll).loadMeasurementUnitList();
                this.Close();
            }
            else
            {
                MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
            }
        }
    }
}
