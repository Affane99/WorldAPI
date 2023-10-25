using MediatR;
using World.Application.DTOs.Region;
using World.Application.DTOs.Search;

namespace World.Application.Features.Region.Query.GetRegion
{
    public class GetRegionQuery : IRequest<SearchResult<RegionDto>>
    {
        public SearchDTO Search { get; set; }
    }
}
