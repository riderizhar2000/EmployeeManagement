using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class DepartmentController : Controller
    {
        public string Index()
        {
            return "List of Departments";
        }

        public string Details()
        {
            return "Details of Department";
        }
    }
}