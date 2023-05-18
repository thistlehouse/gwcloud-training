using MyStoreApi.Contracts.CustomerDto;
using MyStoreApi.Domain.Models;
using MyStoreApi.UseCases;

namespace MyStoreApi.Application.UseCases.CustomerUseCase
{
    public interface ICustomerService
    {
        ServiceResponse<CustomerResponse> CreateCustomer(CustomerRequest request);
        ServiceResponse<List<CustomerResponse>> GetCustomers();
        ServiceResponse<CustomerResponse> GetCustomerById(CustomerByIdRequest request);
        ServiceResponse<CustomerResponse> UpdateCustomer(CustomerRequest request);
        
    }
}