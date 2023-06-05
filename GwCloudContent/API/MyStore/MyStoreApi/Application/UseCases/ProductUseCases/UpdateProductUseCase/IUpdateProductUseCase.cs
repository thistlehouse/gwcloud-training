using MyStoreApi.Contracts.ProductDto;
using MyStoreApi.UseCases;

namespace MyStoreApi.Application.UseCases.ProductUseCases.UpdateProductUseCase
{
    public interface IUpdateProductUseCase
    {
        ServiceResponse<ProductResponse> UpdateProduct(ProductRequest request);
    }
}