using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using World.Application.DTOs.Continent;
using World.Application.Features.Continents.Requests.Queries;
using World.Application.Contracts.Persistence;
using World.Application.DTOs.Search;
using System.Linq;
using World.Domain;

namespace World.Application.Features.Continents.Handlers.Queries
{
    public class GetContinentListRequestHandler : IRequestHandler<GetContinentListRequest, SearchResult<ContinentListDto>>
    {
        private readonly IContinentRepository _continentRepository;
        private readonly IMapper _mapper;

        public GetContinentListRequestHandler(IContinentRepository continentRepository, IMapper mapper) 
        {
            _continentRepository = continentRepository;
            _mapper = mapper;
        }
        public async  Task<SearchResult<ContinentListDto>> Handle(GetContinentListRequest request, CancellationToken cancellationToken)
        {
            return await GetContinentListPageAsync(request.Search.PageIndex, request.Search.PageSize, request.Search.Filters);
        }

        public async Task<SearchResult<ContinentListDto>> GetContinentListPageAsync(int pageIndex, int pageSize, Dictionary<string, string> filters)
        {
            var filteredRequest = GetFilteredQuery(filters);
            var filteredContinent = (pageIndex == -1) ? filteredRequest.ToList() : filteredRequest.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            var rows = _mapper.Map<List<ContinentListDto>>(filteredContinent);

            return new SearchResult<ContinentListDto>
            {
                Page = pageIndex,
                CountPerPage = pageSize,
                TotalCount = filteredRequest.Count(),
                Results = rows
            };
        }

        public IQueryable<Continent> GetFilteredQuery(Dictionary<string, string> filter)
        {
            var continents = _continentRepository.GetQuery();

            foreach (var key in filter.Keys)
            {
                if (string.IsNullOrEmpty(filter[key]))
                {
                    continue;
                }
                switch (key)
                {
                    case "name":
                        continents = _continentRepository.FilterQuery(continents, x => x.Name.ToLower().Contains(filter[key].ToLower()));
                        break;
                }
            }
            return continents;
        }
    }
}
