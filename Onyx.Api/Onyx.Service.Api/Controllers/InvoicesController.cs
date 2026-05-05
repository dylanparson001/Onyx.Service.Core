using Microsoft.AspNetCore.Mvc;
using Onyx.Service.Application.Managers;
using Onyx.Service.Contracts.Dtos.Invoices;
using Onyx.Service.Infrastructure.DataAccess.Interfaces;

namespace Onyx.Service.Api.Controllers
{
    [Route("[controller]")]
    public class InvoicesController : BaseController
    {
        #region Private Properties
        private InvoicesManager _manager { get; }

        #endregion
        #region Constructor
        public InvoicesController(InvoicesManager invoicesManager, ILogger<InvoicesController> logger) : base(logger)
        {
            _manager = invoicesManager;
            _logger = logger;
        }
        #endregion


        [HttpPost("create-invoice")]
        public async Task<ActionResult> CreateInvoice(CreateInvoiceDto newInvoiceDto)
        {
            try
            {
                await _manager.CreateInvoice(newInvoiceDto);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
