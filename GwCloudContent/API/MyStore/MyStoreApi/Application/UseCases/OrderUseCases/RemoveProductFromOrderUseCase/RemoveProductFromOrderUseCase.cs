using System.Net;
using AutoMapper;
using MyStoreApi.Application.Interfaces;
using MyStoreApi.Contracts.OrderDto;
using MyStoreApi.Contracts.OrderProductDto;
using MyStoreApi.Domain.Models;
using MyStoreApi.UseCases;

namespace MyStoreApi.Application.UseCases.OrderUseCases.RemoveProductFromOrderUseCase
{
    public class RemoveProductFromOrderUseCase : IRemoveProductFromOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ServiceResponse<OrderResponse> _serviceResponse;
        private readonly IMapper _mapper;

        public RemoveProductFromOrderUseCase(IOrderRepository orderRepository,
            IProductRepository productRepository,
            ServiceResponse<OrderResponse> serviceResponse,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _serviceResponse = serviceResponse;
            _mapper = mapper;
        }

        public ServiceResponse<OrderResponse> RemoveProduct(OrderProductRequest request)
        {
            try
            {
                Order order = _orderRepository.GetOrderById(request.OrderId);

                if (order is null)
                    return _serviceResponse.Response(HttpStatusCode.NotFound,
                        "Order not found.",
                        false);
                else
                {
                    bool  isProductInOrder = false;
                    Product product = productExitsInOrder(order, request, out isProductInOrder);

                    if (!isProductInOrder)
                        return _serviceResponse.Response(HttpStatusCode.NotFound,
                            "You don't have this product in your order.",
                            false);
                    else
                    {
                        OrderProduct removeProduct = order.OrderProducts
                            .Find(op =>
                                op.OrderId == request.OrderId &&
                                op.ProductId == product.Id);

                        order.OrderProducts.Remove(removeProduct);

                        _serviceResponse.Data = _mapper.Map<OrderResponse>(order);

                        return _serviceResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                return _serviceResponse.Response(ex.Message, false);
            }
        }

        private Product productExitsInOrder(Order order,
            OrderProductRequest request,
            out bool isProductInOrder)
        {
            bool exists = order.OrderProducts
                .Any(op => op.ProductId == request.ProductId);

            isProductInOrder = exists;

            return _productRepository.GetProductById(request.ProductId);
        }

    }
}