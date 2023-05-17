using MyStoreApi.Contracts.ProductDto;
using MyStoreApi.Domain.Models;

namespace MyStoreApi.Application.Interfaces
{
    public interface IProductRepository
    {
        void CreateProduct(Product product);
        Product GetProductById(Guid id);
        void UpdateProduct(Product product);
        void Save();                   
    }
}