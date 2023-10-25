using MediatR;
using System;

namespace World.Application.Features.Village.Command.UpdateVillage
{
    public class UpdateVillageCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid SectorId { get; set; }
    }
}
