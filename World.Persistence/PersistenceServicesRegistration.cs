using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using World.Application.Contracts.Persistence;
using World.Persistence.Repositories;

namespace World.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersitencesServices(this IServiceCollection services, IConfiguration configuration)
        {   
            services.AddDbContext<WorldManagementDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("WorldConnectionString")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IContinentRepository, ContinentRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IRegionRepository, RegionRepository>();
            services.AddScoped<IPrefectureRepository, PrefectureRepository>();
            services.AddScoped<ISubPrefectureRepository, SubPrefectureRepository>();
            services.AddScoped<ISectorRepository, SectorRepository>();
            services.AddScoped<IVillageRepository, VillageRepository>();

            return services;
        }
    }
}
