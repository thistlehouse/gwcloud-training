using MyStore.Models;

namespace MyStore.Contracts.ClientDto
{
    public record ClientResponse( 
        string Name,
        List<Order> Orders
    );
}