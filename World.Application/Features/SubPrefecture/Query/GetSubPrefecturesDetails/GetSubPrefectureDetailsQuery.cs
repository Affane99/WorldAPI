using MediatR;
using System;
using World.Application.DTOs.SubPrefecture;

namespace World.Application.Features.SubPrefecture.Query.GetSubPrefectureDetails
{
    public class GetSubPrefectureDetailsQuery : IRequest<SubPrefectureDto>
    {
        public Guid Id { get; set; }
    }
}
