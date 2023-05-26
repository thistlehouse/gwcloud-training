using System.Net;
using AutoMapper;
using MyStoreApi.Application.Interfaces;
using MyStoreApi.Contracts.ProductDto;
using MyStoreApi.Domain.Models;
using MyStoreApi.UseCases;

namespace MyStoreApi.Application.UseCases.ProductUseCases.GetProductByIdUseCase
{
    public class GetProductByIdUseCase : IGetProductByIdUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly ServiceResponse<ProductResponse> _serviceResponse;
        private readonly IMapper _mapper;

        public GetProductByIdUseCase(IProductRepository productRepository,
            ServiceResponse<ProductResponse> serviceResponse,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _serviceResponse = serviceResponse;
            _mapper = mapper;
        }
        public ServiceResponse<ProductResponse> GetProductById(ProductByIdRequest request)
        {
            try
            {
                Product product = _productRepository.GetProductById(request.Id);

                if (product is null)
                {
                    _serviceResponse.Response(HttpStatusCode.NotFound,
                        "product not found",
                        false);
                }
                else
                {
                    var response = _mapper.Map<ProductResponse>(product);

                    return _serviceResponse.Response(response,
                        HttpStatusCode.OK,
                        "Product found",
                        true);
                }
            }
            catch (Exception ex)
            {
                _serviceResponse.Response(ex.Message, false);
            }

            return _serviceResponse;
        }
    }
}