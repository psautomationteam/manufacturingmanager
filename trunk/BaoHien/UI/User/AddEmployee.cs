using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaoHien.UI.Base;
using DAL;
using BaoHien.Services.Employees;

namespace BaoHien.UI
{
    public partial class AddEmployee : BaseForm
    {
        Employee employee;

        public AddEmployee()
        {
            InitializeComponent();
        }

        public void loadDataForEditEmployee(int employeeId)
        {
            this.Text = "Chỉnh sửa thông tin nhân viên";
            this.btnSave.Text = "Cập nhật";

            EmployeeService employeeService = new EmployeeService();

            employee = employeeService.GetEmployee(employeeId);
            if (employee != null)
            {
                txtAddress.Text = employee.Address ;
                txtDescription.Text = employee.Description;
                txtEmail.Text = employee.Email;
                txtFullName.Text = employee.FullName;
                txtMobilePhone.Text = employee.MobilePhone;
                txtNickName.Text = employee.NickName ;
                txtEmployeeCode.Text = employee.Code;
                cbType.SelectedIndex = (int)employee.Type;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (validator1.Validate())
            {
                if (employee != null && employee.Id > 0)//edit
                {
                    employee.Address = txtAddress.Text;
                    employee.Description = txtDescription.Text;
                    employee.Email = txtEmail.Text;
                    employee.FullName = txtFullName.Text;
                    employee.MobilePhone = txtMobilePhone.Text;
                    employee.NickName = txtNickName.Text;
                    employee.Code = txtEmployeeCode.Text;
                    employee.Type = cbType.SelectedValue != null ? (short)cbType.SelectedValue : (short)0;

                    EmployeeService employeeService = new EmployeeService();
                    bool result = employeeService.UpdateEmployee(employee);
                    if (result)
                    {
                        MessageBox.Show("Thông tin nhân viên đã được cập nhật vào hệ thống");
                        ((EmployeeList)this.CallFromUserControll).loadEmployeeList();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else//add new
                {
                    employee = new Employee
                    {
                        Address = txtAddress.Text,
                        Description = txtDescription.Text,
                        Email = txtEmail.Text,
                        FullName = txtFullName.Text,
                        MobilePhone = txtMobilePhone.Text,
                        NickName = txtNickName.Text,
                        Code = txtEmployeeCode.Text,
                        Type = cbType.SelectedValue != null ? (short)cbType.SelectedValue : (short)0
                    };
                    EmployeeService employeeService = new EmployeeService();
                    bool result = employeeService.AddEmployee(employee);
                    if (result)
                    {
                        MessageBox.Show("Nhân viên đã được thêm mới vào hệ thống");
                        ((EmployeeList)this.CallFromUserControll).loadEmployeeList();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
