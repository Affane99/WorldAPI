using MediatR;
using World.Application.DTOs.Sector;
using World.Application.DTOs.Search;

namespace World.Application.Features.Sector.Query.GetSector
{
    public class GetSectorQuery : IRequest<SearchResult<SectorDto>>
    {
        public SearchDTO Search { get; set; }
    }
}
