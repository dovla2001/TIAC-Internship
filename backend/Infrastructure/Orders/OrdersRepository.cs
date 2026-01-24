using Application.Order.CommonOrders;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Orders
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly TiacWebShopDbContext _dbcontext;

        public OrdersRepository(TiacWebShopDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Domain.Entities.Orders> AddAsync(Domain.Entities.Orders orders, CancellationToken cancellationToken)
        {
            var newOrders = await _dbcontext.Orders.AddAsync(orders, cancellationToken);
            await _dbcontext.SaveChangesAsync(cancellationToken);
            return newOrders.Entity;
        }

        public async Task DeleteOrdersAsync(Domain.Entities.Orders orders, CancellationToken cancellationToken)
        {
            _dbcontext.Orders.Remove(orders);
            await _dbcontext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Domain.Entities.Orders?> GetByIdOrdersAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbcontext.Orders.FirstOrDefaultAsync(o => o.OrdersId == id, cancellationToken);
        }

        public async Task UpdateOrdersAsync(Domain.Entities.Orders orders, CancellationToken cancellationToken)
        {
            _dbcontext.Orders.Update(orders);
            await _dbcontext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Domain.Entities.Orders>> GetAllOrdersAsync(CancellationToken cancellationToken)
        {
            return await _dbcontext.Orders
                .Include(o => o.Employees)
                .Include(o => o.OrderItems)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Domain.Entities.Orders>> GetOrdersByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken)
        {
            return await _dbcontext.Orders
                .Where(o => o.EmployeeId == employeeId)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.ProductVariants)
                        .ThenInclude(pv => pv.Product)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.ProductVariants)
                        .ThenInclude(pv => pv.VariantValues)
                            .ThenInclude(vv => vv.AttributeValue)
                                .ThenInclude(av => av.Attribute)
                .OrderByDescending(o => o.OrderDate)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Domain.Entities.Orders?> GetOrderByIdWithDetailsAsync(int orderId, CancellationToken cancellationToken)
        {
            return await _dbcontext.Orders
                .Include(o => o.Employees)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.ProductVariants)
                        .ThenInclude(pv => pv.Product)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.ProductVariants)
                        .ThenInclude(pv => pv.VariantValues)
                            .ThenInclude(vv => vv.AttributeValue)
                                .ThenInclude(av => av.Attribute)
                .FirstOrDefaultAsync(o => o.OrdersId == orderId, cancellationToken);
        }
    }
}
