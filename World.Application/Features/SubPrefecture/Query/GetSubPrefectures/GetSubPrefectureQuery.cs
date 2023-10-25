using MediatR;
using World.Application.DTOs.SubPrefecture;
using World.Application.DTOs.Search;

namespace World.Application.Features.SubPrefecture.Query.GetSubPrefecture
{
    public class GetSubPrefectureQuery : IRequest<SearchResult<SubPrefectureDto>>
    {
        public SearchDTO Search { get; set; }
    }
}
