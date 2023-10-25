using MediatR;
using System;
using World.Application.Responses;

namespace World.Application.Features.Sector.Command.CreateSector
{
    public class CreateSectorCommand : IRequest<BaseCommandResponse>
    {
        public string Name { get; set; }
        public Guid SubPrefectureId { get; set; }
    }
}
