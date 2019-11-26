using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Interfaces
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int id);
    }
}
