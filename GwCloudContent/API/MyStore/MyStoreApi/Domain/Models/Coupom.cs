namespace MyStoreApi.Domain.Models
{
    public class Coupon : Entity
    {
        public decimal Value { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
        public Order Order { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Coupon() {}

        public Coupon(string code)
        {
            Code = code;
        }

        public Coupon(string code, decimal value, bool active = false)
        {
            Code = code;
            Value = value;
            Active = isBlackFridayToday();
        }

        private bool isBlackFridayToday()
        {
            DateTime today = DateTime.Today;
            var beginBlackFriday = StartDate =
                new DateTime(DateTime.Now.Year, 11, 23);
            var endBlackFriday = ExpirationDate =
                new DateTime(DateTime.Now.Year, 11, 29).AddDays(1);

            if (today >= beginBlackFriday && today <= endBlackFriday)
                return true;

            return false;
        }
    }
}