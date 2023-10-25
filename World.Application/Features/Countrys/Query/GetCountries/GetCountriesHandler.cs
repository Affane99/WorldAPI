using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.DTOs.Continent;
using World.Application.DTOs.Country;
using World.Application.DTOs.Search;
using World.Application.Features.Countries.Query.GetCountries;
using World.Domain;

namespace World.Application.Features.Countrys.Query.GetCountries
{
    public class GetCountriesHandler : IRequestHandler<GetCountriesQuery, SearchResult<CountryListDto>>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public GetCountriesHandler(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }
        public async Task<SearchResult<CountryListDto>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {
            var filters = request.Search.Filters;
            var pageIndex = request.Search.PageIndex;
            var pageSize = request.Search.PageSize;

            var filteredRequest = GetFilteredQuery(filters);
            var filteredContinent = (pageIndex == -1) ? filteredRequest.ToList() : filteredRequest.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            var rows = _mapper.Map<List<CountryListDto>>(filteredContinent);

            return new SearchResult<CountryListDto>
            {
                Page = pageIndex,
                CountPerPage = pageSize,
                TotalCount = filteredRequest.Count(),
                Results = rows
            };
        }

        public IQueryable<Country> GetFilteredQuery(Dictionary<string, string> filter)
        {
            var countries = _countryRepository.GetQuery("Continent");

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
                    case "capitale":
                        countries = _countryRepository.FilterQuery(countries, x => x.Capitale.ToLower().Contains(filter[key].ToLower()));
                        break;
                    case "code":
                        countries = _countryRepository.FilterQuery(countries, x => x.CountryCode.ToLower().Contains(filter[key].ToLower()));
                        break;
                    case "continent":
                        countries = _countryRepository.FilterQuery(countries, x => x.ContinentId.Equals(Guid.Parse(filter[key])));
                        break;

                }
            }
            return countries;
        }
    }
}
