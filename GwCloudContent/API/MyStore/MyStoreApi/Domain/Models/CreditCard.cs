using MyStoreApi.Domain.Validators;

namespace MyStoreApi.Domain.Models
{
    public class CreditCard : Entity
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string Cvv { get; set; }
        public int ExpireYear { get; set; }
        public int ExpireMonth { get; set; }

        public CreditCard() {}

        public CreditCard(string number)
        {
            Number = number;

            Validate(this, new CreditCardValidator());
        }

        public CreditCard(string number,
            string cvv,
            string name,
            int expireYear,
            int expireMonth)
        {
            Number = number;
            Cvv = cvv;
            Name = name;
            ExpireYear = expireYear;
            ExpireMonth = expireMonth;

            Validate(this, new CreditCardValidator());
        }

        public CreditCard(string number,
            string cvv,
            int expireYear,
            int expireMonth)
        {
            Number = number;
            Cvv = cvv;
            ExpireYear = expireYear;
            ExpireMonth = expireMonth;

            Validate(this, new CreditCardValidator());
        }
    }
}