using Xunit;
using FluentAssertions;
using Xunit.Frameworks.Autofac;
using MyStore.xUnit.Fluents;
using MyStoreApi.Contracts.CustomerDto;
using System.Net;
using MyStoreApi.UseCases;
using MyStoreApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MyStore.xUnit.Api.Controllers
{
    [UseAutofacTestFramework]
    public class CustomerControllerTest
    {
        private CustomerController _customerController;

        public CustomerControllerTest(CustomerController customerController)
        {
            _customerController = customerController;
        }

        [Fact]
        public void CreateCustomer_Should_ReturnCustomerAndOk()
        {
            var customerRequest = CustomerRequestFluent.New()
                .Build();

            var result = _customerController.CreateCustomer(customerRequest);

            result.Should()
                .BeOfType<ActionResult<ServiceResponse<CustomerResponse>>>()
                .Which.Result
                    .Should()
                    .BeOfType<OkObjectResult>()
                    .Which.StatusCode
                        .Should()
                        .Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public void CreateCustomer_Should_ReturnNotFound()
        {
            var customerRequest = CustomerRequestFluent.New()
                .WithName("ABCDEFGHIJKLMNOPQRSTUVWYXZABCDEFGHIJKLMNOPQRSTUVWYXZ")
                .Build();

            _customerController.CreateCustomer(customerRequest);

            CustomerByIdRequest request = new CustomerByIdRequest
            {
                Id = customerRequest.Id
            };

            var result = _customerController.GetCustomerById(request);

            result.Should()
                .BeOfType<ActionResult<ServiceResponse<CustomerResponse>>>()
                .Which.Result
                    .Should().BeOfType<NotFoundResult>()
                    .Which.StatusCode
                        .Should()
                        .Be((int)HttpStatusCode.NotFound);
        }

        // [Fact]
        // public void GetCustomerById_Should_ReturnCustomer()
        // {
        //     var customer = CustomerRequestFluent.New().
        //         Build();

        //     _customerService.CreateCustomer(customer);

        //     var request = new CustomerByIdRequest
        //     {
        //         Id = customer.Id
        //     };

        //     var response = _customerService.GetCustomerById(request);

        //     response
        //         .Should()
        //         .BeOfType(typeof(ServiceResponse<CustomerResponse>));
        // }

        // [Fact]
        // public void GetCustomerById_Should_ReturnNotFound()
        // {
        //     var customer = CustomerRequestFluent.New()
        //         .WithName("ABCDEFGHIJKLMNOPQRSTUVWYXZABCDEFGHIJKLMNOPQRSTUVWYXZ")
        //         .Build();

        //     _customerService.CreateCustomer(customer);

        //     var request = new CustomerByIdRequest
        //     {
        //         Id = customer.Id
        //     };

        //     var response = _customerService.GetCustomerById(request);
        //     var errorMessage = new HttpResponseMessage(response.HttpCode);

        //     errorMessage
        //         .Should()
        //         .HaveStatusCode(HttpStatusCode.NotFound);
        // }
    }
}