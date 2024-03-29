﻿using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Interfaces
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int id);

        IEnumerable<Employee> GetAllEmployees();

        Employee Add(Employee employee);

        Employee Update(Employee modifiedEmployee);

        Employee Delete(int id);
    }
}
