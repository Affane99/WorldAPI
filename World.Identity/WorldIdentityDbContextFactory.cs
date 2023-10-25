using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace World.Identity
{
    public class WorldIdentityDbContextFactory :
        IDesignTimeDbContextFactory<WorldIdentityDbContext>
    {
        public WorldIdentityDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "World.API"))
                .AddJsonFile("appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<WorldIdentityDbContext>();
            var connectionString = configuration.GetConnectionString("WorldConnectionString");

            builder.UseSqlServer(connectionString);
            return new WorldIdentityDbContext(builder.Options);
        }
    }
}
