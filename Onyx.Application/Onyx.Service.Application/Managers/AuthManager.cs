using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Onyx.Service.Contracts.Models;
using Onyx.Service.Domain.Models;
using Onyx.Shared.Contracts.Auth;
using Onyx.Shared.Contracts.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Onyx.Service.Application.Managers
{
    public class AuthManager(
        UserManager<User> userManager,
        SignInManager<User> signinManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration,
        ILogger<AuthManager> logger
            )
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly SignInManager<User> _signInManager = signinManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<AuthManager> _logger = logger;

        public async Task<User> RegisterUser(RegisterDto dto)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
                    throw new Exception("Username and password are required.");

                var existing = await _userManager.FindByNameAsync(dto.Username);
                if (existing is not null)
                    throw new Exception("Username already exists.");

                bool userRoleExists = Enum.TryParse<UserRoles>(dto.Role, true, out UserRoles userRole);

                if (!userRoleExists)
                    throw new Exception("User role does not exist, cannot create user");


                var user = new User
                {
                    UserName = dto.Username,
                    Email = dto.Email
                };

                var create = await _userManager.CreateAsync(user, dto.Password);
                if (!create.Succeeded)
                    throw new Exception($"{create.Errors.Select(e => e.Description)}");


                var role = userRole.ToString();

                if (!await _roleManager.RoleExistsAsync(role))
                {
                    var roleCreate = await _roleManager.CreateAsync(new IdentityRole(role));
                    if (!roleCreate.Succeeded)
                        throw new Exception($"{create.Errors.Select(e => e.Description)}");

                }

                var addRole = await _userManager.AddToRoleAsync(user, role);
                if (!addRole.Succeeded)
                    throw new Exception($"{create.Errors.Select(e => e.Description)}");

                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<User> Login(LoginDto dto)
        {
            try
            {

                var user = await _userManager.FindByNameAsync(dto.UserName);
                if (user is null)
                    throw new Exception("Invalid username or password.");

                var signInResult = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: true);
                if (!signInResult.Succeeded)
                    throw new Exception("Invalid username or password.");

                return user;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<LoginResponse> CreateJwtResponseAsync(User user)
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
