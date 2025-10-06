using FastEndpoints;
using MediatR;
using Presentation.Contract.Products;
using Application.Product.Command;

namespace Presentation.Controllers.Product
{
    public class DeleteProduct : EndpointWithoutRequest<DeleteProductResponse>
    {
        private readonly IMediator _mediator;

        public DeleteProduct(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Delete("products/{productId}");
            //AllowAnonymous();
            Roles("Admin");
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            var productId = Route<int>("productId");
            var productDeleted = await _mediator.Send(new DeleteProductCommand(productId), cancellationToken);

            if (!productDeleted)
            {
                await SendNotFoundAsync(cancellationToken);
            }

            await SendNoContentAsync(cancellationToken);
        }
    }
}
