using MyStore.Models;

namespace MyStore.Contracts.CustomerDto
{
    public record CustomerResponse( 
        string Name,
        List<Order> Orders
    );
}