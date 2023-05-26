using MyStoreApi.Contracts.ProductDto;
using MyStoreApi.UseCases;

namespace MyStoreApi.Application.UseCases.ProductUseCases.GetProductByIdUseCase
{
    public interface IGetProductByIdUseCase
    {
        ServiceResponse<ProductResponse> GetProductById(ProductByIdRequest request);
    }
}