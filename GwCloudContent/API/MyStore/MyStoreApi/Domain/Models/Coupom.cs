namespace MyStoreApi.Domain.Models
{
    public class Coupon : Entity
    {
        public decimal Value { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; } = false;
        public Order Order { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Coupon() {}

        public Coupon(string code)
        {
            Code = code;
        }

        public Coupon(decimal value, string code, bool active)
        {
            Value = value;
            Code = code;
            Active = active;
        }
    }
}