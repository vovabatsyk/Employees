using System.Collections.Generic;
using Employees.Models;

namespace Employees.Services
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> Search(string searchTerm);
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployee(int id);
        Employee Update(Employee updatedEmployee);
        Employee Add(Employee newEmployee);
        Employee Delete(int id);
        IEnumerable<DepartmentHeadCount> EmployeeCountByDepartment(Dept? dept);
    }
}