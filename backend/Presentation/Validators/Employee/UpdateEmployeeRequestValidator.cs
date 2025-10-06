using FastEndpoints;
using FluentValidation;
using Presentation.Contract.Employees;

namespace Presentation.Validators.Employee
{
    public class UpdateEmployeeRequestValidator : Validator<UpdateEmployeeRequest>
    {
        public UpdateEmployeeRequestValidator()
        {
            RuleFor(e => e.FirstName).NotEmpty().WithMessage("First Name is required");

            RuleFor(e => e.LastName).NotEmpty().WithMessage("Last Name is required");
            
            RuleFor(e => e.Email).NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("The email address format is incorrect");
        }
    }
}
