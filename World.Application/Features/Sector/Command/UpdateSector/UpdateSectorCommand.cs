using MediatR;
using System;

namespace World.Application.Features.Sector.Command.UpdateSector
{
    public class UpdateSectorCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid SubPrefectureId { get; set; }
    }
}
