using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Contracts.OrderProductDto
{
    public class OrderProductByIdRequest
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
    }
}