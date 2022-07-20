using System.ComponentModel;
using System.Text.RegularExpressions;
using FluentValidation;

namespace Common.Dtos.Authorization.Models;

public class LoginUserRequest {
    public string Email { get; set; }
    public string Password { get; set;}

}
public class LoginRequestValidator : AbstractValidator<LoginUserRequest> 
{
  public LoginRequestValidator() 
  {
    RuleFor(x => x.Email).NotNull().WithMessage("Email cannot be null");
    RuleFor(x => x.Password).NotNull().WithMessage("Password cannot be null");
    RuleFor(x => x.Email).EmailAddress().WithMessage("Please enter a valid email");
  
  }
}