using DotNetCoreProject.Model;
using DotNetCoreProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreProject.Controllers
{
    //[Route("[controller]")]
    public class HomeController:Controller
    {
        private IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
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
        public IActionResult Create(Employee emp)
        {
            if (ModelState.IsValid)
            {
                Employee obj = _employeeRepository.Add(emp);

                //return RedirectToAction("Details", obj.Id);
            }
            return View(emp);
        }

        private Tuple<int,string> Add(int i)
        {
            return new Tuple<int, string>(1, "");
        }

        private (int, string) Add()
        {
            return (1, "");
        }
    }
}
