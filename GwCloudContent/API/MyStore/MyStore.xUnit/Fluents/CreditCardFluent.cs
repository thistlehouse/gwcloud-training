using MyStoreApi.Domain.Models;

namespace MyStore.xUnit.Fluents
{
    public class CreditCardFluent
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string Cvv { get; set; }
        public int ExpireYear { get; set; }
        public int ExpireMonth { get; set; }

        public static CreditCardFluent New()
        {
            return new CreditCardFluent()
            {
                Number = "4317287093903444",
                Name = "Jhon Doe",
                Cvv = "",
                ExpireYear = DateTime.Now.Year + 1,
                ExpireMonth = DateTime.Now.Month + 1
            };
        }

        public CreditCard Build() =>
            new CreditCard(Number, Cvv, Name, ExpireYear, ExpireMonth);

        public CreditCardFluent WithNumber(string number)
        {
            Number = number;
            return this;
        }

        public CreditCardFluent WithCvv(string cvv)
        {
            Cvv = cvv;
            return this;
        }

        public CreditCardFluent WithName(string name)
        {
            Name = name;
            return this;
        }

        public CreditCardFluent WithExpireMonth(int month)
        {
            ExpireMonth = month;
            return this;
        }

        public CreditCardFluent WithExpireYear(int year)
        {
            ExpireYear = year;
            return this;
        }
    }
}