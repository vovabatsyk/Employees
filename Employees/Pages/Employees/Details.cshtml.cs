using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employees.Models;
using Employees.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Employees.Pages.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;
        public Employee Employee { get; private set; }

        public DetailsModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public void OnGet(int id)
        {
            Employee = _employeeRepository.GetEmployee(id);
        }
    }
}
