using MyStoreApi.Contracts.ProductDto;
using MyStoreApi.UseCases;

namespace MyStoreApi.Application.UseCases.ProductUseCases.CreateProductUseCase
{
    public interface ICreateProductUseCase
    {
        ServiceResponse<ProductResponse> CreateProduct(ProductRequest product);
    }
}