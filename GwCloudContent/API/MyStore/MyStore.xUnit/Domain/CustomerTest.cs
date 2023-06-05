using FluentAssertions;
using MyStore.xUnit.Fluents;
using Xunit;

namespace MyStore.xUnit.Domain
{

    public class CustomerTest
    {
        [Fact]
        public void Customer_ShouldBeValid()
        {
            var customer = CustomerFluent.New()
                .Build();

            customer.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Customer_Id_ShouldNotBe_Default()
        {
            var customer = CustomerFluent.New()
                .WithId(new Guid())
                .Build();

            customer.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Customer_Id_ShouldNot_BeEmpty()
        {
            var customer = CustomerFluent.New()
                .WithId(Guid.Empty)
                .Build();

            customer.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Customer_Name_ShouldNot_BeEmpty()
        {
            var customer = CustomerFluent.New()
                .WithName("")
                .Build();

            customer.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Customer_Name_ShouldNot_BeNull()
        {
            var customer = CustomerFluent.New()
                .WithName(null)
                .Build();

            customer.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Customer_Name_ShouldNot_BeLessThan1()
        {
            var customer = CustomerFluent.New()
                .WithName("J")
                .Build();

            customer.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Customer_Name_ShouldNot_GreaterThan30()
        {
            var customer = CustomerFluent.New()
                .WithName("ABCDEFGHIJKLMNOPQRSTUVXYWZABCDEFGHIJKLMNOPQRSTUVXYWZ")
                .Build();

            customer.IsValid.Should().BeFalse();
        }

    }
}