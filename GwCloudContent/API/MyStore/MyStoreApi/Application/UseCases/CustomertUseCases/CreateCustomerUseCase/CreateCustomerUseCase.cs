using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MyStoreApi.Application.Interfaces;
using MyStoreApi.Contracts.CustomerDto;
using MyStoreApi.Domain.Models;
using MyStoreApi.UseCases;

namespace MyStoreApi.Application.UseCases.CustomertUseCases.CreateCustomerUseCase
{
    public class CreateCustomerUseCase : ICreateCustomerUseCase
    {
        private readonly ServiceResponse<CustomerResponse> _serviceResponse;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CreateCustomerUseCase(ServiceResponse<CustomerResponse> serviceResponse,
            ICustomerRepository customerRepository,
            IMapper mapper)
        {
            _serviceResponse = serviceResponse;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public ServiceResponse<CustomerResponse> CreateCustomer(CustomerRequest request)
        {
            try
            {
                Customer customer = _mapper.Map<Customer>(request);

                if (!customer.IsValid)
                {
                    return _serviceResponse.Response(
                        HttpStatusCode.BadRequest,
                        "Customer data is not valid.",
                        false);
                }

                _customerRepository.CreateCustomer(customer);

                var result = _customerRepository.GetCustomerById(customer.Id);
                var response = _mapper.Map<CustomerResponse>(result);

                return _serviceResponse.Response(response,
                    HttpStatusCode.OK,
                    "Customer was added.",
                    true);
            }
            catch (Exception ex)
            {
                return _serviceResponse.Response(ex.Message, false);
            }
        }
    }
}