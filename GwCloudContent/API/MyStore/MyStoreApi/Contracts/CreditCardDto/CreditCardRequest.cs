namespace MyStoreApi.Contracts.CreditCardDto
{
    public class CreditCardRequest
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string Cvv { get; set; }
        public int ExpireYear { get; set; }
        public int ExpireMonth { get; set; }
    }
}