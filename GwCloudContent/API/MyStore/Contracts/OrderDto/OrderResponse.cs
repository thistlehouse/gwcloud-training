using MyStore.Models;

namespace MyStore.Contracts.OrderDto
{
    public record OrderResponse(
        Guid CustomerId,       
        List<OrderProduct> OrderProducts,
        decimal TotalToPay
    );
    
}