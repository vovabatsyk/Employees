 using System;
using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
 using System.Text;

namespace Employees.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name field can't be Empty!")]
        [MaxLength(50), MinLength(2)]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Please, enter a Valid Email!")]
        [MaxLength(50), MinLength(2)]
        public string Email { get; set; }

        public string PhotoPath { get; set; }

        public Dept? Department { get; set; }

    }
}
