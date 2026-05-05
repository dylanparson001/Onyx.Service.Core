using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthController(
            UserManager<User> userManager,
            SignInManager<User> signinManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ILogger<AuthController> logger
            ) : base(logger)
        {
            _userManager = userManager;
            _signInManager = signinManager;
            _roleManager = roleManager;
            _configuration = configuration;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
                    return BadRequest(new { message = "Username and password are required." });

                var existing = await _userManager.FindByNameAsync(dto.Username);
                if (existing is not null)
                    return Conflict(new { message = "Username already exists." });

                bool userRoleExists = Enum.TryParse<UserRoles>(dto.Role, true, out UserRoles userRole);

                if (!userRoleExists)
                    return BadRequest(new { errors = "User role does not exist, cannot create user" });


                var user = new User
                {
                    UserName = dto.Username,
                    Email = dto.Email
                };

                var create = await _userManager.CreateAsync(user, dto.Password);
                if (!create.Succeeded)
                    return BadRequest(new { errors = create.Errors.Select(e => e.Description) });


                var role = userRole.ToString();

                if (!await _roleManager.RoleExistsAsync(role))
                {
                    var roleCreate = await _roleManager.CreateAsync(new IdentityRole(role));
                    if (!roleCreate.Succeeded)
                        return BadRequest(new { errors = roleCreate.Errors.Select(e => e.Description) });
                }

                var addRole = await _userManager.AddToRoleAsync(user, role);
                if (!addRole.Succeeded)
                    return BadRequest(new { errors = addRole.Errors.Select(e => e.Description) });

                return Ok(await CreateJwtResponseAsync(user));
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

                var user = await _userManager.FindByNameAsync(dto.UserName);
                if (user is null)
                    return Unauthorized(new { message = "Invalid username or password." });

                var signInResult = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: true);
                if (!signInResult.Succeeded)
                    return Unauthorized(new { message = "Invalid username or password." });

                return Ok(await CreateJwtResponseAsync(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("Test")]
        public string GetAString()
        {
            return "Woohoo~";
        }

        private async Task<LoginResponse> CreateJwtResponseAsync(User user)
        {
            var jwtSection = _configuration.GetSection("JwtSettings");
            var secret = jwtSection["Secret"]!;
            var issuer = jwtSection["Issuer"]!;
            var audience = jwtSection["Audience"]!;

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.UniqueName, user.UserName ?? string.Empty),
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name, user.UserName ?? string.Empty),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N"))
            };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddHours(8);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expires,
                signingCredentials: creds);

            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                TokenType = "Bearer",
                TokenExpires = expires,
                UserName = user.UserName ?? "",
                Roles = roles.ToList()
            };
        }
    }
}
