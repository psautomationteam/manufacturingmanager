using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;

namespace BaoHien.Services.Employees
{
    public class EmployeeService : BaseService<Employee>
    {
        public Employee GetEmployee(System.Int32 id)
        {
            Employee employee = OnGetItem<Employee>(id.ToString());

            return employee;
        }
        public List<Employee> GetEmployees()
        {
            List<Employee> employees = OnGetItems<Employee>();

            return employees;
        }
        public bool AddEmployee(Employee employee)
        {
            return OnAddItem<Employee>(employee);
        }
        public bool DeleteEmployee(System.Int32 id)
        {
            return OnDeleteItem<Employee>(id.ToString());
        }
        public bool UpdateEmployee(Employee employee)
        {
            return OnUpdateItem<Employee>(employee, employee.Id.ToString());
        }
    }
}
