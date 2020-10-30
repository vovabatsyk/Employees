﻿using System;
using System.Collections.Generic;
using System.Text;
using Employees.Models;

namespace Employees.Services
{
    public class MockEmployeeRepository: IEmployeeRepository
    {
        private List<Employee> _employeesList;

        public MockEmployeeRepository()
        {
            _employeesList = new List<Employee>()
            {
                new Employee()
                {
                    Id = 1, Name = "webProger", Email = "web@gmail.com", PhotoPath = "avatar.png", Department = Dept.HR
                },
                new Employee()
                {
                    Id = 2, Name = "Volodymyr", Email = "Volodymyr@gmail.com", PhotoPath = "avatar2.png", Department = Dept.IT
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
                    Id = 4, Name = "Mary", Email = "mary@gmail.com", Department = Dept.HR
                }

            };   
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeesList;
        }
    }
}