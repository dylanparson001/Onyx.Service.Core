using Microsoft.AspNetCore.Mvc;
using Onyx.Service.Contracts.Dtos.Invoices;
using Onyx.Service.Infrastructure.DataAccess.Interfaces;

namespace Onyx.Service.Api.Controllers
{
    [Route("[controller]")]
    public class InvoicesController : BaseController
    {
        public InvoicesController(IInvoicesRepo repo, ILogger logger) : base(logger)
        {
            _repo = repo;
            _logger = logger;
        }

        private IInvoicesRepo _repo { get; }
        private ILogger _logger { get; }

        [HttpPost("create-invoice")]
        public async Task<ActionResult> CreateInvoice(CreateInvoiceDto newInvoiceDto)
        {
            try
            {
                await _repo.CreateInvoice(newInvoiceDto);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
