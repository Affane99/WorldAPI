using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;

namespace World.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IContinentRepository _continentRepository;
        private ICountryRepository _countryRepository;
        private IPrefectureRepository _prefectureRepository;
        private IRegionRepository _regionRepository;
        private ISubPrefectureRepository _subPrefectureRepository;
        private ISectorRepository _sectorRepository;
        private IVillageRepository _villageRepository;

        private readonly WorldManagementDbContext _context;

        public UnitOfWork(WorldManagementDbContext context)
        {
            _context = context;
        }

        public IContinentRepository ContinentRepository => _continentRepository ?? new ContinentRepository(_context);

        public ICountryRepository CountryRepository => _countryRepository ?? new CountryRepository(_context);

        public IPrefectureRepository PrefectureRepository => _prefectureRepository ?? new PrefectureRepository(_context);

        public IRegionRepository RegionRepository => _regionRepository ?? new RegionRepository(_context);

        public ISubPrefectureRepository SubPrefectureRepository => _subPrefectureRepository ?? new SubPrefectureRepository(_context);

        public ISectorRepository SectorRepository => _sectorRepository ?? new SectorRepository(_context);

        public IVillageRepository VillageRepository => _villageRepository ?? new VillageRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
