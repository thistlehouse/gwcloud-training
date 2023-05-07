using MyStore.Models;
using Microsoft.AspNetCore.Mvc;
using MyStore.Services.Interfaces;
using MyStore.Contracts.ClientDto;

namespace MyStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost("new")]
        public ActionResult<ClientResponse> CreateClient(ClientRequest request)
        {
            Client client = MapClientRequest(request);
            Client clientResponse = _clientService.CreateClient(client);
            
            return CreatedAsGetClient(clientResponse);
        }

        [HttpPost("client")]
        public ActionResult<ClientResponse> GetClientById([FromBody] Guid id)
        {
            Client client = _clientService.GetClientById(id);
            
            return Ok(MapClientResponse(client));
        }

        [HttpGet("clients")]
        public List<ClientResponse> GetClients()
        {
            List<Client> clients = _clientService.GetClients();
            List<ClientResponse> clientsResponse = clients.Select(c => MapClientResponse(c)).ToList();

            return clientsResponse;
        }

        [HttpPut("update")]
        public ActionResult<ClientResponse> UpdateClient(ClientRequest request)
        {
            Client client = MapClientRequest(request);

            _clientService.UpdateClient(client);

            ClientResponse clientResponse = MapClientResponse(client);

            return Ok(clientResponse);
        }

        private static ClientResponse MapClientResponse(Client client)
        {
            return new ClientResponse(                
                client.Name,
                client.Orders
            );
        }

        private static Client MapClientRequest(ClientRequest request)
        {
            return new Client(
                request.Id,
                request.Name,
                request.Orders
            );
        }

        private CreatedAtActionResult CreatedAsGetClient(Client client)
        {
            return CreatedAtAction(
                actionName: nameof(GetClientById),
                routeValues: new {id = client.Id},
                value: MapClientResponse(client)
            );
        }
    }
}