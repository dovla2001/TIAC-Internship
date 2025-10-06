using FastEndpoints;
using FluentValidation;
using Presentation.Contract.Products;

namespace Presentation.Validators.Product
{
    public class UpdateProductRequestValidator : Validator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required!");

            RuleFor(p => p.Description).NotEmpty().WithMessage("Name is required!")
                .MaximumLength(100);

            RuleFor(p => p.BasePrice).NotEmpty().WithMessage("Base price is required!")
                .GreaterThan(0).WithMessage("Base price must be greater then 0!");

            RuleFor(p => p.ImageUrl).NotEmpty().WithMessage("Image is required!");
        }
    }
}
