using MyStore.Models;
using MyStore.Contracts.ClientDto;

namespace MyStore.Services.Interfaces
{
    public interface IClientService
    {
        Client CreateClient(Client client);        
        List<Client> GetClients();
        Client GetClientById(Guid Id);
        Client UpdateClient(Client client);
        
    }
}