using FluentValidation;
using MyStoreApi.Domain.Models;

namespace MyStoreApi.Domain.Validator
{
    public class CupoValidator : AbstractValidator<Coupon>
    {
        public CupoValidator()
        {
            RuleFor(c => c.Value)
                .GreaterThan(-1)
                .WithMessage("Discount should not be negative");

            RuleFor(c => c.Value)
                .LessThanOrEqualTo(50)
                .WithMessage("Should not exceed 50%");

            RuleFor(c => c.Code)
                .NotNull()
                .NotEmpty()
                .WithMessage("Discount's code should neither be empty or null");

            RuleFor(c => c.Code)
                .Equal("black friday")
                .WithMessage("Valid coupon.");
        }
    }
}