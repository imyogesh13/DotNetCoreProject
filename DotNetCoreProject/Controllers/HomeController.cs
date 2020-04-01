using DotNetCoreProject.Model;
using DotNetCoreProject.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreProject.Controllers
{
    //[Route("[controller]")]
    public class HomeController : Controller
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
            Employee emp = _employeeRepository.GetEmployee(Id ?? 1);
            //if(emp == null)
            //{
            //    Response.StatusCode = 404;
            //    return View("PageNotFound", Id.Value);
            //}

            EmployeeViewModel empVM = new EmployeeViewModel()
            {
                Employee = emp,
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
                string uniqueFileName = ProcessUploadFile(model);
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

        private string ProcessUploadFile(EmployeeCreateVM model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileItem = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileItem);
                }
            }
            return uniqueFileName;
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employee obj = _employeeRepository.GetEmployee(id);

            EmployeeEditVM model = new EmployeeEditVM()
            {
                Id = id,
                Name = obj.Name,
                Email = obj.Email,
                Department = obj.Department,
                ExistingPhotoPath = obj.PhotoPath
            };
            return View(model);
        }

        [HttpPut]
        public IActionResult Edit(EmployeeEditVM model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        if (System.IO.File.Exists(filePath))
                            System.IO.File.Delete(filePath);
                    }
                    employee.PhotoPath = ProcessUploadFile(model);
                }
                _employeeRepository.Update(employee);
                return RedirectToAction("Index", model.Id);
            }
            return View(model);
        }
    }
}
