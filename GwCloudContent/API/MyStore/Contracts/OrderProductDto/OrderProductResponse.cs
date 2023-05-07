using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Requests.OrderProduct
{
    public class OrderProductResponse
    {
        public Guid ProductId { get; set; }        
        public Guid OrderId { get; set; }        
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        
    }
}