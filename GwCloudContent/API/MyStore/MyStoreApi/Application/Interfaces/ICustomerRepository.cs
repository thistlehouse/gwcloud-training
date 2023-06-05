using MyStoreApi.Domain.Models;

namespace MyStoreApi.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Customer CreateCustomer(Customer Customer);
        Customer GetCustomerById(Guid id);
        Customer UpdateCustomer(Customer Customer);
        void Save();
    }
}