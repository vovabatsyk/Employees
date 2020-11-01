using System.IO;
using Employees.Models;
using Employees.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Employees.Pages.Employees
{
    public class DeleteModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _environment;


        [BindProperty]
        public Employee Employee { get; set; }

        public DeleteModel(IEmployeeRepository employeeRepository, IWebHostEnvironment environment)
        {
            _employeeRepository = employeeRepository;
            _environment = environment;
        }
        public IActionResult OnGet(int id)
        {
            Employee = _employeeRepository.GetEmployee(id);
            if (Employee == null)
            {
                return RedirectToPage("/NotFound");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            Employee employee = _employeeRepository.Delete(Employee.Id);
            if (employee == null)
                return RedirectToPage("/NotFound");

            if (employee.PhotoPath != null && employee.PhotoPath != "noimage.png")
            {
                string filePath = Path.Combine(_environment.WebRootPath, "images", employee.PhotoPath);
                System.IO.File.Delete(filePath);
            }

            return RedirectToPage("Employees");
        }
    }
}
