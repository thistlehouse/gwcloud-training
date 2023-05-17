using FluentValidation;
using MyStoreApi.Domain.Models;

namespace MyStoreApi.Domain.Validator
{
    public class OrderProductValidator : AbstractValidator<OrderProduct>
    {
        public OrderProductValidator()
        {
            RuleFor(op => op.OrderId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Field should not be empty or null");

            RuleFor(op => op.ProductId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Field should not be empty or null");

            // RuleFor(op => op.Order)
            //     .SetValidator(new OrderValidator());

            // RuleFor(op => op.Product)
            //     .SetValidator(new ProductValidator());
        }
    }
}