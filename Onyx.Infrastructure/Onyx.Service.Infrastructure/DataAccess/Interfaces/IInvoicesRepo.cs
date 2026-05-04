using Onyx.Service.Contracts.Dtos.Invoices;
using Onyx.Service.Infrastructure.DataAccess.DbModels.Jobs;

namespace Onyx.Service.Infrastructure.DataAccess.Interfaces
{
    public interface IInvoicesRepo
    {
        Task CreateInvoice(CreateInvoiceDto newInvoice);
    }
}
