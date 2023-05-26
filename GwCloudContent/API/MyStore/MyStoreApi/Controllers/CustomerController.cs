using Microsoft.AspNetCore.Mvc;
using MyStoreApi.Application.UseCases.CustomertUseCases.CreateCustomerUseCase;
using MyStoreApi.Application.UseCases.CustomertUseCases.GetCustomerByIdUseCase;
using MyStoreApi.Application.UseCases.CustomertUseCases.UpdateCustomerUseCase;
using MyStoreApi.Contracts.CustomerDto;
using MyStoreApi.UseCases;
using System.Net;

namespace MyStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICreateCustomerUseCase _createCustomer;
        private readonly IGetCustomerByIdUseCase _getCustomerById;
        private readonly IUpdateCustomerUseCase _updateCustomer;
        private ServiceResponse<CustomerResponse> _serviceReponse;

        public CustomerController(ServiceResponse<CustomerResponse> serviceReponse,
            IUpdateCustomerUseCase updateCustomer,
            IGetCustomerByIdUseCase getCustomerById,
            ICreateCustomerUseCase createCustomer)
        {
            _serviceReponse = serviceReponse;
            _updateCustomer = updateCustomer;
            _getCustomerById = getCustomerById;
            _createCustomer = createCustomer;
        }

        [HttpPost("new")]
        public ActionResult<ServiceResponse<CustomerResponse>> CreateCustomer(CustomerRequest request)
        {
            _serviceReponse = _createCustomer.CreateCustomer(request);

            return Ok(_serviceReponse.Data);
        }

        [HttpPost("Customer")]
        public ActionResult<ServiceResponse<CustomerResponse>> GetCustomerById([FromBody] CustomerByIdRequest request)
        {
            _serviceReponse = _getCustomerById.GetCustomerById(request);

            if (!_serviceReponse.Success)
            {
                return NotFound();
            }

            return Ok(_serviceReponse.Data);
        }

        [HttpPut("update")]
        public ActionResult<CustomerResponse> UpdateCustomer(CustomerRequest request)
        {
            return Ok(_updateCustomer.UpdateCustomer(request));
        }
    }
}