using Microsoft.EntityFrameworkCore;
using MyStoreApi.Domain.Models;
using MyStoreApi.Infrastructure.Persistence;

namespace MyStore.xUnit.Infrastructure
{
    public class MyStoreDbContextMock : MyStoreApiDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("MyStoreInMemory");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}