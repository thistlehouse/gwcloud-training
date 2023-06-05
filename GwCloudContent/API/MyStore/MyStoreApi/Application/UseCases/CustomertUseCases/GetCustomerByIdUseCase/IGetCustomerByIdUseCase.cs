using MyStoreApi.Contracts.CustomerDto;
using MyStoreApi.UseCases;

namespace MyStoreApi.Application.UseCases.CustomertUseCases.GetCustomerByIdUseCase
{
    public interface IGetCustomerByIdUseCase
    {
        ServiceResponse<CustomerResponse> GetCustomerById(CustomerByIdRequest request);
    }
}