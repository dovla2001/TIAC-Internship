using FastEndpoints;
using MediatR;
using Presentation.Contract.Products;
using Presentation.Mappers;
using static Application.Product.Queries.GetAllProducts;

namespace Presentation.Controllers.Product
{
    public class GetAllProductForAdmin : EndpointWithoutRequest<List<ReadProductForAdmin>>
    {
        private IMediator _mediator;

        public GetAllProductForAdmin(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("products/getAllForAdmin");
            //AllowAnonymous();
            Roles("Admin");
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            var productFromDb = await _mediator.Send(new GetAllProductsQuery(), cancellationToken);

            var response = productFromDb.Select(p => p.ToSimpleApiResponse()).ToList();

            await SendOkAsync(response, cancellationToken);
        }
    }
}
