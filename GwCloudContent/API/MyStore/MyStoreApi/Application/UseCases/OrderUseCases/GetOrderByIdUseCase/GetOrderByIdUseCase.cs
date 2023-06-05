using System.Net;
using AutoMapper;
using MyStoreApi.Application.Interfaces;
using MyStoreApi.Contracts.OrderDto;
using MyStoreApi.Domain.Models;
using MyStoreApi.UseCases;

namespace MyStoreApi.Application.UseCases.OrderUseCases.GetOrderByIdUseCase
{
    public class GetOrderByIdUseCase : IGetOrderByIdUseCase
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly ServiceResponse<OrderResponse> _serviceResponse;

        public GetOrderByIdUseCase(IMapper mapper,
            IOrderRepository orderRepository,
            ServiceResponse<OrderResponse> serviceResponse)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _serviceResponse = serviceResponse;
        }

        public ServiceResponse<OrderResponse> GetOrderById(OrderByIdRequest request)
        {
            try
            {
                Order order = _mapper.Map<Order>(request);
                var result = _orderRepository.GetOrderById(order.Id);

                if (result is null)
                {
                    return _serviceResponse.Response(HttpStatusCode.NotFound,
                        "Order not found.",
                        false);
                }
                else
                {
                    var response = _mapper.Map<OrderResponse>(result);

                    _serviceResponse.Response(response,
                        HttpStatusCode.OK,
                        "Found.");
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