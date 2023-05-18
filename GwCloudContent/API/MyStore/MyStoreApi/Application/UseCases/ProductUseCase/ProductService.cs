using MyStoreApi.Contracts.ProductDto;
using MyStoreApi.Domain.Models;
using MyStoreApi.Application.Interfaces;
using MyStoreApi.Application.UseCases.ProductUserCase;
using AutoMapper;
using System.Net;

namespace MyStoreApi.UseCases.ProductUserCase
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ServiceResponse<ProductResponse> _serviceResponse;

        public ProductService(IProductRepository productRepository,
            ServiceResponse<ProductResponse> serviceReponse,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _serviceResponse = serviceReponse;
            _mapper = mapper;
        }

        public ServiceResponse<ProductResponse> CreateProduct(ProductRequest request)
        {
            Product product = _mapper.Map<Product>(request);

            _productRepository.CreateProduct(product);

            var result = _productRepository.GetProductById(product.Id);
            var response = _mapper.Map<ProductResponse>(result);

            _serviceResponse.Response(response,
                HttpStatusCode.Created,
                "Product was added.",
                true);

            return _serviceResponse;
        }

        public ServiceResponse<ProductResponse> GetProductById(ProductByIdRequest request)
        {
            try
            {
                Product product = _productRepository.GetProductById(request.Id);

                if (product is null)
                {
                    string message = "Could not found the product.";

                    _serviceResponse.Response(HttpStatusCode.NotFound, message, false);
                }
                else
                {
                    var response = _mapper.Map<ProductResponse>(product);

                    _serviceResponse.Response(response, HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                _serviceResponse.Response(ex.Message, false);
            }

            return _serviceResponse;
        }

        public ServiceResponse<ProductResponse> UpdateProduct(ProductRequest request)
        {
            try
            {
                Product product = _productRepository.GetProductById(request.Id);

                if (product is null)
                {
                    _serviceResponse.Response(HttpStatusCode.NotFound,
                        "Could not update the product.",
                        false);
                }
                else
                {
                    _mapper.Map(request, product);
                    _productRepository.Save();
                    _serviceResponse.Response(HttpStatusCode.OK, "Product was updated.");
                }

                _serviceResponse.Data = _mapper.Map<ProductResponse>(product);
            }
            catch (Exception ex)
            {
                _serviceResponse.Response(ex.Message, false);
            }

            return _serviceResponse;
        }
    }
}