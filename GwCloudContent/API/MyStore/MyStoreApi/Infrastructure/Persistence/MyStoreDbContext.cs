using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyStoreApi.Domain.Models;

namespace MyStoreApi.Infrastructure.Persistence
{
    public class MyStoreApiDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Server=localhost;Port=5432;Database=FicticiousStore;Uid=postgres;Pwd=r00t");
        }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderProduct> OrdersProducts => Set<OrderProduct>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().Ignore(o => o.Coupon);

            // modelBuilder.Entity<Coupon>()
            //     .HasNoKey();

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<Order>()
                .Ignore(o => o.Coupon);

            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });
                // .HasNoKey();

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(op => op.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Product)
                .WithMany(op => op.OrderProducts)
                .HasForeignKey(op => op.ProductId);


            modelBuilder.Entity<Customer>()
                .HasData(FakeDataGenerator.GenerateCustomers(100));

            modelBuilder.Entity<Product>()
                .HasData(FakeDataGenerator.GenerateProducts(100));
        }
    }
}