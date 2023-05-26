using Microsoft.EntityFrameworkCore;
using MyStoreApi.Domain.Models;
using MyStoreApi.Application.Interfaces;
using MyStoreApi.Infrastructure.Persistence;

namespace MyStoreApi.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MyStoreApiDbContext _MyStoreApiContext;

        public CustomerRepository(MyStoreApiDbContext MyStoreApiContext)
        {
            _MyStoreApiContext = MyStoreApiContext;
        }

        public Customer CreateCustomer(Customer Customer)
        {
            _MyStoreApiContext.Add(Customer);

            Save();

            return Customer;
        }

        public Customer GetCustomerById(Guid id)
        {
            return _MyStoreApiContext.Customers
                .Include(c => c.Orders)
                .ThenInclude(o => o.OrderProducts)
                .FirstOrDefault(c => c.Id == id);
        }

        public Customer UpdateCustomer(Customer Customer)
        {
            _MyStoreApiContext.Customers.Update(Customer);

            Save();

            return Customer;

        }

        public void Save()
        {
            _MyStoreApiContext.SaveChanges();
        }
    }
}