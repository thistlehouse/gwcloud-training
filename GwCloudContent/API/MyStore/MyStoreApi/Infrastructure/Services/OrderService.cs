using MyStoreApi.Contracts.OrderDto;
using MyStoreApi.Contracts.OrderProductDto;
using MyStoreApi.Domain.Models;
using MyStoreApi.Application.Interfaces;
using MyStoreApi.UseCases;
using AutoMapper;
using System.Net;

namespace MyStoreApi.Infrastructure.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        ServiceResponse<OrderResponse> _serviceResponse;

        public OrderService(IOrderRepository orderRepository,
            IProductRepository productRepository,
            ServiceResponse<OrderResponse> serviceResponse,
            ICustomerRepository customerRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _serviceResponse = serviceResponse;
            _customerRepository = customerRepository;
            _mapper = mapper;
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

        public ServiceResponse<OrderResponse> CreateOrder(OrderRequest request)
        {
            try
            {
                Customer customer = _customerRepository.GetCustomerById(request.CustomerId);

                if (customer.IsValid)
                {
                    return _serviceResponse.Response(HttpStatusCode.NotFound,
                        "Customer not found",
                        false);
                }

                Order order = new Order(Guid.NewGuid(), customer.Id);

                foreach (OrderProduct op in request.OrderProducts)
                {
                    Product product = _productRepository.GetProductById(op.ProductId);

                    OrderProduct orderProduct = new OrderProduct(
                        order.Id,
                        product.Id,
                        op.Quantity,
                        product.Price * op.Quantity
                    );

                    order.OrderProducts.Add(orderProduct);
                }

                Coupon coupon = new Coupon(request.Coupon.Code);

                order.Coupon = coupon;

                OrderTotalToPay(order);

                _orderRepository.CreateOrder(order);

                var response = _mapper.Map<OrderResponse>(order);

                _serviceResponse.Response(response,
                    HttpStatusCode.Created,
                    "Order created.",
                    true);
            }
            catch (Exception ex)
            {
                _serviceResponse.Response(ex.Message, false);
            }

            return _serviceResponse;
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

        public ServiceResponse<OrderResponse> RemoveProduct(OrderProductRequest request)
        {
            try
            {
                Order order = _orderRepository.GetOrderById(request.OrderId);

                if (order is null)
                    _serviceResponse.Response(HttpStatusCode.NotFound,
                        "Order not found.",
                        false);
                else
                {
                    bool isProductInOrder = false;
                    Product product = productExitsInOrder(order, request, out isProductInOrder);

                    if (!isProductInOrder)
                        _serviceResponse.Response(HttpStatusCode.NotFound,
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
                    }
                }
            }
            catch (Exception ex)
            {
                _serviceResponse.Response(ex.Message, false);
            }

            return _serviceResponse;
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
                    order.TotalToPay = decimal.Round(order.OrderProducts
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