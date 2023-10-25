using MediatR;
using System;
using World.Application.DTOs.Village;

namespace World.Application.Features.Village.Query.GetVillageDetails
{
    public class GetVillageDetailsQuery : IRequest<VillageDto>
    {
        public Guid Id { get; set; }
    }
}
