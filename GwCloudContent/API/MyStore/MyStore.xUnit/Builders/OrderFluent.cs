using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStoreApi.Domain.Models;

namespace MyStore.xUnit.Builders
{
    public class OrderFluent
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public Coupon Coupon { get; set; }
        public decimal TotalToPay { get; set; }

        public static OrderFluent New()
        {
            Guid orderId = Guid.NewGuid();

            var products = FakeDataGenerator.GenerateProducts(2);
            var customer = FakeDataGenerator.GetCustomer();

            List<OrderProduct> orderProducts = new();

            foreach (var product in products)
            {
                OrderProduct orderPoduct = new OrderProduct
                {
                    OrderId = orderId,
                    ProductId = product.Id,
                    Product = product,
                    Quantity = 1
                };

                orderProducts.Add(orderPoduct);
            }

            return new OrderFluent()
            {
                Id = orderId,
                CustomerId = customer.Id,
                Customer = new Customer
                {
                    Id = customer.Id,
                    Name = customer.Name
                },
                Coupon = new Coupon(0, "black friday", false),
                OrderProducts = orderProducts
            };
        }

        public Order Build() =>
            new Order(Id, CustomerId,Customer,
                OrderProducts, Coupon, TotalToPay);

        public OrderFluent WithId(Guid id)
        {
            Id = id;
            return this;
        }

        public OrderFluent WithCustomerId(Guid id)
        {
            CustomerId = id;
            return this;
        }

        public OrderFluent WithCustomer(Customer customer)
        {
            Customer = customer;
            return this;
        }

        public OrderFluent WithTotalPrice(decimal totalToPay)
        {
            TotalToPay = totalToPay;
            return this;
        }

        public OrderFluent WithOrderProducts(OrderProduct orderProduct)
        {
            OrderProducts.Add(orderProduct);
            return this;
        }

        public OrderFluent WithOrderProductsEmpty()
        {
            OrderProducts.Clear();
            return this;
        }

        public OrderFluent WithTotal(decimal total)
        {
            TotalToPay = total;
            return this;
        }

        public OrderFluent WithCoupon(Coupon coupon)
        {
            Coupon = coupon;
            return this;
        }
    }
}