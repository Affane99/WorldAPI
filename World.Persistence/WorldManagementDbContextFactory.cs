using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace World.Persistence
{
    public class WorldManagementDbContextFactory :
        IDesignTimeDbContextFactory<WorldManagementDbContext>
    {
        public WorldManagementDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(),"..", "World.API"))
                .AddJsonFile("appsettings.json").Build();

            var builder = new DbContextOptionsBuilder<WorldManagementDbContext>();
            
            var connectionString = configuration.GetConnectionString("WorldConnectionString");
            
            builder.UseSqlServer(connectionString);

            return new WorldManagementDbContext(builder.Options);
        }
    }
}
