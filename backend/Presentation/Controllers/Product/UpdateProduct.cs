using Application.Product.Command;
using FastEndpoints;
using MediatR;
using Presentation.Contract.Products;
using Presentation.Validators.Product;

namespace Presentation.Controllers.Product
{
    public class UpdateProduct : Endpoint<UpdateProductRequest, UpdateProductResponse>
    {
        private readonly IMediator _mediator;

        public UpdateProduct(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Put("products/{productId}");
            //AllowAnonymous();
            Roles("Admin");
            Validator<UpdateProductRequestValidator>();
        }

        public override async Task HandleAsync(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateProductCommand(request.ProductId, request.Name, request.Description, request.BasePrice, request.ImageUrl);
            var updatedProduct = await _mediator.Send(command, cancellationToken);

            var response = new UpdateProductResponse
            {
                ProductId = updatedProduct.ProductId,
                Name = updatedProduct.Name,
                Description = updatedProduct.Decsription,
                BasePrice = updatedProduct.BasePrice,
                ImageUrl = updatedProduct.ImageUrl,
            };

            await SendOkAsync(response, cancellationToken);
        }
    }
}
