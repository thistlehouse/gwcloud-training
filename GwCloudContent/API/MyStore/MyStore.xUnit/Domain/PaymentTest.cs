using Xunit;
using FluentAssertions;
using MyStore.xUnit.Fluents;

namespace MyStore.xUnit.Domain
{
    public class PaymentTest
    {
        [Fact]
        public void Payment_Should_ReturnValid()
        {
            var payment = PaymentFluent.New()
                .Build();

            payment.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Payment_Should_ReturnInvalid()
        {
            var payment = PaymentFluent.New()
                .WithCreditCardCvv(CreditCardFluent.New()
                    .WithCvv("abc4")
                    .Build())
                .Build();

            payment.IsValid.Should().BeFalse();
        }
    }
}