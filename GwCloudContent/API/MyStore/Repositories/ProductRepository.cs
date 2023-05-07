using MyStore.Models;
using MyStore.Persistence;
using MyStore.Repositories.Interfaces;

namespace MyStore.Repositories
{
    public class ProductRepository : IProductRepository
    {
        MyStoreDbContext _myStoreContext;

        public ProductRepository(MyStoreDbContext myStoreContext)
        {
            _myStoreContext = myStoreContext;
        }
        
        public Product CreateProduct(Product product)
        {
            _myStoreContext.Products.Add(product);

            Save();

            return product;
        }

        public Product GetById(Guid id)
        {
            return _myStoreContext.Products.Find(id);
        }

        public Product UpdateProduct(Product product)
        {
             _myStoreContext.Products.Update(product);

             Save();

             return product;
        }

        public void Save()
        {
            _myStoreContext.SaveChanges();
        }
    }
}