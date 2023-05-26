using System.Net;
using AutoMapper;
using MyStoreApi.Application.Interfaces;
using MyStoreApi.Contracts.OrderDto;
using MyStoreApi.Domain.Models;
using MyStoreApi.UseCases;

namespace MyStoreApi.Application.UseCases.OrderUseCases.CreateOrderUseCase
{
    public class CreateOrderUseCase : ICreateOrderUseCase
    {
        private readonly ServiceResponse<OrderResponse> _serviceResponse;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CreateOrderUseCase(ServiceResponse<OrderResponse> serviceResponse,
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            ICustomerRepository customerRepository,
            IMapper mapper)
        {
            _serviceResponse = serviceResponse;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
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

                return _serviceResponse.Response(response,
                    HttpStatusCode.Created,
                    "Order created.",
                    true);
            }
            catch (Exception ex)
            {
                return _serviceResponse.Response(ex.Message, false);
            }
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