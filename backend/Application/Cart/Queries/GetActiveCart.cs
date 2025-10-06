using Application.Cart.CommonCarts;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
