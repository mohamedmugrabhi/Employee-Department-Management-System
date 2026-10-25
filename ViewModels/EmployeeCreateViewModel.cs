using EmployeeDepartmentManagement.Validation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDepartmentManagement.ViewModels
{
    public class EmployeeCreateViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [NoNumbers(ErrorMessage = "Name cannot contain numbers")]
        public string Name { get; set; } = string.Empty;


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [Remote(
            action: "IsEmailAvailable",
            controller: "Employee",
            ErrorMessage = "This email is already registered."
        )]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "Salary is required")]
        [Range(
            0,
            double.MaxValue,
            ErrorMessage = "Salary must be greater than or equal to 0"
        )]
        public decimal Salary { get; set; }


        [Range(
            1,
            int.MaxValue,
            ErrorMessage = "Please select a department"
        )]
        public int DepartmentId { get; set; }
    }
}