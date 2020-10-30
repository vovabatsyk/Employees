using System.Collections.Generic;
using Employees.Models;
using Employees.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Employees.Pages.Employees
{
    public class EmployeesModel : PageModel
    {
        private readonly IEmployeeRepository _db;

        public EmployeesModel(IEmployeeRepository db)
        {
            _db = db;
        }

        public IEnumerable<Employee> Employees { get; set; }
       
        public void OnGet()
        {
            Employees = _db.GetAllEmployees();
        }
    }
}
