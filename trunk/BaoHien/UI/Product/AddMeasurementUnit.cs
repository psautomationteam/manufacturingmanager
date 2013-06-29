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
        MeasurementUnit measurementUnit;
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
            if (validator1.Validate())
            {
                if (measurementUnit != null && measurementUnit.Id > 0)
                {
                    measurementUnit.Description = txtDescription.Text;
                    measurementUnit.Name = txtName.Text;
                    measurementUnit.UnitCode = txtCode.Text;

                    MeasurementUnitService measurementUnitService = new MeasurementUnitService();
                    bool result = measurementUnitService.UpdateMeasurementUnit(measurementUnit);
                    if (result)
                    {
                        MessageBox.Show("Loại đơn vị đã được cập nhật vào hệ thống");
                        ((BaseUnitList)this.CallFromUserControll).loadMeasurementUnitList();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    measurementUnit = new MeasurementUnit
                    {

                        Description = txtDescription.Text,
                        Name = txtName.Text,
                        UnitCode = txtCode.Text
                    };
                    MeasurementUnitService measurementUnitService = new MeasurementUnitService();
                    bool result = measurementUnitService.AddMeasurementUnit(measurementUnit);
                    if (result)
                    {
                        MessageBox.Show("Đơn vị tính đã được thêm mới vào hệ thống");
                        ((BaseUnitList)this.CallFromUserControll).loadMeasurementUnitList();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            
            
        }
        public void loadDataForEditMeasurementUnit(int measurementUnitId)
        {
            this.Text = "Chỉnh sửa đơn vị này phẩm này";
            this.btnSave.Text = "Cập nhật";

            MeasurementUnitService measurementUnitService = new MeasurementUnitService();

            measurementUnit = measurementUnitService.GetMeasurementUnit(measurementUnitId);
            if (measurementUnit != null)
            {
                txtDescription.Text = measurementUnit.Description;
                txtCode.Text = measurementUnit.UnitCode;
                txtName.Text = measurementUnit.Name;
            }
        }
    }
}
