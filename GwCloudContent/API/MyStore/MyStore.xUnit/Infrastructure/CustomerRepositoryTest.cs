using Xunit;
using FluentAssertions;
using Xunit.Frameworks.Autofac;
using MyStore.xUnit.Fluents;
using MyStoreApi.Application.Interfaces;
using MyStoreApi.Infrastructure.Persistence;
using MyStoreApi.Domain.Models;
using System.Reflection;

namespace MyStore.xUnit.Infrastructure
{
    [UseAutofacTestFramework]
    public class CustomerRepositoryTest
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly MyStoreApiDbContext _myStoreDbContext;
        public CustomerRepositoryTest(ICustomerRepository customerRepository,
            MyStoreApiDbContext myStoreDbContext)
        {
            _customerRepository = customerRepository;
            _myStoreDbContext = myStoreDbContext;
        }

        [Fact]
        public void CreateCustomer_Should_AddToDatabase()
        {
            var customer = CustomerFluent.New()
                .Build();

            if (customer.IsValid)
                _customerRepository.CreateCustomer(customer);

            _myStoreDbContext.Customers.Should().Contain(customer);
        }

        [Fact]
        public void CreateCustomer_ShouldNot_AddToDatabase()
        {
            var customer = CustomerFluent.New()
                .WithName("ABCDEFGHIJKLMNOPQRSTUVWYXZABCDEFGHIJKLMNOPQRSTUVWYXZ")
                .Build();

            if (customer.IsValid)
                _customerRepository.CreateCustomer(customer);

            _myStoreDbContext.Customers.Should().NotContain(customer);
        }

        [Fact]
        public void GetCustomerById_Should_ReturnCustomer()
        {
            var customer = CustomerFluent.New()
                .Build();

            _customerRepository.CreateCustomer(customer);

            var result = _customerRepository.GetCustomerById(customer.Id);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void GetCustomerById_Should_BeNull()
        {
            var customer = CustomerFluent.New()
                .Build();

            var result = _customerRepository.GetCustomerById(customer.Id);

            result.Should().BeNull();
        }

        [Fact]
        public void UpdateCustomer_Should_ChangeCustomer()
        {
            var customer = CustomerFluent.New()
                .Build();

            var customerToCompar = CustomerFluent.New()
                .WithId(customer.Id)
                .Build();

            _customerRepository.CreateCustomer(customer);

            customer = _customerRepository.GetCustomerById(customer.Id);

            PropertyInfo name = typeof(Customer).GetProperty(nameof(customer.Name));

            name.SetValue(customer, "New Customer name");

            _customerRepository.UpdateCustomer(customer);

            var result = _customerRepository.GetCustomerById(customer.Id);

            result.Should().NotBeEquivalentTo(customerToCompar);
        }

        [Fact]
        public void UpdateCustomer_ShouldNot_ChangeCustomer()
        {
            var customer = CustomerFluent.New()
                .Build();


        }
    }
}