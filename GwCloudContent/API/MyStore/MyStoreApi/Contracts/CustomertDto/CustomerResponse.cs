using MyStoreApi.Domain.Models;

namespace MyStoreApi.Contracts.CustomerDto
{
    public record CustomerResponse( 
        string Name,
        List<Order> Orders
    );
}