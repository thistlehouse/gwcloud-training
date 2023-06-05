using AutoMapper;
using MyStoreApi.Contracts.CustomerDto;
using MyStoreApi.Contracts.ProductDto;
using MyStoreApi.Domain.Models;

namespace MyStoreApi.Contracts
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customer, CustomerRequest>();
            CreateMap<CustomerByIdRequest, Customer>();
            
            CreateMap<ProductRequest, Product>();
            CreateMap<ProductByIdRequest, Product>();
            CreateMap<Product, ProductResponse>();
        }
    }
}