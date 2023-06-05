using Bogus;
using MyStoreApi.Domain.Models;
using MyStoreApi.Infrastructure.Persistence;

public static class FakeDataGenerator
{
    public static Customer GetCustomer()
    {
        var customer = new Faker<Customer>()
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.Name, f => f.Name.FullName())
            .Generate();

        return customer;
    }

    public static List<Customer> GenerateCustomers(int count)
    {
        var customers = new Faker<Customer>()
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.Name, f => f.Name.FullName())
            .Generate(count);

        return customers;
    }

    public static Product GenerateProduct()
    {
        var product = new Faker<Product>()
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Price, f => f.Finance.Amount(1.00m, 299.99m))
            .Generate();

        return product;
    }

    public static List<Product> GenerateProducts(int count)
    {
        var products = new Faker<Product>()
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Price, f => f.Finance.Amount(5.0m, 299.99m))
            .Generate(count);

        return products;
    }

    public static List<Order> GenerateOrders(List<Customer> customers,
        List<Product> products,
        int count)
    {
        var orders = new Faker<Order>()
            .RuleFor(o => o.Id, f => Guid.NewGuid())
            .RuleFor(o => o.CustomerId, f => f.PickRandom(customers).Id)
            .RuleFor(o => o.OrderProducts, f =>
                new Faker<OrderProduct>()
                    .RuleFor(op => op.OrderId, (f, op) => op.Order.Id)
                    .RuleFor(op => op.ProductId, p => p.PickRandom(products).Id)
                    .RuleFor(op => op.Quantity, p => p.Random.Int(1, 10))
                    .RuleFor(op => op.Total, (f, op) => op.Quantity * products
                        .Find(prd => prd.Id == op.ProductId).Price)
                    .Generate(f.Random.Int(1, 5)))
            .Generate(count);

        return orders;
    }

    public static List<Guid> ValidProductIds(List<Product> products)
    {
        return products.Select(p => p.Id).ToList();
    }
}