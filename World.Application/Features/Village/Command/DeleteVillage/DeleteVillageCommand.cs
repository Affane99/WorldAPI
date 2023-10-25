using MediatR;
using System;

namespace World.Application.Features.Village.Command.DeleteVillage
{
    public class DeleteVillageCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
