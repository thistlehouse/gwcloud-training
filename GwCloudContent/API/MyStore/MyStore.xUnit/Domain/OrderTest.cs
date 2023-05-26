using Xunit;
using FluentAssertions;
using MyStore.xUnit.Fluents;
using MyStoreApi.Domain.Models;

namespace MyStore.xUnit.Domain
{
    public class OrderTest
    {
        [Fact]
        public void Order_Should_BeInvalid()
        {
            var order = OrderFluent.New()
                .WithId(default(Guid))
                .WithOrderProductsEmpty()
                .Build();

            order.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Order_Should_BeValid()
        {
            var order = OrderFluent.New()
                .WithTotalPrice(20.00m)
                .Build();

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
        public void OrderProduct_Product_Id_ShouldNot_BeDefault()
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
        public void OrderProduct_Product_Id_ShouldNot_BeEmptyorDefault()
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
        public void Customer_Id_InOrder_ShoudNot_BeEmptyOrDefault()
        {
            var order = OrderFluent.New()
                .WithCustomerId(Guid.Empty)
                .Build();

            order.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Order_Total_Should_BeGreaterThan0()
        {
            var order = OrderFluent.New()
                .WithTotal(-1.00m)
                .Build();

            order.IsValid.Should().BeFalse();
        }
    }
}