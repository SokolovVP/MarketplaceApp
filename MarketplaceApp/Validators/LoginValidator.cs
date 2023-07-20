using MarketplaceApp.Data;
using System.ComponentModel.DataAnnotations;

public class LoginValidator : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null) { 
        var _context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
        if (!_context.user.Any(u => u.login == value.ToString()))
        {
            return ValidationResult.Success;
        }
        return new ValidationResult("login is already used");
        }
        return new ValidationResult("login is the required field");
    }
}
