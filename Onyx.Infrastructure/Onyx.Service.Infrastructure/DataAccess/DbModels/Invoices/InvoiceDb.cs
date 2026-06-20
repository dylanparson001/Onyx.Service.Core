using Onyx.Shared.Contracts.Invoices;

namespace Onyx.Service.Infrastructure.DataAccess.DbModels.Invoices
{
    public class InvoiceDb 
    {
        public long Id { get; set; }
        public long JobId { get; set; }
        public long TechnicianId { get; set; }
        public long CustomerId { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? DatePaid { get; set; }
        public DateTime DateDue { get; set; }

        public InvoicesDto ToInvoiceDto()
        {
            return new InvoicesDto()
            {
                Id = Id,
                JobId = JobId,
                TechnicianId = TechnicianId,
                CustomerId = CustomerId,
                IsPaid = IsPaid,
                DatePaid = DatePaid,
                DateDue = DateDue
            };
        }
    }
}
