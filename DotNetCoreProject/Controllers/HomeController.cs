using DotNetCoreProject.Model;
using DotNetCoreProject.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreProject.Controllers
{
    //[Route("[controller]")]
    public class HomeController:Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(IEmployeeRepository employeeRepository, IHostingEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        //[Route("")]
        //[Route("[action]")]
        //[Route("~/")]//Without controller name and action name
        public ViewResult Index()
        {
            //int? i = null;
            //add code
            IEnumerable<Employee> objLiEmp = _employeeRepository.GetAllEmployee();
            return View(objLiEmp);
             
        }
        
        //[Route("[action]/{id?}")]
        public ViewResult Details(int? Id)
        {
            EmployeeViewModel empVM = new EmployeeViewModel() { 
            Employee = _employeeRepository.GetEmployee(Id??1),
            PageTitle = "Employee Details"
            };
            return View(empVM);
        }

        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateVM model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = string.Empty;
                if(model.Photo != null)
                {
                    string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                   string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));

                }
                Employee obj = new Employee()
                {
                    Name = model.Name,
                    Email = model.Email,
                    PhotoPath = uniqueFileName,
                    Department = model.Department
                };
                _employeeRepository.Add(obj);

                return RedirectToAction("Details", obj.Id);
            }
            return View();
        }
    }
}
