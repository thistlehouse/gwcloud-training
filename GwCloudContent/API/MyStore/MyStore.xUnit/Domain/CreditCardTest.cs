using Xunit;
using FluentAssertions;
using MyStoreApi.Domain.Models;
using MyStore.xUnit.Fluents;

namespace MyStore.xUnit.Domain
{
    public class CreditCardTest
    {
        [Fact]
        public void CreditCard_Should_BeValid()
        {
            var cc = CreditCardFluent.New()
                .WithCvv("777")
                .Build();

            cc.IsValid.Should().BeTrue();
        }

        [Fact]
        public void CreditCard_Should_BeInvalid()
        {
            var cc = CreditCardFluent.New()
                .WithNumber("0000000000000000")
                .WithExpireYear(2022)
                .WithExpireMonth(2)
                .Build();

            cc.IsValid.Should().BeFalse();
        }

        [Fact]
        public void CreditCard_Year_Should_BeValid()
        {
            var cc = CreditCardFluent.New()
                .WithExpireYear(2024)
                .Build();

            cc.IsValid.Should().BeFalse();
        }

        [Fact]
        public void CreditCard_Year_Should_BeInvalid()
        {
            var cc = CreditCardFluent.New()
                .WithExpireYear(2022)
                .Build();

            cc.IsValid.Should().BeFalse();
        }

        [Fact]
        public void CreditCard_Month_Should_BeValid()
        {
            var cc = CreditCardFluent.New()
                .WithExpireYear(2023)
                .WithExpireMonth(6)
                .Build();

            cc.IsValid.Should().BeFalse();
        }

        [Fact]
        public void CreditCard_Month_Should_BeInvalid()
        {
            var cc = CreditCardFluent.New()
                .WithExpireYear(2023)
                .WithExpireMonth(4)
                .Build();

            cc.IsValid.Should().BeFalse();
        }

        [Fact]
        public void CreditCard_Cvv_Should_BeValid()
        {
            var cc = CreditCardFluent.New()
                .WithCvv("777")
                .Build();
        }

        [Fact]
        public void CreditCard_Cvv_Should_BeInvalid()
        {
            var cc = CreditCardFluent.New()
                .WithCvv("a3c")
                .Build();
        }
    }
}