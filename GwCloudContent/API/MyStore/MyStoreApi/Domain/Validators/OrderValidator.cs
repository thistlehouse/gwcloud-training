using FluentValidation;
using MyStoreApi.Domain.Models;

namespace MyStoreApi.Domain.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(o => o.Id)
                .NotEqual(default(Guid))
                .WithMessage("This field should receive a valid value.");

            RuleFor(o => o.Customer)
                .SetValidator(new CustomerValidator());

            RuleFor(o => o.TotalToPay)
                .GreaterThan(0.00m)
                .WithMessage("Total can't be 0.00");

            RuleForEach(o => o.OrderProducts)
                .NotNull()
                .SetValidator(new OrderProductValidator());
        }
    }
}