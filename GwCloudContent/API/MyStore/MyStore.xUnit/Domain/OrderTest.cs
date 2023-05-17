using Xunit;
using FluentAssertions;
using MyStore.xUnit.Builders;
using MyStoreApi.Domain.Models;

namespace MyStore.xUnit.Domain
{
    public class OrderTest
    {
        [Fact]
        public void Order_Shoud_BeInvalid()
        {
            var order = OrderFluent.New()
                .WithId(default(Guid))
                .WithOrderProductsEmpty()
                .Build();

            order.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Order_Shoud_BeValid()
        {
            var order = OrderFluent.New()
                .Build();

            Console.WriteLine(order.ValidationResult);
            order.IsValid.Should().BeTrue();
        }

        [Fact]
        public void OrderProudct_ShouldNot_BeNull()
        {
            var order = OrderFluent.New()
                .WithOrderProductsEmpty()
                .Build();

            order.IsValid.Should().BeFalse();
        }

        [Fact]
        public void OrderProductId_ShouldNot_BeDefault()
        {
            Guid orderId = Guid.NewGuid();
            OrderProduct orderProduct = new OrderProduct
            {
                OrderId = orderId,
                ProductId = default(Guid)
            };

            var order = OrderFluent.New()
                .WithId(orderId)
                .WithOrderProducts(orderProduct)
                .Build();

            order.IsValid.Should().BeFalse();
        }

        [Fact]
        public void OrderProductId_ShouldNot_BeEmptyorDefault()
        {
            Guid orderId = Guid.NewGuid();
            OrderProduct orderProduct = new OrderProduct
            {
                OrderId = orderId,
                ProductId = Guid.Empty
            };

            var order = OrderFluent.New()
                .WithId(orderId)
                .WithOrderProducts(orderProduct)
                .Build();

            order.IsValid.Should().BeFalse();
        }

        [Fact]
        public void CustomerId_InOrder_ShoudNot_BeEmptyOrDefault()
        {
            var order = OrderFluent.New()
                .WithCustomerId(Guid.Empty)
                .Build();

            order.IsValid.Should().BeFalse();
        }

        [Fact]
        public void OrderTotal_Should_BeGreaterThan0()
        {
            var order = OrderFluent.New()
                .WithTotal(-1.00m)
                .Build();

            order.IsValid.Should().BeFalse();
        }
    }
}