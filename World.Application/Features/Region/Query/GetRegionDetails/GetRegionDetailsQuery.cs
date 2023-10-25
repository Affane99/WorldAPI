using MediatR;
using System;
using World.Application.DTOs.Region;

namespace World.Application.Features.Region.Query.GetRegionDetails
{
    public class GetRegionDetailsQuery : IRequest<RegionDto>
    {
        public Guid Id { get; set; }
    }
}
