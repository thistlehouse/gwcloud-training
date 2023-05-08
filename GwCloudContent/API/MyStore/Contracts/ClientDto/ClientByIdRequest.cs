using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Contracts.CustomerDto
{
    public class CustomerByIdRequest
    {
        public Guid Id { get; set; }
    }
}