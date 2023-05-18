using System.Text.Json.Serialization;
using MyStoreApi.Domain.Validator;

namespace MyStoreApi.Domain.Models
{
    public class Order : Entity
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }

        [JsonIgnore]
        public Customer Customer { get; set; }
        [JsonIgnore]
        public List<OrderProduct> OrderProducts { get; set; } = new();
        public Coupon Coupon { get; set; }
        public decimal TotalToPay { get; set; }

        public Order() {}

        public Order(Guid id,
            Guid customerId,
            Customer customer,
            List<OrderProduct> orderProducts,
            Coupon coupon)
        {
            Id = id;
            CustomerId = customerId;
            Customer = customer;
            OrderProducts = orderProducts;
            Coupon = coupon;

            Validate(this, new OrderValidator());
        }

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