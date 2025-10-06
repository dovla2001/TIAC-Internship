using Application.Cart.CommonCarts;
using Application.EmailService;
using Application.Employee.CommonEmployees;
using Application.Order.CommonOrders;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Order.Command
{
    public class CreateOrderFromCartCommandHandler : IRequestHandler<CreateOrderFromCartCommand, int>
    {
        private readonly ICartsRepository _cartRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IEmailService _emailService;
        private readonly IEmployeesRepository _employeesRepository;

        public CreateOrderFromCartCommandHandler(ICartsRepository cartRepository, IOrdersRepository ordersRepository, IEmailService emailService, IEmployeesRepository employeesRepository)
        {
            _cartRepository = cartRepository;
            _ordersRepository = ordersRepository;
            _emailService = emailService;
            _employeesRepository = employeesRepository;
        }

        public async Task<int> Handle(CreateOrderFromCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetActiveCartDetailsAsync(request.EmployeeId, cancellationToken);

            if (cart is null)
            {
                throw new Exception("Activ card isn't found");
            }

            if (!cart.CartItems.Any())
            {
                throw new Exception("Active card is empty");
            }

            var calculatedTotalPrice = cart.CartItems.Sum(item => item.TotalPrice); 

            var newOrder = new Orders
            {
                EmployeeId = request.EmployeeId,
                OrderDate = DateTime.UtcNow,
                TotalPrice = calculatedTotalPrice  
            };

            foreach (var cartItem in cart.CartItems)
            {
                newOrder.OrderItems.Add(new OrderItems
                {
                    ProductVariantId = cartItem.ProductVariantId,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Price,
                    TotalPrice = cartItem.Quantity * cartItem.Price
                });
            }

            var createdOrder = await _ordersRepository.AddAsync(newOrder, cancellationToken);

            cart.IsCartActive = false;
            await _cartRepository.UpdateCartsAsync(cart, cancellationToken);

            var fullOrderDetails = await _ordersRepository.GetOrderByIdWithDetailsAsync(createdOrder.OrdersId, cancellationToken);

            var admins = await _employeesRepository.GetAdminsAsync(cancellationToken);

            if (fullOrderDetails != null)
            {
                try
                {
                    await _emailService.SendOrderConfirmationEmailAsync(
                        fullOrderDetails,
                        fullOrderDetails.Employees.Email,
                        cancellationToken);

                    foreach (var admin in admins)
                    {
                        await _emailService.SendAdminOrderNotificationAsync(
                        fullOrderDetails,
                        admin.Email,
                        cancellationToken);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending email: {ex.Message}");
                }
            }

            return createdOrder.OrdersId;
        }
    }

    public record CreateOrderFromCartCommand(int EmployeeId) : IRequest<int>;
}
