using System.Collections.Generic;
using Employees.Models;

namespace Employees.Services
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployees();
    }
}