using MyStore.Models;
using MyStore.Contracts.CustomerDto;

namespace MyStore.Services.Interfaces
{
    public interface ICustomerService
    {
        Customer CreateCustomer(Customer Customer);        
        List<Customer> GetCustomers();
        Customer GetCustomerById(Guid Id);
        Customer UpdateCustomer(Customer Customer);
        
    }
}