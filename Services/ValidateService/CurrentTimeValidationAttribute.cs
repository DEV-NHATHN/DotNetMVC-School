using System.ComponentModel.DataAnnotations;

namespace AppMVC.Services.ValidateService
{
    public class CurrentTimeValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime foundedTime;
            if (DateTime.TryParse(value.ToString(), out foundedTime))
            {
                if (foundedTime > DateTime.Now)
                {
                    return new ValidationResult("Founded date cannot be in the future.");
                }
            }
            return ValidationResult.Success;
        }
    }

}
