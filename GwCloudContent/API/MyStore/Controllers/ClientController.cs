using MyStore.Models;
using Microsoft.AspNetCore.Mvc;
using MyStore.Services.Interfaces;
using MyStore.Contracts.CustomerDto;

namespace MyStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _CustomerService;
        
        public CustomerController(ICustomerService CustomerService)
        {
            _CustomerService = CustomerService;
        }

        [HttpPost("new")]
        public ActionResult<CustomerResponse> CreateCustomer(CustomerRequest request)
        {
            Customer Customer = MapCustomerRequest(request);
            Customer CustomerResponse = _CustomerService.CreateCustomer(Customer);
            
            return CreatedAsGetCustomer(CustomerResponse);
        }

        [HttpPost("Customer")]
        public ActionResult<CustomerResponse> GetCustomerById([FromBody] Guid id)
        {
            Customer Customer = _CustomerService.GetCustomerById(id);
            
            return Ok(MapCustomerResponse(Customer));
        }

        [HttpGet("Customers")]
        public List<CustomerResponse> GetCustomers()
        {
            List<Customer> Customers = _CustomerService.GetCustomers();
            List<CustomerResponse> CustomersResponse = Customers.Select(c => MapCustomerResponse(c)).ToList();

            return CustomersResponse;
        }

        [HttpPut("update")]
        public ActionResult<CustomerResponse> UpdateCustomer(CustomerRequest request)
        {
            Customer Customer = MapCustomerRequest(request);

            _CustomerService.UpdateCustomer(Customer);

            CustomerResponse CustomerResponse = MapCustomerResponse(Customer);

            return Ok(CustomerResponse);
        }

        private static CustomerResponse MapCustomerResponse(Customer Customer)
        {
            return new CustomerResponse(                
                Customer.Name,
                Customer.Orders
            );
        }

        private static Customer MapCustomerRequest(CustomerRequest request)
        {
            return new Customer(
                request.Id,
                request.Name,
                request.Orders
            );
        }

        private CreatedAtActionResult CreatedAsGetCustomer(Customer Customer)
        {
            return CreatedAtAction(
                actionName: nameof(GetCustomerById),
                routeValues: new {id = Customer.Id},
                value: MapCustomerResponse(Customer)
            );
        }
    }
}