using Microsoft.EntityFrameworkCore;
using MyStore.Models;
using MyStore.Persistence;
using MyStore.Repositories.Interfaces;

namespace MyStore.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MyStoreDbContext _myStoreContext;

        public CustomerRepository(MyStoreDbContext myStoreContext)
        {
            _myStoreContext = myStoreContext;
        }

        public Customer CreateCustomer(Customer Customer)
        {
            _myStoreContext.Add(Customer);
            
            Save();

            return Customer;
        }
    
        public Customer GetCustomerById(Guid id)
        {
            return _myStoreContext.Customers
                .Include(c => c.Orders)
                .ThenInclude(o => o.OrderProducts)
                .FirstOrDefault(c => c.Id == id);
        }

        public List<Customer> GetCustomers()
        {
            return _myStoreContext.Customers.ToList();
        }

        public Customer UpdateCustomer(Customer Customer)
        {
            _myStoreContext.Customers.Update(Customer);

            Save();

            return Customer;

        }

        public void Save()
        {
            _myStoreContext.SaveChanges();
        }
    }
}