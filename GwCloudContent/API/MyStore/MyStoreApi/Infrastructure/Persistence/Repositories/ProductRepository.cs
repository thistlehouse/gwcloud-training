using MyStoreApi.Domain.Models;
using MyStoreApi.Application.Interfaces;
using MyStoreApi.Infrastructure.Persistence;
using MyStoreApi.Contracts.ProductDto;

namespace MyStoreApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        MyStoreApiDbContext _myStoreApiContext;

        public ProductRepository(MyStoreApiDbContext myStoreApiContext)
        {
            _myStoreApiContext = myStoreApiContext;
        }
        
        public void CreateProduct(Product product)
        {
            _myStoreApiContext.Products.Add(product);

            Save();
        }

        public Product GetProductById(Guid id)
        {
            return _myStoreApiContext.Products.Find(id);
        }

        public void UpdateProduct(Product product)
        {
             _myStoreApiContext.Products.Update(product);

             Save();
        }

        public void Save()
        {
            _myStoreApiContext.SaveChanges();
        }
    }
}