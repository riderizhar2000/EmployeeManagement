using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{    
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public HomeController(IEmployeeRepository employeeRepository, IHostingEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ViewResult Index()
        {
            var employees = _employeeRepository.GetAllEmployees();
            return View(employees);
        }
      
        [HttpGet]
        public ViewResult Details(int id = 1)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmployee(id),
                Title = "Employee Details"
            };
                        
            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                Employee newEmployee = new Employee()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    ImagePath = uniqueFileName
                };
                _employeeRepository.Add(newEmployee);
                return RedirectToAction("Details", "Home", new { id = newEmployee.Id });
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingImagePath = employee.ImagePath
            };
            return View(employeeEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                if (model.Image != null)
                {
                    if (model.ExistingImagePath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "img", model.ExistingImagePath);
                        System.IO.File.Delete(filePath);
                    }
                    employee.ImagePath = ProcessUploadedFile(model);
                }       
                               
                _employeeRepository.Update(employee);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        private string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                if (model.Image.Length > 0)
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Image.CopyToAsync(fileStream);
                    }                                        
                }
            }
            return uniqueFileName;
        }
    }
}