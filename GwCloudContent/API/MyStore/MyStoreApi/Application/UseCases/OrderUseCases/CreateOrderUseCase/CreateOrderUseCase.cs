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

                List<OrderProduct> orderProducts = new List<OrderProduct>();

                foreach (OrderProduct op in request.OrderProducts)
                {
                    Product product = _productRepository.GetProductById(op.ProductId);

                    OrderProduct orderProduct = new OrderProduct(
                        product.Id,
                        op.Quantity,
                        product.Price);

                    orderProducts.Add(orderProduct);
                }

                // OrderTotalToPay(order);
                Coupon coupon = new Coupon(request.Coupon.Code, 30);
                Order order = new Order(Guid.NewGuid(),
                    customer.Id,
                    orderProducts,
                    coupon,
                    "Waiting Payment");

                _orderRepository.CreateOrder(order);

                var response = _mapper.Map<OrderResponse>(order);

                return _serviceResponse.Response(response,
                    HttpStatusCode.Created,
                    "Order created.",
                    true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return _serviceResponse.Response(ex.Message, false);
            }
        }


    }
}