using Infrastructure.Attributes;
using Infrastructure.AttributesValues;
using Infrastructure.Cart;
using Infrastructure.CartItem;
using Infrastructure.Employees;
using Infrastructure.OrderItems;
using Infrastructure.Orders;
using Infrastructure.Products;
using Infrastructure.ProductVariants;
using Infrastructure.VariantsValues;
using Infrastructure.WishListItem;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityFramework
{
    public class TiacWebShopDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Domain.Entities.Employees> Employees { get; set; }
        public DbSet<Domain.Entities.Products> Products { get; set; }
        public DbSet<Domain.Entities.Orders> Orders { get; set; }
        public DbSet<Domain.Entities.Attributes> Attributes { get; set; }
        public DbSet<Domain.Entities.AttributesValues> AttributesValues { get; set; }
        public DbSet<Domain.Entities.ProductVariants> ProductVariants { get; set; }
        public DbSet<Domain.Entities.VariantValues> VariantValues { get; set; }
        public DbSet<Domain.Entities.Carts> Carts { get; set; }
        public DbSet<Domain.Entities.CartItem> CartItems { get; set; }
        public DbSet<Domain.Entities.OrderItems> OrderItems { get; set; }
        public DbSet<Domain.Entities.WishListItem> WishListItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeesConfiguration());
            modelBuilder.ApplyConfiguration(new ProductsConfiguration());
            modelBuilder.ApplyConfiguration(new OrdersConfiguration());
            modelBuilder.ApplyConfiguration(new AttributesConfiguration());
            modelBuilder.ApplyConfiguration(new AttributesValuesConfiguration());
            modelBuilder.ApplyConfiguration(new ProductVariantsConfiguration());
            modelBuilder.ApplyConfiguration(new VariantsValuesConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new CartItemConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemsConfiguration());
            modelBuilder.ApplyConfiguration(new WishListItemConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
