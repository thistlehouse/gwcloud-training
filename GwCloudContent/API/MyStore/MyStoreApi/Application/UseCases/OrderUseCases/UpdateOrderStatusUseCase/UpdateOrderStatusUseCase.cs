using System.Net;
using AutoMapper;
using MyStoreApi.Application.Interfaces;
using MyStoreApi.Contracts.OrderDto;
using MyStoreApi.Contracts.PaymentDto;
using MyStoreApi.Domain.Models;
using MyStoreApi.UseCases;

namespace MyStoreApi.Application.UseCases.OrderUseCases.UpdateOrderStatusUseCase
{
    public class UpdateOrderStatusUseCase : IUpdateOrderStatusUseCase
    {
        private readonly ServiceResponse<OrderResponse> _serviceResponse;
        private readonly IPaymentService _paymentService;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public UpdateOrderStatusUseCase(ServiceResponse<OrderResponse> serviceResponse,
            IPaymentService paymentService,
            IOrderRepository orderRepository,
            IMapper mapper)
        {
            _serviceResponse = serviceResponse;
            _paymentService = paymentService;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public ServiceResponse<OrderResponse> UpdateOrderStatus(PaymentLoad load)
        {
            var order = _orderRepository.GetOrderById(load.OrderId);

            Console.WriteLine($"Order {order.OrderStatus} is being updated");

            if (load.Status == PaymentStatus.Paid)
            {
                order.OrderStatus = "Paid";

                _orderRepository.Save();
            }

            var response = _mapper.Map<OrderResponse>(order);

            return _serviceResponse.Response(
                response,
                HttpStatusCode.OK,
                $"Order Status Updated to: {order.OrderStatus}",
                true);
        }
    }
}