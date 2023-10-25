using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace World.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IContinentRepository ContinentRepository { get; }
        ICountryRepository CountryRepository { get; }
        IPrefectureRepository PrefectureRepository { get; }
        IRegionRepository RegionRepository { get; }
        ISubPrefectureRepository SubPrefectureRepository { get; }
        ISectorRepository SectorRepository { get; }
        IVillageRepository VillageRepository { get; }
        Task Save();
    }
}
