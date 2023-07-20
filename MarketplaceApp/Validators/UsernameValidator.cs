using MarketplaceApp.Data;
using System.ComponentModel.DataAnnotations;

public class UsernameValidator : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            var _context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
            if (!_context.user.Any(u => u.userName == value.ToString()))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("username is already used");
        }
        return new ValidationResult("username is the required field");
    }
}
