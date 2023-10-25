using MediatR;
using World.Application.DTOs.Prefecture;
using World.Application.DTOs.Search;

namespace World.Application.Features.Prefecture.Query.GetPrefecture
{
    public class GetPrefectureQuery : IRequest<SearchResult<PrefectureDto>>
    {
        public SearchDTO Search { get; set; }
    }
}
