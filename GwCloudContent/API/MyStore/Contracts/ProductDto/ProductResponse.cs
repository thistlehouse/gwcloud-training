using MyStore.Models;

namespace MyStore.Contracts.ProductDto
{
    public record ProductResponse(        
        string Name,
        decimal Price,
        List<OrderProduct> OrderProducts
    );
    
}