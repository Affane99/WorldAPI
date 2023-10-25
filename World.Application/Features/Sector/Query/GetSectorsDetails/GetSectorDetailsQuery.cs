using MediatR;
using System;
using World.Application.DTOs.Sector;

namespace World.Application.Features.Sector.Query.GetSectorDetails
{
    public class GetSectorDetailsQuery : IRequest<SectorDto>
    {
        public Guid Id { get; set; }
    }
}
