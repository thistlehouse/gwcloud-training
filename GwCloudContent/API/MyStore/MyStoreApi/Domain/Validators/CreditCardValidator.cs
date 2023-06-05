using FluentValidation;
using FluentValidation.Validators;
using MyStoreApi.Domain.Models;

namespace MyStoreApi.Domain.Validators
{
    public class CreditCardValidator : AbstractValidator<CreditCard>
    {
        public CreditCardValidator()
        {
            RuleFor(c => c.Number)
                .NotEmpty()
                .NotNull()
                .WithMessage("Number can't be null or empty.");

            RuleFor(c => c.Number)
                .Must(n => !isCreditCardNumberValid(n))
                .WithMessage("Credit card number must be valid.");

            RuleFor(c => c.Cvv)
                .Custom((cvv, context) =>
                {
                    if (!cvv.All(char.IsDigit) ||
                        cvv.Length < 3 || cvv.Length > 4)
                    {
                        context.AddFailure("Cvv", "Invalide Cvv");
                        context.RootContextData["IsValid"] = false;
                    }
                });

            RuleFor(c => c.ExpireYear)
                .NotEmpty()
                .WithMessage("Credit card expiration year is required.")
                .Must(y => y >= DateTime.Now.Year)
                .WithMessage("Credit card expiration year is invalid.");

            RuleFor(c => c.ExpireMonth)
                .NotEmpty()
                .WithMessage("Credit card expiration month is required.")
                .Must(m => m >= DateTime.Now.Month)
                .WithMessage("Credit card expiration month is invalid.");
        }

        private bool isCreditCardNumberValid(string number)
        {
            number = number.Replace("-", "").Replace(" ", "");

            int checkSum = 0;
            string lastDigit = number.Substring(number.Length - 1, 1);
            string numberWithouLastDigit = number.Substring(0, number.Length - 1);
            char[] reversedNumber = numberWithouLastDigit.ToCharArray();

            Array.Reverse(reversedNumber);

            for (int i = 0; i < reversedNumber.Length; i++)
            {
                if (!char.IsDigit(reversedNumber[i])) return false;

                bool isEven = i % 2 == 0 ? true : false;
                int value = (reversedNumber[i] - '0');

                if (isEven)
                {
                    value *= 2;

                    if (value > 9) value -= 9;
                }

                checkSum += value;
            }

            return (checkSum % 10) == Convert.ToInt32(lastDigit);
        }
    }
}