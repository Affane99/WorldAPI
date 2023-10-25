using MediatR;
using System;
using World.Application.Responses;

namespace World.Application.Features.Village.Command.CreateVillage
{
    public class CreateVillageCommand : IRequest<BaseCommandResponse>
    {
        public string Name { get; set; }
        public Guid SectorId { get; set; }
    }
}
