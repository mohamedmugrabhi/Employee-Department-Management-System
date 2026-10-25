using System.ComponentModel.DataAnnotations;

namespace EmployeeDepartmentManagement.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Department name is required")]
        public string Name { get; set; } = string.Empty;
    }
}