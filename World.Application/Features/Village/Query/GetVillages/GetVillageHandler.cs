using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.DTOs.Village;
using World.Application.DTOs.Search;

namespace World.Application.Features.Village.Query.GetVillage
{
    public class GetVillageHandler : IRequestHandler<GetVillageQuery, SearchResult<VillageDto>>
    {
        private readonly IVillageRepository _villageRepository;
        private readonly IMapper _mapper;

        public GetVillageHandler(IVillageRepository villageRepository, IMapper mapper)
        {
            _villageRepository = villageRepository;
            _mapper = mapper;
        }
        public async Task<SearchResult<VillageDto>> Handle(GetVillageQuery request, CancellationToken cancellationToken)
        {
            var filters = request.Search.Filters;
            var pageIndex = request.Search.PageIndex;
            var pageSize = request.Search.PageSize;

            var filteredRequest = GetFilteredQuery(filters);
            var filteredVillage = (pageIndex == -1) ? filteredRequest.ToList() : filteredRequest.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            var rows = _mapper.Map<List<VillageDto>>(filteredVillage);

            return new SearchResult<VillageDto>
            {
                Page = pageIndex,
                CountPerPage = pageSize,
                TotalCount = filteredRequest.Count(),
                Results = rows
            };
        }

        public IQueryable<Domain.Village> GetFilteredQuery(Dictionary<string, string> filter)
        {
            var villages = _villageRepository.GetQuery("Sector.SubPrefecture.Prefecture.Region.Country.Continent");

            foreach (var key in filter.Keys)
            {
                if (string.IsNullOrEmpty(filter[key]))
                {
                    continue;
                }
                switch (key)
                {
                    case "name":
                        villages = _villageRepository.FilterQuery(villages, x => x.Name.ToLower().Contains(filter[key].ToLower()));
                        break;
                    case "sector":
                        villages = _villageRepository.FilterQuery(villages, x => x.SectorId.Equals(Guid.Parse(filter[key])));
                        break;

                }
            }
            return villages;
        }
    }
}
