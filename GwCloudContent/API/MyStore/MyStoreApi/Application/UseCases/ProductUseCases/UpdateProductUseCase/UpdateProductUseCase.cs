using System.Net;
using AutoMapper;
using MyStoreApi.Application.Interfaces;
using MyStoreApi.Contracts.ProductDto;
using MyStoreApi.Domain.Models;
using MyStoreApi.UseCases;

namespace MyStoreApi.Application.UseCases.ProductUseCases.UpdateProductUseCase
{
    public class UpdateProductUseCase : IUpdateProductUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly ServiceResponse<ProductResponse> _serviceResponse;
        private readonly IMapper _mapper;

        public UpdateProductUseCase(IProductRepository productRepository,
            ServiceResponse<ProductResponse> serviceResponse,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _serviceResponse = serviceResponse;
            _mapper = mapper;
        }

        public ServiceResponse<ProductResponse> UpdateProduct(ProductRequest request)
        {
            try
            {
                Product product = _productRepository.GetProductById(request.Id);

                if (product is null)
                {
                    return _serviceResponse.Response(HttpStatusCode.NotFound,
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

                return _serviceResponse;

                }
                catch (Exception ex)
                {
                    return _serviceResponse.Response(ex.Message, false);
                }

        }
    }
}