using MediatR;
using System;

namespace World.Application.Features.Prefecture.Command.UpdatePrefecture
{
    public class UpdatePrefectureCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid RegionId { get; set; }
    }
}
