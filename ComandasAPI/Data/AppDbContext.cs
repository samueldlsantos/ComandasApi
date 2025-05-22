using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComandasAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ComandasAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductOption> ProductOptions => Set<ProductOption>();
        public DbSet<OptionValue> OptionValues => Set<OptionValue>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<OrderItemOption> OrderItemOptions => Set<OrderItemOption>();
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User -> Role
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Order -> User (CreatedByUser)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.CreatedByUser)
                .WithMany()
                .HasForeignKey(o => o.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ProductOption -> Product
            modelBuilder.Entity<ProductOption>()
                .HasOne(po => po.Product)
                .WithMany(p => p.Options)
                .HasForeignKey(po => po.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // OptionValue -> ProductOption
            modelBuilder.Entity<OptionValue>()
                .HasOne(ov => ov.ProductOption)
                .WithMany(po => po.Values)
                .HasForeignKey(ov => ov.ProductOptionId)
                .OnDelete(DeleteBehavior.Cascade);

            // OrderItem -> Order
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // OrderItem -> Product
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // OrderItemOption -> OrderItem
            modelBuilder.Entity<OrderItemOption>()
                .HasOne(oio => oio.OrderItem)
                .WithMany(oi => oi.Options)
                .HasForeignKey(oio => oio.OrderItemId)
                .OnDelete(DeleteBehavior.Cascade);

            // OrderItemOption -> OptionValue
            modelBuilder.Entity<OrderItemOption>()
                .HasOne(oio => oio.OptionValue)
                .WithMany()
                .HasForeignKey(oio => oio.OptionValueId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        
    }
}