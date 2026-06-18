using Microsoft.AspNetCore.Mvc;
using Onyx.Service.Application.Managers;
using Onyx.Service.Contracts.Dtos.Users;

namespace Onyx.Service.Api.Controllers
{
    [Route("[controller]")]
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
        public async Task<ActionResult<List<EmployeeDto>>> GetAciveTechnicians()
        {
            List<EmployeeDto> activeTechs = [];

            try
            {
                activeTechs = await _userManager.GetActiveTechnicians();
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
