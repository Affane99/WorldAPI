using MediatR;
using System;
using World.Application.DTOs.Prefecture;

namespace World.Application.Features.Prefecture.Query.GetPrefectureDetails
{
    public class GetPrefectureDetailsQuery : IRequest<PrefectureDto>
    {
        public Guid Id { get; set; }
    }
}
