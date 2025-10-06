using Application.Order.Queries;
using FastEndpoints;
using MediatR;
using Presentation.Contract.Order;
using System.Security.Claims;

namespace Presentation.Controllers.Order
{
    public class GetEmployeeOrders : EndpointWithoutRequest<List<OrderResponse>>
    {
        private IMediator _mediator;

        public GetEmployeeOrders(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("orders/history");
            Roles("Employee");
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            var employeeIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            
            if (employeeIdClaim is null || !int.TryParse(employeeIdClaim.Value, out var employeeId))
            {
                await SendUnauthorizedAsync(cancellationToken);
                return;
            }

            var query = new GetEmployeeOrdersQuery(employeeId);

            var orders = await _mediator.Send(query, cancellationToken);

            var response = orders.Select(order => new OrderResponse
            {
                OrderId = order.OrdersId,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Items = order.OrderItems.Select(item =>
                {
                    var productName = item.ProductVariants.Product.Name;
                    var attributes = item.ProductVariants.VariantValues
                                        .Select(vv => vv.AttributeValue.Value)
                                        .ToList();
                    var fullProductName = $"{productName} ({string.Join(", ", attributes)})";

                    return new OrderItemResponse
                    {
                        FullProductName = fullProductName, 
                        Quantity = item.Quantity,
                        PricePerItem = item.Price,
                        TotalPrice = item.TotalPrice
                    };
                }).ToList()
            }).ToList();

            await SendOkAsync(response, cancellationToken);
        }
    }
}
