using Application.OrderItem.CommonOrderItem;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.OrderItems
{
    public class OrderItemsRepository : IOrderItemsRepository
    {
        private readonly TiacWebShopDbContext _dbContext;

        public OrderItemsRepository(TiacWebShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Entities.OrderItems> CreateOrderItemsAsync(Domain.Entities.OrderItems orderItems, CancellationToken cancellationToken)
        {
            var newCreateOrder = await _dbContext.OrderItems.AddAsync(orderItems, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return newCreateOrder.Entity;
        }

        public async Task DeleteOrderItemsAsync(Domain.Entities.OrderItems orderItems, CancellationToken cancellationToken)
        {
            _dbContext.OrderItems.Remove(orderItems);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Domain.Entities.OrderItems?> GetByIdOrderItemsAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.OrderItems.FirstOrDefaultAsync(oi => oi.OrderItemId == id, cancellationToken);
        }

        public async Task UpdateOrderItemsAsync(Domain.Entities.OrderItems orderItems, CancellationToken cancellationToken)
        {
            _dbContext.OrderItems.Update(orderItems);
            await _dbContext.SaveChangesAsync(cancellationToken);

        }
    }
}
