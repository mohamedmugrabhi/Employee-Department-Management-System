using System.ComponentModel.DataAnnotations;

namespace EmployeeDepartmentManagement.Validation
{
    public class NoNumbersAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            string valueAsString = value.ToString()!;

            foreach (char character in valueAsString)
            {
                if (char.IsDigit(character))
                {
                    return new ValidationResult(
                        ErrorMessage ?? "Name cannot contain numbers."
                    );
                }
            }

            return ValidationResult.Success;
        }
    }
}