using MyStore.Contracts.ProductDto;
using MyStore.Models;
using MyStore.Repositories.Interfaces;
using MyStore.Services.Interfaces;

namespace MyStore.Services
{
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product CreateProduct(Product product)
        {
            if (product is null)
                Console.WriteLine("Treat error here");
            
            return _productRepository.CreateProduct(product);
        }

        public Product GetById(ProductRequest request)
        {
            Product product = _productRepository.GetById(request.Id);

            if (request is null)
                Console.WriteLine($"{request} is null here");
            
            return product;
        }

        public Product UpdateProduct(Product request)
        {
            Product product = _productRepository.GetById(request.Id);            

            return _productRepository.UpdateProduct(product);
        }
    }
}