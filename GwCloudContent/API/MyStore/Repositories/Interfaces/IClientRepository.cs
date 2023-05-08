using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStore.Models;

namespace MyStore.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Customer CreateCustomer(Customer Customer);        
        List<Customer> GetCustomers();
        Customer GetCustomerById(Guid id);
        Customer UpdateCustomer(Customer Customer);
        void Save();
    }
}