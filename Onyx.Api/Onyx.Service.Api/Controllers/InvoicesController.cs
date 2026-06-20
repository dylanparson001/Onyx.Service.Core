using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onyx.Service.Application.Managers;
using Onyx.Shared.Contracts.Invoices;
using Onyx.Shared.Contracts.Responses;

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
        public async Task<ActionResult<CreateInvoiceResponse>> CreateInvoice(CreateInvoiceDto newInvoiceDto)
        {
            try
            {
                var response = await _manager.CreateInvoice(newInvoiceDto);

                return response.IsSuccess ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles ="Admin, Office, Manager")]
        [HttpGet("get-invoices-technician")]
        
        public async Task<ActionResult<List<InvoicesDto>>> GetInvoicesByTechnician(long technicianId, DateTime serviceDate)
        {
            try
            {
                List<InvoicesDto> result = await _manager.GetInvoicesByTechnicianAndDate(technicianId, serviceDate);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin, Office, Manager")]
        [HttpGet("get-invoices-customer")]
        public async Task<ActionResult<List<InvoicesDto>>> GetInvoicesByCustomer(long customerId, DateTime serviceDate)
        {
            try
            {
                List<InvoicesDto> result = await _manager.GetInvoicesByCustomerAndDate(customerId, serviceDate);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
