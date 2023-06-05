using FluentValidation;
using MyStoreApi.Domain.Models;

namespace MyStoreApi.Domain.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.Id)
                .NotEqual(default(Guid))
                .WithMessage("This field should receive a valid value.");

            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("This field can't be empty.");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Customer's name can't be empty.");

            RuleFor(c => c.Name)
                .NotNull()
                .WithMessage("Customer's name can't be blank.");

            RuleFor(c => c.Name)
                .Length(2, 30)
                .WithMessage("Customer's name can't be empty and should be between 10 and 30 characters.");
        }
    }
}