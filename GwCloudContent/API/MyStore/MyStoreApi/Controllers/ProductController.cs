using Microsoft.AspNetCore.Mvc;
using MyStoreApi.Contracts.ProductDto;
using MyStoreApi.UseCases;
using System.Net;
using MyStoreApi.Application.UseCases.ProductUseCases.UpdateProductUseCase;
using MyStoreApi.Application.UseCases.ProductUseCases.CreateProductUseCase;
using MyStoreApi.Application.UseCases.ProductUseCases.GetProductByIdUseCase;

namespace MyStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IUpdateProductUseCase _updateProduct;
        private readonly ICreateProductUseCase _createProduct;
        private readonly IGetProductByIdUseCase _getProductById;

        public ProductController(IUpdateProductUseCase updateProduct,
            ICreateProductUseCase createProduct,
            IGetProductByIdUseCase getProductById)
        {
            _updateProduct = updateProduct;
            _createProduct = createProduct;
            _getProductById = getProductById;
        }

        [HttpPost("new")]
        public ActionResult<ServiceResponse<ProductResponse>> CreateProduct(ProductRequest request)
        {
            return Ok(_createProduct.CreateProduct(request));
        }

        [HttpPost("product")]
        public ActionResult<ServiceResponse<ProductResponse>> GetById([FromBody] ProductByIdRequest request)
        {
            var response = _getProductById.GetProductById(request);

            if (response.HttpCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }

            return Ok(response.Data);
        }

        [HttpPut("update")]
        public ActionResult<ServiceResponse<ProductResponse>> UpdateProduct(ProductRequest request)
        {
            var response = _updateProduct.UpdateProduct(request);

            if (response.HttpCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}