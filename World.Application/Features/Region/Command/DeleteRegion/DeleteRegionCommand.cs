using MediatR;
using System;

namespace World.Application.Features.Region.Command.DeleteRegion
{
    public class DeleteRegionCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
