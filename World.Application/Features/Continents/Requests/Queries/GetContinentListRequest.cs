using MediatR;
using System.Collections.Generic;
using World.Application.DTOs.Continent;
using World.Application.DTOs.Search;

namespace World.Application.Features.Continents.Requests.Queries
{
    public class GetContinentListRequest : IRequest<SearchResult<ContinentListDto>>
    {
        public SearchDTO Search { get; set; }
    }
}
