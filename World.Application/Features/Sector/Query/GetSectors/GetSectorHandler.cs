using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.DTOs.Sector;
using World.Application.DTOs.Search;

namespace World.Application.Features.Sector.Query.GetSector
{
    public class GetSectorHandler : IRequestHandler<GetSectorQuery, SearchResult<SectorDto>>
    {
        private readonly ISectorRepository _sectorRepository;
        private readonly IMapper _mapper;

        public GetSectorHandler(ISectorRepository sectorRepository, IMapper mapper)
        {
            _sectorRepository = sectorRepository;
            _mapper = mapper;
        }
        public async Task<SearchResult<SectorDto>> Handle(GetSectorQuery request, CancellationToken cancellationToken)
        {
            var filters = request.Search.Filters;
            var pageIndex = request.Search.PageIndex;
            var pageSize = request.Search.PageSize;

            var filteredRequest = GetFilteredQuery(filters);
            var filteredSector = (pageIndex == -1) ? filteredRequest.ToList() : filteredRequest.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            var rows = _mapper.Map<List<SectorDto>>(filteredSector);

            return new SearchResult<SectorDto>
            {
                Page = pageIndex,
                CountPerPage = pageSize,
                TotalCount = filteredRequest.Count(),
                Results = rows
            };
        }

        public IQueryable<Domain.Sector> GetFilteredQuery(Dictionary<string, string> filter)
        {
            var sectors = _sectorRepository.GetQuery("SubPrefecture.Prefecture.Region.Country.Continent");

            foreach (var key in filter.Keys)
            {
                if (string.IsNullOrEmpty(filter[key]))
                {
                    continue;
                }
                switch (key)
                {
                    case "name":
                        sectors = _sectorRepository.FilterQuery(sectors, x => x.Name.ToLower().Contains(filter[key].ToLower()));
                        break;
                    case "subPrefecture":
                        sectors = _sectorRepository.FilterQuery(sectors, x => x.SubPrefectureId.Equals(Guid.Parse(filter[key])));
                        break;

                }
            }
            return sectors;
        }
    }
}
