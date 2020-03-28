using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreProject.Model
{
    public interface IEmployeeRepository
    {
        public Employee GetEmployee(int id);
        public IEnumerable<Employee> GetAllEmployee();
        public Employee Add(Employee obj);
        public Employee Update(Employee obj);
        public Employee Delete(int id);
    }
}
