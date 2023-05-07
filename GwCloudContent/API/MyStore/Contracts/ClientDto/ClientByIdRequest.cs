using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Contracts.ClientDto
{
    public class ClientByIdRequest
    {
        public Guid Id { get; set; }
    }
}