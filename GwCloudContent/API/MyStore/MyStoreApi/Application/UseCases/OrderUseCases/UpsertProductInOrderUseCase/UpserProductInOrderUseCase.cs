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
                    OrderProduct orderProduct = new OrderProduct();
                    bool isProductInOrder = false;
                    Product product = productExitsInOrder(order, request, out isProductInOrder);

                    if (isProductInOrder)
                    {
                        orderProduct = order.OrderProducts
                            .Find(op =>
                                op.OrderId == request.OrderId &&
                                op.ProductId == request.ProductId);

                        if (request.Quantity == 0)
                        {
                            order.OrderProducts.Remove(orderProduct);

                            _serviceResponse.Response(HttpStatusCode.OK,
                                $"Product removed from order id: {order.Id}.",
                                true);
                        }
                        else
                        {
                            orderProduct.Quantity += request.Quantity;
                            orderProduct.Total = orderProduct.Quantity * product.Price;

                            return _serviceResponse.Response(HttpStatusCode.OK,
                                $"Product updated in order id: {order.Id}.",
                                true);
                        }
                    }
                    else
                    {
                        orderProduct.OrderId = request.OrderId;
                        orderProduct.ProductId = request.ProductId;
                        orderProduct.Quantity = request.Quantity;
                        orderProduct.Total = request.Quantity * product.Price;

                        order.OrderProducts.Add(orderProduct);

                        return _serviceResponse.Response(HttpStatusCode.Created,
                            $"Product added to order id: {order.Id}.");

                    }

                     OrderTotalToPay(order);

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

                private Product productExitsInOrder(Order order,
            OrderProductRequest request,
            out bool isProductInOrder)
        {
            bool exists = order.OrderProducts
                .Any(op => op.ProductId == request.ProductId);

            isProductInOrder = exists;

            return _productRepository.GetProductById(request.ProductId);
        }

        private void OrderTotalToPay(Order order)
        {
            order.Coupon.Value = 30;

            if (IsBlackFridayToday(order))
            {
                if (order.Coupon.Active)
                    order.TotalToPay = Decimal.Round(order.OrderProducts
                        .Sum(op => op.Total) * ((100 - order.Coupon.Value) / 100m),
                        2,
                        MidpointRounding.AwayFromZero);
            }
            else
                order.TotalToPay = order.OrderProducts.Sum(op => op.Total);
        }

        private void ActivateCoupon(Order order)
        {
            order.Coupon.Active = true;
        }

        private bool IsBlackFridayToday(Order order)
        {
            DateTime today = DateTime.Today;
            var beginBlackFriday = order.Coupon.StartDate =
                new DateTime(DateTime.Now.Year, 11, 23);
            var endBlackFriday = order.Coupon.ExpirationDate =
                new DateTime(DateTime.Now.Year, 11, 29).AddDays(1);

            if (today >= beginBlackFriday && today <= endBlackFriday)
            {
                if (ValidCoupon(order.Coupon))
                {
                    ActivateCoupon(order);

                    return true;
                }
            }

            return false;
        }

        private bool ValidCoupon(Coupon coupon)
        {
            List<string> validCode = new List<string>
            {
                { "black friday "}
            };

            bool valid = validCode[0].Contains(coupon.Code.ToLower());

            return valid;
        }
    }
}