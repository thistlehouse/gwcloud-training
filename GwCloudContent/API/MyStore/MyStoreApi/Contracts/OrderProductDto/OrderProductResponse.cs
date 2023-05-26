using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStoreApi.Requests.OrderProduct
{
    public class OrderProductResponse
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }

    }
}