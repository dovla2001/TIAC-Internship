using Application;
using Application.Attribute.CommonAttributes;
using Application.AttributeValues.CommonAttributeValues;
using Application.Cart.CommonCarts;
using Application.CartsItem.CommonCartItem;
using Application.EmailService;
using Application.Employee.CommonEmployees;
using Application.Employee.Services;
using Application.Order.CommonOrders;
using Application.OrderItem.CommonOrderItem;
using Application.Product.CommonProducts;
using Application.ProductVariant.CommonProductVariants;
using Application.VariantsValues.CommonVariantValues;
using Application.WishListItems.CommonWishListItem;
using Infrastructure.Attributes;
using Infrastructure.AttributesValues;
using Infrastructure.Cart;
using Infrastructure.CartItem;
using Infrastructure.Employees;
using Infrastructure.Employees.Services;
using Infrastructure.EntityFramework;
using Infrastructure.OrderItems;
using Infrastructure.Orders;
using Infrastructure.Products;
using Infrastructure.ProductVariants;
using Infrastructure.VariantsValues;
using Infrastructure.WishListItem;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TiacWebShopDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IEmployeesRepository, EmployeesRepository>();
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IProductsVariantsRepository, ProductVariantsRepository>();
            services.AddScoped<IAttributesRepository, AttributesRepository>();
            services.AddScoped<IAttributeValuesRepository, AttributesValuesRepository>();
            services.AddScoped<ICartsRepository, CartRepository>();
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<IOrderItemsRepository, OrderItemsRepository>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<IVariantValuesRepository, VariantsValuesRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IEmailService, EmailService.EmailService>();
            services.AddScoped<IWishListRepository, WishListItemRepository>();
            return services;
        }
    }
}
