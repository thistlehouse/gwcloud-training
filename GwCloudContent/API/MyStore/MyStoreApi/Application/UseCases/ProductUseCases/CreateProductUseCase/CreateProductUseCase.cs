using System.Net;
using AutoMapper;
using MyStoreApi.Application.Interfaces;
using MyStoreApi.Contracts.ProductDto;
using MyStoreApi.Domain.Models;
using MyStoreApi.UseCases;

namespace MyStoreApi.Application.UseCases.ProductUseCases.CreateProductUseCase
{
    public class CreateProductUseCase : ICreateProductUseCase
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ServiceResponse<ProductResponse> _serviceResponse;
        public CreateProductUseCase(IMapper mapper,
            ServiceResponse<ProductResponse> serviceResponse,
            IProductRepository productRepository)
        {
            _mapper = mapper;
            _serviceResponse = serviceResponse;
            _productRepository = productRepository;
        }

        public ServiceResponse<ProductResponse> CreateProduct(ProductRequest request)
        {
            try
            {
                Product product = _mapper.Map<Product>(request);

                if (!product.IsValid)
                    return _serviceResponse.Response(null,
                        HttpStatusCode.BadRequest,
                        false);

                _productRepository.CreateProduct(product);

                var result = _productRepository.GetProductById(product.Id);
                var response = _mapper.Map<ProductResponse>(result);

                _serviceResponse.Response(response,
                    HttpStatusCode.Created,
                    "Product was added.",
                    true);

                return _serviceResponse;
            }
            catch (Exception ex)
            {
                return _serviceResponse.Response(
                    HttpStatusCode.InternalServerError,
                    ex.Message,
                    false);
            }
        }
    }
}