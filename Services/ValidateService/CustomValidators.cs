using System.ComponentModel.DataAnnotations;

namespace AppMVC.Services.ValidateService
{
    public class CustomValidators
    {
        public static ValidationResult ValidateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;

            if (dateOfBirth > today.AddYears(-age))
            {
                age--;
            }

            if (age < 18)
            {
                return new ValidationResult("You must be at least 18 years old");
            }

            return ValidationResult.Success;
        }
    }
}
