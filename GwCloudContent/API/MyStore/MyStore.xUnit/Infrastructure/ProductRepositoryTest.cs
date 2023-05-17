using FluentAssertions;
using Xunit.Frameworks.Autofac;
using Xunit;
using MyStoreApi.Application.Interfaces;
using MyStore.xUnit.Builders;
using System.Net;
using MyStoreApi.Domain.Models;
using MyStoreApi.Infrastructure.Persistence;

namespace MyStore.xUnit.Infrastructure
{
    [UseAutofacTestFramework]
    public class ProductRepositoryTest
    {
        private readonly IProductRepository _productRespository;
        private readonly MyStoreApiDbContext _myStoreDbContext;
        public ProductRepositoryTest(IProductRepository productRepository,
            MyStoreApiDbContext myStoreDbContext)
        {
            _productRespository = productRepository;
            _myStoreDbContext = myStoreDbContext;
        }

        [Fact]
        public void ProductById_Should_ReturnProduct()
        {
            var product = ProductFluent.New()
                .Build();

            _productRespository.CreateProduct(product);

            var result = _productRespository.GetProductById(product.Id);

            result.Should().BeOfType(typeof(Product));
        }

        [Fact]
        public void ProductById_Should_ReturnNull()
        {
            var product = ProductFluent.New()
                .Build();

            var result = _productRespository.GetProductById(product.Id);

            result.Should().BeNull();
        }

        [Fact]
        public void CreateProduct_Should_AddToDatabase()
        {
            var product = ProductFluent.New()
                .Build();

            _productRespository.CreateProduct(product);
            _myStoreDbContext.Products.Should().Contain(product);
        }

        [Fact]
        public void CreateProduct_ShouldNot_AddToDatabase()
        {
            var product = ProductFluent.New()
                .Build();

            _productRespository.CreateProduct(new Product());
            _myStoreDbContext.Products.Should().NotContain(product);
        }

        [Fact]
        public void UpdateProduct_Should_ChangeProduct()
        {
            var product = ProductFluent.New()
                .Build();

            _productRespository.CreateProduct(product);

            var result = _productRespository.GetProductById(product.Id);

            Product productToUpdate = new Product(result.Name,
                16.57m,
                result.OrderProducts);

            _productRespository.UpdateProduct(productToUpdate);

            _myStoreDbContext.Products.Should().Contain(productToUpdate);
        }

        [Fact]
        public void UpdateProduct_ShouldNot_ChangeProduct()
        {
            var product = ProductFluent.New()
                .Build();

            _productRespository.CreateProduct(product);

            var result = _productRespository.GetProductById(product.Id);

            Product productToUpdate = new Product(result.Name,
                16.57m,
                result.OrderProducts);

            _productRespository.UpdateProduct(product);

            _myStoreDbContext.Products.Should().Contain(product);
        }
    }
}