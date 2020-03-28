using DotNetCoreProject.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreProject.Model
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext db;

        public SQLEmployeeRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public Employee Add(Employee obj)
        {
            db.Employees.Add(obj);
            db.SaveChanges();
            return obj;
        }

        public Employee Delete(int id)
        {
            Employee _emp = db.Employees.Find(id);
            if(_emp != null)
            {
                db.Employees.Remove(_emp);
                db.SaveChanges();
            }
            return _emp;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return db.Employees;
        }

        public Employee GetEmployee(int id)
        {
            return db.Employees.Find(id);
        }

        public Employee Update(Employee obj)
        {
            var emp = db.Employees.Attach(obj);
            emp.State = EntityState.Modified;
            db.SaveChanges();
            return obj;
        }
    }
}
