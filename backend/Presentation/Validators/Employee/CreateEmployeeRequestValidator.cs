using FastEndpoints;
using FluentValidation;
using Presentation.Contract.Employees;

namespace Presentation.Validators.Employee
{
    public class CreateEmployeeRequestValidator : Validator<CreateEmployeeRequest>
    {
        public CreateEmployeeRequestValidator()
        {
            RuleFor(e => e.FirstName).NotEmpty().WithMessage("First Name is required");
            
            RuleFor(e => e.LastName).NotEmpty().WithMessage("Last Name is required");
            
            RuleFor(e => e.Email).NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("The email address format is incorrect");
            
            RuleFor(e => e.Password).NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must have at least 8 characters");
        }
    }
}

