using FluentAssertions;
using MyStoreApi.Application.UseCases.ProductUseCases.CreateProductUseCase;
using MyStoreApi.Application.UseCases.ProductUseCases.GetProductByIdUseCase;
using MyStoreApi.Application.UseCases.ProductUseCases.UpdateProductUseCase;
using MyStoreApi.Contracts.ProductDto;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace MyStore.xUnit.Application.Services.UseCases.ProductUserCase
{
    [UseAutofacTestFramework]
    public class ProductServiceTest
    {
        private readonly ICreateProductUseCase _createProduct;
        private readonly IGetProductByIdUseCase _getProductById;
        private readonly IUpdateProductUseCase _updateProduct;


        public ProductServiceTest(IUpdateProductUseCase updateProduct,
            IGetProductByIdUseCase getProductById,
            ICreateProductUseCase createProduct)
        {
            _updateProduct = updateProduct;
            _getProductById = getProductById;
            _createProduct = createProduct;
        }

        [Fact]
        public void GetProductById_Should_ReturnProduct_WhenProductExists()
        {
            ProductRequest request = new ProductRequest
            {
                Id = Guid.NewGuid(),
                Name = "New Product Creation Test",
                Price = 23.83m
            };

            _createProduct.CreateProduct(request);

            ProductByIdRequest prodcutId = new ProductByIdRequest();
            prodcutId.Id = request.Id;

            var response =_getProductById.GetProductById(prodcutId);

            response.Success.Should().BeTrue();
        }

        [Fact]
        public void GetProductById_ShouldReturn_NotFound()
        {
            ProductByIdRequest productId = new ProductByIdRequest
            {
                Id = Guid.NewGuid()
            };

            var response = _getProductById.GetProductById(productId);
            var errorResponse = new HttpResponseMessage(response.HttpCode);

            errorResponse.Should().HaveClientError("404, Product Not Found.");
        }

        [Fact]
        public void CreateProduct_ShouldReturn_OK()
        {
            ProductRequest request = new ProductRequest
            {
                Name = "Product Create Testing",
                Price = 15.55m
            };

            var response = _createProduct.CreateProduct(request);
            var successResponse = new HttpResponseMessage(response.HttpCode);

            successResponse.Should().BeSuccessful("Product created successfully.");
        }

        [Fact]
        public void UpdateProduct_ShouldReturn_OK()
        {
            Guid id = Guid.NewGuid();

            ProductRequest request = new ProductRequest
            {
                Id = id,
                Name = "Product Update Testing",
                Price = 15.55m
            };

            _createProduct.CreateProduct(request);

            ProductRequest update = new ProductRequest
            {
                Id = id,
                Name = "Product Changed",
                Price = 16.55m
            };

            var response = _updateProduct.UpdateProduct(update);
            var successResponse = new HttpResponseMessage(response.HttpCode);

            successResponse.Should().BeSuccessful("Product updated successfully.");
        }

        [Fact]
        public void UpdateProduct_ShouldReturn_NotFound()
        {
            ProductRequest productId = new ProductRequest
            {
                Id = Guid.NewGuid()
            };

            var response = _updateProduct.UpdateProduct(productId);
            var successResponse = new HttpResponseMessage(response.HttpCode);

            successResponse.Should().HaveClientError("404, Product Not Found.");
        }
    }
}