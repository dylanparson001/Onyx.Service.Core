using System;
using System.Collections.Generic;
using System.Text;

namespace Onyx.Service.Contracts.Dtos.Invoices
{
    public class InvoicesDto
    {
        public long Id { get; set; }
        public long JobId { get; set; }
        public long TechnicianId { get; set; }
        public long CustomerId { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? DatePaid { get; set; }
        public DateTime DateDue { get; set; }
    }
}
