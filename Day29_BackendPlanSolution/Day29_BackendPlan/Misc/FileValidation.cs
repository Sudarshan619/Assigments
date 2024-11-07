using Day29_BackendPlan.Model;
using System.ComponentModel.DataAnnotations;

namespace Day29_BackendPlan.Misc
{
    public class FileValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Policyname cannot be empty");
            }
            string username = value.ToString() ?? "";
            if (username.Length != 6)
            {
                return new ValidationResult("Username must be equal to 6 characters long");
            }
            if (!username.StartsWith("POL"))
            {
                return new ValidationResult("Policyname should start with POL");
            }
            return ValidationResult.Success;
        }
    }
}
