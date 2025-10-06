using FastEndpoints;
using FluentValidation;
using Presentation.Contract.Employees;

namespace Presentation.Validators.Employee
{
    public class LoginEmployeeRequestValidator : Validator<LoginEmployeeRequest>
    {
        public LoginEmployeeRequestValidator()
        {
            RuleFor(e => e.Email).NotEmpty().WithMessage("Email is required!")
                .EmailAddress().WithMessage("The email address format is incorrect");

            RuleFor(e => e.Password).NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must have at least 8 characters");
        }
    }
}
