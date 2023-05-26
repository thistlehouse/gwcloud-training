using FluentAssertions;
using MyStoreApi.Application.UseCases.CustomertUseCases.CreateCustomerUseCase;
using MyStoreApi.Application.UseCases.CustomertUseCases.GetCustomerByIdUseCase;
using MyStoreApi.Application.UseCases.CustomertUseCases.UpdateCustomerUseCase;
using MyStoreApi.Contracts.CustomerDto;

using Xunit;
using Xunit.Frameworks.Autofac;

namespace MyStore.xUnit.Application.Services.UseCases.CustomerUserCase
{
    [UseAutofacTestFramework]
    public class CustomerServiceTest
    {
        private readonly ICreateCustomerUseCase _createCustomer;
        private readonly IGetCustomerByIdUseCase _getCustomerById;
        private readonly IUpdateCustomerUseCase _updateCustomer;

        public CustomerServiceTest(ICreateCustomerUseCase createCustomer,
            IGetCustomerByIdUseCase getCustomerById,
            IUpdateCustomerUseCase updateCustomer)
        {
            _createCustomer = createCustomer;
            _getCustomerById = getCustomerById;
            _updateCustomer = updateCustomer;
        }

        [Fact]
        public void GetCustomerById_Should_ReturnCustomer_WhenCustomerExists()
        {
            CustomerRequest request = new CustomerRequest
            {
                Id = Guid.NewGuid(),
                Name = "New Customer Creation"
            };

            _createCustomer.CreateCustomer(request);

            CustomerByIdRequest customerId = new CustomerByIdRequest();
            customerId.Id = request.Id;

            var response =_getCustomerById.GetCustomerById(customerId);

            response.Success.Should().BeTrue();
        }

        [Fact]
        public void GetCustomerById_Should_ReturnNotFound()
        {
            CustomerByIdRequest CustomerId = new CustomerByIdRequest
            {
                Id = Guid.NewGuid()
            };

            var response = _getCustomerById.GetCustomerById(CustomerId);
            var errorResponse = new HttpResponseMessage(response.HttpCode);

            errorResponse.Should().HaveClientError("404, Customer Not Found.");
        }

        [Fact]
        public void CreateCustomer_Should_ReturnOK()
        {
            CustomerRequest request = new CustomerRequest
            {
                Name = "Customer Create Testing"
            };

            var response = _createCustomer.CreateCustomer(request);
            var successResponse = new HttpResponseMessage(response.HttpCode);

            successResponse.Should().BeSuccessful("Customer created successfully.");
        }

        [Fact]
        public void UpdateCustomer_Should_ReturnOK()
        {
            Guid id = Guid.NewGuid();

            CustomerRequest request = new CustomerRequest
            {
                Id = id,
                Name = "Customer Update Testing"
            };

            _createCustomer.CreateCustomer(request);

            CustomerRequest update = new CustomerRequest
            {
                Id = id,
                Name = "Customer Change Updated"
            };

            var response = _updateCustomer.UpdateCustomer(update);
            var successResponse = new HttpResponseMessage(response.HttpCode);

            successResponse.Should().BeSuccessful("Customer updated successfully.");
        }

        [Fact]
        public void UpdateCustomer_Should_ReturnNotFound()
        {
            CustomerRequest CustomerId = new CustomerRequest
            {
                Id = Guid.NewGuid()
            };

            var response = _updateCustomer.UpdateCustomer(CustomerId);
            var successResponse = new HttpResponseMessage(response.HttpCode);

            successResponse.Should().HaveClientError("404, Customer Not Found.");
        }
    }
}