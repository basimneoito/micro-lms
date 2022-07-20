using System.ComponentModel;
using System.Text.RegularExpressions;
using FluentValidation;

namespace Common.Dtos.Authorization.Models;

public class RegisterRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ProfilePhotoURL { get; set; }
    public DateTime CreatedDate { get; set;}
    public string CreatedUser { get; set; }

}

public class RegisterRequestValidator : AbstractValidator<RegisterRequest> 
{
    public RegisterRequestValidator()
    {
      RuleFor(x => x.Email).NotNull().WithMessage("Email cannot be null");
      RuleFor(x => x.Password).NotNull().WithMessage("Password cannot be null");
      RuleFor(x => x.FirstName).NotNull().WithMessage("Firstname cannot be null");
      RuleFor(x => x.LastName).NotNull().WithMessage("Lastname cannot be null");
      RuleFor(x => x.Password).Password();
      RuleFor(x => x.Email).EmailAddress().WithMessage("Please enter a valid email");
    }
}

public static class RuleBuilderExtensions
{
    public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength = 8)
    {
        var options = ruleBuilder
            .NotEmpty().WithMessage("Password cannot be empty")
            .MinimumLength(minimumLength).WithMessage("Minimum 8 Character")
            .Matches("[A-Z]").WithMessage("Password must contain 1 uppercase letter")
            .Matches("[a-z]").WithMessage("Password must contain 1 lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain 1 digit")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain 1 special character");
        return options;
    }
}