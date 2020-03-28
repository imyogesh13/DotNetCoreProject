using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreProject.Model
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "Yogesh",
                    Email = "patil.yps@gmail.com",
                    Department = Dept.DotNet
                },
                 new Employee
                 {
                     Id = 2,
                     Name = "Vishwajit",
                     Email = "vishwajit@gmail.com",
                     Department = Dept.Java
                 }
                );
        }
    }
}
