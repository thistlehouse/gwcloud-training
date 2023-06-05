using MyStoreApi.Contracts.CustomerDto;
using MyStoreApi.UseCases;

namespace MyStoreApi.Application.UseCases.CustomertUseCases.UpdateCustomerUseCase
{
    public interface IUpdateCustomerUseCase
    {
        ServiceResponse<CustomerResponse> UpdateCustomer(CustomerRequest request);
    }
}