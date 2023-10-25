using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using World.Identity.Configurations;
using World.Identity.Models;

namespace World.Identity
{
    public class WorldIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public WorldIdentityDbContext(DbContextOptions<WorldIdentityDbContext> dbContext): base(dbContext)
        {}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
        }

    }
}
