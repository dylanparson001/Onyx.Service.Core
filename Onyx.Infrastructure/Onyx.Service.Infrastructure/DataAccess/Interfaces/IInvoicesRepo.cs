using Onyx.Service.Infrastructure.DataAccess.DbModels.Invoices;
using Onyx.Shared.Contracts.Invoices;

namespace Onyx.Service.Infrastructure.DataAccess.Interfaces
{
    public interface IInvoicesRepo
    {
        Task CreateInvoice(CreateInvoiceDto newInvoice);
        Task<List<InvoiceDb>> GetInvoicesByTechnician(long technicianId, DateTime serviceDate);
        Task<List<InvoiceDb>> GetInvoicesByCustomer(long customerId, DateTime serviceDate);

    }
}
