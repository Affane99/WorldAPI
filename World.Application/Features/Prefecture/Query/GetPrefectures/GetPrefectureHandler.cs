using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.DTOs.Prefecture;
using World.Application.DTOs.Search;

namespace World.Application.Features.Prefecture.Query.GetPrefecture
{
    public class GetPrefectureHandler : IRequestHandler<GetPrefectureQuery, SearchResult<PrefectureDto>>
    {
        private readonly IPrefectureRepository _prefectureRepository;
        private readonly IMapper _mapper;

        public GetPrefectureHandler(IPrefectureRepository prefectureRepository, IMapper mapper)
        {
            _prefectureRepository = prefectureRepository;
            _mapper = mapper;
        }
        public async Task<SearchResult<PrefectureDto>> Handle(GetPrefectureQuery request, CancellationToken cancellationToken)
        {
            var filters = request.Search.Filters;
            var pageIndex = request.Search.PageIndex;
            var pageSize = request.Search.PageSize;

            var filteredRequest = GetFilteredQuery(filters);
            var filteredPrefecture = (pageIndex == -1) ? filteredRequest.ToList() : filteredRequest.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            var rows = _mapper.Map<List<PrefectureDto>>(filteredPrefecture);

            return new SearchResult<PrefectureDto>
            {
                Page = pageIndex,
                CountPerPage = pageSize,
                TotalCount = filteredRequest.Count(),
                Results = rows
            };
        }

        public IQueryable<Domain.Prefecture> GetFilteredQuery(Dictionary<string, string> filter)
        {
            var prefectures = _prefectureRepository.GetQuery("Region.Country.Continent");

            foreach (var key in filter.Keys)
            {
                if (string.IsNullOrEmpty(filter[key]))
                {
                    continue;
                }
                switch (key)
                {
                    case "name":
                        prefectures = _prefectureRepository.FilterQuery(prefectures, x => x.Name.ToLower().Contains(filter[key].ToLower()));
                        break;
                    case "region":
                        prefectures = _prefectureRepository.FilterQuery(prefectures, x => x.RegionId.Equals(Guid.Parse(filter[key])));
                        break;

                }
            }
            return prefectures;
        }
    }
}
