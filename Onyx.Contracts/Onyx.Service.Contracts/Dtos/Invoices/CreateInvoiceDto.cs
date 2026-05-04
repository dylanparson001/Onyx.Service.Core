using System;
using System.Collections.Generic;
using System.Text;

namespace Onyx.Service.Contracts.Dtos.Invoices
{
    public class CreateInvoiceDto
    {
        public long JobId { get; set; }
        public long TechnicianId { get; set; }
        public long CustomerId { get; set; }
    }
}
