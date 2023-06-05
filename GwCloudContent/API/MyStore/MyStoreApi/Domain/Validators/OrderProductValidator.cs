using FluentValidation;
using MyStoreApi.Domain.Models;

namespace MyStoreApi.Domain.Validators
{
    public class OrderProductValidator : AbstractValidator<OrderProduct>
    {
        public OrderProductValidator()
        {
            RuleFor(op => op.ProductId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Field should not be empty or null");

            RuleFor(op => op.Quantity)
                .NotNull()
                .NotEmpty()
                .WithMessage("Quantity cannot be neither null, empty nor deafult");

            RuleFor(op => op.Quantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantity should be greater or equal to 0");

            RuleFor(op => op.Total)
                .GreaterThan(0.00m)
                .WithMessage("Quantity should be greater than 0");
        }
    }
}