using MarketplaceApp.Data;
using System.ComponentModel.DataAnnotations;

public class EmailValidator: ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value !=  null) { 
        var _context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
        if (!_context.user.Any(u => u.email == value.ToString()))
        {
            return ValidationResult.Success;
        }
        return new ValidationResult("email is already used");
        }
        return new ValidationResult("email is the required field");
    }
}
