﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeManagementModels
{
    public class Employee
    {
        public int DepartmentId;

  
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(100,MinimumLength =2)]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }
        public DateTime DateOfBrith { get; set; }
        public Gender Gender { get; set; }
        public int DepartmenId { get; set; }
        public string PhotoPath { get; set; }
    }
}
