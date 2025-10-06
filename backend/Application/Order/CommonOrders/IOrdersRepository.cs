using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Order.CommonOrders
{
    public interface IOrdersRepository
    {
        public Task<Orders?> GetByIdOrdersAsync(int id, CancellationToken cancellationToken);
        public Task<Orders> AddAsync(Orders orders, CancellationToken cancellationToken);
        public Task UpdateOrdersAsync(Orders orders, CancellationToken cancellationToken);
        public Task DeleteOrdersAsync(Orders orders, CancellationToken cancellationToken);
        public Task<List<Orders>> GetAllOrdersAsync(CancellationToken cancellationToken);
        public Task<List<Orders>> GetOrdersByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken);
        public Task<Orders?> GetOrderByIdWithDetailsAsync(int orderId, CancellationToken cancellationToken);
    }
}
