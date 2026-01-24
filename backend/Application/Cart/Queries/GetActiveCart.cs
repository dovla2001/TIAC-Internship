using Application.Cart.CommonCarts;
using Domain.Entities;
using MediatR;

namespace Application.Cart.Queries
{
    public class GetActiveCartQueryHandler : IRequestHandler<GetActiveCartQuery, Carts?>
    {
        private readonly ICartsRepository _cartRepository;

        public GetActiveCartQueryHandler(ICartsRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<Carts?> Handle(GetActiveCartQuery request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetActiveCartDetailsAsync(request.employeeId, cancellationToken);
            return cart;
        }
    }

    public record GetActiveCartQuery(int employeeId) : IRequest<Carts?>;
}
