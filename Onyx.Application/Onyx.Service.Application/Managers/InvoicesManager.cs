using Microsoft.Extensions.Logging;
using Onyx.Service.Infrastructure.DataAccess.DbModels.Invoices;
using Onyx.Service.Infrastructure.DataAccess.Interfaces;
using Onyx.Shared.Contracts.Invoices;
using Onyx.Shared.Contracts.Responses;

namespace Onyx.Service.Application.Managers
{
    public class InvoicesManager(IInvoicesRepo invoiceRepo, ILogger<InvoicesManager> logger)
    {
        #region Private Properties
        private IInvoicesRepo _invoiceRepo { get; } = invoiceRepo;
        private ILogger<InvoicesManager> _logger { get; } = logger;

        #endregion
        #region Constructor
        #endregion

        #region Public Methods
        public async Task<CreateInvoiceResponse> CreateInvoice(CreateInvoiceDto newInvoiceDto)
        {
            try
            {
                var response = new CreateInvoiceResponse();
                if (newInvoiceDto == null)
                    return new CreateInvoiceResponse("New Invoice was null");

                await _invoiceRepo.CreateInvoice(newInvoiceDto);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new CreateInvoiceResponse(ex.Message);
            }
        }

        public async Task<List<InvoicesDto>> GetInvoicesByTechnicianAndDate(long id, DateTime serviceDate)
        {
            try
            {
                List<InvoiceDb> result = await _invoiceRepo.GetInvoicesByTechnician(id, serviceDate);

                return result.Select(x => x.ToInvoiceDto()).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<InvoicesDto>> GetInvoicesByCustomerAndDate(long id, DateTime serviceDate)
        {
            try
            {
                List<InvoiceDb> result = await _invoiceRepo.GetInvoicesByCustomer(id, serviceDate);

                return result.Select(x => x.ToInvoiceDto()).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        #endregion




        #region Private Methods

        #endregion
    }
}
