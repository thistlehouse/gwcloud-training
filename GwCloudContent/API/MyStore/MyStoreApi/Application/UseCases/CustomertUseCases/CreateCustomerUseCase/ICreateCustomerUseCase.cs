using MyStoreApi.Contracts.CustomerDto;
using MyStoreApi.UseCases;

namespace MyStoreApi.Application.UseCases.CustomertUseCases.CreateCustomerUseCase
{
    public interface ICreateCustomerUseCase
    {
        ServiceResponse<CustomerResponse> CreateCustomer(CustomerRequest request);
    }
}