using Application.Cart.Queries;
using FastEndpoints;
using MediatR;
using Presentation.Contract.Carts;
using Presentation.Mappers;
using System.Security.Claims;

namespace Presentation.Controllers.Cart
{
    public class GetActiveCart : EndpointWithoutRequest<CartResponseDto>
    {
        private readonly IMediator _mediator;

        public GetActiveCart(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("/cart");
            Roles("Employee");
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            var employeeId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var query = new GetActiveCartQuery(employeeId);

            var cartFromDb = await _mediator.Send(query, cancellationToken);

            if (cartFromDb is null)
            {
                await SendOkAsync(new CartResponseDto(), cancellationToken);
                return;
            }

            var response = cartFromDb.ToCartResponseDto();

            await SendOkAsync(response, cancellationToken);
        }
    }
}
