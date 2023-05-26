using System.Net;
using AutoMapper;
using MyStoreApi.Application.Interfaces;
using MyStoreApi.Contracts.CustomerDto;
using MyStoreApi.Domain.Models;
using MyStoreApi.UseCases;

namespace MyStoreApi.Application.UseCases.CustomertUseCases.GetCustomerByIdUseCase
{
    public class GetCustomerByIdUseCase : IGetCustomerByIdUseCase
    {
        private ServiceResponse<CustomerResponse> _serviceResponse;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerByIdUseCase(ServiceResponse<CustomerResponse> serviceResponse,
            ICustomerRepository customerRepository,
            IMapper mapper)
        {
            _serviceResponse = serviceResponse;
            _customerRepository = customerRepository;
            _mapper = mapper;
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
    }
}