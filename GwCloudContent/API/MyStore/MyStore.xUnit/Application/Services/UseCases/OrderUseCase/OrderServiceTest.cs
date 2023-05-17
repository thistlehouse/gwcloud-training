using MyStore.xUnit.Builders;
using FluentAssertions;
using Xunit;
using Xunit.Frameworks.Autofac;
using MyStoreApi.Application.Interfaces;
using MyStoreApi.Contracts.OrderDto;
using MyStoreApi.Domain.Models;
using MyStoreApi.Contracts.CustomerDto;
using MyStoreApi.Contracts.OrderProductDto;

namespace MyStore.xUnit.Application.Services.UseCases.OrderUseCase
{
    [UseAutofacTestFramework]
    public class OrderServiceTest
    {
        private readonly IOrderService _orderService;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrderServiceTest(IOrderService orderService,
            ICustomerRepository customerRepository,
            IProductRepository productRepository)
        {
            _orderService = orderService;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }

        [Fact]
        public void CreateOrder_Should_ReturnOK()
        {
            var order = OrderFluent.New()
                .Build();

            _customerRepository.CreateCustomer(order.Customer);

            foreach(var op in order.OrderProducts)
            {
                _productRepository.CreateProduct(op.Product);
            }

            OrderRequest request = new OrderRequest
            {
                CustomerId = order.Customer.Id,
                OrderProducts = order.OrderProducts,
                Coupon = order.Coupon
            };

            var response = _orderService.CreateOrder(request);
            var successResponse = new HttpResponseMessage(response.HttpCode);

            successResponse.Should().BeSuccessful("Order created successfully.");
        }

        [Fact]
        public void GetOrderById_Should_ReturnProduct_WhenOrderExists()
        {
            var order = OrderFluent.New()
                .Build();

            _customerRepository.CreateCustomer(order.Customer);

            foreach(var op in order.OrderProducts)
            {
                _productRepository.CreateProduct(op.Product);
            }

            OrderRequest request = new OrderRequest
            {
                Id = order.Id,
                CustomerId = order.Customer.Id,
                Coupon = order.Coupon,
                OrderProducts = order.OrderProducts
            };

            var orderResponse = _orderService.CreateOrder(request);

            OrderByIdRequest orderByIdRequest = new OrderByIdRequest
            {
                Id = order.Id
            };

            var response = _orderService.GetOrderById(orderByIdRequest);

            response.Success.Should().BeTrue();
        }

        [Fact]
        public void UpdateOrderProduct_Should_Rerturn_OkWhenUpdated()
        {
            var order = OrderFluent.New()
                .Build();

            _customerRepository.CreateCustomer(order.Customer);

            foreach(var op in order.OrderProducts)
            {
                _productRepository.CreateProduct(op.Product);
            }

            OrderRequest request = new OrderRequest
            {
                CustomerId = order.Customer.Id,
                OrderProducts = order.OrderProducts,
                Coupon = order.Coupon
            };

            var orderResponse = _orderService.CreateOrder(request);

            var product = ProductFluent.New()
                .Build();

            _productRepository.CreateProduct(product);

            OrderProductRequest orderProductRequest = new OrderProductRequest
            {
                ProductId = product.Id,
                OrderId = order.Id,
                Quantity = 1
            };

            var reponse = _orderService.UpSertProduct(orderProductRequest);
            var successResponse = new HttpResponseMessage(reponse.HttpCode);

            successResponse.Should().BeSuccessful("Order Updated.");
        }

        [Fact]
        public void OrderTotalToPay_Should_Return_CorrectValue()
        {
            var order = OrderFluent.New()
                .Build();

            _customerRepository.CreateCustomer(order.Customer);

            foreach(var op in order.OrderProducts)
            {
                _productRepository.CreateProduct(op.Product);
            }

            OrderRequest request = new OrderRequest
            {
                Id = order.Id,
                CustomerId = order.Customer.Id,
                OrderProducts = order.OrderProducts,
                Coupon = order.Coupon
            };

            var orderResponse = _orderService.CreateOrder(request);

            OrderByIdRequest orderByIdRequest = new OrderByIdRequest
            {
                Id = order.Id
            };

            var response = _orderService.GetOrderById(orderByIdRequest);

            var isTotalCorret = order.TotalToPay == response.Data.TotalToPay
                ? true
                : false;

            isTotalCorret.Should().BeTrue();
        }

        [Fact]
        public void OrderCoupun_Should_BeInvalid()
        {
            var order = OrderFluent.New()
                .WithCoupon(new Coupon { Code = "Helloween" })
                .Build();

            _customerRepository.CreateCustomer(order.Customer);

            foreach(var op in order.OrderProducts)
            {
                _productRepository.CreateProduct(op.Product);
            }

            OrderRequest request = new OrderRequest
            {
                Id = order.Id,
                CustomerId = order.Customer.Id,
                OrderProducts = order.OrderProducts,
                Coupon = order.Coupon
            };

            var orderResponse = _orderService.CreateOrder(request);

            OrderByIdRequest orderByIdRequest = new OrderByIdRequest
            {
                Id = order.Id
            };

            var response = _orderService.GetOrderById(orderByIdRequest);
            var errorMessage = new HttpResponseMessage(response.HttpCode);
        }
    }
}