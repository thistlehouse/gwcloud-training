using MyStore.Contracts.ProductDto;
using MyStore.Models;

namespace MyStore.Services.Interfaces
{
    public interface IProductService
    {
        Product CreateProduct(Product product);
        Product GetById(ProductRequest request);
        Product UpdateProduct(Product product);
    }
}