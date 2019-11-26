using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public string Index(int id = 1)
        {
            return _employeeRepository.GetEmployee(id).Name;
        }

        public ViewResult Details(int id = 1)
        {
            Employee employee = _employeeRepository.GetEmployee(id);            
            ViewBag.Title = "Employee Details";
            return View(employee);
        }
    }
}