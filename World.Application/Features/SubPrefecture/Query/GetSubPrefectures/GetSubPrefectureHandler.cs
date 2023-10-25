using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.DTOs.SubPrefecture;
using World.Application.DTOs.Search;

namespace World.Application.Features.SubPrefecture.Query.GetSubPrefecture
{
    public class GetSubPrefectureHandler : IRequestHandler<GetSubPrefectureQuery, SearchResult<SubPrefectureDto>>
    {
        private readonly ISubPrefectureRepository _subPrefectureRepository;
        private readonly IMapper _mapper;

        public GetSubPrefectureHandler(ISubPrefectureRepository subPrefectureRepository, IMapper mapper)
        {
            _subPrefectureRepository = subPrefectureRepository;
            _mapper = mapper;
        }
        public async Task<SearchResult<SubPrefectureDto>> Handle(GetSubPrefectureQuery request, CancellationToken cancellationToken)
        {
            var filters = request.Search.Filters;
            var pageIndex = request.Search.PageIndex;
            var pageSize = request.Search.PageSize;

            var filteredRequest = GetFilteredQuery(filters);
            var filteredSubPrefecture = (pageIndex == -1) ? filteredRequest.ToList() : filteredRequest.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            var rows = _mapper.Map<List<SubPrefectureDto>>(filteredSubPrefecture);

            return new SearchResult<SubPrefectureDto>
            {
                Page = pageIndex,
                CountPerPage = pageSize,
                TotalCount = filteredRequest.Count(),
                Results = rows
            };
        }

        public IQueryable<Domain.SubPrefecture> GetFilteredQuery(Dictionary<string, string> filter)
        {
            var subPrefectures = _subPrefectureRepository.GetQuery("Prefecture.Region.Country.Continent");

            foreach (var key in filter.Keys)
            {
                if (string.IsNullOrEmpty(filter[key]))
                {
                    continue;
                }
                switch (key)
                {
                    case "name":
                        subPrefectures = _subPrefectureRepository.FilterQuery(subPrefectures, x => x.Name.ToLower().Contains(filter[key].ToLower()));
                        break;
                    case "prefecture":
                        subPrefectures = _subPrefectureRepository.FilterQuery(subPrefectures, x => x.PrefectureId.Equals(Guid.Parse(filter[key])));
                        break;

                }
            }
            return subPrefectures;
        }
    }
}
