using MyStoreApi.Domain.Models;

namespace MyStoreApi.Contracts.ProductDto
{
    public record ProductResponse(        
        string Name,        
        decimal Price
    );    
}