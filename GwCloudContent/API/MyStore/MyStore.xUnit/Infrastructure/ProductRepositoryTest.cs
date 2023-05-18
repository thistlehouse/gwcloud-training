using FluentAssertions;
using Xunit.Frameworks.Autofac;
using Xunit;
using MyStoreApi.Application.Interfaces;
using MyStore.xUnit.Builders;
using System.Net;
using MyStoreApi.Domain.Models;
using MyStoreApi.Infrastructure.Persistence;
using System.Reflection;

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

            if (product.IsValid)
                _productRespository.CreateProduct(product);

            _myStoreDbContext.Products.Should().Contain(product);
        }

        [Fact]
        public void CreateProduct_ShouldNot_AddToDatabase()
        {
            var product = ProductFluent.New()
                .WithId(default(Guid))
                .WithName("ABCDEFGHIJKLMNOPQRSTUVWYXZABCDEFGHIJKLMNOPQRSTUVWYXZ01234")
                .WithPrice(0)
                .Build();

            if (product.IsValid)
                _productRespository.CreateProduct(product);

            _myStoreDbContext.Products.Should().NotContain(product);
        }

        [Fact]
        public void UpdateProduct_Should_ChangeProduct()
        {
            var product = ProductFluent.New()
                .Build();

            _productRespository.CreateProduct(product);

            var result = _productRespository.GetProductById(product.Id);

            PropertyInfo name = typeof(Product).GetProperty(nameof(Product.Name));
            PropertyInfo price = typeof(Product).GetProperty(nameof(Product.Price));

            name.SetValue(result, "New Product Name");
            price.SetValue(result, 16.78m);

            _productRespository.UpdateProduct(result);

            var productCopy = ProductFluent.New()
                .WithId(product.Id)
                .WithName(product.Name)
                .WithPrice(product.Price)
                .Build();

            _myStoreDbContext.Products.Should().Contain(result);
        }

        [Fact]
        public void UpdateProduct_ShouldNot_ChangeProduct()
        {
            var product = ProductFluent.New()
                .Build();

            _productRespository.CreateProduct(product);

            var result = _productRespository.GetProductById(product.Id);

            var productToUpdate = ProductFluent.New()
                .WithName("ABCDEFGHIJKLMNOPQRSTUVWYXZABCDEFGHIJKLMNOPQRSTUVWYXZ")
                .Build();

            if (productToUpdate.IsValid)
                _productRespository.UpdateProduct(product);

            _myStoreDbContext.Products.Should().NotContain(productToUpdate);
        }
    }
}