using MyStore.xUnit.Fluents;
using FluentAssertions;
using Xunit;
using Xunit.Frameworks.Autofac;
using MyStoreApi.Application.Interfaces;
using MyStoreApi.Contracts.OrderDto;
using MyStoreApi.Domain.Models;
using MyStoreApi.Contracts.OrderProductDto;
using MyStoreApi.Application.UseCases.OrderUseCases.CreateOrderUseCase;
using MyStoreApi.Application.UseCases.OrderUseCases.GetOrderByIdUseCase;
using MyStoreApi.Application.UseCases.OrderUseCases.UpsertProductInOrderUseCase;
using MyStoreApi.Application.UseCases.OrderUseCases.RemoveProductFromOrderUseCase;

namespace MyStore.xUnit.Application.Services.UseCases.OrderUseCase
{
    [UseAutofacTestFramework]
    public class OrderServiceTest
    {
        private readonly ICreateOrderUseCase _createOrder;
        private readonly IGetOrderByIdUseCase _getOrderById;
        private readonly IUpsertProductInOrderUseCase _upSertProduct;
        private readonly IRemoveProductFromOrderUseCase _removeProductFromOrder;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrderServiceTest(ICreateOrderUseCase createOrder,
            IGetOrderByIdUseCase getOrderById,
            IUpsertProductInOrderUseCase upsertProduct,
            ICustomerRepository customerRepository,
            IProductRepository productRepository
            )
        {
            _createOrder = createOrder;
            _getOrderById = getOrderById;
            _upSertProduct = upsertProduct;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _createOrder = createOrder;
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

            var response = _createOrder.CreateOrder(request);
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

            var orderResponse = _createOrder.CreateOrder(request);

            OrderByIdRequest orderByIdRequest = new OrderByIdRequest
            {
                Id = order.Id
            };

            var response = _getOrderById.GetOrderById(orderByIdRequest);

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

            var orderResponse = _createOrder.CreateOrder(request);

            var product = ProductFluent.New()
                .Build();

            _productRepository.CreateProduct(product);

            OrderProductRequest orderProductRequest = new OrderProductRequest
            {
                ProductId = product.Id,
                OrderId = order.Id,
                Quantity = 1
            };

            var reponse = _upSertProduct.UpSertProduct(orderProductRequest);
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

            var orderResponse = _createOrder.CreateOrder(request);

            OrderByIdRequest orderByIdRequest = new OrderByIdRequest
            {
                Id = order.Id
            };

            var response = _getOrderById.GetOrderById(orderByIdRequest);

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

            var orderResponse = _createOrder.CreateOrder(request);

            OrderByIdRequest orderByIdRequest = new OrderByIdRequest
            {
                Id = order.Id
            };

            var response = _getOrderById.GetOrderById(orderByIdRequest);
            var errorMessage = new HttpResponseMessage(response.HttpCode);
        }
    }
}