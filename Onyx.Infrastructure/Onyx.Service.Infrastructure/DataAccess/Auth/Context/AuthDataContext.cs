using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Onyx.Service.Domain.Models;

namespace Onyx.Service.Infrastructure.DataAccess.Auth.Context
{
    public class AuthDataContext : IdentityDbContext<User>
    {
        public AuthDataContext(DbContextOptions<AuthDataContext> options) : base(options)
        {

        }
    }
}
