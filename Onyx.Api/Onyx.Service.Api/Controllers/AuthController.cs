using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Onyx.Service.Application.Managers;
using Onyx.Service.Contracts.Dtos.Auth;
using Onyx.Service.Contracts.Models;
using Onyx.Service.Contracts.Responses;
using Onyx.Service.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Onyx.Service.Api.Controllers
{
    [Route("[controller]")]
    public class AuthController : BaseController
    {
        private readonly AuthManager _manager;

        public AuthController(
            AuthManager authManager,
            ILogger<AuthController> logger
            ) : base(logger)
        {
            _manager = authManager;
        }
        
        [Authorize(Roles = "Office, Manager, Admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                User user = await _manager.RegisterUser(dto);

                return Ok(await _manager.CreateJwtResponseAsync(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginDto dto)
        {
            try
            {
                User user = await _manager.Login(dto);

                if (user == null)
                    return BadRequest("User not found");

                return Ok(await _manager.CreateJwtResponseAsync(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
