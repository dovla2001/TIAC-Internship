using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OrderItem.CommonOrderItem
{
    public interface IOrderItemsRepository
    {
        public Task<OrderItems?> GetByIdOrderItemsAsync(int id, CancellationToken cancellationToken);
        public Task<OrderItems> CreateOrderItemsAsync(OrderItems orderItems, CancellationToken cancellationToken);
        public Task UpdateOrderItemsAsync(OrderItems orderItems, CancellationToken cancellationToken);
        public Task DeleteOrderItemsAsync(OrderItems orderItems, CancellationToken cancellationToken);
    }
}
