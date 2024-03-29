﻿using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Repositories
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee() {Id = 1, Name = "John", Email = "john@abc.com", Department = Dept.HR},
                new Employee() {Id = 2, Name = "Sam", Email = "sam@abc.com", Department = Dept.IT},
                new Employee() {Id = 3, Name = "mary", Email = "mary@abc.com", Department = Dept.Finance}
            };
        }

        public Employee Add(Employee employee)
        {
            employee.Id = _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                _employeeList.Remove(employee);
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == id);
        }

        public Employee Update(Employee modifiedEmployee)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == modifiedEmployee.Id);
            if (employee != null)
            {
                employee = modifiedEmployee;
            }
            return employee;
        }
    }
}
