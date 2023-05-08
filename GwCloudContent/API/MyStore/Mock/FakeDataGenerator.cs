using Bogus;
using MyStore.Models;
using MyStore.Persistence;

public static class FakeDataGenerator
{
    public static List<Customer> GenerateCustomers(int count)
    {
        var Customers = new Faker<Customer>()
            .RuleFor(c => c.Name, f => f.Name.FullName())
            .Generate(count);

        return Customers;
    }

    public static List<Product> GenerateProducts(int count)
    {
        var products = new Faker<Product>()
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Price, f => f.Finance.Amount(5.0m, 299.99m))
            .Generate(count);

        return products;
    }

    // public static List<Order> GenerateOrders(int count, List<Customer> Customers, List<Product> products)
    // {
    //     var orders = new Faker<Order>()
    //         .RuleFor(o => o.CustomerId, f => f.PickRandom(Customers).Id)            
    //         .RuleFor(o => o.OrderProducts, f =>
    //             new Faker<OrderProduct>()
    //                 .RuleFor(op => op.ProductId, p => p.PickRandom(products).Id)
    //                 .RuleFor(op => op.Quantity, p => p.Random.Int(1, 10))
    //                 .RuleFor(op => op.Total, 
    //                     (f, op) => op.Quantity * products
    //                         .Find(prd => prd.Id == op.ProductId).Price)
    //                 .Generate(f.Random.Int(1, 5)))
    //         .Generate(count);

    //     return orders;
    // }

    public static void GenerateFakeData()
    {
        List<Customer> Customers = FakeDataGenerator.GenerateCustomers(50);
        List<Product> products = FakeDataGenerator.GenerateProducts(100);
        // List<Order> orders = FakeDataGenerator.GenerateOrders(20, Customers, products);

        MyStoreDbContext _doggoDbContext = new();

        if (!_doggoDbContext.Customers.Any())
        {
            _doggoDbContext.Customers.AddRange(Customers);
            _doggoDbContext.SaveChanges();
        }

        if (!_doggoDbContext.Products.Any())
        {
            _doggoDbContext.Products.AddRange(products);
            _doggoDbContext.SaveChanges();
        }
    }
}