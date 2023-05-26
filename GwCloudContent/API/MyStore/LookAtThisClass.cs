public class OrderResponseDto
{
    public Guid CustomerId;
    public List<OrderProductDto> OrderProducts;
    public decimal Total;
}

public class OrderProductDto
{
    public Guid OrderId;
    public Guid ProductId;
    public int Quantity;
    public decimal Total;
    public ProductDto Product;
}

public class ProductDto
{
    public string ProductName;
    public decimal ProductPrice;
}

public class OrderProduct
{
    public Guid OrderId;
    public Guid ProductId;
    public int Quantity;
    public decimal Total;
    public Order Order;
    public Product Product;

}

public class Product
{
    public Guid Id;
    public string Name;
    public decimal Price;
}

public class Order
{
    public Guid Id;
    public Guid CustomerId;
    public decimal OrderTotalPrice;
    public List<OrderProduct> OrderProducts;
}



public class GetOrderProductByOrderId
{
    public OrderResponseDto
    GetByOrderId(Guid orderId, Guid customerId)
    {
        var order = new Order();
        var orderProductsDtoList = order.OrderProducts
            .Select(orderProduct =>
                new OrderProductDto()
                {
                    OrderId = orderProduct.OrderId,
                    ProductId = orderProduct.ProductId,
                    Quantity = orderProduct.Quantity,
                    Total = orderProduct.Total,
                    Product = new ProductDto()
                    {
                        ProductName = orderProduct.Product.Name,
                        ProductPrice = orderProduct.Product.Price
                    }
                }).ToList();

        var response = new OrderResponseDto()
        {
            CustomerId = customerId,
            OrderProducts = orderProductsDtoList,
            Total = order.OrderTotalPrice
        };

        return response;
    }
}