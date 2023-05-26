using System.ComponentModel.DataAnnotations.Schema;
using MyStoreApi.Domain.Validators;
using Newtonsoft.Json;

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
        [NotMapped] public Coupon Coupon { get; set; }
        public decimal TotalToPay { get; set; }
        public string OrderStatus { get; set; }
        public Order() {}

        public Order(Guid id)
        {
            Id = id;

            Validate(this, new OrderValidator());
        }

        public Order(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;

            Validate(this, new OrderValidator());
        }

        public Order(
            Guid customerId,
            Customer customer,
            List<OrderProduct> orderProducts,
            Coupon coupon,
            decimal totalToPay)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            Customer = customer;
            OrderProducts = orderProducts;
            Coupon = coupon;
            TotalToPay = totalToPay;

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
    }
}