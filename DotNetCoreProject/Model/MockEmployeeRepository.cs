using DotNetCoreProject.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreProject.Model
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeData;

        public MockEmployeeRepository()
        {
            _employeeData = new List<Employee>()
            {
                new Employee {Id=1,Name="Yogesh", Email="patil.yps@gmail.com", Department=Dept.DotNet},
                new Employee {Id=2,Name="Ramu", Email="ramu.p@gmail.com", Department=Dept.Java},
                new Employee {Id=3,Name="Ranjeet", Email="patil.ranjeet@gmail.com", Department=Dept.AI},
                new Employee {Id=4,Name="Vishwajit", Email="mane.vijwajit@gmail.com", Department=Dept.HR}
            };
        }

        public Employee Add(Employee obj)
        {
            obj.Id = _employeeData.Max(x => x.Id) + 1;
            _employeeData.Add(obj);
            return obj;
        }

        public Employee Delete(int id)
        {
            Employee employee = _employeeData.FirstOrDefault(x => x.Id == id);
            if (employee != null)
            {
                _employeeData.Remove(employee);
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeData.ToList();
        }

        public Employee GetEmployee(int id)
        {
            return _employeeData.Where(x => x.Id == id).FirstOrDefault();
        }

        public Employee Update(Employee obj)
        {
            Employee _emp = _employeeData.FirstOrDefault(x => x.Id == obj.Id);
            if (_emp != null)
            {
                _emp.Name = obj.Name;
                _emp.Email = obj.Email;
                _emp.Department = obj.Department;
            }
            return _emp;
        }
    }
}
