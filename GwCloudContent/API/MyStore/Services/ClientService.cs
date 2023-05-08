using MyStore.Models;
using MyStore.Repositories.Interfaces;
using MyStore.Services.Interfaces;

namespace MyStore.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _CustomerRepository;

        public CustomerService(ICustomerRepository CustomerRepository)
        {
            _CustomerRepository = CustomerRepository;
        }

        public Customer CreateCustomer(Customer Customer)
        {
            if (Customer is null)
                Console.WriteLine($"Treat for error: {Customer} is null");
            
            _CustomerRepository.CreateCustomer(Customer);

            return Customer;
        }
        
        public List<Customer> GetCustomers()
        {
            return  _CustomerRepository.GetCustomers();            
        }

        public Customer GetCustomerById(Guid id)
        {
            return _CustomerRepository.GetCustomerById(id);
        }

        public Customer UpdateCustomer(Customer request)
        {
            Customer Customer = _CustomerRepository.GetCustomerById(request.Id);

            Customer.Name = request.Name;

            return _CustomerRepository.UpdateCustomer(Customer);
        }
    }
}