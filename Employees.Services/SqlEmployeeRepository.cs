using System;
using System.Collections.Generic;
using System.Linq;
using Employees.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees.Services
{
    public class SqlEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public SqlEmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return _context.Employees;

            return _context.Employees
                .Where(e => e.Name.ToLower().Contains(searchTerm.ToLower()) 
                            || e.Email.ToLower().Contains(searchTerm.ToLower()));
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _context.Employees;
        }

        public Employee GetEmployee(int id)
        {
            return _context.Employees.Find(id);
        }

        public Employee Update(Employee updatedEmployee)
        {
            var employee = _context.Employees.Attach(updatedEmployee);
            employee.State = EntityState.Modified;
            _context.SaveChanges();

            return updatedEmployee;
        }

        public Employee Add(Employee newEmployee)
        {
            _context.Employees.Add(newEmployee);
            _context.SaveChanges();

            return newEmployee;
        }

        public Employee Delete(int id)
        {
            var employeeToDelete = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employeeToDelete != null)
            {
                _context.Employees.Remove(employeeToDelete);
                _context.SaveChanges();
            }

            return employeeToDelete;
        }

        public IEnumerable<DepartmentHeadCount> EmployeeCountByDepartment(Dept? dept = Dept.None)
        {
            IEnumerable<Employee> query = _context.Employees;
            if (dept.HasValue)
            {
                query = query.Where(x => x.Department == dept.Value);
            }
    
            return query.GroupBy(e => e.Department)
                .Select(x => new DepartmentHeadCount()
                {
                    Department = x.Key.Value,
                    Count = x.Count()
                }).ToList();
        }
    }
}
