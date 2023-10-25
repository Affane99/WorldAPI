using MediatR;
using System;

namespace World.Application.Features.Sector.Command.DeleteSector
{
    public class DeleteSectorCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
