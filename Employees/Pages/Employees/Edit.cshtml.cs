using System;
using System.IO;
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

        [BindProperty]
        public Employee Employee { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        [BindProperty]
        public bool Notify { get; set; }

        public string Message { get; set; }

        public string AlertColor { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
            {
                Employee = _employeeRepository.GetEmployee(id.Value);
            }
            else
            {
                Employee = new Employee();
            }

            if (Employee == null)
                return RedirectToPage("/NotFound");

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    if (Employee.PhotoPath != null)
                    {
                        string filePath = Path.Combine(_environment.WebRootPath, "images", Employee.PhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    Employee.PhotoPath = ProcessUploadedFile();
                }

                if (Employee.Id > 0)
                {
                    Employee = _employeeRepository.Update(Employee);
                    TempData["SuccessMessage"] = $"Update {Employee.Name} successful!";
                }
                else
                {
                    Employee = _employeeRepository.Add(Employee);
                    TempData["SuccessMessage"] = $"Adding {Employee.Name} successful!";
                }

                return RedirectToPage("Employees");
            }
            return Page();
        }

        public void OnPostUpdateNotificationPreference(int id)
        {
            if (Notify)
            {
                Message = "Thank you for turning on notification";
                AlertColor = "alert-success";
            }
            else
            {
                Message = "You have turning off notification";
                AlertColor = "alert-danger";
            }

            Employee = _employeeRepository.GetEmployee(id);
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
