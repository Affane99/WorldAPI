using MediatR;
using World.Application.DTOs.Village;
using World.Application.DTOs.Search;

namespace World.Application.Features.Village.Query.GetVillage
{
    public class GetVillageQuery : IRequest<SearchResult<VillageDto>>
    {
        public SearchDTO Search { get; set; }
    }
}
