using System.Net;
using AutoMapper;
using MyStoreApi.Application.Interfaces;
using MyStoreApi.Contracts.OrderDto;
using MyStoreApi.Contracts.OrderProductDto;
using MyStoreApi.Domain.Models;
using MyStoreApi.UseCases;

namespace MyStoreApi.Application.UseCases.OrderUseCases.UpsertProductInOrderUseCase
{
    public class UpsertProductInOrderUseCase : IUpsertProductInOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ServiceResponse<OrderResponse> _serviceResponse;
        private readonly IMapper _mapper;

        public UpsertProductInOrderUseCase(IOrderRepository orderRepository,
            IProductRepository productRepository,
            ServiceResponse<OrderResponse> serviceResponse,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _serviceResponse = serviceResponse;
            _mapper = mapper;
        }

        public ServiceResponse<OrderResponse> UpSertProduct(OrderProductRequest request)
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
                    bool isProductInOrder = productExitsInOrder(order, request);
                    Product product = _productRepository.GetProductById(request.ProductId);

                    if (isProductInOrder)
                    {
                        if (request.Quantity <= 0)
                        {
                            OrderProduct orderProduct = order.OrderProducts
                                .Find(op =>
                                    op.OrderId == request.OrderId &&
                                    op.ProductId == product.Id);

                            order.OrderProducts.Remove(orderProduct);

                            _serviceResponse.Response(HttpStatusCode.OK,
                                $"Product removed from order id: {order.Id}.",
                                true);
                        }
                        else
                        {
                            OrderProduct orderProduct = order.OrderProducts
                                .Find(op => op.OrderId == order.Id &&
                                    op.ProductId == product.Id);

                            orderProduct.SetQuantity(request.Quantity);
                            orderProduct.SetTotal(product.Price);

                            _orderRepository.Save();

                            var response = _mapper.Map<OrderResponse>(order);

                            return _serviceResponse.Response(response,
                                HttpStatusCode.OK,
                                $"Product updated in order id: {order.Id}.",
                                true);
                        }
                    }
                    else
                    {
                        if (!(request.Quantity <= 0))
                        {
                            OrderProduct orderProduct = new OrderProduct(product.Id,
                                request.Quantity,
                                product.Price);

                            order.OrderProducts.Add(orderProduct);

                            _orderRepository.Save();

                            var response = _mapper.Map<OrderResponse>(order);

                            return _serviceResponse.Response(response,
                                HttpStatusCode.Created,
                                $"Product added to order id: {order.Id}.");
                        }

                        return _serviceResponse.Response(
                            HttpStatusCode.BadRequest,
                            "Can't add product to order");
                    }

                    _orderRepository.Save();

                    _serviceResponse.Data = _mapper.Map<OrderResponse>(order);

                    return _serviceResponse;
                }
            }
            catch (Exception ex)
            {
                return _serviceResponse.Response(ex.Message, false);
            }
        }

        private bool productExitsInOrder(Order order,
            OrderProductRequest request)
        {
            return order.OrderProducts
                .Any(op => op.OrderId == request.OrderId &&
                    op.ProductId == request.ProductId);
        }
    }
}