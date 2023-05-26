using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStoreApi.Contracts.ProductDto
{
    public class ProductByIdRequest
    {
        public Guid Id { get; set; }
    }
}