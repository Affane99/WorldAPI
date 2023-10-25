using MediatR;
using System;

namespace World.Application.Features.Region.Command.UpdateRegion
{
    public class UpdateRegionCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CountryId { get; set; }
    }
}
