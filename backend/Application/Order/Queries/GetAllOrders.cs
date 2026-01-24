using Application.Order.CommonOrders;
using Domain.Entities;
using MediatR;

namespace Application.Order.Queries
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, List<Orders>>
    {
        private IOrdersRepository _ordersRepository;

        public GetAllOrdersQueryHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public Task<List<Orders>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var allOrders = _ordersRepository.GetAllOrdersAsync(cancellationToken);

            return allOrders;
        }
    }

    public record GetAllOrdersQuery : IRequest<List<Orders>>;
}
