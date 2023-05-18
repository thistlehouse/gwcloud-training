using Microsoft.AspNetCore.Mvc;
using MyStoreApi.Contracts.ProductDto;
using MyStoreApi.Domain.Models;
using MyStoreApi.Application.UseCases.ProductUserCase;
using MyStoreApi.UseCases;
using AutoMapper;
using System.Net;

namespace MyStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("new")]
        public ActionResult<ServiceResponse<ProductResponse>> CreateProduct(ProductRequest request)
        {            
            return Ok(_productService.CreateProduct(request));        
        }

        [HttpPost("product")]
        public ActionResult<ServiceResponse<ProductResponse>> GetById([FromBody] ProductByIdRequest request)
        {
            var response = _productService.GetProductById(request);

            if (response.Success == false)
            {
                Console.WriteLine(response.Message);
            }

            if (response.HttpCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }

            return Ok(response.Data);
        }

        [HttpPut("update")]
        public ActionResult<ServiceResponse<ProductResponse>> UpdateProduct(ProductRequest request)
        {
            var response = _productService.UpdateProduct(request);
            
            if (response.HttpCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}