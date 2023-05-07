using MyStore.Models;

namespace MyStore.Contracts.OrderDto
{
    public record OrderResponse(
        Guid ClientId,       
        List<OrderProduct> OrderProducts,
        decimal TotalToPay
    );
    
}