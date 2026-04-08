using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using onyx_services_core.DataAccess.Auth.Models;

namespace onyx_services_core.DataAccess.Auth.Context
{
    public class AuthDataContext : IdentityDbContext<User>
    {
        public AuthDataContext(DbContextOptions<AuthDataContext> options) : base(options)
        {

        }
    }
}
