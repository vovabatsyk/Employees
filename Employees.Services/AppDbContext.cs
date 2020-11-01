﻿using Employees.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees.Services
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
