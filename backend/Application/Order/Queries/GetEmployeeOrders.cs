using Application.Order.CommonOrders;
using Domain.Entities;
using MediatR;

namespace Application.Order.Queries
{
    public class GetEmployeeOrdersQueryHandler : IRequestHandler<GetEmployeeOrdersQuery, List<Orders>>
    {
        private readonly IOrdersRepository _ordersRepository;

        public GetEmployeeOrdersQueryHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<List<Orders>> Handle(GetEmployeeOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _ordersRepository.GetOrdersByEmployeeIdAsync(request.EmployeeId, cancellationToken);
            return orders;
        }
    }

    public record GetEmployeeOrdersQuery(int EmployeeId) : IRequest<List<Orders>>;
}
