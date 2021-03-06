﻿using System.Collections.Generic;
using System.Linq;
using Employees.Models;

namespace Employees.Services
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> _employeesList;

        public MockEmployeeRepository()
        {
            _employeesList = new List<Employee>()
            {
                new Employee()
                {
                    Id = 0, Name = "webDesign", Email = "admin@gmail.com", PhotoPath = "avatar5.png", Department = Dept.None
                },
                new Employee()
                {
                    Id = 1, Name = "webProger", Email = "web@gmail.com", PhotoPath = "avatar.png", Department = Dept.HR
                },
                new Employee()
                {
                    Id = 2, Name = "Volodymyr", Email = "Volodymyr@gmail.com", PhotoPath = "avatar4.png", Department = Dept.IT
                },
                new Employee()
                {
                    Id = 3, Name = "Alex", Email = "alex@gmail.com", PhotoPath = "avatar3.png", Department = Dept.Payroll
                },
                new Employee()
                {
                    Id = 4, Name = "John", Email = "bat@gmail.com", PhotoPath = "avatar4.png", Department = Dept.None
                },
                new Employee()
                {
                    Id = 5, Name = "Mary", Email = "mary@gmail.com",  Department = Dept.HR
                }

            };
        }

        public IEnumerable<Employee> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return _employeesList;

            return _employeesList.Where(e => e.Name.ToLower().Contains(searchTerm.ToLower()) || e.Email.ToLower().Contains(searchTerm.ToLower()));
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeesList;
        }

        public Employee GetEmployee(int id)
        {
            return _employeesList.FirstOrDefault(e => e.Id == id);
        }

        public Employee Update(Employee updatedEmployee)
        {
            Employee employee = _employeesList.FirstOrDefault(e => e.Id == updatedEmployee.Id);

            if (employee != null)
            {
                employee.Name = updatedEmployee.Name;
                employee.Email = updatedEmployee.Email;
                employee.PhotoPath = updatedEmployee.PhotoPath;
                employee.Department = updatedEmployee.Department;
            }

            return employee;
        }

        public Employee Add(Employee newEmployee)
        {
            newEmployee.Id = _employeesList.Max(x => x.Id) + 1;
            _employeesList.Add(newEmployee);

            return newEmployee;
        }

        public Employee Delete(int id)
        {
            Employee employee = _employeesList.FirstOrDefault(e => e.Id == id);
            if (employee != null) _employeesList.Remove(employee);

            return employee;
        }

        public IEnumerable<DepartmentHeadCount> EmployeeCountByDepartment(Dept? dept)
        {
            IEnumerable<Employee> query = _employeesList;
            if (dept.HasValue) query = query.Where(x => x.Department == dept.Value);
            
            return query.GroupBy(e => e.Department)
                .Select(x => new DepartmentHeadCount()
                {
                    Department = x.Key.Value,
                    Count = x.Count()
                }).ToList();
        }
    }
}
