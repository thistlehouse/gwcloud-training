using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStore.Models;

namespace MyStore.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Client CreateClient(Client client);        
        List<Client> GetClients();
        Client GetClientById(Guid id);
        Client UpdateClient(Client client);
        void Save();
    }
}