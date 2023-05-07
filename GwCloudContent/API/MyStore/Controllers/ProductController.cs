using Microsoft.AspNetCore.Mvc;
using MyStore.Contracts.ProductDto;
using MyStore.Models;
using MyStore.Services.Interfaces;

namespace MyStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        
        [HttpPost("new")]
        public ActionResult<ProductResponse> CreateProduct(ProductRequest request)
        {
            Product product = MapProductRequest(request);
            Product productResponse = _productService.CreateProduct(product);

            return MapProductResponse(product);
        }

        [HttpPost("product")]
        public ProductResponse GetById([FromBody] ProductRequest request)
        {
            Product product = _productService.GetById(request);

            return MapProductResponse(product);
        }

        [HttpPut("update")]
        public ProductResponse UpdateProduct(ProductRequest request)
        {
            Product product = MapProductRequest(request);
            Product productResponse = _productService.UpdateProduct(product);
            
            return MapProductResponse(productResponse);
        }

        private Product MapProductRequest(ProductRequest request)
        {
            return new Product(
                request.Id,
                request.Name,
                request.Price);
        }

        private ProductResponse MapProductResponse(Product product)
        {
            return new ProductResponse(                
                product.Name,
                product.Price,
                product.OrderProducts);
        }
    }
}