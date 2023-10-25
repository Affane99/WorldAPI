using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.DTOs.Region;
using World.Application.DTOs.Search;

namespace World.Application.Features.Region.Query.GetRegion
{
    public class GetRegionHandler : IRequestHandler<GetRegionQuery, SearchResult<RegionDto>>
    {
        private readonly IRegionRepository _countryRepository;
        private readonly IMapper _mapper;

        public GetRegionHandler(IRegionRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }
        public async Task<SearchResult<RegionDto>> Handle(GetRegionQuery request, CancellationToken cancellationToken)
        {
            var filters = request.Search.Filters;
            var pageIndex = request.Search.PageIndex;
            var pageSize = request.Search.PageSize;

            var filteredRequest = GetFilteredQuery(filters);
            var filteredRegion = (pageIndex == -1) ? filteredRequest.ToList() : filteredRequest.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            var rows = _mapper.Map<List<RegionDto>>(filteredRegion);

            return new SearchResult<RegionDto>
            {
                Page = pageIndex,
                CountPerPage = pageSize,
                TotalCount = filteredRequest.Count(),
                Results = rows
            };
        }

        public IQueryable<Domain.Region> GetFilteredQuery(Dictionary<string, string> filter)
        {
            var countries = _countryRepository.GetQuery("Country.Continent");

            foreach (var key in filter.Keys)
            {
                if (string.IsNullOrEmpty(filter[key]))
                {
                    continue;
                }
                switch (key)
                {
                    case "name":
                        countries = _countryRepository.FilterQuery(countries, x => x.Name.ToLower().Contains(filter[key].ToLower()));
                        break;
                    case "country":
                        countries = _countryRepository.FilterQuery(countries, x => x.CountryId.Equals(Guid.Parse(filter[key])));
                        break;

                }
            }
            return countries;
        }
    }
}
