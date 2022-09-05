//using EmployeeManagement.Models.CustomValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage="Firstname is mandatory")]
        [MinLength(2)]
        //[StringLength(100,MinimumLength =2)]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

       // [Required]
       [EmailAddress]
        //[EmailDomainValidator(AllowedDomain = "Innohive.com",
        //    ErrorMessage = "Only Innohive.com is allowed")]
        public string Email { get; set; }
        public DateTime DateOfBrith { get; set; }
        public Gender Gender { get; set; }
        public int DepartmentId { get; set; }

        public string PhotoPath { get; set; }

        //[ValidateComplexType]
        public Department Department { get; set; } 
       


    }
}
