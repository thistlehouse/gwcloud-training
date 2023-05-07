using MyStore.Models;

namespace MyStore.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Product CreateProduct(Product product);
        Product GetById(Guid id);
        Product UpdateProduct(Product product);
        void Save();                   
    }
}