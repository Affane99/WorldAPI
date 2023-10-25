using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Domain;
using World.Domain.Common;

namespace World.Persistence
{
    public class WorldManagementDbContext : DbContext
    {
        public WorldManagementDbContext(DbContextOptions<WorldManagementDbContext> options): base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorldManagementDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainEntity>())
            {
                if(entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedDate = DateTime.Now;
                }

                if(entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Continent> Continents { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<Prefecture> Prefecture { get; set; }
        public DbSet<SubPrefecture> SubPrefecture { get; set; }
        public DbSet<Sector> Sector { get; set; }
        public DbSet<Village> Village { get; set; }
    }
}
