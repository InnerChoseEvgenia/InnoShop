using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User.Core.Entities;

namespace User.Infrastructure.Data
{
    public class UserDbContext: IdentityDbContext<ApplicationUser>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) 
            : base(options)
        {

        }

    }
}
