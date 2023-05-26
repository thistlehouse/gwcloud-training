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

                        OrderTotalToPay(order);

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