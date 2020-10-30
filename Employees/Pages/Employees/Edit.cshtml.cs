using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Employees.Models;
using Employees.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Employees.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _environment;

        public EditModel(IEmployeeRepository employeeRepository, IWebHostEnvironment environment)
        {
            _employeeRepository = employeeRepository;
            _environment = environment;
        }

        public Employee Employee { get; private set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        public IActionResult OnGet(int id)
        {
            Employee = _employeeRepository.GetEmployee(id);

            if (Employee == null)
            {
                return RedirectToPage("/NotFound");
            }

            return Page();
        }

        public IActionResult OnPost(Employee employee)
        {
            if (Photo != null)
            {
                if (employee.PhotoPath != null)
                {
                    string filePath = Path.Combine(_environment.WebRootPath, "images", employee.PhotoPath);
                    System.IO.File.Delete(filePath);
                }
                employee.PhotoPath = ProcessUploadedFile();
            }


            Employee = _employeeRepository.Update(employee);
            return RedirectToPage("Employees");
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;

            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fs);
                }
            }

            return uniqueFileName;
        }
    }
}
