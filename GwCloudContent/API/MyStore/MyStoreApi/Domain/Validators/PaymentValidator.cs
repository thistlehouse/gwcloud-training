using FluentValidation;
using MyStoreApi.Domain.Models;

namespace MyStoreApi.Domain.Validators
{
    public class PaymentValidator : AbstractValidator<Payment>
    {
        public PaymentValidator()
        {
            RuleFor(p => p.CreditCard)
                .SetValidator(new CreditCardValidator());

            RuleFor(p => p.Total)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0)
                .WithMessage("This field can't be neither null, empty nor less than or equal to 0.");
        }
    }
}