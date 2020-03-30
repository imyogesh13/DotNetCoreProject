using DotNetCoreProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreProject.ViewModel
{
    public class EmployeeEditVM : EmployeeCreateVM    {
        public int Id { get; set; }
        public  string ExistingPhotoPath { get; set; }
    }
}
