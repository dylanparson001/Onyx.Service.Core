using Microsoft.Extensions.Logging;
using Onyx.Service.Contracts.Dtos.Invoices;
using Onyx.Service.Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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
        public async Task CreateInvoice(CreateInvoiceDto newInvoiceDto)
        {
            try
            {
                if (newInvoiceDto == null)
                    return;

                await _invoiceRepo.CreateInvoice(newInvoiceDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
        #endregion


        #region Private Methods

        #endregion
    }
}
