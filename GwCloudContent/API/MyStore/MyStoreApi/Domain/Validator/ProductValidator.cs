using FluentValidation;
using MyStoreApi.Domain.Models;

namespace MyStoreApi.Domain.Validator
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Id)
                .NotEqual(default(Guid))
                .WithMessage("This field should receive a valid value.");

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Product's name cannot be blank.");

            RuleFor(p => p.Name)
                .Length(10, 25)
                .WithMessage("Product's name should be between 10 and 25 characters.");

            RuleFor(p => p.Price)
                .GreaterThan(0.00m)
                .WithMessage("Product's price should be greater than 0");
        }
    }
}