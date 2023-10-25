using MediatR;
using System;
using World.Application.Responses;

namespace World.Application.Features.Region.Command.CreateRegion
{
    public class CreateRegionCommand : IRequest<BaseCommandResponse>
    {
        public string Name { get; set; }
        public Guid CountryId { get; set; }
    }
}
