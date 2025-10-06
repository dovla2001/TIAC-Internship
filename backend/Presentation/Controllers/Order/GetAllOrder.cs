using Application.Order.Queries;
using FastEndpoints;
using MediatR;
using Presentation.Contract.Order;

namespace Presentation.Controllers.Order
{
    public class GetAllOrder : EndpointWithoutRequest<List<OrderListResponse>>
    {
        private IMediator _mediator;

        public GetAllOrder(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("orders/allOrders");
            Roles("Admin");
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            var query = new GetAllOrdersQuery();

            var result = await _mediator.Send(query, cancellationToken);

            var response = result.Select(order => new OrderListResponse
            {
                OrderId = order.OrdersId,
                CustomerName = $"{order.Employees.FirstName} {order.Employees.LastName}",
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                NumberOfItems = order.OrderItems.Count()
            }).ToList();

            await SendOkAsync(response, cancellationToken);
        }
    }
}
