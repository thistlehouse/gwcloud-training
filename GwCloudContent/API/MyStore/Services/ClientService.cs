using MyStore.Models;
using MyStore.Repositories.Interfaces;
using MyStore.Services.Interfaces;

namespace MyStore.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Client CreateClient(Client client)
        {
            if (client is null)
                Console.WriteLine($"Treat for error: {client} is null");
            
            _clientRepository.CreateClient(client);

            return client;
        }
        
        public List<Client> GetClients()
        {
            return  _clientRepository.GetClients();            
        }

        public Client GetClientById(Guid id)
        {
            return _clientRepository.GetClientById(id);
        }

        public Client UpdateClient(Client request)
        {
            Client client = _clientRepository.GetClientById(request.Id);

            client.Name = request.Name;

            return _clientRepository.UpdateClient(client);
        }
    }
}