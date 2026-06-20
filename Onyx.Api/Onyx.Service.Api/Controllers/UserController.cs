using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onyx.Service.Application.Managers;
using Onyx.Shared.Contracts.Users;

namespace Onyx.Service.Api.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Office, Manager, Admin")]
    public class UserController : BaseController
    {
        private readonly UserManager _userManager;
        #region Constructor
        public UserController(UserManager userManager, ILogger<UserController> logger) : base(logger)
        {
            _userManager = userManager;
        }
        #endregion

        #region Get Methods

        [HttpGet("get-active-technicians")]
        public async Task<ActionResult<List<EmployeeDto>>> GetAciveTechnicians(DateTime date)
        {
            List<EmployeeDto> activeTechs = [];

            try
            {
                activeTechs = await _userManager.GetActiveTechniciansByDate(date);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return Ok(activeTechs);
        }

        #endregion
    }
}
