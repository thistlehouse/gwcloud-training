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

namespace MyStoreApi.Application.UseCases.CustomertUseCases.UpdateCustomerUseCase
{
    public class UpdateCustomerUseCase : IUpdateCustomerUseCase
    {
        private readonly ServiceResponse<CustomerResponse> _serviceResponse;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public UpdateCustomerUseCase(IMapper mapper,
            ICustomerRepository customerRepository,
            ServiceResponse<CustomerResponse> serviceResponse)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _serviceResponse = serviceResponse;
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

                    return _serviceResponse.Response(response,
                        HttpStatusCode.NoContent,
                        "Customer was updated.");
                }
                else
                {
                    return _serviceResponse.Response(HttpStatusCode.NotFound,
                        "Customer not found.",
                        false);
                }
            }
            catch (Exception ex)
            {
                return _serviceResponse.Response(ex.Message, false);
            }
        }
    }
}