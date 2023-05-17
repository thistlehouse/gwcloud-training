using MyStoreApi.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using MyStoreApi.Application.UseCases.CustomerUseCase;
using MyStoreApi.Contracts.CustomerDto;
using MyStoreApi.UseCases;
using System.Net;

namespace MyStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private ServiceResponse<CustomerResponse> _serviceReponse;        

        public CustomerController(ICustomerService customerService,
            ServiceResponse<CustomerResponse> serviceReponse)
        {
            _customerService = customerService;
            _serviceReponse = serviceReponse;            
        }

        [HttpPost("new")]
        public ActionResult<ServiceResponse<CustomerResponse>> CreateCustomer(CustomerRequest request)
        {
            _serviceReponse = _customerService.CreateCustomer(request);

            return Ok(_serviceReponse.Data);
        }

        [HttpPost("Customer")]
        public ActionResult<CustomerResponse> GetCustomerById([FromBody] CustomerByIdRequest request)
        {
            _serviceReponse = _customerService.GetCustomerById(request);
            
            if (!_serviceReponse.Success)
            {
                return NotFound();
            }

            return Ok(_serviceReponse.Data);
        }

        [HttpGet("Customers")]
        public ActionResult<ServiceResponse<List<CustomerResponse>>> GetCustomers()
        {
            ServiceResponse<List<CustomerResponse>> response = 
                new ServiceResponse<List<CustomerResponse>>();

            response = _customerService.GetCustomers();

            if (!response.Success) return NotFound();

            return Ok(response);
        }

        [HttpPut("update")]
        public ActionResult<CustomerResponse> UpdateCustomer(CustomerRequest request)
        {
            return Ok(_customerService.UpdateCustomer(request));
        }

        // private CreatedAtActionResult CreatedAsGetCustomer(Customer Customer)
        // {
        //     return CreatedAtAction(
        //         actionName: nameof(GetCustomerById),
        //         routeValues: new {id = Customer.Id},
        //         value: Customer
        //     );
        // }
    }
}