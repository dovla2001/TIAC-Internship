using FastEndpoints;
using MediatR;
using Presentation.Contract.Products;
using Presentation.Mappers;
using static Application.Product.Queries.GetProductById;

namespace Presentation.Controllers.Product
{
    public class GetProductById : Endpoint<ReadProductRequest, ReadProductResponse>
    {
        private readonly IMediator _mediator;

        public GetProductById(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("products/{productId}");
            //AllowAnonymous();
            Roles("Admin");
        }

        public override async Task HandleAsync(ReadProductRequest request, CancellationToken cancellationToken)
        {
            var productId = Route<int>("productId");
            var product = await _mediator.Send(new GetProductByIdQuery(productId));
            if (product is null)
            {
                await SendNotFoundAsync(cancellationToken);
            }

            await SendOkAsync(product.ToApiResponse(), cancellationToken);
        }
    }
}
