using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Domain;

namespace World.Persistence.Repositories
{
    public class RegionRepository : GenericRepository<Region>, IRegionRepository
    {
        public RegionRepository(WorldManagementDbContext worldManagementDbContext) : base(worldManagementDbContext)
        {
        }
    }
}
