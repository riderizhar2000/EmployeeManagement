using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstAspNetCoreWebApp.Interfaces;
using FirstAspNetCoreWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstAspNetCoreWebApp.Controllers
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
            ViewData["Employee"] = employee;
            ViewData["Title"] = "Employee Details";
            return View();
        }
    }
}