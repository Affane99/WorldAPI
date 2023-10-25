using World.Application.Contracts.Persistence;
using World.Domain;

namespace World.Persistence.Repositories
{
    public class VillageRepository : GenericRepository<Village>, IVillageRepository
    {
        public VillageRepository(WorldManagementDbContext worldManagementDbContext) : base(worldManagementDbContext)
        {
        } 
    }
}
