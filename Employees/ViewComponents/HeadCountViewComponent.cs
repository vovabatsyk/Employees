using Employees.Models;
using Employees.Services;
using Microsoft.AspNetCore.Mvc;

namespace Employees.ViewComponents
{
    public class HeadCountViewComponent : ViewComponent
    {
        private readonly IEmployeeRepository _employeeRepository;

        public HeadCountViewComponent(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IViewComponentResult Invoke(Dept? dept = null)
        {
            var result = _employeeRepository.EmployeeCountByDepartment(dept);
            return View(result);
        }
    }
}
