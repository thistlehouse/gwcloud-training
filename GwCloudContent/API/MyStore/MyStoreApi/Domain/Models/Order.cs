using System.ComponentModel.DataAnnotations.Schema;
using MyStoreApi.Domain.Validators;
using Newtonsoft.Json;

namespace MyStoreApi.Domain.Models
{
    public class Order : Entity
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        [JsonIgnore]
        public Customer Customer { get; private set; }
        [JsonIgnore]
        public List<OrderProduct> OrderProducts { get; private set; } = new();
        [NotMapped] public Coupon Coupon { get; private set; }
        public decimal TotalToPay { get; private set; }
        public string OrderStatus { get; private set; } = "In Progress";
        public Order() {}

        public Order(Guid id)
        {
            Id = id;

            Validate(this, new OrderValidator());
        }

        public Order(Guid id,
            Guid customerId,
            List<OrderProduct> orderProducts,
            Coupon coupon,
            string orderStatus)
        {
            Id = id;
            CustomerId = customerId;
            Coupon = coupon;
            OrderProducts = orderProducts;
            OrderStatus = orderStatus;
            TotalToPay = Math.Round(orderTotal() * discount(), 2);

            Validate(this, new OrderValidator());
        }

        public void Status(string status)
        {
            OrderStatus = status;
        }

        private decimal orderTotal()
        {
            return OrderProducts.Sum(op => op.Total);
        }

        private decimal discount()
        {
            if (!ValidCoupon(Coupon))
            {
                return 0.00m;
            }

            return ((100 - Coupon.Value) / 100);
        }

        private bool ValidCoupon(Coupon coupon)
        {
            List<string> validCode = new List<string>
            {
                { "black friday "}
            };

            bool valid = validCode[0].Contains(coupon.Code.ToLower());

            return valid;
        }
    }
}