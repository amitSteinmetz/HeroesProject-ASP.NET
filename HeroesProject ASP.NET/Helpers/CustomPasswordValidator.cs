using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HeroesProject_ASP.NET.Helpers
{
    public class CustomPasswordValidator: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("Password is required.");

            string password = value.ToString();
            var errors = new System.Collections.Generic.List<string>();

            // Rule 1: Minimum length of 8 characters
            if (password.Length < 8)
                errors.Add("Password must be at least 8 characters long.");

            // Rule 2: At least one uppercase letter
            if (!password.Any(char.IsUpper))
                errors.Add("Password must contain at least one uppercase letter.");

            // Rule 3: At least one lowercase letter
            if (!password.Any(char.IsLower))
                errors.Add("Password must contain at least one lowercase letter.");

            // Rule 4: At least one digit
            if (!password.Any(char.IsDigit))
                errors.Add("Password must contain at least one number.");

            // Rule 5: At least one special character
            if (!password.Any(ch => "!@#$%^&*()-_=+[]{}|;:'\",.<>?/".Contains(ch)))
                errors.Add("Password must contain at least one special character (!@#$%^&* etc.).");

            // Return validation errors if there are any
            return errors.Count == 0 ? ValidationResult.Success : new ValidationResult(string.Join(" ", errors));
        }
    }
}



