using MyStoreApi.Domain.Models;
using MyStoreApi.Application.Interfaces;
using MyStoreApi.Application.UseCases.CustomerUseCase;
using MyStoreApi.Contracts.CustomerDto;
using MyStoreApi.UseCases;
using AutoMapper;
using System.Net;

namespace MyStoreApi.Application.UseCases.CustomerUseCase
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ServiceResponse<CustomerResponse> _serviceResponse;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository,
            ServiceResponse<CustomerResponse> serviceResponse,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _serviceResponse = serviceResponse;
            _mapper = mapper;
        }

        public ServiceResponse<CustomerResponse> CreateCustomer(CustomerRequest request)
        {
            try
            {
                Customer customer = _mapper.Map<Customer>(request);

                _customerRepository.CreateCustomer(customer);

                var result = _customerRepository.GetCustomerById(customer.Id);
                var response = _mapper.Map<CustomerResponse>(result);

                _serviceResponse.Response(response,
                    HttpStatusCode.OK,
                    "Customer was added.",
                    true);
            }
            catch (Exception ex)
            {
                _serviceResponse.Response(ex.Message, false);
            }

            return _serviceResponse;
        }

        public ServiceResponse<List<CustomerResponse>> GetCustomers()
        {
            ServiceResponse<List<CustomerResponse>> response =
                new ServiceResponse<List<CustomerResponse>>();

            try
            {
                List<Customer> customers = _customerRepository.GetCustomers();

                response.Data = customers
                    .Select(c => _mapper.Map<CustomerResponse>(c))
                    .ToList();
            }
            catch (Exception ex)
            {
                _serviceResponse.Response(ex.Message, false);
            }

            return response;
        }

        public ServiceResponse<CustomerResponse> GetCustomerById(CustomerByIdRequest request)
        {
            try
            {
                Customer customer = _mapper.Map<Customer>(request);
                var response = _customerRepository.GetCustomerById(customer.Id);

                if (response is null)
                {
                    string message = "Customer could not be found.";

                    _serviceResponse.Response(HttpStatusCode.NotFound, message, false);
                }
                else
                {
                    _serviceResponse.Data = _mapper.Map<CustomerResponse>(response);
                    _serviceResponse.HttpCode = HttpStatusCode.OK;
                }

            }
            catch (Exception ex)
            {
               _serviceResponse.Response(ex.Message, false);
            }

            return _serviceResponse;
        }

        public ServiceResponse<CustomerResponse> UpdateCustomer(CustomerRequest request)
        {
            try
            {
                Customer customer = _customerRepository.GetCustomerById(request.Id);

                if (!(customer is null))
                {
                    _mapper.Map(request, customer);
                    _customerRepository.Save();

                    var response = _mapper.Map<CustomerResponse>(customer);

                    _serviceResponse.Response(response,
                        HttpStatusCode.NoContent,
                        "Customer was updated.");
                }
                else
                {
                    _serviceResponse.Response(HttpStatusCode.NotFound,
                        "Customer not found.",
                        false);
                }
            }
            catch (Exception ex)
            {
                _serviceResponse.Response(ex.Message, false);
            }

            return _serviceResponse;
        }
    }
}