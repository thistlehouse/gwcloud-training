using System.Text.Json.Serialization;
using MyStoreApi.Domain.Validator;

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
        public Coupon Coupon { get; set; }
        public decimal TotalToPay { get; set; }

        public Order() {}

        public Order(Guid id,
            Guid customerId,
            Customer customer,
            List<OrderProduct> orderProducts,
            Coupon coupon,
            decimal totalToPay)
        {
            Id = id;
            CustomerId = customerId;
            Customer = customer;
            OrderProducts = orderProducts;
            Coupon = coupon;
            TotalToPay = totalToPay;

            Validate(this, new OrderValidator());
        }

        public Order(
            Guid customerId,
            List<OrderProduct> orderProducts,
            decimal totalToPay)
        {
            CustomerId = customerId;
            OrderProducts = orderProducts;
            TotalToPay = totalToPay;

            Validate(this, new OrderValidator());
        }
    }
}