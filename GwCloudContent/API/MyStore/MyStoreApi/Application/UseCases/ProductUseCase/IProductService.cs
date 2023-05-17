using MyStoreApi.Contracts.ProductDto;
using MyStoreApi.Domain.Models;
using MyStoreApi.UseCases;

namespace MyStoreApi.Application.UseCases.ProductUserCase
{
    public interface IProductService
    {
        ServiceResponse<ProductResponse> CreateProduct(ProductRequest product);
        ServiceResponse<ProductResponse> GetProductById(ProductByIdRequest request);
        ServiceResponse<ProductResponse> UpdateProduct(ProductRequest product);
    }
}